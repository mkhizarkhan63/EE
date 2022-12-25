using Common;
using EagleEye_Service.App_Code;
using EagleEye_Service.DAL;
using FKWeb;
using log4net;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EagleEye_Service
{
    public class HttpServer
    {
        private readonly HttpListener _listener;
        private readonly Thread _listenerThread;
        private readonly Thread[] _workers;
        private readonly ManualResetEvent _stop, _ready;
        private Queue<HttpListenerContext> _queue;
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        ILog logger = log4net.LogManager.GetLogger("SiteLogger");

        private const string REQ_CODE_RECV_CMD = "receive_cmd";
        private const string REQ_CODE_SEND_CMD_RESULT = "send_cmd_result";
        private const string REQ_CODE_REALTIME_GLOG = "realtime_glog";
        private const string REQ_CODE_REALTIME_ENROLL = "realtime_enroll_data";
        private const string REQ_CODE_REALTIME_DOOR = "realtime_door_status";
        private string TRANSIDKEY = FKWebCmdTrans.REQUEST_HEADER_KEY_TRANSID;
        string ConnectionString = "";
        string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";


        AppSettingModel app = new AppSettingModel();


        public HttpServer(int maxThreads, AppSettingModel setting)
        {
            app = setting;
            _workers = new Thread[maxThreads];
            _queue = new Queue<HttpListenerContext>();
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _listener = new HttpListener();
            _listenerThread = new Thread(HandleRequests);
            ConnectionString = File.ReadAllText(s);

        }

        public void Start(string ip, int port)
        {
            _listener.Prefixes.Add(String.Format(@"http://{0}:{1}/", ip, port));
            _listener.Start();
            _listenerThread.Start();

            for (int i = 0; i < _workers.Length; i++)
            {
                _workers[i] = new Thread(Worker);
                _workers[i].Start();
            }
        }

        public void Dispose()
        { Stop(); }

        public void Stop()
        {
            _stop.Set();
            _listenerThread.Join();
            foreach (Thread worker in _workers)
                worker.Join();
            _listener.Stop();
        }

        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                var context = _listener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                    return;
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                lock (_queue)
                {
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerContext context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        context = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try
                {

                    if (context != null)
                    {
                        ProcessRequest(context);
                        context.Response.Close();
                    }
                }
                catch (Exception e) { Console.Error.WriteLine(e); }
            }
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            const string csFuncName = "Page_Load";
            string sDevId;
            string sTransId;
            string sRequestCode;
            string stokenid;
            string sDevModel;
            int lenContent;
            byte[] bytRequestBin;
            byte[] bytRequestTotal;
            byte[] bytEmpty = new byte[0];


            try
            {



                HttpListenerRequest Request = context.Request;
                HttpListenerResponse Response = context.Response;

                FKWebCmdTrans cmdTrans = new FKWebCmdTrans(app);

                TRANSIDKEY = cmdTrans.getTransIdKey();

                stokenid = cmdTrans.getTokenId();
                if (stokenid != null)
                {
                    if (!stokenid.Equals(Request.Headers["token_id"]))
                        return;
                }

                sDevModel = Request.Headers["dev_model"];
                if (FKWebTools.IsValidEngDigitString(sDevModel, 32))
                {
                    cmdTrans.PrintDebugMsg(csFuncName, "DevModel : " + sDevModel);
                    cmdTrans.DevModel = sDevModel;
                }

                sRequestCode = Request.Headers["request_code"];


                if (!FKWebTools.IsValidEngDigitString(sRequestCode, 32))
                {
                    cmdTrans.PrintDebugMsg(csFuncName, "error - Invalid request_code : " + sRequestCode);
                    Response.Close();
                    return;
                }

                try
                {
                    sTransId = Request.Headers[TRANSIDKEY];
                    if (sTransId.Length > 0)
                        cmdTrans.PrintDebugMsg(csFuncName, "**************** sRequestCode = " + sRequestCode + " dev_id=" + Request.Headers["dev_id"] + " trans_id=" + sTransId + " content_length=" + Request.Headers["Content-Length"]);

                }
                catch (Exception)
                {
                    sTransId = "";
                }
                cmdTrans.PrintDebugMsg(csFuncName, "1 - request_code : " + sRequestCode + " ,trans_id : " + sTransId);

                sDevId = Request.Headers["dev_id"];
                Console.WriteLine(sRequestCode + " Device=" + sDevId);

                if (!FKWebTools.IsValidEngDigitString(sDevId, 18))
                {
                    cmdTrans.PrintDebugMsg(csFuncName, "error - Invalid dev_id : " + sDevId);
                    Response.Close();
                    return;
                }

                lenContent = GetRequestStreamBytes(Request, out bytRequestBin);
                if (lenContent < 0)
                {
                    cmdTrans.PrintDebugMsg1(csFuncName, "2.1" + lenContent);

                    // 만일 HTTP 헤더의 Content-Length만한 바이트를 다 접수하지 못한 경우는 접속을 차단한다.
                    Response.Close();
                    return;
                }

                if (cmdTrans.DevModel == null)
                {

                    int nBlkNo = Convert.ToInt32(Request.Headers["blk_no"]);
                    int nBlkLen = Convert.ToInt32(Request.Headers["blk_len"]);
                    int vRet;
                    if (nBlkNo > 0)
                    {
                        cmdTrans.PrintDebugMsg1(csFuncName, "3.0 - blk_no=" + Convert.ToString(nBlkNo) + ", blk_len=" + Convert.ToString(nBlkLen));

                        vRet = AddBlockData(context, sDevId, nBlkNo, bytRequestBin);
                        if (vRet != 0)
                        {
                            cmdTrans.PrintDebugMsg1(csFuncName, "3.0 - error - AddBlockData:" + Convert.ToString(vRet));
                            SendResponseToClient(Response, "ERROR_ADD_BLOCK_DATA", sTransId, "", bytEmpty);
                            return;
                        }

                        cmdTrans.PrintDebugMsg1(csFuncName, "3.1");

                        SendResponseToClient(Response, "OK", sTransId, "", bytEmpty);
                        return;
                    }
                    else if (nBlkNo < 0)
                    {
                        cmdTrans.PrintDebugMsg(csFuncName, "3.3 - blk_no=" + Convert.ToString(nBlkNo) + ", blk_len=" + Convert.ToString(nBlkLen));

                        // 비정상적인 HTTP요구(블로크번호가 무효한 값)가 올라온 경우이다.
                        SendResponseToClient(Response, "ERROR_INVLAID_BLOCK_NO", sTransId, "", bytEmpty);
                        return;
                    }
                    else
                    {
                        cmdTrans.PrintDebugMsg(csFuncName, "3.4 - blk_no=" + Convert.ToString(nBlkNo) + ", blk_len=" + Convert.ToString(nBlkLen));

                        // 기대측에서 결과자료를 보낼때 마지막 블로크를 보냈다면
                        //  메모리스트림에 루적하였던 자료를 얻어내여 그 뒤에 최종으로 받은 블로크를 덧붙인다.
                        GetBlockDataAndRemove(sDevId, out bytRequestTotal);
                        FKWebTools.ConcateByteArray(ref bytRequestTotal, bytRequestBin);
                    }
                }
                else
                {
                    bytRequestTotal = new byte[bytRequestBin.Length];
                    System.Array.Copy(bytRequestBin, bytRequestTotal, bytRequestBin.Length);
                }

                if (sRequestCode == REQ_CODE_RECV_CMD)
                {

                    RemoveOldBlockStream();

                    OnReceiveCmd(Response, cmdTrans, sDevId, sTransId, bytRequestTotal);
                }
                else if (sRequestCode == REQ_CODE_SEND_CMD_RESULT)
                {
                    OnSendCmdResult(Request, Response, cmdTrans, sDevId, sTransId, bytRequestTotal);
                }
                else if (sRequestCode == REQ_CODE_REALTIME_GLOG)
                {
                    OnRealtimeGLog(Response, cmdTrans, sDevId, bytRequestTotal);
                }
                else if (sRequestCode == REQ_CODE_REALTIME_ENROLL)
                {
                    OnRealtimeEnrollData(Response, cmdTrans, sDevId, bytRequestTotal);
                }
                else if (sRequestCode == REQ_CODE_REALTIME_DOOR)
                {
                    OnRealtimeDoorData(Response, cmdTrans, sDevId, bytRequestTotal);
                }
                /*else if (sRequestCode == REQ_CODE_REALTIME_SLOG)
                {
                    OnRealtimeSLog(cmdTrans, sDevId, bytRequestTotal);
                }*/
                else
                {
                    SendResponseToClient(Response, "ERROR_INVLAID_REQUEST_CODE", sTransId, "", bytEmpty);
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _ready.Set();
            }


        }


        #region Methods

        protected int GetRequestStreamBytes(HttpListenerRequest Request,
        out byte[] abytReceived)
        {
            abytReceived = new byte[0];
            int lenContent = Convert.ToInt32((string)Request.Headers["Content-Length"]);
            if (lenContent < 1)
                return 0;

            Stream streamIn = Request.InputStream;
            byte[] bytRecv = new byte[lenContent];
            int lenRead;
            lenRead = streamIn.Read(bytRecv, 0, lenContent);
            if (lenRead != lenContent)
            {
                // 만일 읽어야 할 길이만큼 다 읽지 못하면
                return -1;
            }

            abytReceived = bytRecv;
            return lenContent;
        }

        protected void SendResponseToClient(HttpListenerResponse Response,
        string asResponseCode,
        string asTransId,
        string asCmdCode,
        byte[] abytCmdParam)
        {
            Response.AddHeader("response_code", asResponseCode);
            Response.AddHeader(TRANSIDKEY, asTransId);
            Response.AddHeader("cmd_code", asCmdCode);

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Length", Convert.ToString(abytCmdParam.Length));
            //Response.Flush();

            if (abytCmdParam.Length > 0)
            {
                Stream streamOut = Response.OutputStream;
                streamOut.Write(abytCmdParam, 0, abytCmdParam.Length);
                streamOut.Close();
            }
        }


        protected void OnReceiveCmd(HttpListenerResponse Response, FKWebCmdTrans aCmdTrans, string asDevId, string asTransId, byte[] abytReuest)
        {
            const string csFuncName = "Page_Load - receive_cmd";

            string sRequest;
            string sResponse;
            string sTransId = asTransId;
            byte[] bytRequest = abytReuest;
            string sCmdCode;

            try
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "2");

                if (aCmdTrans.DevModel == null)
                    sRequest = FKWebCmdTrans.GetStringFromBSCommBuffer(bytRequest);
                else
                    sRequest = System.Text.Encoding.UTF8.GetString(bytRequest);
                if (sRequest.Length == 0)
                {
                    // 만일 지령접수요구로 올라온 문자렬의 길이가 0이면
                    //  잘못된 요구가 올라온것으로 보고 접속을 차단한다.
                    Response.Close();
                    return;
                }

                // 지령접수요구가 올라올때 기대이름, 기대시간, 기대정보가
                //  body부분에 포함되여 올라오게 되여있다.
                string sDevName;
                string sDevTime;
                string sDevInfo;

                JObject jobjRequest = JObject.Parse(sRequest);
                sDevName = jobjRequest["fk_name"].ToString();
                sDevTime = jobjRequest["fk_time"].ToString();
                sDevInfo = jobjRequest["fk_info"].ToString(Newtonsoft.Json.Formatting.None);

                aCmdTrans.PrintDebugMsg(csFuncName, "3 - DevName=" + sDevName + ", DevTime=" + sDevTime + ", DevInfo=" + sDevInfo);

                byte[] bytCmdParam = new byte[0];
                aCmdTrans.ReceiveCmd(asDevId, sDevName, sDevTime, sDevInfo,
                                    out sResponse, out sTransId, out sCmdCode, out bytCmdParam);

                aCmdTrans.PrintDebugMsg(csFuncName, "4");

                SendResponseToClient(
                    Response,
                    sResponse,
                    sTransId,
                    sCmdCode,
                    bytCmdParam);

                aCmdTrans.PrintDebugMsg(csFuncName, "5");
            }
            catch (Exception ex)
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "Except - 1 - " + ex.ToString());
                // 지령접수처리과정에 exception이 발생하면 접속을 차단한다.                
                Response.Close();
                return;
            }
        }

        protected int AddBlockData(HttpListenerContext Context, string asDevId, int anBlkNo, byte[] abytBlkData)
        {
            if (asDevId.Length == 0)
                return -1; // -1 : 파라메터가 비정상

            if (anBlkNo < 1)
                return -1;

            if (abytBlkData.Length == 0)
                return -1;

            try
            {
                string app_key;

                AppContext.Lock();
                app_key = "key_dev_" + asDevId;

                if (anBlkNo == 1)
                {
                    FKWebTransBlockData app_val_blk = (FKWebTransBlockData)AppContext.Get(app_key); //(FKWebTransBlockData)Context.Application.Get(app_key);
                    if (app_val_blk == null)
                    {
                        // 이전에 해당 기대에 대한 블로크자료를 루적하기 위한 오브젝트가 창조되여 있지 않은 경우
                        //  새 오브젝트를 창조하고 Dictionary에 추가한다.
                        app_val_blk = new FKWebTransBlockData();
                        AppContext.Add(app_key, app_val_blk);
                    }
                    else
                    {
                        // 이전에 해당 기대에 대한 블로크자료를 루적하기 위한 오브젝트가 창조되여 있은 경우
                        //  그 오브젝트를 삭제하고 새 오브젝트를 창조한 다음 Dictionary에 추가한다.
                        AppContext.Remove(app_key);
                        app_val_blk = new FKWebTransBlockData();
                        AppContext.Add(app_key, app_val_blk);
                    }

                    // 첫 블로크자료를 블로크자료보관용 스트림에 추가한다.
                    app_val_blk.LastBlockNo = 1;
                    app_val_blk.TmLastModified = DateTime.Now;
                    app_val_blk.MemStream = new MemoryStream();
                    app_val_blk.MemStream.Write(abytBlkData, 0, abytBlkData.Length);
                }
                else
                {
                    // 블로크번호가 1 이 아닌 경우
                    FKWebTransBlockData app_val_blk = (FKWebTransBlockData)AppContext.Get(app_key); //(FKWebTransBlockData)Context.Application.Get(app_key);
                    if (app_val_blk == null)
                    {
                        // 이미 기대에 대한 오브젝트가 창조되여 있지 않은 상태라면 
                        AppContext.UnLock();
                        return -2;
                    }
                    if (app_val_blk.LastBlockNo != anBlkNo - 1)
                    {
                        // 만일 마지막으로 받은 블로크번호가 새로 받을 블로크번호와 련속이 되지 않는다면
                        AppContext.UnLock();
                        return -3;
                    }

                    // 새로 받은 블로크자료를 블로크자료보관용 스트림의 마지막에 추가한다.
                    app_val_blk.LastBlockNo = anBlkNo;
                    app_val_blk.TmLastModified = DateTime.Now;
                    app_val_blk.MemStream.Seek(0, SeekOrigin.End);
                    app_val_blk.MemStream.Write(abytBlkData, 0, abytBlkData.Length);
                }

                AppContext.UnLock();
                return 0;
            }
            catch
            {
                AppContext.UnLock();
                return -11;
            }
        }

        protected int GetBlockDataAndRemove(string asDevId, out byte[] abytBlkData)
        {
            abytBlkData = new byte[0];

            if (asDevId.Length == 0)
                return -1;

            try
            {
                string app_key;

                AppContext.Lock();
                app_key = "key_dev_" + asDevId;

                FKWebTransBlockData app_val_blk = (FKWebTransBlockData)AppContext.Get(app_key);
                if (app_val_blk == null)
                {
                    AppContext.UnLock();
                    return 0;
                }

                app_val_blk.MemStream.Seek(0, SeekOrigin.Begin);
                abytBlkData = new byte[app_val_blk.MemStream.Length];
                app_val_blk.MemStream.Read(abytBlkData, 0, abytBlkData.Length);
                AppContext.Remove(app_key);

                AppContext.UnLock();
                return 0;
            }
            catch
            {
                AppContext.UnLock();
                return -11;
            }
        }

        protected void RemoveOldBlockStream()
        {
            DateTime dtCur = DateTime.Now;
            TimeSpan delta;
            try
            {
                AppContext.Lock();

                List<string> listDevIdKey = new List<string>();
                FKWebTransBlockData app_val_blk;
                int k;
                int cnt = AppContext.Count();
                for (k = 0; k < cnt; k++)
                {
                    string sKey = AppContext.GetKey(k);
                    if (String.Compare(sKey, 0, "key_dev_", 0, 8, true) != 0)
                        continue;

                    app_val_blk = (FKWebTransBlockData)AppContext.Get(k);
                    delta = dtCur - app_val_blk.TmLastModified;
                    if (delta.Minutes > 30)
                        listDevIdKey.Add(sKey);
                }

                foreach (string key_dev in listDevIdKey)
                    AppContext.Remove(key_dev);

                AppContext.UnLock();
            }
            catch
            {
                AppContext.UnLock();
            }
        }

        protected void OnSendCmdResult(HttpListenerRequest Request, HttpListenerResponse Response, FKWebCmdTrans aCmdTrans, string asDevId, string asTransId, byte[] abytRequest)
        {
            const string csFuncName = "Page_Load - send_cmd_result";

            byte[] bytCmdResult = abytRequest;
            byte[] bytEmpty = new byte[0];
            string sResponse;
            string sTransId;
            string sReturnCode;

            aCmdTrans.PrintDebugMsg1(csFuncName, "1");

            sTransId = asTransId;

            SqlConnection sqlConn;
            string sDbConn = ConnectionString;
            sqlConn = new SqlConnection(sDbConn);
            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
            }
            catch (Exception)
            {
                aCmdTrans.PrintDebugMsg1(csFuncName, "1.1");

                sqlConn.Close();
                sqlConn.Dispose();
                SendResponseToClient(Response, "ERROR_DB_ACCESS", sTransId, "", bytEmpty);
                return;
            }

            try
            {
                aCmdTrans.PrintDebugMsg1(csFuncName, "2 - trans_id:" + sTransId);

                // 해당 지령을 통신하는 기대에 대해 '기대재기동'지령이 발행된것이 있는가 조사하여
                //  있으면 기대에 응답으로서 '기대재기동'을 내려보낸다.
                string sTransIdTemp;
                if (aCmdTrans.CheckResetCmd(sqlConn, asDevId, out sTransIdTemp))
                {
                    aCmdTrans.PrintDebugMsg1(csFuncName, "2.1");

                    sqlConn.Close();
                    sqlConn.Dispose();
                    SendResponseToClient(Response, "RESET_FK", sTransId, "", bytEmpty);
                    return;
                }
                // 해당 지령이 '취소'되였으면 그에 대해 응답한다.
                if (aCmdTrans.IsCancelledCmd(sqlConn, asDevId, sTransId))
                {
                    aCmdTrans.PrintDebugMsg1(csFuncName, "2.2");
                    sqlConn.Close();
                    sqlConn.Dispose();
                    SendResponseToClient(Response, "ERROR_CANCELED", sTransId, "", bytEmpty);
                    return;
                }
                sqlConn.Close();
                sqlConn.Dispose();

                aCmdTrans.PrintDebugMsg1(csFuncName, "3");

                sReturnCode = Request.Headers["cmd_return_code"].ToString();

                // 지령처리결과자료를 자료기지에 보관한다.
                aCmdTrans.SetCmdResult(sTransId, asDevId, sReturnCode, bytCmdResult, out sResponse);




                aCmdTrans.PrintDebugMsg1(csFuncName, "4");

                // HTTP클라이언트에 응답을 보낸다.
                SendResponseToClient(Response, sResponse, sTransId, "", bytEmpty);

                aCmdTrans.PrintDebugMsg1(csFuncName, "5");
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                sqlConn.Dispose();
                aCmdTrans.PrintDebugMsg(csFuncName, "Except - 1 - " + ex.ToString());
                // exception이 발생하면 접속을 차단한다.                
                Response.Close();
                return;
            }
        }

        protected void OnRealtimeGLog(HttpListenerResponse Response, FKWebCmdTrans aCmdTrans, string asDevId, byte[] abytRequest)
        {
            const string csFuncName = "Page_Load - realtime_glog";

            string sRequest = "";
            string sResponse;
            byte[] bytRequest = abytRequest;
            byte[] bytLogImage = new byte[0];

            try
            {
                aCmdTrans.PrintDebugMsg1(csFuncName, "1------------------>");

                // 실시간로그자료가 올라올때 사용자ID, 확인방식, 출입방식, 출입시간이 body부분에 포함되여 올라온다.
                // 로그화상자료는 경우에 따라 존재할수도 있고 존재하지 않을수도 있다.
                if (aCmdTrans.DevModel == null)
                {
                    FKWebCmdTrans.GetStringAnd1stBinaryFromBSCommBuffer(
                        bytRequest, out sRequest, out bytLogImage);
                }
                else
                {
                    sRequest = System.Text.Encoding.UTF8.GetString(bytRequest);
                }
                if (sRequest.Length == 0)
                {
                    aCmdTrans.PrintDebugMsg1(csFuncName, "1-- length=" + sRequest.Length);
                    // 만일 실시간로그자료접수시에 올라온 문자렬의 길이가 0이면
                    //  잘못된 요구가 올라온것으로 보고 접속을 차단한다.
                    Response.Close();
                    return;
                }

                aCmdTrans.PrintDebugMsg1(csFuncName, "2");

                string sUserId;
                string sVerifyMode;
                string sIOMode;
                string sDoorStatus;
                string sIOTime;
                string sWorkCode;
                string sLogImg;
                string sFKBinDataLib;
                string sExtraStatus = "";

                JObject jobjRequest = JObject.Parse(sRequest);
                aCmdTrans.PrintDebugMsg1(csFuncName, "2-1");
                // 이 필드는 올라온 실시간로그자료의 확인방식, 출입방식의 상수들을 해석하는데 리용된다. 
                sFKBinDataLib = jobjRequest["fk_bin_data_lib"].ToString();
                if (sFKBinDataLib.Length == 0)
                {
                    aCmdTrans.PrintDebugMsg1(csFuncName, "2-- sFKBinDataLib.length=" + sFKBinDataLib.Length);
                    Response.AddHeader("response_code", "ERROR_INVALID_LIB_NAME");
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Length", Convert.ToString(0));
                    Response.Close();
                }
                aCmdTrans.PrintDebugMsg1(csFuncName, "2-2");
                sUserId = jobjRequest["user_id"].ToString();

                sVerifyMode = jobjRequest["verify_mode"].ToString();
                sIOMode = jobjRequest["io_mode"].ToString();
                sIOTime = jobjRequest["io_time"].ToString();
                sDoorStatus = "";
                try
                {
                    sWorkCode = jobjRequest["work_code"].ToString();
                }
                catch (Exception)
                {
                    sWorkCode = "0";
                }
                try
                {
                    sLogImg = jobjRequest["log_image"].ToString();
                }
                catch (Exception)
                {
                    sLogImg = "";
                }
                aCmdTrans.PrintDebugMsg1(csFuncName, "2-3");
                aCmdTrans.PrintDebugMsg1(csFuncName, "2" + "user_id = " + sUserId + "  sIOTime = " + sIOTime);

                if (aCmdTrans.DevModel == null)
                {
                    // 확인방식, 출입방식을 변환한다.
                    if (sFKBinDataLib == "FKDataHS101" || sFKBinDataLib.ToUpper() == "FKDATAHS101")
                    {
                        sIOMode = FKDataHS101.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        sVerifyMode = FKDataHS101.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }
                    else if (sFKBinDataLib == "FKDataHS102")
                    {
                        sIOMode = FKDataHS102.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        sVerifyMode = FKDataHS102.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }
                    else if (sFKBinDataLib == "FKDataHS103")
                    {
                        sIOMode = FKDataHS103.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        sVerifyMode = FKDataHS103.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }
                    else if (sFKBinDataLib == "FKDataHS105")
                    {
                        sIOMode = FKDataHS105.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        //  clsWriterLog.WriteAppLog(sUserId, sIOMode);
                        //   LogService.WriteLog("userid: "+sUserId + " | status: " + sIOMode + " | datetime: " + sIOTime);
                        sIOMode = sIOMode.Replace("(", "").Replace(")", "").Replace(" ", "");

                        //storing extra status
                        sExtraStatus = sIOMode;

                        DALDevice objDev = new DALDevice();
                        bool IsSlave = objDev.IsSlaveConnected(asDevId);


                        string[] sIOModes = sIOMode.Split('&');
                        string sStatus = "";
                        switch (sIOModes.Length)
                        {
                            case 2:
                                sIOMode = sIOModes[0];
                                //sDoorStatus = "Door Remain Closed";
                                sDoorStatus = "Anti Passback / Time zone violation";
                                sStatus = sIOModes[1];
                                if (IsSlave)
                                {
                                    sIOMode = GetSlaveStatus(sIOMode, sStatus);
                                }

                                break;
                            case 1:
                                sDoorStatus = sIOModes[0];
                                sIOMode = "";
                                break;
                            case 3:
                                sIOMode = sIOModes[0];
                                sDoorStatus = sIOModes[1];
                                sStatus = sIOModes[2];
                                if (IsSlave)
                                {
                                    sIOMode = GetSlaveStatus(sIOMode, sStatus);
                                }
                                break;
                        }
                        //sIOMode = sIOModes[0];
                        //sDoorStatus = sIOModes[1];
                        sVerifyMode = FKDataHS105.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }
                    else if (sFKBinDataLib == "FKDataHS100")
                    {
                        sIOMode = FKDataHS100.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        sIOMode = sIOMode.Replace("(", "").Replace(")", "").Replace(" ", "");
                        string[] sIOModes = sIOMode.Split('&');
                        sIOMode = sIOModes[0];
                        sDoorStatus = sIOModes[1];
                        sVerifyMode = FKDataHS100.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }

                    else if (sFKBinDataLib == "FKDataHS200")
                    {
                        sIOMode = FKDataHS200.GLog.GetInOutModeString(Convert.ToInt32(sIOMode));
                        sIOMode = sIOMode.Replace("(", "").Replace(")", "").Replace(" ", "");
                        string[] sIOModes = sIOMode.Split('&');
                        sIOMode = sIOModes[0];
                        sDoorStatus = sIOModes[1];
                        sVerifyMode = FKDataHS200.GLog.GetVerifyModeString(Convert.ToInt32(sVerifyMode));
                    }
                }
                else
                {
                    if (sLogImg.Length > 0)
                        bytLogImage = Convert.FromBase64String(sLogImg);
                }

                aCmdTrans.PrintDebugMsg1(csFuncName, "3");

                sResponse = aCmdTrans.InsertRealtimeGLog(
                        asDevId,
                        sUserId,
                        sVerifyMode,
                        sIOMode,
                        sIOTime,
                        sWorkCode,
                        bytLogImage
                    );

                //to stop status monitoring logs 27-04-2022
                if (sUserId != "0")
                {
                    bool awt_dev = false;
                    DALAwaitingDevice dALAwaitingDevice = new DALAwaitingDevice();
                    awt_dev = dALAwaitingDevice.DetectDeviceInAwaitingDevice(asDevId);
                    if (!awt_dev)
                    {
                        //Send to Client
                        EventLogs log = new EventLogs
                        {
                            DeviceID = asDevId,
                            UserID = sUserId,
                            VerifyMode = sVerifyMode.Replace("\"", "").Replace("[", "").Replace("]", ""),
                            Status = sIOMode,
                            DateTime = sIOTime,
                            WorkCode = sWorkCode,
                            DoorStatus = sDoorStatus,
                            Photo = Convert.ToBase64String(bytLogImage),
                            Polling_DateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                            // ExtraStatus = sExtraStatus
                        };

                        DALWorkPolicyWithEmployee wp = new DALWorkPolicyWithEmployee();
                        wp.InsertWorkPolicy__Employee(log);
                        aCmdTrans.InsertLog(log, true, 0);
                    }
                }


                Response.AddHeader("response_code", sResponse);
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Length", Convert.ToString(0));
                //Response.Flush();

                aCmdTrans.PrintDebugMsg1(csFuncName, "4");
            }
            catch (Exception ex)
            {
                aCmdTrans.PrintDebugMsg1(csFuncName, "Except - 1 - " + ex.ToString());
                // exception이 발생하면 접속을 차단한다.                
                Response.Close();
                return;
            }
        }

        protected string GetSlaveStatus(string sIOMode, string sStatus)
        {

            string status = sIOMode;
            try
            {

                switch (sStatus.ToLower())
                {
                    case "in":
                        status = "1";
                        break;
                    case "out":
                        status = "2";
                        break;
                    default:
                        break;
                }


                //if (!string.IsNullOrEmpty(sStatus))
                //{
                //    string evenNOdd = string.Empty;

                //    if (Formatter.SetValidValueToInt(sIOMode) % 2 == 0)
                //        evenNOdd = "even";
                //    else
                //        evenNOdd = "odd";


                //    switch (sStatus)
                //    {
                //        case "In":
                //            if (evenNOdd == "even")
                //                status = (Formatter.SetValidValueToInt(status) - 1).ToString();
                //            break;
                //        case "Out":
                //            if (evenNOdd == "odd")
                //                status = (Formatter.SetValidValueToInt(status) + 1).ToString();
                //            break;
                //        default:
                //            break;
                //    }
                //}



            }
            catch (Exception ex)
            {
            }
            return status;

        }

        protected void OnRealtimeEnrollData(HttpListenerResponse Response, FKWebCmdTrans aCmdTrans, string asDevId, byte[] abytRequest)
        {
            const string csFuncName = "Page_Load - realtime_enroll_data";

            string sRequest;
            string sResponse;
            byte[] bytRequest = abytRequest;
            byte[] bytEmpty = new byte[0];

            try
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "1");

                // 실시간등록자료가 올라올때 사용자ID는 body부분에 포함되여 올라온다.
                if (aCmdTrans.DevModel == null)
                    sRequest = FKWebCmdTrans.GetStringFromBSCommBuffer(bytRequest);
                else
                    sRequest = System.Text.Encoding.UTF8.GetString(bytRequest);
                if (sRequest.Length == 0)
                {
                    // 만일 실시간등록자료접수시에 올라온 문자렬의 길이가 0이면
                    //  잘못된 요구가 올라온것으로 보고 접속을 차단한다.
                    Response.Close();
                    return;
                }

                aCmdTrans.PrintDebugMsg(csFuncName, "2");

                JObject jobjRequest = JObject.Parse(sRequest);
                string sUserId = jobjRequest["user_id"].ToString();

                aCmdTrans.PrintDebugMsg(csFuncName, "3");

                sResponse = aCmdTrans.InsertRealtimeEnrollData(
                        asDevId,
                        sUserId,
                        bytRequest
                    );

                Response.AddHeader("response_code", sResponse);
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Length", Convert.ToString(0));
                //Response.Flush();

                aCmdTrans.PrintDebugMsg(csFuncName, "4");
            }
            catch (Exception ex)
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "Except - 1 - " + ex.ToString());
                // exception이 발생하면 접속을 차단한다.                
                Response.Close();
                return;
            }
        }

        protected void OnRealtimeDoorData(HttpListenerResponse Response, FKWebCmdTrans aCmdTrans, string asDevId, byte[] abytRequest)
        {
            const string csFuncName = "Page_Load - realtime_door_data";

            string sRequest;
            string sResponse;
            byte[] bytRequest = abytRequest;
            byte[] bytEmpty = new byte[0];

            try
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "1");

                // 실시간등록자료가 올라올때 사용자ID는 body부분에 포함되여 올라온다.
                if (aCmdTrans.DevModel == null)
                    sRequest = FKWebCmdTrans.GetStringFromBSCommBuffer(bytRequest);
                else
                    sRequest = System.Text.Encoding.UTF8.GetString(bytRequest);
                if (sRequest.Length == 0)
                {
                    // 만일 실시간등록자료접수시에 올라온 문자렬의 길이가 0이면
                    //  잘못된 요구가 올라온것으로 보고 접속을 차단한다.
                    Response.Close();
                    return;
                }

                aCmdTrans.PrintDebugMsg(csFuncName, "2");

                JObject jobjRequest = JObject.Parse(sRequest);
                string sDoorStatus = jobjRequest["door_status"].ToString();

                aCmdTrans.PrintDebugMsg(csFuncName, "3");

                sResponse = aCmdTrans.InsertRealtimeDoorData(
                        asDevId,
                        sDoorStatus
                    );

                Response.AddHeader("response_code", sResponse);
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Length", Convert.ToString(0));
                //Response.Flush();

                aCmdTrans.PrintDebugMsg(csFuncName, "4");
            }
            catch (Exception ex)
            {
                aCmdTrans.PrintDebugMsg(csFuncName, "Except - 1 - " + ex.ToString());
                // exception이 발생하면 접속을 차단한다.                
                Response.Close();
                return;
            }
        }




        #endregion




    }
}
