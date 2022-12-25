using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALAwaitingDevice : DataAccess
    {

        public DataTable GetAllFkWebServerDevices()
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_fkdevice_status where connected=1";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;
        }

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

        public DataTable GetAllAwaitingDevices()
        {
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_awaitingdevice";
                dt = ExecuteDataTable();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return dt;

        }


        public bool DetectDevice(string deviceID)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_fkdevice_status where device_id ='" + deviceID + "'";
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

        public bool DetectDeviceInAwaitingDevice(string deviceID)
        {
            bool flag = false;
            DataTable dt = new DataTable();
            try
            {
                query = @"Select * from tbl_awaitingdevice where device_id ='" + deviceID + "'";
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
        public bool AddConnectedDevice(DataTable dtConnected)
        {

            //    DataTable dtFkConnected = obj.GetAllFkWebServerDevices();
            //    if (dtFkConnected.Rows.Count > 0)
            //    {

            //        obj.AddConnectedDevice(dtConnected);
            //        //string Device_ID = dtConnected.Rows[i]["device_id"].ToString();
            //        //string Name = dtConnected.Rows[i]["device_name"].ToString();
            //        //string Connected = dtConnected.Rows[i]["connected"].ToString();
            //        // string CommKey = dtConnected.Rows[i]["Device_Communication"].ToString();
            //        //HFaceGo.Controller.HFaceGo objFaceGo = new HFaceGo.Controller.HFaceGo(Device_IP, 9922, CommKey);
            //        // string msg = "";

            //        if (Connected != "0")
            //        {
            //            bool flag = false;
            //            //if (objFaceGo != null)
            //            //{
            //            //  flag = objFaceGo.DetectDevice(out msg);

            //            //if (flag == false)
            //            //{
            //            //    string ip = HelpingMethod.RemoveZeros(Device_ID);
            //            //    flag = HelpingMethod.PingHost(ip);
            //            //    clsWriterLog.WriteDevLog("Device Ping", ip + " flag=" + flag);
            //            //}

            //            // }

            //            objDevice.UpdateConnectedDeviceStatus(Device_ID, flag);
            //            hubContext.Clients.All.refreshConnectedDevices(Device_ID, flag);
            //        }
            //    }

            //}

            //LoadSettings();
            bool flag = false;
            try
            {
                DALDevice objDevice = new DALDevice();
                for (int i = 0; i < dtConnected.Rows.Count; i++)
                {
                    string Device_ID = dtConnected.Rows[i]["device_id"].ToString();
                    string Device_Status = dtConnected.Rows[i]["connected"].ToString();
                    if (objDevice.CheckDeviceExistInDB(Device_ID))
                    {
                        query = @"Insert into tbl_device (Device_ID,Device_Status) values('" + Device_ID + "','" + Device_Status + "')";

                        int res = ExecuteNonQuery();
                        if (res == 1)
                            flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }
        public bool AddAwaitingDevice(string DeviceID, string DeviceName, string isConnected, string DeviceInfo, string Device_Status_Info)
        {
            bool flag = false;
            try
            {
                query = @"Insert into tbl_awaitingdevice (Device_Id,Device_Name,IsConnected,Device_Info,Device_Status_Info) values('" + DeviceID + "','" + DeviceName + "','" + isConnected + "','" + DeviceInfo + "','" + Device_Status_Info + "')";
                // clsWriterLog.WriteAppLog("AddAwaitingDevice | ", "Query | " + query);
                clsWriterLog.WriteAppLog("Add Awaiting Device", query);
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

        public bool DeleteAwaitingDevice(string DeviceID)
        {
            bool flag = false;
            try
            {
                query = @"delete from tbl_awaitingdevice where Device_ID='" + DeviceID + "'";
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




    }
}
