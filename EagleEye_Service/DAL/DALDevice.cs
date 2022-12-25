using Common;
using FKWeb;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALDevice : DataAccess
    {
        SqlConnection msqlConn;
        string conStr = "";
        string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";

        public DataTable GetFkWebServerDevice(string deviceID)
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_fkdevice_status where device_id ='" + deviceID + "'";
                dt = ExecuteDataTable();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;
        }
        public bool GetFKStatusInfo(string deviceID)
        {
            bool flag = false;
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                string sSql;
                sSql = "UPDATE tbl_fkdevice_status SET dev_status_info=' ' where device_id='" + deviceID + "'";
                FKWebTools.ExecuteSimpleCmd(msqlConn, sSql);
                FKWebTools.MakeCmd(msqlConn, "GET_DEVICE_STATUS", deviceID, null);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }
        public DataTable GetAllDevices()
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_device where Active=1";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;

        }

        public DataTable GetAllConnectedDevices()
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_device where Active=1 And Device_Status=1";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;

        }

        public bool IsSlaveConnected(string DeviceID)
        {
            bool flag = false;
            try
            {
                query = @"Select IsSlave from tbl_device where Device_ID='" + DeviceID + "'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    string IsSlave = dt.Rows[0]["IsSlave"].ToString();
                    if (IsSlave == "True")
                        flag = true;

                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public bool UpdateConnectedDeviceStatus(string Code, DataTable dtFK, out bool IsConnected)
        {
            IsConnected = false;
            double nTimeDiff = 0;

            // int status = 0;
            try
            {
                DateTime dtDev = Convert.ToDateTime(dtFK.Rows[0]["last_update_time"].ToString());
                DateTime dtNow = DateTime.Now;
                int conn_status = GetFKDeviceConnStatus(dtFK.Rows[0]["device_id"].ToString());
                nTimeDiff = (dtNow - dtDev).TotalSeconds;
                if (nTimeDiff > 60)
                {
                    query = @"Update tbl_device set Device_Status=0 where Code='" + Code + "'";
                    int res = ExecuteNonQuery();
                    //if (res == 1)
                    IsConnected = false;

                    query = @"Update tbl_fkdevice_status set connected=0 where device_id='" + dtFK.Rows[0]["device_id"].ToString() + "'";
                    res = ExecuteNonQuery();


                    if (conn_status != 0)
                    {
                        string TransID = GetNewOnlineTransId();
                        string devid = dtFK.Rows[0]["device_id"].ToString();
                        string devName = GetDeviceName(dtFK.Rows[0]["device_id"].ToString());
                        string action = "Device Connectivity Status";
                        string status = "1";
                        string msg = "Device Offline at " + dtDev.ToString("dd-MM-yyyy HH:mm:ss");
                        string username = " - ";
                        int devStatus = 0; string updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        query = @"INSERT INTO [dbo].[tbl_operationlog]
                    ([Trans_ID]
                    ,[Device_ID]
                    ,[Device_Name]
                    ,[Action]
                    ,[Status]
                    ,[Message]
                    ,[UpdateTime]
                    ,[UserName]
                    ,[Device_Status])
                    VALUES
                    ('" + TransID + "'" +
                      ",'" + devid + "'" +
                      ",'" + devName + "'" +
                      ",'" + action + "'" +
                      ",'" + status + "'" +
                      ",'" + msg + "'" +
                      ",'" + updatetime + "'" +
                      ",'" + username + "'" +
                      ",'" + devStatus + "')";


                        SqlConnection con = new SqlConnection(conStr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        query = @"Update tbl_fkdevice_status set con_status=0 where device_id='" + dtFK.Rows[0]["device_id"].ToString() + "'";
                        res = ExecuteNonQuery();

                    }
                    //query = @"Update tbl_fkdevice_status set connected=0 where device_id='" + dtFK.Rows[0]["device_id"].ToString() + "'";
                    //res = ExecuteNonQuery();
                    //  LogService.WriteServiceLog("Method: [UpdateConnectedDeviceStatus] - Update Connect/Disconnect Device..");
                }
                else
                {
                    query = @"Update tbl_device set Device_Status=1 where Code='" + Code + "'";
                    int res = ExecuteNonQuery();
                    //if (res == 1)
                    IsConnected = true;

                    if (conn_status != 1)
                    {
                        string TransID = GetNewOnlineTransId();
                        string devid = dtFK.Rows[0]["device_id"].ToString();
                        string devName = GetDeviceName(dtFK.Rows[0]["device_id"].ToString());
                        string action = "Device Connectivity Status";
                        string status = "1";
                        string msg = "Device Online at " + dtDev.ToString("dd-MM-yyyy HH:mm:ss");
                        string username = " - ";
                        int devStatus = 1;
                        string updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        query = @"INSERT INTO [dbo].[tbl_operationlog]
                    ([Trans_ID]
                    ,[Device_ID]
                    ,[Device_Name]
                    ,[Action]
                    ,[Status]
                    ,[Message]
                    ,[UpdateTime]
                    ,[UserName]
                    ,[Device_Status])
                    VALUES
                    ('" + TransID + "'" +
                      ",'" + devid + "'" +
                      ",'" + devName + "'" +
                      ",'" + action + "'" +
                      ",'" + status + "'" +
                      ",'" + msg + "'" +
                      ",'" + updatetime + "'" +
                      ",'" + username + "'" +
                      ",'" + devStatus + "')";


                        SqlConnection con = new SqlConnection(conStr);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                        query = @"Update tbl_fkdevice_status set con_status=1 where device_id='" + dtFK.Rows[0]["device_id"].ToString() + "'";
                        res = ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return IsConnected;
        }

        public int GetFKDeviceConnStatus(string DeviceID)
        {
            int conn_status = 0;
            try
            {
                query = "Select * from tbl_fkdevice_status where device_id='" + DeviceID + "'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    conn_status = Formatter.SetValidValueToInt(dt.Rows[0]["con_status"]);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return conn_status;
        }
        public bool UpdateFKDeviceStatus(string Code, DataTable dtFK)
        {
            //LogService.WriteServiceLog("Method: [UpdateFKDeviceStatus] : Status Updation Statrted");
            bool flag = false;
            List<string> list = new List<string>();
            try
            {
                string device_Status = dtFK.Rows[0][7].ToString();
                //LogService.WriteServiceLog("Device Status: " + device_Status);
                if (device_Status != "" && device_Status != null && device_Status != " ")
                {
                    string rem = device_Status.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\n", "");
                    string[] dev_Status = rem.Split(',');
                    for (int i = 0; i < dev_Status.Length; i++)
                    {
                        string[] ds = dev_Status[i].Split(':');
                        for (int j = 0; j < ds.Length; j++)
                        {
                            list.Add(ds[j]);
                        }
                    }
                    query = @"Update tbl_device set Device_Status_Info='" + device_Status + "', " +
                        "Max_Record='" + list[1].ToString() + "'," +
                        "Real_FaceReg='" + list[3].ToString() + "'," +
                        "Max_FaceReg='" + list[5].ToString() + "'," +
                        "Real_FPReg='" + list[7].ToString() + "'," +
                        "Max_FPReg='" + list[9].ToString() + "'," +
                        "Real_IDCardReg='" + list[11].ToString() + "'," +
                        "Max_IDCardReg='" + list[13].ToString() + "'," +
                        "Real_Manager='" + list[15].ToString() + "'," +
                        "Max_Manager='" + list[17].ToString() + "'," +
                        "Real_PasswordReg= '" + list[19].ToString() + "'," +
                        "Max_PasswordReg='" + list[21].ToString() + "'," +
                        "Real_PvReg='" + list[23].ToString() + "'," +
                        "Max_PvReg='" + list[25].ToString() + "'," +
                        "Total_log_Count='" + list[27].ToString() + "'," +
                        "Total_log_Max='" + list[29].ToString() + "'," +
                        "Real_Employee='" + list[31].ToString() + "'," +
                        "Max_Employee='" + list[33].ToString() + "' " +
                        "where Code='" + Code + "'";
                    // LogService.WriteServiceLog("Method: [UpdateFKDeviceStatus] - Device Status Query: " + query);
                    int res = ExecuteNonQuery();
                    if (res == 1)
                        flag = false;
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }
        public bool UpdateLastPolledRecordTime(string DeviceID, string time)
        {
            bool flag = false;
            try
            {
                query = @"Update tbl_device set Last_Polled_Record='" + time + "' where Device_ID='" + DeviceID + "'";
                int res = ExecuteNonQuery();
                if (res == 1)
                    flag = true;
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public bool CheckDeviceExistInDB(string DeviceID)
        {
            bool flag = true;
            try
            {
                query = @"Select * from tbl_device where Device_ID='" + DeviceID + "' AND Active=1";
                DataTable dt1 = new DataTable();
                dt1 = ExecuteDataTable();
                if (dt1.Rows.Count == 0)
                {
                    query = @"Select * from tbl_awaitingdevice where Device_ID='" + DeviceID + "'";
                    DataTable dt2 = new DataTable();
                    dt2 = ExecuteDataTable();
                    if (dt2.Rows.Count == 0)
                    {
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public bool IsPTSModeON(string DeviceID)
        {
            bool flag = false;
            try
            {
                query = @"Select * from tbl_device where Device_ID='" + DeviceID + "' AND Enable_Server=1 AND Active=1";

                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public string GetDeviceName(string DeviceID)
        {
            string name = "";
            try
            {
                query = @"Select Device_Name from tbl_device where Device_ID='" + DeviceID + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    name = dt.Rows[0]["Device_Name"].ToString();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return name;
        }

        public DataTable GetDeviceByCode(int code)
        {

            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_device where Code=" + code + "";

                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return dt;
        }

        public string GetReaderID(string DeviceID)
        {
            string readerID = "0";
            try
            {
                query = "Select Reader_ID from tbl_device where Device_ID='" + DeviceID + "'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    readerID = dt.Rows[0]["Reader_ID"].ToString();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return readerID;
        }

        public int GetDeviceCode(string DeviceID)
        {
            int Code = 0;
            try
            {
                query = "Select Code from tbl_device where Device_ID='" + DeviceID + "'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    Code = Formatter.SetValidValueToInt(dt.Rows[0]["Code"]);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return Code;
        }

        public DataTable GetDisconnectedDevices()
        {
            DataTable dt = new DataTable();
            try
            {
                string d1 = DateTime.Now.ToString("dd-MMM-yyy") + " 00:00:00";
                string d2 = DateTime.Now.ToString("dd-MMM-yyy") + " 23:59:59";
                query = "Select * from tbl_device d where d.Device_ID not in (select Device_ID from attendance where Attendance_DateTime between '" + d1 + "' and '" + d2 + "')";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return dt;
        }

        public bool IsWANWaitingDevice(string DeviceID)
        {

            bool flag = false;
            try
            {
                query = "Select * from awaitingdevice where Device_ID='" + DeviceID + "' AND connectedby='WAN'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return flag;
        }

        public DataTable GetCommand(string device_id, string cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_temptable where Device_ID='" + device_id + "' AND Cmd='" + cmd + "'";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;

        }

        public DataTable GetFkCommands(string device_id)
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_fkcmd_trans_offline where device_id='" + device_id + "'";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;
        }

        public bool InsertFkCommands(string deviceID, string transID, string timezone_no)
        {
            bool flag = false;
            byte[] strParam = new byte[0];
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                msqlConn.Open();
                query = @"Select * from tbl_fkcmd_trans_cmd_param_offline where device_id='" + deviceID + "' AND trans_id ='" + transID + "'";
                DataTable dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    strParam = (byte[])dt.Rows[0]["cmd_param"];

                string mTransIdTxt = FKWebTools.MakeCmdForTZ(msqlConn, "SET_TIMEZONE", deviceID, strParam, timezone_no);
                flag = true;
                msqlConn.Close();
                //FKWebTools.MakeCmd(msqlConn, "GET_DEVICE_STATUS", deviceID, null);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public bool DeleteFkCommand(string deviceID, string transID)
        {
            bool flag = false;
            byte[] strParam = new byte[0];
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                msqlConn.Open();
                query = @"Delete from tbl_fkcmd_trans_offline where device_id='" + deviceID + "' AND trans_id ='" + transID + "'";
                int i = ExecuteNonQuery();
                if (i == 1)
                {
                    query = @"Delete from tbl_fkcmd_trans_cmd_param_offline where device_id='" + deviceID + "' AND trans_id ='" + transID + "'";
                    int j = ExecuteNonQuery();
                    flag = true;
                }
                flag = true;
                msqlConn.Close();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public bool DeleteUserfromMachine(string sUserId, string mDevId, AppSettingModel app)
        {
            bool flag = false;
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                msqlConn.Open();
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans(app);
                JObject vResultJson = new JObject();
                vResultJson.Add("user_id", sUserId);
                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                cmdTrans.CreateBSCommBufferFromStrings(sFinal, out strParam);
                string mTransIdTxt = FKWebTools.MakeCmd(msqlConn, "DELETE_USER", mDevId, strParam);
                flag = true;
                msqlConn.Close();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            msqlConn = null;
            return flag;
        }

        public bool DeleteTempEmployee(string DeviceID, string Emp_ID)
        {
            bool flag = false;
            try
            {
                query = @"Delete from tbl_temptable where Device_ID='" + DeviceID + "' AND Employee_ID='" + Emp_ID + "'";

                DataTable dt = new DataTable();
                ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public DataTable GetFkCmdTrans(string trans_id)
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_fkcmd_trans where Trans_ID='" + trans_id + "'";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;

        }

        public string GetNewOnlineTransId()
        {
            int nTransId;
            string sTransId = "";
            string sSql;
            conStr = File.ReadAllText(s);
            msqlConn = new SqlConnection(conStr);
            msqlConn.Open();
            sSql = "SELECT MAX([Trans_ID]) from [tbl_operationlog] where Trans_ID LIKE 'A%'";

            SqlCommand sqlCmd = new SqlCommand(sSql, msqlConn);
            SqlDataReader sqlDr = sqlCmd.ExecuteReader();

            if (sqlDr.HasRows)
            {
                if (sqlDr.Read())
                    sTransId = sqlDr[0].ToString();
            }
            sqlDr.Close();
            sqlCmd.Dispose();

            if (sTransId == "")
                sTransId = "C1000";

            string[] sTransIds = sTransId.Split('C');
            nTransId = Formatter.SetValidValueToInt(sTransIds[1].ToString());
            msqlConn.Close();
            sTransId = ("C" + (nTransId + 1)).ToString();
            return sTransId;
        }


    }
}
