using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SQLIntigeration
    {
        string mstrConnectionString;
        string strConnectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
        SQLDbAccess ObjClsDatabaseSQL;

        private static string profilePath = null;
        private static string flag = "";
        private static object myLock = 0;

        static string logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        static string logFilePath = logDirectoryPath + "\\AppLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";
        static string DevFilePath = logDirectoryPath + "\\DevLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";
        static string ProcFilePath = logDirectoryPath + "\\ProcLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";



        static string errDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLogs";
        static string errFilePath = errDirectoryPath + "\\ErrorLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";
        public SQLIntigeration(AppSettingModel appModel)
        {

            mstrConnectionString = string.Format(strConnectionString,
               appModel.SQLServerName,
               appModel.SQLDBName,
              appModel.SQLUserId,
               appModel.SQLPassword);
            ObjClsDatabaseSQL = new SQLDbAccess(mstrConnectionString);

        }

        public bool InsertData(EventLogs log, string ReaderID, string MachineName, AppSettingModel appModel)
        {

            bool flag = false;
            try
            {
                DateTime rdtEventDateTime = Convert.ToDateTime(log.DateTime);
                string rstrEmployeeID = log.UserID;
                string rstrMachineID = log.DeviceID;
                string rstrStatus = log.Status;
                ReaderID = string.IsNullOrEmpty(ReaderID) ? "0" : ReaderID;


                //string RawData = rdtEventDateTime.ToString("31yyyyMMddHHmm") + "00" + rstrStatus.PadLeft(2, '0') + rstrEmployeeID.PadLeft(10, '0') + "00" + rstrMachineID.PadLeft(2, '0');
                string RawData = rdtEventDateTime.ToString("31yyyyMMddHHmm") + "00" + rstrStatus.PadLeft(2, '0') + rstrEmployeeID.PadLeft(10, '0') + "0" + rstrMachineID.PadLeft(3, '0');

                //string strInsert = "Insert Into HRTMachinedata (RawDat,Code,CDate,RDate,RTime,Shift,Status,EmployeeNo,ManaCode,TerminalNo) " +
                //                           " Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";

                string rstrMonth = rdtEventDateTime.Month.ToString();
                string rstrDay = rdtEventDateTime.Day.ToString();
                string rstrYear = rdtEventDateTime.Year.ToString();
                string rstrHour = rdtEventDateTime.Hour.ToString();
                string rstrMinute = rdtEventDateTime.Minute.ToString();
                string selectDataCheck = "SELECT EMPLOYEE_CODE FROM ParkingAttendance WHERE EMPLOYEE_CODE = '" + rstrEmployeeID + "' AND DATE_TIME = '" + rdtEventDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND DEVICE_ID ='" + rstrMachineID + "'";
                ObjClsDatabaseSQL.mstrSQL = selectDataCheck;
                var dt = ObjClsDatabaseSQL.ExecuteDataTable();
                if (dt.Rows.Count == 0)
                {

                    ObjClsDatabaseSQL.mstrSQL = string.Format(appModel.SQLInQr,
                                 RawData,                                           //0
                                rstrEmployeeID,                                     //1
                                rdtEventDateTime.ToString("dd-MMM-yyyy HH:mm:ss"),  //2
                                rdtEventDateTime.ToString("dd-MMM-yyyy"),           //3
                                rdtEventDateTime.ToString("HH:mm:ss"),              //4
                                rstrMachineID,                                      //5
                                rstrStatus,                                         //6 (1 or 2)
                                MachineName);                                       //7


                    //WriteProcessLog("Sql Integ", ObjClsDatabaseSQL.mstrSQL);
                    int res = ObjClsDatabaseSQL.ExecuteNonQuery();
                    if (res > 0)
                        flag = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public bool CheckConnection()
        {
            bool flag = false;
            try
            {
                flag = ObjClsDatabaseSQL.OpenDatabaseConnection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjClsDatabaseSQL.CloseDatabaseConnection();
            }

            return flag;
        }

        public static void WriteProcessLog(string EventName, string Message)
        {

            try
            {

                lock (myLock)
                {
                    if (!Directory.Exists(logDirectoryPath))
                    {
                        Directory.CreateDirectory(logDirectoryPath);
                    }

                    bool fileCreated = (!File.Exists(ProcFilePath));

                    using (StreamWriter sw = new StreamWriter(ProcFilePath, true))
                    {
                        string r = "";

                        if (fileCreated)
                        {
                            r = "Event Time,Event Name,Description";
                            sw.WriteLine(r);
                        }

                        r = "{0},{1},{2}";
                        r = string.Format(r, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), EventName, Message);
                        sw.WriteLine(r);
                        sw.Close();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
