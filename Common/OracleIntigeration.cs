using Common;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OracleIntigeration
    {
        
        public bool InsertData(EventLogs log, string ReaderID,string MachineName, AppSettingModel appModel)
        {

            bool flag = false;
            try
            {
                DateTime rdtEventDateTime = Convert.ToDateTime(log.DateTime);
                string rstrEmployeeID = log.UserID;
                string rstrMachineID = log.DeviceID;
                string rstrStatus = log.Status;
                ReaderID = string.IsNullOrEmpty(ReaderID) ? "0" : ReaderID;

                OracleConnectionStringBuilder csb = new OracleConnectionStringBuilder
                {
                    DataSource = appModel.OrclServerName + "/" + appModel.OrclServicName,
                    UserID = appModel.OrclUserId,
                    Password = appModel.OrclPassword
                };

                OracleDbAccess ObjClsDatabaseOracle = new OracleDbAccess(csb.ConnectionString) ;

                //string RawData = rdtEventDateTime.ToString("31yyyyMMddHHmm") + "00" + rstrStatus.PadLeft(2, '0') + rstrEmployeeID.PadLeft(10, '0') + "00" + rstrMachineID.PadLeft(2, '0');
                string RawData = rdtEventDateTime.ToString("31yyyyMMddHHmm") + "00" + rstrStatus.PadLeft(2, '0') + rstrEmployeeID.PadLeft(10, '0') + "0" + rstrMachineID.PadLeft(3, '0');

                
                string rstrMonth = rdtEventDateTime.Month.ToString();
                string rstrDay = rdtEventDateTime.Day.ToString();
                string rstrYear = rdtEventDateTime.Year.ToString();
                string rstrHour = rdtEventDateTime.Hour.ToString();
                string rstrMinute = rdtEventDateTime.Minute.ToString();
                
                ObjClsDatabaseOracle.mstrOracle = string.Format(appModel.ORCLInQr,
                            RawData,                                            //0
                            rstrEmployeeID,                                     //1
                            rdtEventDateTime.ToString("dd-MMM-yyyy HH:mm:ss"),  //2
                            rdtEventDateTime.ToString("dd-MMM-yyyy"),           //3
                            rdtEventDateTime.ToString("HH:mm:ss"),              //4
                            rstrMachineID,                                      //5
                            rstrStatus,                                         //6 (1 or 2)
                            MachineName);                                       //7

                int res = ObjClsDatabaseOracle.ExecuteNonQuery();
                if (res > 0)
                    flag = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            return flag;
        }


    }
}
