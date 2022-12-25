
using Common;
using System;
using System.Data;

namespace Common
{
    public class TISIntigeration
    {

        string mstrConnectionString;
        string strConnectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
        string strTCPConnectionString = "Data Source={0},{4};Network Library=DBMSSOCN;Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
        SQLDbAccess ObjClsDatabaseSQL;

        public TISIntigeration(AppSettingModel appModel)
        {


            if (appModel.UseSQLTcp == false)
            {
                mstrConnectionString = string.Format(strConnectionString,
                   appModel.TISSQLServerName,
                   appModel.TISSQLDBName,
                  appModel.TISSQLUserID,
                   appModel.TISSQLPassword);
            }
            else
            {
                mstrConnectionString = string.Format(strTCPConnectionString,
                   appModel.TISSQLServerName,
                    appModel.TISSQLDBName,
                   appModel.TISSQLUserID,
                    appModel.TISSQLPassword,
                    appModel.TISSQLPort);
            }
            ObjClsDatabaseSQL = new SQLDbAccess(mstrConnectionString);

        }

        public bool InsertData(EventLogs log, string ReaderID)
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

                string strInsert = "Insert Into HRTMachinedata (RawDat,Code,CDate,RDate,RTime,Shift,Status,EmployeeNo,ManaCode,TerminalNo) " +
                                           " Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";

                string rstrMonth = rdtEventDateTime.Month.ToString();
                string rstrDay = rdtEventDateTime.Day.ToString();
                string rstrYear = rdtEventDateTime.Year.ToString();
                string rstrHour = rdtEventDateTime.Hour.ToString();
                string rstrMinute = rdtEventDateTime.Minute.ToString();

                ObjClsDatabaseSQL.mstrSQL = string.Format(strInsert,
                              RawData,//Encryption.EncryptedData(RawData)
                              "31",
                              rdtEventDateTime.ToString("dd-MMM-yyyy"),
                              rstrYear + rstrMonth.PadLeft(2, '0') + rstrDay.PadLeft(2, '0'),
                              rstrHour.PadLeft(2, '0') + ":" + rstrMinute.PadLeft(2, '0'),
                              "00",
                              "01",//rstrStatus.PadLeft(2, '0'),
                             rstrEmployeeID.PadLeft(10, '0'),// Encryption.EncryptedData(rstrEmployeeID.PadLeft(10, '0'))
                              "00",
                             ReaderID.PadLeft(2, '0'));
                int res = ObjClsDatabaseSQL.ExecuteNonQuery();
                if (res > 0)
                    flag = true;

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
                flag= ObjClsDatabaseSQL.OpenDatabaseConnection();
             
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
