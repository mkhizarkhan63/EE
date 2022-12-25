using System;
using System.IO;
using System.Reflection;


namespace Common
{

    public class MySQLIntegration
    {
        private static string profilePath = null;
        private static string flag = "";
        private static object myLock = 0;

        static string logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        static string logFilePath = logDirectoryPath + "\\AppLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";
        static string DevFilePath = logDirectoryPath + "\\DevLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";
        static string ProcFilePath = logDirectoryPath + "\\ProcLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";



        static string errDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLogs";
        static string errFilePath = errDirectoryPath + "\\ErrorLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";

        string mstrConnectionString;
        string strConnectionString = "Data Source={0};Port={1};Initial Catalog={2};User ID={3};Password={4};";
        MySQLDbAccess ObjClsDatabaseSQL;


        public MySQLIntegration(AppSettingModel appModel)
        {

            mstrConnectionString = string.Format(strConnectionString,
               appModel.MySqlServerName,
               appModel.MySqlPort,
               appModel.MySqlDBName,
              appModel.MySqlUserId,
               appModel.MySqlPassword);
            ObjClsDatabaseSQL = new MySQLDbAccess(mstrConnectionString);

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

                ObjClsDatabaseSQL.mstrSQL = string.Format(appModel.SQLInQr,
                             RawData,                                           //0
                            rstrEmployeeID,                                     //1
                            rdtEventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),  //2
                            rdtEventDateTime.ToString("yyyy-MM-dd"),           //3
                            rdtEventDateTime.ToString("HH:mm:ss"),              //4
                            rstrMachineID,                                      //5
                            rstrStatus,                                         //6 (1 or 2)
                            MachineName,//7
                            ReaderID);     //8     

                WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ObjClsDatabaseSQL.mstrSQL);
                int res = ObjClsDatabaseSQL.ExecuteNonQuery();
                if (res > 0)
                    flag = true;
                //ObjClsDatabaseSQL.mstrSQL = @"Update tbl_attendence set Status_MySQL =1 where Code=" + rstrEmployeeID;
                //int updated = ObjClsDatabaseSQL.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return flag;
        }
        public static void WriteError(string Namespace, string ClassName, string Method, string Error)
        {
            try
            {

                lock (myLock)
                {
                    if (!Directory.Exists(errDirectoryPath))
                    {
                        Directory.CreateDirectory(errDirectoryPath);
                    }

                    bool fileCreated = (!File.Exists(errFilePath));
                    using (StreamWriter sw = new StreamWriter(errFilePath, true))
                    {
                        string r = "";
                        if (fileCreated)
                        {
                            r = "Error Time,Project,ClassName,Method Name,Line#,Error";
                            sw.WriteLine(r);
                        }

                        int lineNo = 0;

                        r = "{0},{1},{2},{3},{4},{5}";
                        r = string.Format(r, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"),
                            Namespace, ClassName, Method, lineNo, Error);
                        sw.WriteLine(r);
                        sw.Close();
                    }
                }

            }
            catch (Exception)
            { }
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
    }
}
