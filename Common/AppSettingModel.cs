using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class AppSettingModel
    {
        public string HiddenFilePath { get; set; }
        public string BackUpPath { get; set; }
        public bool IsDup { get; set; }
        public bool IsSync { get; set; }
        public bool AllowPlainList { get; set; }
        public bool HoldPolling { get; set; }
        public bool ByPassandProceed { get; set; }
        public bool Prompt { get; set; }
        public bool AttendenceImage { get; set; }
        public string PollingFileName { get; set; }
        public string PollingFolderName { get; set; }
        public bool IsPollEvery { get; set; }
        public bool EnableScheduling { get; set; }
        public string PollEverySpan { get; set; }
        public bool EnableAutoPolling { get; set; }
        public string Schedule1 { get; set; }
        public string Schedule2 { get; set; }
        public string Schedule3 { get; set; }
        public string Schedule4 { get; set; }
        public string Schedule5 { get; set; }
        public bool Schedule1Enabled { get; set; }
        public bool Schedule2Enabled { get; set; }
        public bool Schedule3Enabled { get; set; }
        public bool Schedule4Enabled { get; set; }
        public bool Schedule5Enabled { get; set; }
        public bool UseDateTimePollingFile { get; set; }
        public bool UseFixedPollingFile { get; set; }


        public bool LDAT { get; set; }
        public string SQLInQr { get; set; }
        public string ORCLInQr { get; set; }
        public bool OrclInt { get; set; }
        public bool TISInt { get; set; }
        public bool SQLInt { get; set; }
        public bool MySqlInt { get; set; }
       // public string MySqlInQr { get; set; }
        public bool IntAuPoAct { get; set; }

        /// <DBSettingsModel>

        public string TISSQLServerName { get; set; }
        public string TISSQLDBName { get; set; }
        public string TISSQLUserID { get; set; }
        public string TISSQLPassword { get; set; }
        public string TISSQLPort { get; set; }
        public bool EnableTISIntegration { get; set; }
        public bool UseSQLTcp { get; set; }

        public bool EnableOrclIntegration { get; set; }
        public string OrclServerName { get; set; }
        public string OrclServicName { get; set; }
        public string OrclUserId { get; set; }
        public string OrclPassword { get; set; }

        public bool EnableSQLIntegration { get; set; }
        public string SQLServerName { get; set; }
        public string SQLDBName { get; set; }
        public string SQLUserId { get; set; }
        public string SQLPassword { get; set; }

        // mysql DbSetting

        public bool EnableMySqlIntegration { get; set; }
        public string MySqlPort { get; set; }
        public string MySqlServerName { get; set; }
        public string MySqlDBName { get; set; }
        public string MySqlUserId { get; set; }
        public string MySqlPassword { get; set; }


        /// </DBSettingsModel>
        /// 
        ////< EmailSetting>///
        ///

        public bool SendingEmail { get; set; }
        public string SMTPServerIP { get; set; }
        public string UserName { get; set; }
        public string AuthenticationMode { get; set; }
        public string SupervisorEmail { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
        public string MailPort { get; set; }

        public string WebTimeout { get; set; }
        public string ProfilePath { get; set; }

        public string irregularPath { get; set; }

        public List<Att_Status_P> ListAttendenceStatus { get; set; }
        public List<Att_Status_P.Department_P> ListDepartments { get; set; }
        public List<Att_Status_P.Location_P> ListLocations { get; set; }
        public List<Att_Status_P.Designation_P> ListDesignations { get; set; }
        public List<Att_Status_P.EmployeeType_P> ListEmployeeTypes { get; set; }
        public List<Att_Status_P.WorkCode_P> ListWorkCodes { get; set; }
        public List<Att_Status_P.TimeZone_P> ListTimeZones { get; set; }
        //  public List<string> ListLocations { get; set; }

    }
}