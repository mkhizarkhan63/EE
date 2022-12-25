using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALAttendance : DataAccess
    {
        public bool InsertAttendance(EventLogs log, string Status, string WorkCode)
        {
            bool flag = false;
            try
            {
                query = @"INSERT INTO [dbo].[tbl_attendence]
           ([Attendance_DateTime]
           ,[Attendance_Photo]
           ,[Polling_DateTime]
           ,[Device_ID]
           ,[Employee_ID],[Employee_Name]
           ,[Status]
           ,[Status_Description]
           ,[WorkCode]
           ,[WorkCode_Description]
           ,[DoorStatus]
           ,[Verify_Mode]
           ,[Ext_Status])
            VALUES
           ('" + log.DateTime + "'" +
           ",'" + log.Photo + "'" +
           ",'" + log.Polling_DateTime + "'" +
           ",'" + log.DeviceID + "'" +
           ",'" + log.UserID + "'" +
           ",'" + log.UserName + "'" +
           ",'" + log.Status + "'" +
           ",'" + Status + "'" +
           ",'" + log.WorkCode + "'" +
           ",'" + WorkCode + "'" +
           ",'" + log.DoorStatus + "'" +
           ",'" + log.VerifyMode + "'" +
           ",'" + log.ExtraStatus + "')";



                int res = ExecuteNonQuery();
                if (res > 0)
                    flag = true;

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        public string GetStatusName(string code, string DeviceID)
        {
            string name = "";
            bool isSlave = false;
            try
            {
                query = @"Select Name from tbl_att_status where Code = '" + code + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    name = dt.Rows[0]["Name"].ToString();
                query = @"Select IsSlave from tbl_device where Device_ID = '" + DeviceID + "'";
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    if (!string.IsNullOrEmpty(dt.Rows[0]["IsSlave"].ToString()))
                    {
                        isSlave = Convert.ToBoolean(dt.Rows[0]["IsSlave"].ToString());
                        if (isSlave)
                        {

                            switch (code)
                            {
                                case "1":
                                case "3":
                                case "5":
                                case "7":
                                case "9":
                                    {
                                        name = "Entrance";
                                    }
                                    break;
                                case "2":
                                case "4":
                                case "6":
                                case "8":
                                case "10":
                                    {
                                        name = "Exit";
                                    }
                                    break;
                            }
                        }

                        //if (name == "OUT")
                        //    name = "Exit";
                        //else if (name == "IN")
                        //    name = "Entrance";
                    }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return name;
        }

        public string GetWorkCodeName(string code)
        {
            string name = "";
            try
            {
                query = @"Select Name from tbl_workcode where Code = " + code + "";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    name = dt.Rows[0]["Name"].ToString();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return name;
        }

        public string GetStatusCode(string name)
        {
            string code = "";
            try
            {
                query = @"Select Code from tbl_att_status where Name = '" + name + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                    code = dt.Rows[0]["Code"].ToString();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return code;
        }

        public bool InsertAttendanceWODuplicate(EventLogs log, string Status, string WorkCode)
        {
            bool flag = false;
            try
            {
                query = @"Select Code from tbl_attendence where Employee_ID='" + log.UserID + "' AND Attendance_DateTime='" + Convert.ToDateTime(log.DateTime).ToString("yyyy-MM-dd HH:mm:ss") /*+ "' AND Status='" + log.Status*/ + "'";

                DataTable res = ExecuteDataTable();
                if (res.Rows.Count == 0)
                {
                    flag = InsertAttendance(log, Status, WorkCode);
                }
                

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }

        //********************************************** New Feature
       

    }
}
