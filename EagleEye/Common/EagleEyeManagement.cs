using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Collections.Generic;
using System.Data.SqlClient;
using EagleEye.DAL.Partial;
using System.Data;
using System.IO;
using EagleEye.BLL;
using System.Threading;
using System.Configuration;
using Common;

namespace EagleEye.Common
{
    public class EagleEyeManagement : System.Web.UI.Page
    {
        string mDevModel = "";
        string mTransId = "";
        SqlConnection msqlConn;
        //  BLLDevice objBLL = new BLLDevice();
        BLLEmployee objEmpBLL = new BLLEmployee();
        BLLOperationLog objOLog = new BLLOperationLog();
        BLLDevice objDev = new BLLDevice();
        public SqlConnection GetDBPool()
        {
            string msDbConn;
            try
            {
                if (msqlConn == null)
                {

                    msDbConn = ConfigurationManager.ConnectionStrings["testCon"].ConnectionString.ToString();
                    msqlConn = new SqlConnection(msDbConn);
                    msqlConn.Open();
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return msqlConn;
        }
        private string GetJsonString(string sTransId)
        {
            FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
            string sCmdCode = "";
            string sResultText = "";
            string sReturn_code = "";
            try
            {
                if (sTransId.Length == 0)
                    return null;
                string sSql = "select trans_id, cmd_code, return_code from tbl_fkcmd_trans where trans_id='" + sTransId + "' AND status='RESULT'";
                if (msqlConn.State != ConnectionState.Open)
                    msqlConn.Open();

                SqlCommand sqlCmd = new SqlCommand(sSql, msqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                sTransId = "";
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        sTransId = sqlReader.GetString(0);
                        sCmdCode = sqlReader.GetString(1);
                        sReturn_code = sqlReader.GetString(2);
                        if (!"OK".Equals(sReturn_code))
                        {

                            return null;
                        }
                    }
                }
                sqlReader.Close();

                if (sTransId.Length == 0)
                    return null;

                sSql = "select @cmd_result=cmd_result from tbl_fkcmd_trans_cmd_result where trans_id='" + sTransId + "'";
                sqlCmd = new SqlCommand(sSql, msqlConn);
                SqlParameter sqlParamCmdParamBin = new SqlParameter("@cmd_result", SqlDbType.VarBinary);
                sqlParamCmdParamBin.Direction = ParameterDirection.Output;
                sqlParamCmdParamBin.Size = -1;
                sqlCmd.Parameters.Add(sqlParamCmdParamBin);

                sqlCmd.ExecuteNonQuery();

                byte[] bytCmdResult = (byte[])sqlParamCmdParamBin.Value;

                byte[] bytResultBin = new byte[0];



                cmdTrans.GetStringAndBinaryFromBSCommBuffer(bytCmdResult, out sResultText, out bytResultBin);

                if (sResultText.Length == 0)
                    return null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            finally
            {
                msqlConn.Close();
            }

            return sResultText;
        }

        public bool GetDeviceSettings(string mDevId, string Username)
        {
            msqlConn = GetDBPool();
            string mDevModel = "";
            bool flag = false;
            byte[] mByteParam = new byte[0];
            string mStrParam;
            try
            {
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                JObject vResultJson = new JObject();

                vResultJson.Add("dev_id", mDevId);
                mStrParam = vResultJson.ToString(Formatting.None);
                // LogService.WriteLog("[GetDeviceStatus]: " + mStrParam);
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(mStrParam, out mByteParam);
                else
                    mByteParam = System.Text.Encoding.UTF8.GetBytes(mStrParam);
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_DEVICE_SETTING", mDevId, mByteParam);

                bool oflag = InsertOperationLog(mDevId, "Getting Device Settings", mTransId, Username);
                flag = true;
                // sTransId = mTransId;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            //sTransId = mTransId;
            msqlConn = null;
            return flag;
        }

        public bool GetDeviceStatus(string mDevId, string Username)
        {
            msqlConn = GetDBPool();
            string mDevModel = "";
            bool flag = false;
            byte[] mByteParam = new byte[0];
            string mStrParam;
            try
            {
                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();

                vResultJson.Add("dev_id", mDevId);
                mStrParam = vResultJson.ToString(Formatting.None);
                // LogService.WriteLog("[GetDeviceStatus]: " + mStrParam);
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(mStrParam, out mByteParam);
                else
                    mByteParam = System.Text.Encoding.UTF8.GetBytes(mStrParam);
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_DEVICE_STATUS", mDevId, mByteParam);
                // sTransId = mTransId; 

                bool oflag = InsertOperationLog(mDevId, "Getting Device Capicity", mTransId, Username);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            // sTransId = mTransId;
            msqlConn = null;
            return flag;

        }

        public bool GetUserInfo(string mDevId, string mUserId, string Username)
        {
            msqlConn = GetDBPool();
            bool flag = false;
            byte[] mByteParam = new byte[0];
            string mStrParam;
            try
            {
                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                vResultJson.Add("user_id", mUserId);
                mStrParam = vResultJson.ToString(Formatting.None);
                cmdTrans.CreateBSCommBufferFromString(mStrParam, out mByteParam);
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_USER_INFO", mDevId, mByteParam);
                // sTransId = mTransId; 
                string action = "Getting User Info by User ID: " + mUserId;
                bool oflag = InsertOperationLog(mDevId, action, mTransId, Username);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            // sTransId = mTransId;
            msqlConn = null;
            return flag;

        }

        public Device_P DisplayDeviceSetting(string sTransId, int Code)
        {
            JObject vResultJson;
            Device_P deviceInfo = new Device_P();
            msqlConn = GetDBPool();
            try
            {
                string json_str = GetJsonString(sTransId);
                Thread.Sleep(2000);
                if (json_str == null)
                {
                    Thread.Sleep(2000);
                    msqlConn.Close();
                    msqlConn = null;
                    return DisplayDeviceSetting(sTransId, Code);

                }
                else
                {
                    vResultJson = JObject.Parse(json_str);
                    //LogService.WriteLog("[DisplayDeviceSetting]" + vResultJson);
                    deviceInfo.Code = Code;
                    if (vResultJson.Property("OpenDoor_Delay") != null)
                        deviceInfo.OpenDoor_Delay = vResultJson["OpenDoor_Delay"].ToString();
                    if (vResultJson.Property("DoorMagnetic_Delay") != null)
                        deviceInfo.DoorMagnetic_Delay = vResultJson["DoorMagnetic_Delay"].ToString();
                    if (vResultJson.Property("Alarm_Delay") != null)
                        deviceInfo.Alarm_Delay = vResultJson["Alarm_Delay"].ToString();
                    if (vResultJson.Property("Sleep_Time") != null)
                        deviceInfo.Sleep_Time = vResultJson["Sleep_Time"].ToString();
                    if (vResultJson.Property("Screensavers_Time") != null)
                        deviceInfo.Screensavers_Time = vResultJson["Screensavers_Time"].ToString();
                    if (vResultJson.Property("Reverify_Time") != null)
                        deviceInfo.Reverify_Time = vResultJson["Reverify_Time"].ToString();
                    if (vResultJson.Property("DoorMagnetic_Type") != null)
                        deviceInfo.DoorMagnetic_Type = vResultJson["DoorMagnetic_Type"].ToString();
                    if (vResultJson.Property("Anti-back") != null)
                        deviceInfo.Anti_back = vResultJson["Anti-back"].ToString();
                    if (vResultJson.Property("Use_Alarm") != null)
                        deviceInfo.Use_Alarm = vResultJson["Use_Alarm"].ToString();
                    if (vResultJson.Property("Wiegand_Type") != null)
                        deviceInfo.Wiegand_Type = vResultJson["Wiegand_Type"].ToString();
                    if (vResultJson.Property("Glog_Warning") != null)
                        deviceInfo.Glog_Warning = vResultJson["Glog_Warning"].ToString();
                    if (vResultJson.Property("Volume") != null)
                        deviceInfo.Volume = vResultJson["Volume"].ToString();
                    if (vResultJson.Property("Allow_EarlyTime") != null)
                        deviceInfo.Allow_EarlyTime = vResultJson["Allow_EarlyTime"].ToString();
                    if (vResultJson.Property("Allow_LateTime") != null)
                        deviceInfo.Allow_LateTime = vResultJson["Allow_LateTime"].ToString();
                    if (vResultJson.Property("Receive_Interval") != null)
                        deviceInfo.Receive_Interval = vResultJson["Receive_Interval"].ToString();
                    if (vResultJson.Property("Wiegand_Input") != null)
                        deviceInfo.Wiegand_Input = vResultJson["Wiegand_Input"].ToString();
                    if (vResultJson.Property("Wiegand_Output") != null)
                        deviceInfo.Wiegand_Output = vResultJson["Wiegand_Output"].ToString();
                    if (vResultJson.Property("Show_ResultTime") != null)
                        deviceInfo.Show_ResultTime = vResultJson["Show_ResultTime"].ToString();
                    //objBLL.UpdateDeviceSettings(deviceInfo);
                }
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return deviceInfo;
        }

        public bool SetDeviceTIme(string mDevId, DateTime dt, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                // DateTime now = DateTime.Now;
                string sNowTxt = FKWebTools.GetFKTimeString14(dt);
                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                vResultJson.Add("time", sNowTxt);
                string sFinal = vResultJson.ToString(Formatting.None);
                // LogService.WriteLog("[SetDeviceTIme]: " + sFinal);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);
                mTransId = FKWebTools.MakeCmd(msqlConn, "SET_TIME", mDevId, strParam);

                bool oflag = InsertOperationLog(mDevId, "Sync Device Time", mTransId, Username);
                //Enables(false);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool SetDeviceName(string mDevId, string mDevName, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();

                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                vResultJson.Add("fk_name", mDevName);
                string sFinal = vResultJson.ToString(Formatting.None);
                //  LogService.WriteLog("[SetDeviceName]: " + sFinal);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);
                mTransId = FKWebTools.MakeCmd(msqlConn, "SET_FK_NAME", mDevId, strParam);

                bool oflag = InsertOperationLog(mDevId, "Setting Device Name", mTransId, Username);

                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public void DeleteCommands(string transID, string DeviceID)
        {
            try
            {
                msqlConn = GetDBPool();
                FKWebTools.DelCmd(msqlConn, transID, DeviceID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
        }

        public bool SetNetworkInfo(string mDevId, string ServerIP, string ServerPort, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();

                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                vResultJson.Add("ip_address", ServerIP);
                vResultJson.Add("port", ServerPort);
                string sFinal = vResultJson.ToString(Formatting.None);
                //   LogService.WriteLog("[SetNetworkInfo]: " + sFinal);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);
                mTransId = FKWebTools.MakeCmd(msqlConn, "SET_WEB_SERVER_INFO", mDevId, strParam);

                bool oflag = InsertOperationLog(mDevId, "Setting Network Info", mTransId, Username);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool InitDeviceAdmin(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "CLEAR_MANAGER", mDevId, null);
                bool oflag = InsertOperationLog(mDevId, "Clearing All Managers", mTransId, Username);
                flag = true;
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool InitDeviceLogs(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "CLEAR_LOG_DATA", mDevId, null);

                bool oflag = InsertOperationLog(mDevId, "Clearing All Log Data", mTransId, Username);
                flag = true;
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool ResetFK(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "RESET_FK", mDevId, null);

                bool oflag = InsertOperationLog(mDevId, "Reset Device", mTransId, Username);
                flag = true;
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool InitDeviceUsers(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "CLEAR_ENROLL_DATA", mDevId, null);
                bool oflag = InsertOperationLog(mDevId, "Clearing All Enrolled Users", mTransId, Username);
                flag = true;
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool SetDeviceSetting(Device_P deviceInfo, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                JObject vResultJson = new JObject();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                string vdata = "";
                string mDevId = deviceInfo.Device_ID;
                vdata = deviceInfo.OpenDoor_Delay.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("OpenDoor_Delay", vdata);

                //  LogService.WriteLog("[OpenDoor_Delay]: " + vdata);

                if (deviceInfo.DoorMagnetic_Type != "NO ACCESS")
                {
                    vdata = deviceInfo.DoorMagnetic_Type.ToString();
                    if (!string.IsNullOrEmpty(vdata))
                        vResultJson.Add("DoorMagnetic_Type", vdata);
                    //LogService.WriteLog("[DoorMagnetic_Type]: " + vdata);
                }
                if (deviceInfo.DoorMagnetic_Delay != "NO ACCESS")
                {
                    vdata = deviceInfo.DoorMagnetic_Delay.ToString();
                    if (!string.IsNullOrEmpty(vdata))
                        vResultJson.Add("DoorMagnetic_Delay", vdata);

                    //LogService.WriteLog("[DoorMagnetic_Delay]: " + vdata);
                }
                if (deviceInfo.Anti_back != "NO ACCESS")
                {
                    vdata = deviceInfo.Anti_back.ToString();
                    if (string.IsNullOrEmpty(vdata))
                        vResultJson.Add("Anti-back", "no");
                    else
                        vResultJson.Add("Anti-back", vdata);


                    //   LogService.WriteLog("[Anti-back]: " + vdata);
                }
                //if (deviceInfo.Alarm_Delay != "NO ACCESS")
                //{
                //    vdata = deviceInfo.Alarm_Delay.ToString();
                //    if (!string.IsNullOrEmpty(vdata))
                //        vResultJson.Add("Alarm_Delay", vdata);

                //    //  LogService.WriteLog("[Alarm_Delay]: " + vdata);
                //}
                //if (deviceInfo.Use_Alarm != "NO ACCESS")
                //{
                //    vdata = deviceInfo.Use_Alarm.ToString();
                //    if (!string.IsNullOrEmpty(vdata))
                //        vResultJson.Add("Use_Alarm", vdata);

                //    //  LogService.WriteLog("[Use_Alarm]: " + vdata);
                //}
                vdata = deviceInfo.Wiegand_Type.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Wiegand_Type", vdata);

                //  LogService.WriteLog("[Wiegand_Type]: " + vdata);

                vdata = deviceInfo.Wiegand_Input.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Wiegand_Input", vdata);

                //  LogService.WriteLog("[Wiegand_Input]: " + vdata);

                vdata = deviceInfo.Wiegand_Output.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Wiegand_Output", vdata);
                // LogService.WriteLog("[Wiegand_Output]: " + vdata);

                vdata = deviceInfo.Sleep_Time.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Sleep_Time", vdata);

                // LogService.WriteLog("[Sleep_Time]: " + vdata);

                vdata = deviceInfo.Screensavers_Time.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Screensavers_Time", vdata);

                //  LogService.WriteLog("[Screensavers_Time]: " + vdata);

                vdata = deviceInfo.Reverify_Time.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Reverify_Time", vdata);

                //LogService.WriteLog("[Reverify_Time]: " + vdata);

                //if (deviceInfo.Receive_Interval != null)
                //{
                //    vdata = deviceInfo.Receive_Interval.ToString();
                //    if (!string.IsNullOrEmpty(vdata))
                //        vResultJson.Add("Receive_Interval", vdata);

                //    //LogService.WriteLog("[Receive_Interval]: " + vdata);
                //}

                vdata = deviceInfo.Glog_Warning.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Glog_Warning", vdata);

                //LogService.WriteLog("[Glog_Warning]: " + vdata);

                vdata = deviceInfo.Volume.ToString();
                if (!string.IsNullOrEmpty(vdata))
                    vResultJson.Add("Volume", vdata);

                //LogService.WriteLog("[Volume]: " + vdata);

                //vdata = deviceInfo.Allow_EarlyTime.ToString();
                //if (!string.IsNullOrEmpty(vdata))
                //    vResultJson.Add("Allow_EarlyTime", vdata);

                ////   LogService.WriteLog("[Allow_EarlyTime]: " + vdata);

                //vdata = deviceInfo.Allow_LateTime.ToString();
                //if (!string.IsNullOrEmpty(vdata))
                //    vResultJson.Add("Allow_LateTime", vdata);

                // LogService.WriteLog("[Allow_LateTime]: " + vdata);

                if (deviceInfo.Show_ResultTime != null)
                {
                    vdata = deviceInfo.Show_ResultTime.ToString();
                    if (!string.IsNullOrEmpty(vdata))
                        vResultJson.Add("Show_ResultTime", vdata);
                    //   LogService.WriteLog("[Show_ResultTime]: " + vdata);
                }
                if (deviceInfo.Multi_Users != null)
                {
                    vdata = deviceInfo.Multi_Users.ToString();
                    if (!string.IsNullOrEmpty(vdata))
                        vResultJson.Add("MutiUser", vdata);
                    //   LogService.WriteLog("[Show_ResultTime]: " + vdata);
                }

                if (deviceInfo.Receive_Interval != null)
                {
                    vdata = deviceInfo.Receive_Interval.ToString();
                    if(!string.IsNullOrEmpty(vdata))
                        vResultJson.Add("Receive_Interval", vdata);
                }

               
                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);
                mTransId = FKWebTools.MakeCmd(msqlConn, "SET_DEVICE_SETTING", mDevId, strParam);

                bool oflag = InsertOperationLog(mDevId, "Updating Device Settings", mTransId, Username);
                flag = true;

                msqlConn = null;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteUserfromMachine(string sUserId, string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                JObject vResultJson = new JObject();
                vResultJson.Add("user_id", sUserId);
                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                mTransId = FKWebTools.MakeCmd(msqlConn, "DELETE_USER", mDevId, strParam);
                string action = "Delete User by User ID: " + sUserId;
                bool oflag = InsertOperationLog(mDevId, action, mTransId, Username);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool DeleteUserfromMachine(string sUserId, string mDevId, string Username, bool Active)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                JObject vResultJson = new JObject();
                vResultJson.Add("user_id", sUserId);
                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);

                if (Active == false)
                    mTransId = FKWebTools.MakeCmd(msqlConn, "DELETE_USER", mDevId, strParam, sUserId);
                string action = "Delete User by User ID: " + sUserId;
                bool oflag = InsertOperationLog(mDevId, action, mTransId, Username);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        //public List<Employee_P> GetEmployees(string mDevId)
        //{
        //    List<Employee_P> userList = new List<Employee_P>();
        //    try
        //    {
        //        msqlConn = GetDBPool();
        //        mTransId = FKWebTools.MakeCmd(msqlConn, "GET_USER_ID_LIST", mDevId, null);
        //       // objEmpBLL.DeleteEmployeeByDevice_ID(mDevId);
        //        Thread.Sleep(8000);

        //        userList = objEmpBLL.GetAllUserIds(mDevId);
        //        if (userList.Count == 0)
        //        {

        //            GetEmployees(mDevId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    msqlConn = null;
        //    return userList;
        //}

        public bool GetEmployees(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_USER_ID_LIST", mDevId, null);
                // string action = "Delete User by User ID: " + sUserId;
                bool oflag = InsertOperationLog(mDevId, "Getting All Users IDs", mTransId, Username);
                flag = true;
                // objEmpBLL.DeleteEmployeeByDevice_ID(mDevId);
                //Thread.Sleep(8000);

                //userList = objEmpBLL.GetAllUserIds(mDevId);
                //if (userList.Count == 0)
                //{

                //    GetEmployees(mDevId);
                //}

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool SyncDevice(string mDevId, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_ALL_USER_INFO", mDevId, null);

                bool oflag = InsertOperationLog(mDevId, "Sync Users from Device", mTransId, Username);
                // flag = objEmpBLL.DeleteEmployeeByCode(mDevId);
                flag = true;
                msqlConn = null;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        public bool SetTimeZone(string mDevId, TimeZone_P tz, int status, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                JObject vResultJson = new JObject();
                JObject vT;
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                string start = "", end = "";
                vResultJson.Add("TimeZone_No", tz.Timezone_No);

                vT = new JObject();
                start = tz.Period_1_Start.ToString();
                end = tz.Period_1_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T1", vT);

                vT = new JObject();
                start = tz.Period_2_Start.ToString();
                end = tz.Period_2_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T2", vT);

                vT = new JObject();
                start = tz.Period_3_Start.ToString();
                end = tz.Period_3_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T3", vT);

                vT = new JObject();
                start = tz.Period_4_Start.ToString();
                end = tz.Period_4_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T4", vT);

                vT = new JObject();
                start = tz.Period_5_Start.ToString();
                end = tz.Period_5_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T5", vT);

                vT = new JObject();
                start = tz.Period_6_Start.ToString();
                end = tz.Period_6_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T6", vT);

                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);

                if (status != 0)
                    mTransId = FKWebTools.MakeCmdForTZ(msqlConn, "SET_TIMEZONE", mDevId, strParam, tz.Timezone_No);
                else
                    mTransId = FKWebTools.MakeCmdForTZOffline(msqlConn, "SET_TIMEZONE", mDevId, strParam, tz.Timezone_No);

                string action = "Set TimeZone No: " + tz.Timezone_No;
                bool oflag = InsertOperationLog(mDevId, action, mTransId, Username);

                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool SetPassTime(string mDevId, Employee_P passtime, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                JObject vResultJson = new JObject();
                JArray vResultJarr = new JArray();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                string vuser_id = passtime.Employee_ID;
                string vValide_Date_start = passtime.Valid_DateStart;
                string vValide_Date_end = passtime.Valid_DateEnd;
                string vTimeZoneNo = "";

                if (!string.IsNullOrEmpty(vuser_id) || !string.IsNullOrEmpty(vValide_Date_start) || !string.IsNullOrEmpty(vValide_Date_end))
                {

                    vResultJson.Add("user_id", vuser_id);
                    vResultJson.Add("Valide_Date_start", vValide_Date_start);
                    vResultJson.Add("Valide_Date_end", vValide_Date_end);

                    vTimeZoneNo = passtime.Sunday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Monday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Tuesday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Wednesday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Thursday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Friday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vTimeZoneNo = passtime.Saturday;
                    if (string.IsNullOrEmpty(vTimeZoneNo)) vTimeZoneNo = "1";
                    vResultJarr.Add(vTimeZoneNo);

                    vResultJson.Add("Week_TimeZone_No", vResultJarr);
                    string sFinal = vResultJson.ToString(Formatting.None);
                    byte[] strParam = new byte[0];
                    if (string.IsNullOrEmpty(mDevModel))
                        cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);
                    else
                        strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);
                    mTransId = FKWebTools.MakeCmd(msqlConn, "SET_USER_PASSTIME", mDevId, strParam);

                    string action = "Set User Access by User ID: " + vuser_id;
                    bool oflag = InsertOperationLog(mDevId, action, mTransId, Username);
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }
        public Employee_P DecryptEmployees(byte[] encEmployee, string mDevId, out Employee_P employee)
        {
            FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
            BLLDevice bLLDevice = new BLLDevice();
            JObject vResultJson;
            byte[] bytResultBin = new byte[0];
            string sResultText = "";
            employee = new Employee_P();
            string vStrUserPhotoBinIndex = "";
            string enroll_data = "";
            //  string Status = "";
            try
            {
                cmdTrans.GetStringAndBinaryFromBSCommBuffer(encEmployee, out sResultText, out bytResultBin);
                if (sResultText.Length == 0)
                    return employee = null;

                vResultJson = JObject.Parse(sResultText);
                employee.Employee_ID = vResultJson["user_id"].ToString();
                employee.Employee_Name = vResultJson["user_name"].ToString();
                employee.User_Privilege = vResultJson["user_privilege"].ToString();

                employee.fkLocation_Code = Formatter.SetValidValueToInt(bLLDevice.GetDeviceByDevice_ID(mDevId).Device_Location);
                //try
                //{
                //    sUserVID = vResultJson["user_vid"].ToString();
                //    //VID.Text = sUserVID;
                //}
                //catch (Exception ex) { LogException(ex, ExceptionLayer.Controller, GetCurrentMethod()); }
                int vnBinIndex = 0;
                int vnBinCount = FKWebTools.BACKUP_MAX + 1;
                int[] vnBackupNumbers = new int[vnBinCount];

                for (int i = 0; i < vnBinCount; i++)
                {
                    vnBackupNumbers[i] = -1;
                    if (i <= FKWebTools.BACKUP_FP_9)
                        FKWebTools.mFinger[i] = new byte[0];
                    if ((i >= FKWebTools.BACKUP_PALM_1) && (i <= FKWebTools.BACKUP_PALM_2))
                        FKWebTools.mPalm[i - FKWebTools.BACKUP_PALM_1] = new byte[0];
                }
                FKWebTools.mFace = new byte[0];
                FKWebTools.mPhoto = new byte[0];


                if (string.IsNullOrEmpty(mDevModel))
                {
                    try
                    {
                        if (vResultJson.ContainsKey("user_photo"))
                        {
                            vStrUserPhotoBinIndex = vResultJson["user_photo"].ToString(); //aCmdParamJson.get("user_photo", "").asString();
                            if (vStrUserPhotoBinIndex.Length != 0)
                            {
                                vnBinIndex = FKWebTools.GetBinIndex(vStrUserPhotoBinIndex) - 1;
                                vnBackupNumbers[vnBinIndex] = FKWebTools.BACKUP_USER_PHOTO;
                            }
                        }

                    }
                    catch (Exception ex) { LogException(ex, ExceptionLayer.Controller, GetCurrentMethod()); }

                    enroll_data = vResultJson["enroll_data_array"].ToString();

                    if (enroll_data.Equals("null") || enroll_data == "null" || enroll_data.Length == 0)
                    {
                        //string Status = "Enroll data is empty !!!";
                        //return;
                    }
                    JArray vEnrollDataArrayJson = new JArray();
                    //  string read = vResultJson["enroll_data_array"].ToString();
                    if (vResultJson["enroll_data_array"].ToString() != "")
                    {
                        vEnrollDataArrayJson = JArray.Parse(vResultJson["enroll_data_array"].ToString());
                    }
                    foreach (JObject content in vEnrollDataArrayJson.Children<JObject>())
                    {
                        int vnBackupNumber = Convert.ToInt32(content["backup_number"].ToString());

                        string vStrBinIndex = content["enroll_data"].ToString();
                        vnBinIndex = FKWebTools.GetBinIndex(vStrBinIndex) - 1;
                        vnBackupNumbers[vnBinIndex] = vnBackupNumber;

                    }
                    for (int i = 0; i < vnBinCount; i++)
                    {
                        if (vnBackupNumbers[i] == -1) continue;

                        if (vnBackupNumbers[i] == FKWebTools.BACKUP_USER_PHOTO)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);

                            if (!Directory.Exists(Server.MapPath("~/profiles/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/profiles/"));
                            }
                            string AbsImgUri = Server.MapPath("~/profiles/") + "Profile" + "_" + employee.Employee_ID + ".jpg";
                            string relativeImgUrl = "/profiles/" + "Profile" + "_" + employee.Employee_ID + ".jpg";
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);
                            FKWebTools.mPhoto = new byte[vnBinLen];
                            employee.photo_data = new byte[vnBinLen];

                            Array.Copy(bytResultBinParam, FKWebTools.mPhoto, vnBinLen);
                            Array.Copy(bytResultBinParam, employee.photo_data, vnBinLen);
                            try
                            {
                                FileStream fs = new FileStream(AbsImgUri, FileMode.Create, FileAccess.Write);
                                fs.Write(bytResultBinParam, 0, bytResultBinParam.Length);
                                fs.Close();
                            }
                            catch
                            { }
                            employee.Employee_Photo = relativeImgUrl;

                        }
                        if (vnBackupNumbers[i] == FKWebTools.BACKUP_PSW)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            employee.Password = cmdTrans.GetStringFromBSCommBuffer(bytResultBin);
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);

                        }
                        if (vnBackupNumbers[i] == FKWebTools.BACKUP_CARD)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            employee.Card_No = cmdTrans.GetStringFromBSCommBuffer(bytResultBin);
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);
                        }

                        if (vnBackupNumbers[i] == FKWebTools.BACKUP_FACE)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);
                            employee.Face = true;
                            FKWebTools.mFace = new byte[vnBinLen];
                            employee.face_data = new byte[vnBinLen];
                            Array.Copy(bytResultBinParam, FKWebTools.mFace, vnBinLen);
                            Array.Copy(bytResultBinParam, employee.face_data, vnBinLen);
                        }

                        if (vnBackupNumbers[i] >= FKWebTools.BACKUP_PALM_1 && vnBackupNumbers[i] <= FKWebTools.BACKUP_PALM_2)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);
                            employee.Palm = true;
                            FKWebTools.mPalm[vnBackupNumbers[i] - FKWebTools.BACKUP_PALM_1] = new byte[vnBinLen];
                            //mPalm = new byte[vnBinLen];
                            switch (vnBackupNumbers[i])
                            {
                                case 13:
                                    employee.palm_0 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.palm_0, vnBinLen);
                                    break;
                                case 14:
                                    employee.palm_1 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.palm_1, vnBinLen);
                                    break;
                            }
                            Array.Copy(bytResultBinParam, FKWebTools.mPalm[vnBackupNumbers[i] - FKWebTools.BACKUP_PALM_1], vnBinLen);
                        }

                        if (vnBackupNumbers[i] >= FKWebTools.BACKUP_FP_0 && vnBackupNumbers[i] <= FKWebTools.BACKUP_FP_9)
                        {
                            byte[] bytResultBinParam = new byte[0];
                            int vnBinLen = FKWebTools.GetBinarySize(bytResultBin, out bytResultBin);
                            FKWebTools.GetBinaryData(bytResultBin, vnBinLen, out bytResultBinParam, out bytResultBin);
                            employee.FingerPrint = true;
                            FKWebTools.mFinger[vnBackupNumbers[i]] = new byte[vnBinLen];
                            byte[] mFinger = bytResultBinParam;
                            switch (vnBackupNumbers[i])
                            {
                                case 0:
                                    employee.finger_0 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_0, vnBinLen);
                                    break;
                                case 1:
                                    employee.finger_1 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_1, vnBinLen);
                                    break;
                                case 2:
                                    employee.finger_2 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_2, vnBinLen);
                                    break;
                                case 3:
                                    employee.finger_3 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_3, vnBinLen);
                                    break;
                                case 4:
                                    employee.finger_4 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_4, vnBinLen);
                                    break;
                                case 5:
                                    employee.finger_5 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_5, vnBinLen);
                                    break;
                                case 6:
                                    employee.finger_6 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_6, vnBinLen);
                                    break;
                                case 7:
                                    employee.finger_7 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_7, vnBinLen);
                                    break;
                                case 8:
                                    employee.finger_8 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_8, vnBinLen);
                                    break;
                                case 9:
                                    employee.finger_9 = new byte[vnBinLen];
                                    Array.Copy(bytResultBinParam, employee.finger_9, vnBinLen);
                                    break;
                            }

                            Array.Copy(bytResultBinParam, FKWebTools.mFinger[vnBackupNumbers[i]], vnBinLen);
                        }
                    }
                }
                else
                {

                    enroll_data = vResultJson["enroll_data_array"].ToString();

                    if (enroll_data.Equals("null") || enroll_data == "null" || enroll_data.Length == 0)
                    {
                        // string Status = "Enroll data is empty !!!";

                    }

                    JArray vEnrollDataArrayJson = JArray.Parse(vResultJson["enroll_data_array"].ToString());

                    foreach (JObject content in vEnrollDataArrayJson.Children<JObject>())
                    {
                        int vnBackupNumber = Convert.ToInt32(content["backup_number"].ToString());

                        if (vnBackupNumber == FKWebTools.BACKUP_PSW)
                        {
                            employee.Password = content["enroll_data"].ToString();
                        }
                        else if (vnBackupNumber == FKWebTools.BACKUP_CARD)
                        {
                            employee.Card_No = content["enroll_data"].ToString();
                        }
                        else if (vnBackupNumber == FKWebTools.BACKUP_FACE)
                        {
                            employee.Face = true;
                            employee.face_data = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                            FKWebTools.mFace = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());

                        }
                        else if (vnBackupNumber >= FKWebTools.BACKUP_PALM_1 && vnBackupNumber <= FKWebTools.BACKUP_PALM_2)
                        {
                            employee.Palm = true;
                            switch (vnBackupNumber)
                            {
                                case 0:
                                    employee.palm_0 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 1:
                                    employee.palm_1 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                            }


                            FKWebTools.mPalm[vnBackupNumber - FKWebTools.BACKUP_PALM_1] = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                        }
                        else if (vnBackupNumber >= FKWebTools.BACKUP_FP_0 && vnBackupNumber <= FKWebTools.BACKUP_FP_9)
                        {
                            employee.FingerPrint = true;
                            switch (vnBackupNumber)
                            {
                                case 0:
                                    employee.finger_0 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 1:
                                    employee.finger_1 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 2:
                                    employee.finger_2 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 3:
                                    employee.finger_3 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 4:
                                    employee.finger_4 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 5:
                                    employee.finger_5 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 6:
                                    employee.finger_6 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 7:
                                    employee.finger_7 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 8:
                                    employee.finger_8 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                                case 9:
                                    employee.finger_9 = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                                    break;
                            }
                            FKWebTools.mFinger[vnBackupNumber] = System.Text.Encoding.UTF8.GetBytes(content["enroll_data"].ToString());
                        }
                    }

                    try
                    {
                        string vsUserPhoto = vResultJson["user_photo"].ToString();

                        if (!Directory.Exists(Server.MapPath("~/profiles/"))) { Directory.CreateDirectory(Server.MapPath("~/profiles/")); }
                        string AbsImgUri = Server.MapPath("~/profiles/") + mDevId + "_" + employee.Employee_ID + ".jpg";
                        string relativeImgUrl = Server.MapPath("~/profiles/") + mDevId + "_" + employee.Employee_ID + ".jpg";
                        employee.photo_data = System.Text.Encoding.UTF8.GetBytes(vsUserPhoto);
                        FKWebTools.mPhoto = System.Text.Encoding.UTF8.GetBytes(vsUserPhoto);
                        FileStream fs = new FileStream(AbsImgUri, FileMode.Create, FileAccess.Write);
                        fs.Write(FKWebTools.mPhoto, 0, FKWebTools.mPhoto.Length);

                        fs.Close();
                        employee.Employee_Photo = relativeImgUrl;
                    }
                    catch (Exception ex)
                    {
                        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                    }
                }

                //LogService.WriteLog("Employee_ID: " + employee.Employee_ID + " | Employee_Name: " + employee.Employee_Name + " | User_Privilege: " + employee.User_Privilege
                //      + " | sUserVID: " + sUserVID + " | vStrUserPhotoBinIndex: " + vStrUserPhotoBinIndex + " | enroll_data: "
                //      + enroll_data + " | Status: " + Status + " | FP: " + employee.FingerPrint + " | Face: " + employee.Face + " | Palm: " + employee.Palm
                //      + " | Password: " + employee.Password + " | Card_No: " + employee.Card_No + " | Employee_Photo: " + employee.Employee_Photo);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return employee;
        }

        public byte[] EncryptEmployee(Employee_P employee, out byte[] encEmployee)
        {
            encEmployee = new byte[0];
            try
            {
                JObject vResultJson = new JObject();
                JArray vEnrollDataArrayJson = new JArray();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                string sUserPwd = employee.Password;
                string sUserCard = employee.Card_No;
                int index = 1;
                string sUserId = employee.Employee_ID;
                string sUserName = employee.Employee_Name;
                string sUserPriv = employee.User_Privilege;
                string sUserVID = "";
                string sDevId = employee.Device_Id;


                if (sUserVID.Length == 0)
                    sUserVID = " ";


                string ImageUrl = Server.MapPath("~/profiles/") + sDevId + "_" + sUserId + ".jpg";

                vResultJson.Add("user_id", sUserId);
                vResultJson.Add("user_name", sUserName);
                vResultJson.Add("user_privilege", sUserPriv);
                vResultJson.Add("user_vid", sUserVID);

                //if (FKWebTools.mPhoto.Length > 0)
                //    vResultJson.Add("user_photo", FKWebTools.GetBinIndexString(index++));

                if (employee.photo_data.Length > 0)
                    vResultJson.Add("user_photo", FKWebTools.GetBinIndexString(index++));


                for (int nIndex = 0; nIndex <= FKWebTools.BACKUP_FP_9; nIndex++)
                {

                    //if (FKWebTools.mFinger[nIndex].Length > 0)
                    //{
                    //    JObject vEnrollDataJson = new JObject();
                    //    vEnrollDataJson.Add("backup_number", nIndex);
                    //    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    //    vEnrollDataArrayJson.Add(vEnrollDataJson);
                    //}
                    switch (nIndex)
                    {
                        case 0:
                            if (employee.finger_0.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 1:
                            if (employee.finger_1.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 2:
                            if (employee.finger_2.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 3:
                            if (employee.finger_3.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 4:
                            if (employee.finger_4.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 5:
                            if (employee.finger_5.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 6:
                            if (employee.finger_6.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 7:
                            if (employee.finger_7.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 8:
                            if (employee.finger_8.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 9:
                            if (employee.finger_9.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                    }
                    //if (employee.finger_0.Length > 0)
                    //{
                    //    JObject vEnrollDataJson = new JObject();
                    //    vEnrollDataJson.Add("backup_number", nIndex);
                    //    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    //    vEnrollDataArrayJson.Add(vEnrollDataJson);
                    //}
                }
                for (int nIndex = FKWebTools.BACKUP_PALM_1; nIndex <= FKWebTools.BACKUP_PALM_2; nIndex++)
                {

                    switch (nIndex)
                    {
                        case 0:
                            if (employee.palm_0.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                        case 1:
                            if (employee.palm_1.Length > 0)
                            {
                                JObject vEnrollDataJson = new JObject();
                                vEnrollDataJson.Add("backup_number", nIndex);
                                vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                                vEnrollDataArrayJson.Add(vEnrollDataJson);
                            }
                            break;
                    }

                    //if (FKWebTools.mPalm[nIndex - FKWebTools.BACKUP_PALM_1].Length > 0)
                    //{
                    //    JObject vEnrollDataJson = new JObject();
                    //    vEnrollDataJson.Add("backup_number", nIndex);
                    //    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    //    vEnrollDataArrayJson.Add(vEnrollDataJson);
                    //}

                }
                if (sUserPwd.Length > 0)
                {
                    JObject vEnrollDataJson = new JObject();
                    vEnrollDataJson.Add("backup_number", FKWebTools.BACKUP_PSW);
                    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    vEnrollDataArrayJson.Add(vEnrollDataJson);
                }

                if (sUserCard.Length > 0)
                {
                    JObject vEnrollDataJson = new JObject();
                    vEnrollDataJson.Add("backup_number", FKWebTools.BACKUP_CARD);
                    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    vEnrollDataArrayJson.Add(vEnrollDataJson);
                }
                //if (FKWebTools.mFace.Length > 0)
                //{
                //    JObject vEnrollDataJson = new JObject();
                //    vEnrollDataJson.Add("backup_number", FKWebTools.BACKUP_FACE);
                //    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                //    vEnrollDataArrayJson.Add(vEnrollDataJson);
                //}
                if (employee.face_data.Length > 0)
                {
                    JObject vEnrollDataJson = new JObject();
                    vEnrollDataJson.Add("backup_number", FKWebTools.BACKUP_FACE);
                    vEnrollDataJson.Add("enroll_data", FKWebTools.GetBinIndexString(index++));
                    vEnrollDataArrayJson.Add(vEnrollDataJson);
                }

                vResultJson.Add("enroll_data_array", vEnrollDataArrayJson);
                string sFinal = vResultJson.ToString(Formatting.None);
                //DevID.Text = sFinal;
                byte[] binData = new byte[0];
                byte[] strParam = new byte[0];

                //if (FKWebTools.mPhoto.Length > 0)
                //{
                //    FKWebTools.AppendBinaryData(ref binData, FKWebTools.mPhoto);
                //}

                //for (int nIndex = 0; nIndex <= FKWebTools.BACKUP_FP_9; nIndex++)
                //{
                //    if (FKWebTools.mFinger[nIndex].Length > 0)
                //    {
                //        FKWebTools.AppendBinaryData(ref binData, FKWebTools.mFinger[nIndex]);
                //    }
                //}
                ////
                //for (int nIndex = FKWebTools.BACKUP_PALM_1; nIndex <= FKWebTools.BACKUP_PALM_2; nIndex++)
                //{

                //    if (FKWebTools.mPalm[nIndex - FKWebTools.BACKUP_PALM_1].Length > 0)
                //    {
                //        FKWebTools.AppendBinaryData(ref binData, FKWebTools.mPalm[nIndex - FKWebTools.BACKUP_PALM_1]);
                //    }

                //}
                //if (sUserPwd.Length > 0)
                //{
                //    byte[] mPwdBin = new byte[0];
                //    cmdTrans.CreateBSCommBufferFromString(sUserPwd, out mPwdBin);
                //    FKWebTools.ConcateByteArray(ref binData, mPwdBin);
                //}
                //if (sUserCard.Length > 0)
                //{
                //    byte[] mCardBin = new byte[0];
                //    cmdTrans.CreateBSCommBufferFromString(sUserCard, out mCardBin);
                //    FKWebTools.ConcateByteArray(ref binData, mCardBin);
                //}
                //if (FKWebTools.mFace.Length > 0)
                //{
                //    FKWebTools.AppendBinaryData(ref binData, FKWebTools.mFace);
                //}
                //cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);

                //FKWebTools.ConcateByteArray(ref strParam, binData);
                //encEmployee = strParam;


                if (employee.photo_data.Length > 0)
                {
                    FKWebTools.AppendBinaryData(ref binData, employee.photo_data);
                }

                for (int nIndex = 0; nIndex <= FKWebTools.BACKUP_FP_9; nIndex++)
                {
                    switch (nIndex)
                    {
                        case 0:
                            if (employee.finger_0.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_0);
                            }
                            break;
                        case 1:
                            if (employee.finger_1.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_1);
                            }
                            break;
                        case 2:
                            if (employee.finger_2.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_2);
                            }
                            break;
                        case 3:
                            if (employee.finger_3.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_3);
                            }
                            break;
                        case 4:
                            if (employee.finger_4.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_4);
                            }
                            break;
                        case 5:
                            if (employee.finger_5.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_5);
                            }
                            break;
                        case 6:
                            if (employee.finger_6.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_6);
                            }
                            break;
                        case 7:
                            if (employee.finger_7.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_7);
                            }
                            break;
                        case 8:
                            if (employee.finger_8.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_8);
                            }
                            break;
                        case 9:
                            if (employee.finger_9.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.finger_9);
                            }
                            break;
                    }

                }
                //
                for (int nIndex = FKWebTools.BACKUP_PALM_1; nIndex <= FKWebTools.BACKUP_PALM_2; nIndex++)
                {

                    switch (nIndex)
                    {
                        case 0:
                            if (employee.palm_0.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.palm_0);
                            }
                            break;
                        case 1:
                            if (employee.palm_1.Length > 0)
                            {
                                FKWebTools.AppendBinaryData(ref binData, employee.palm_1);
                            }
                            break;
                    }

                }
                if (sUserPwd.Length > 0)
                {
                    byte[] mPwdBin = new byte[0];
                    cmdTrans.CreateBSCommBufferFromString(sUserPwd, out mPwdBin);
                    FKWebTools.ConcateByteArray(ref binData, mPwdBin);
                }
                if (sUserCard.Length > 0)
                {
                    byte[] mCardBin = new byte[0];
                    cmdTrans.CreateBSCommBufferFromString(sUserCard, out mCardBin);
                    FKWebTools.ConcateByteArray(ref binData, mCardBin);
                }
                if (employee.face_data.Length > 0)
                {
                    FKWebTools.AppendBinaryData(ref binData, employee.face_data);
                }
                cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);

                FKWebTools.ConcateByteArray(ref strParam, binData);
                encEmployee = strParam;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return encEmployee;
        }

        public bool SendEmployeeToDevice(string ToDeviceID, Employee_P employee, string Username)
        {
            EagleEyeManagement hm = new EagleEyeManagement();

            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                string sDevId = ToDeviceID;
                byte[] strParam = new byte[0];

                strParam = employee.Cmd_Param;
                //    LogService.WriteLog("[SendEmployeeToDevice]:strParam " + strParam);
                mTransId = FKWebTools.MakeCmdForUser(msqlConn, "SET_USER_INFO", sDevId, strParam, employee.Employee_ID);

                string action = "Setting User Info by User ID: " + employee.Employee_ID;
                bool oflag = InsertOperationLog(sDevId, action, mTransId, Username);
                //LogService.WriteLog("[SendEmployeeToDevice]:mTransIdTxt " + mTransIdTxt);
                //flag = true;

                //if (flag)
                //{
                //    if (employee.Valid_DateEnd == "" && employee.Valid_DateStart == "")
                //    {
                //        employee.Valid_DateEnd = "";
                //        employee.Valid_DateStart = "";
                //        flag = hm.SetPassTime(ToDeviceID, employee, Username);

                //    }
                //    else
                //    {
                //employee.Valid_DateEnd = Formatter.SetValidValueToDateTime(employee.Valid_DateEnd).ToString("yyyyMMdd");
                //employee.Valid_DateStart = Formatter.SetValidValueToDateTime(employee.Valid_DateStart).ToString("yyyyMMdd");
                ////if (employee.Valid_DateEnd == "00010101" || employee.Valid_DateStart == "00010101")
                ////{
                ////    employee.Valid_DateStart = "20220101";
                ////    employee.Valid_DateEnd = "20420101";
                ////}
                //if (employee.Valid_DateEnd != "00010101" || employee.Valid_DateStart != "00010101")
                //    flag = hm.SetPassTime(ToDeviceID, employee, Username);



                // }
            }
            catch (Exception ex)
            {
                flag = false;
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            msqlConn = null;
            return flag;
        }

        public bool GetAllRecords(Device_P device, DateTime dt1, DateTime dt2, string Username)
        {
            bool flag = false;
            try
            {
                msqlConn = GetDBPool();
                string sDevId = device.Device_ID;
                JObject vResultJson = new JObject();
                string sBeginDate = "";
                string sEndDate = "";

                sBeginDate = FKWebTools.GetFKTimeString14(dt1);
                vResultJson.Add("begin_time", sBeginDate);
                sEndDate = FKWebTools.GetFKTimeString14(dt2);
                vResultJson.Add("end_time", sEndDate);

                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans();
                cmdTrans.CreateBSCommBufferFromString(sFinal, out strParam);

                if (msqlConn.State == ConnectionState.Closed)
                    msqlConn.Open();
                mTransId = FKWebTools.MakeCmd(msqlConn, "GET_LOG_DATA", sDevId, strParam);

                bool oflag = InsertOperationLog(sDevId, "Gettings Records from Device", mTransId, Username);
                flag = true;

            }
            catch (Exception ex)
            {
                flag = false;
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return flag;
        }

        public bool InsertOperationLog(string DeviceID, string Action, string Trans_ID, string UserName)
        {
            bool flag = false;
            OperationLog_P olog = new OperationLog_P();
            try
            {
                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string devName = objDev.GetDeviceName(DeviceID);
                olog.Device_ID = DeviceID;
                olog.Device_Name = devName;
                olog.Action = Action;
                olog.Status = "0";
                olog.Trans_ID = Trans_ID;
                olog.UpdateTime = Formatter.SetValidValueToDateTime(dt);
                olog.UserName = UserName;
                //olog.Device_Status = 0;
                bool oflag = objOLog.AddUpdateOperationLog(olog);
            }
            catch (Exception ex)
            {
                flag = false;
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

    }


}
