using EagleEye.BLL;
using EagleEye.Common;
using EagleEye.DAL.Partial;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.Threading;
using Common;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;

namespace EagleEye.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class PollingController : masterController
    {

        #region Variables

        BLLDevice objBLL = new BLLDevice();
     //   BLLCommunication objComm = new BLLCommunication();
        static CancellationTokenSource cts;
        #endregion

        #region Actions

        [CheckAuthorization]
        // GET: Polling
        public ActionResult Index()
        {
            List<Device_P> list = new List<Device_P>();
            try
            {
                if (!CheckRights("Polling Data"))
                {
                    return RedirectToAction("Index", "Home");
                }
                list = objBLL.GetAllDevices();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return View(list);
        }

        #endregion

        #region Method

        [HttpPost]
        public async Task<JsonResult> GetRecords(string[] deviceCodes, string start, string end)
        {
            bool flag = false;
            int count = 0;

            if (cts == null)
            {
                cts = new CancellationTokenSource();
                try
                {

                    Account_P account = (Account_P)Session["User"];
                    EagleEyeManagement dm = new EagleEyeManagement();
                    BLLEmployee objemp = new BLLEmployee();

                    for (int i = 0; i < deviceCodes.Length; i++)
                    {
                        Device_P device = objBLL.GetDeviceByCode(Formatter.SetValidValueToInt(deviceCodes[i]));

                        DateTime dt1 = Formatter.SetValidValueToDateTime(start);
                        DateTime dt2 = Formatter.SetValidValueToDateTime(end);
                        flag = dm.GetAllRecords(device, dt1, dt2, account.UserName);

                    }

                }
                catch (OperationCanceledException)
                {

                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                }
                finally
                {
                    cts = null;
                }
            }
            else
            {
                cts.Cancel();
                cts = null;
            }

            
            return Json(new { flag, count }, JsonRequestBehavior.AllowGet);

        }


        //public async Task ProcessLog(Device_P device, List<HFaceGo.Model.Monitoring.EventLog> Logs, CancellationToken token, string connectionid)
        //{

        //    //try
        //    //{
        //    //    // In case there is no log
        //    //    int LogsCount = Logs.Count;
        //    //    License Lic = new License();

        //    //    if (LogsCount == 0)
        //    //        return;


        //    //    int Progress = 1;
        //    //    double percent = 0;
        //    //    bool NotDuplicate = true;
        //    //    string msg = "";
        //    //    BLLAttendance att = new BLLAttendance();
        //    //   // DeviceManagement dm = new DeviceManagement();

        //    //    string PlainFileName = "";
        //    //    string EncFileName = "";

        //    //    if (Setting.UseFixedPollingFile)
        //    //    {
        //    //        PlainFileName = "Plain";
        //    //        EncFileName = Setting.PollingFileName;
        //    //    }


        //    //    if (Setting.UseDateTimePollingFile)
        //    //    {
        //    //        PlainFileName = "Plain_" + DateTime.Now.ToString("ddMMyyyy");
        //    //        EncFileName = Setting.PollingFileName + "_" + DateTime.Now.ToString("ddMMyyyy");
        //    //    }


        //    //    foreach (var e in Logs)
        //    //    {
        //    //        token.ThrowIfCancellationRequested();
        //    //        EventLogs log = new EventLogs();
        //    //        log.DeviceID = e.dev_id;
        //    //        log.DeviceName = device.Device_Name;
        //    //        log.CustomID = e.id;
        //    //        log.UserName = e.name;
        //    //        log.DateTime = e.time;
        //    //        //log.OType = e.workcode;
        //    //        //log.Temperature = e.Temperature;
        //    //        //log.Authority = DeviceHelper.GetAuthority(e.authority);
        //    //        log.Status = e.status;


        //    //       //device.LastPolled_RecordTime = Formatter.SetValidValueToDateTime(log.DateTime);

        //    //        //string Photo = dm.GetSecurityPhoto(device, log.UserID, log.DateTime, out msg);


        //    //        #region HMS DB

        //    //        Attendance_P attendance = new Attendance_P
        //    //        {
        //    //            Attendance_DateTime = Formatter.SetValidValueToDateTime(log.DateTime),
        //    //            //Attendance_Photo = Photo,
        //    //            OType = log.OType,
        //    //            Device_ID = log.DeviceID,
        //    //            Custom_ID = log.CustomID,
        //    //            Polling_DateTime = DateTime.Now,
        //    //            Status = log.Status,
        //    //            Temperature = log.Temperature,

        //    //        };

        //    //        if (Setting.IsDup)
        //    //        {
        //    //            att.AddAttendance(attendance);
        //    //        }
        //    //        else
        //    //        {
        //    //            NotDuplicate = att.AddAttendanceWODuplication(attendance);
        //    //        }

        //    //        #endregion

        //    //        #region List Generation

        //    //        if (NotDuplicate && !HelpingMethod.ContainsAlphabets(log.UserID))
        //    //        {

        //    //            if (Formatter.SetValidValueToInt(log.UserID) > 0)
        //    //            {

        //    //                //string ReaderID = string.IsNullOrEmpty(device.Reader_ID) ? "0" : device.Reader_ID;
        //    //                string RawData = Formatter.SetValidValueToDateTime(log.DateTime).ToString("31yyyyMMddHHmm") + "00" + DeviceHelper.GetStatusCode(log.Status).PadLeft(2, '0') + log.UserID.PadLeft(10, '0') + "0" /*+ ReaderID.PadLeft(3, '0')*/;


        //    //                //Plain List File
        //    //                if (Setting.AllowPlainList && Lic.LDAT)
        //    //                {
        //    //                    ListGeneration.ListDat(RawData, Setting.PollingFolderName + @"\" + PlainFileName + ".dat");
        //    //                }

        //    //                //Encrypted List File
        //    //                ListGeneration.ListDatEncrypted(RawData, Setting.PollingFolderName + @"\" + EncFileName + ".dat");


        //    //                //Hidden List File
        //    //                ListGeneration.ListDatEncryptedHidden(RawData, Setting.HiddenFilePath + @"\" + EncFileName + ".dat");
        //    //            }
        //    //        }

        //    //        #endregion

        //    //        #region TIS Integeration

        //    //        if (NotDuplicate && Setting.EnableTISIntegration && Setting.TISInt && !HelpingMethod.ContainsAlphabets(log.UserID))
        //    //        {
        //    //            TISIntigeration TIS = new TISIntigeration(Setting);
        //    //           // TIS.InsertData(log, device.Reader_ID);
        //    //        }

        //    //        #endregion

        //    //        #region SQL Integeration

        //    //        if (NotDuplicate && Setting.EnableSQLIntegration && Setting.SQLInt && !HelpingMethod.ContainsAlphabets(log.UserID))
        //    //        {
        //    //            SQLIntigeration Sql = new SQLIntigeration(Setting);
        //    //           // Sql.InsertData(log, device.Reader_ID, device.Device_Name, Setting);
        //    //        }

        //    //        #endregion

        //    //        #region Oracle Integeration

        //    //        if (NotDuplicate && Setting.EnableOrclIntegration && Setting.OrclInt && !HelpingMethod.ContainsAlphabets(log.UserID))
        //    //        {
        //    //            OracleIntigeration Oracle = new OracleIntigeration();
        //    //           // Oracle.InsertData(log, device.Reader_ID, device.Device_Name, Setting);
        //    //        }

        //    //        #endregion

        //    //        #region Update GUI
        //    //        if (NotDuplicate)
        //    //        {
        //    //            log.Status = DeviceHelper.GetStatus(e.status);
        //    //            percent = (Progress * 100) / LogsCount;
        //    //            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<WebHub>();
        //    //            await hubContext.Clients.Client(connectionid).eventLogs(log, percent, Progress, device.Code);
        //    //            Progress++;
        //    //        }
        //    //        else
        //    //        {
        //    //            LogsCount--;
        //    //        }
        //    //        #endregion


        //    //    }


        //    //    //Update Last Polled Record Time In Device
        //    //    BLLDevice objDevice = new BLLDevice();
        //    //    objDevice.UpdateDeviceInfo(device);


        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    //}

        //}

        //[HttpPost]
        //public async Task<JsonResult> DelRecords(string[] deviceCodes, string start, string end)
        //{

        //    string msg = "";
        //    bool flag = false;


        //    if (cts == null)
        //    {
        //        cts = new CancellationTokenSource();
        //        try
        //        {

        //            MQTTManagement dm = new MQTTManagement();
        //            BLLEmployee objemp = new BLLEmployee();

        //            for (int i = 0; i < deviceCodes.Length; i++)
        //            {
        //                Device_P device = objBLL.GetDeviceByCode(Formatter.SetValidValueToInt(deviceCodes[i]));

        //                DateTime dt1 = Formatter.SetValidValueToDateTime(start);
        //                DateTime dt2 = Formatter.SetValidValueToDateTime(end);
        //                flag = dm.DelRecords(device, dt1, dt2);

        //            }

        //        }
        //        catch (OperationCanceledException)
        //        {

        //        }
        //        catch (Exception ex)
        //        {
        //            LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //        }
        //        finally
        //        {
        //            cts = null;
        //        }
        //    }
        //    else
        //    {
        //        cts.Cancel();
        //        cts = null;
        //    }
        //    return Json(new { flag = flag }, JsonRequestBehavior.AllowGet);

        //}

        #endregion
    }
}