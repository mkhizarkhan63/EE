using Common;
using EagleEye_Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALOperationLog : DataAccess
    {
        SqlConnection msqlConn;
        string conStr = "";
        string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";
        public List<OperationLog> GetAllOperationLog()
        {
            List<OperationLog> list = new List<OperationLog>();
            try
            {
                query = @"Select * from tbl_operationlog where Device_Status = '0'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OperationLog log = new OperationLog();
                        log.Trans_ID = dt.Rows[i]["Trans_ID"].ToString();
                        list.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return list;
        }
        
        public List<OperationLog> GetOperationLog()
        {
            List<OperationLog> list = new List<OperationLog>();
            try
            {
                query = @"Select * from tbl_operationlog where Status = '0'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OperationLog log = new OperationLog();
                        log.Trans_ID = dt.Rows[i]["Trans_ID"].ToString();
                        log.Device_Status = dt.Rows[i]["Device_Status"].ToString();
                        list.Add(log);
                    }
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return list;
        }
        public bool UpdateOperationLog(string trans_id, string status, string deviceStatus, string msg, out string response)
        {
            bool flag = false;
            response = "";
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                msqlConn.Open();
                if (status == "OK")
                {
                    response = "1";
                    query = @"UPDATE tbl_operationlog SET Status = '" + response + "', Message = '" + status + "', Device_Status = '" + deviceStatus + "' where Trans_ID = '" + trans_id + "'";
                }
                else if (status == "" || status == "NULL")
                {
                    response = "0";
                    query = @"UPDATE tbl_operationlog SET Status = '" + response + "', Message = '" + status + "', Device_Status = '" + deviceStatus + "' where Trans_ID = '" + trans_id + "'";
                }
                else if (msg == "CANCELLED")
                {
                    response = "2";
                    query = @"UPDATE tbl_operationlog SET Status = '" + response + "', Message = '" + status + "', Device_Status = '" + deviceStatus + "' where Trans_ID = '" + trans_id + "'";
                }
                else
                {
                    response = "2";
                    query = @"UPDATE tbl_operationlog SET Status = '" + response + "', Message = '" + status + "', Device_Status = '" + deviceStatus + "' where Trans_ID = '" + trans_id + "'";
                }
                int res = ExecuteNonQuery();
                if (res == 1)
                    flag = true;

                msqlConn.Close();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }
        public bool UpdateOperationDeviceStatus(string device_id, string status)
        {
            bool flag = false;
            try
            {
                query = @"UPDATE tbl_operationlog SET Device_Status = " + status + " where Device_ID = '" + device_id + "' AND Status ='0'";

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
