using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALLog : DataAccess
    {
        public List<EventLogs> GetLog(string Type)
        {
            List<EventLogs> ListLog = new List<EventLogs>();
            try
            {
                string Col = "";

                switch (Type)
                {
                    case "TIS":
                        Col = "Status_TIS";
                        break;
                    case "SQL":
                        Col = "Status_SQL";
                        break;
                    case "Oracle":
                        Col = "Status_Oracle";
                        break;
                    case "MySQL":
                        Col = "Status_MySQL";
                        break;
                }

                query = @"Select * from tbl_attendence where " + Col + " is null or " + Col + "=0";
                DataTable dt = ExecuteDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EventLogs log = new EventLogs
                    {
                        Code = Convert.ToInt32(dt.Rows[i]["Code"]),
                        UserID = dt.Rows[i]["Employee_ID"].ToString(),
                        DateTime = dt.Rows[i]["Attendance_DateTime"].ToString(),
                        Status = dt.Rows[i]["Status"].ToString(),
                        DeviceID = dt.Rows[i]["Device_ID"].ToString(),
                    };
                    ListLog.Add(log);
                }


            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return ListLog;
        }

        public void UpdateLogStatus(string Type, int Code)
        {

            try
            {
                string Col = "";

                switch (Type)
                {
                    case "TIS":
                        Col = "Status_TIS";
                        break;
                    case "SQL":
                        Col = "Status_SQL";
                        break;
                    case "Oracle":
                        Col = "Status_Oracle";
                        break;
                    case "MySQL":
                        Col = "Status_MySQL";
                        break;

                }

                query = @"Update tbl_attendence set " + Col + "=1 where Code=" + Code;
                ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }
    }
}
