
using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web.Mvc;
using EagleEye.Common;
using System.Data.SqlClient;
using Common;
using Oracle.DataAccess.Client;
using System.Threading.Tasks;
using EagleEye.DAL.Partial;
using EagleEye.BLL;
using EagleEye.Hubs;
using Microsoft.AspNet.SignalR;
using EagleEye_Service;
using System.Reflection;

namespace EagleEye.Controllers
{
    public class SettingsController : masterController
    {
        AppSettingModel app = new AppSettingModel();
        BLLAtt_Status objBLL = new BLLAtt_Status();
        BLLDepartment objDepBLL = new BLLDepartment();
        BLLDesignation objDesBLL = new BLLDesignation();
        BLLLocation objLocBLL = new BLLLocation();
        BLLWorkCode objWcBLL = new BLLWorkCode();
        BLLTimeZone objTZBLL = new BLLTimeZone();
        BLLEmployeeType objETBLL = new BLLEmployeeType();
        AMS.Profile.Xml profile = null;
        // GET: Settings

        [CheckAuthorization]
        public ActionResult Index()
        {

            if (!CheckRights("Settings"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(LoadDataFromRegistry());
        }

        [HttpPost]
        public JsonResult UpdateFile(AppSettingModel appModel)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);

                profile.SetValue("Machine Setting", "WebTimeout", appModel.WebTimeout);
                profile.SetValue("DB Setting", "DBBackupPath", appModel.BackUpPath);
                profile.SetValue("TIS DB Setting", "UseTCPOnlyForSQL", appModel.UseSQLTcp);
                profile.SetValue("TIS DB Setting", "EnableTISIntegration", appModel.EnableTISIntegration);
                profile.SetValue("Oracle DB Setting", "EnableOracleIntegration", appModel.EnableOrclIntegration);
                profile.SetValue("SQL DB Setting", "EnableSqlIntegration", appModel.EnableSQLIntegration);
                profile.SetValue("MySql DB Setting", "EnableMySqlIntegration", appModel.EnableMySqlIntegration);

                if (appModel.EnableTISIntegration == true)
                {
                   
                    profile.SetValue("TIS DB Setting", "ServerForSQL", appModel.TISSQLServerName);
                    profile.SetValue("TIS DB Setting", "DataBaseForSQL", appModel.TISSQLDBName);
                    profile.SetValue("TIS DB Setting", "UserNameForSQL", appModel.TISSQLUserID);
                    profile.SetValue("TIS DB Setting", "PasswordForSQL", appModel.TISSQLPassword);
                    profile.SetValue("TIS DB Setting", "PortForSQL", appModel.TISSQLPort);
                }
                if (appModel.EnableSQLIntegration == true)
                {
                    profile.SetValue("SQL DB Setting", "SqlServerName", appModel.SQLInt);
                    profile.SetValue("SQL DB Setting", "SqlServerName", appModel.SQLServerName);
                    profile.SetValue("SQL DB Setting", "SqlDatabaseName", appModel.SQLDBName);
                    profile.SetValue("SQL DB Setting", "SqlUserId", appModel.SQLUserId);
                    profile.SetValue("SQL DB Setting", "SqlPassword", appModel.SQLPassword);
                }
                if (appModel.EnableOrclIntegration == true)
                {
                    profile.SetValue("Oracle DB Setting", "OracleServiceName", appModel.OrclServicName);
                    profile.SetValue("Oracle DB Setting", "OracleUserId", appModel.OrclUserId);
                    profile.SetValue("Oracle DB Setting", "OraclePassword", appModel.OrclPassword);
                    profile.SetValue("Oracle DB Setting", "OracleServerName", appModel.OrclServerName);
                }
                if (appModel.EnableMySqlIntegration == true)
                {
                    profile.SetValue("MySql DB Setting", "EnableMySqlIntegration", appModel.EnableMySqlIntegration);
                    profile.SetValue("MySql DB Setting", "mySqlServerName", appModel.MySqlServerName);
                    profile.SetValue("MySql DB Setting", "mySqlDatabaseName", appModel.MySqlDBName);
                    profile.SetValue("MySql DB Setting", "mySqlUserId", appModel.MySqlUserId);
                    profile.SetValue("MySql DB Setting", "mySqlPassword", appModel.MySqlPassword);
                    profile.SetValue("MySql DB Setting", "mySqlPort", appModel.MySqlPort);

                }

                profile.SetValue("Email Configuration", "SendingEmail", appModel.SendingEmail);
                profile.SetValue("Email Configuration", "UserName", appModel.UserName);
                profile.SetValue("Email Configuration", "Password", appModel.Password);
                profile.SetValue("Email Configuration", "SMTPServerIP", appModel.SMTPServerIP);
                profile.SetValue("Email Configuration", "SupervisorEmail", appModel.SupervisorEmail);
                profile.SetValue("Email Configuration", "MailPort", appModel.MailPort);
                profile.SetValue("Email Configuration", "SenderEmail", appModel.SenderEmail);
                profile.SetValue("Email Configuration", "AuthenticationMode", appModel.AuthenticationMode);

                profile.SetValue("Polling Setting", "EnableAutoPolling", appModel.EnableAutoPolling);
                profile.SetValue("Polling Setting", "PollEverySpan", appModel.PollEverySpan);

                profile.SetValue("File Setting", "DuplicateEntriesAllowed", appModel.IsDup);
                profile.SetValue("File Setting", "PollingFolderName", appModel.PollingFolderName);
                profile.SetValue("File Setting", "PollingFileName", appModel.PollingFileName);
                profile.SetValue("File Setting", "HiddenPath", appModel.HiddenFilePath);
                profile.SetValue("File Setting", "UseFixedPollingFile", appModel.UseFixedPollingFile);
                profile.SetValue("File Setting", "UseDateTimePollingFile", appModel.UseDateTimePollingFile);
                profile.SetValue("File Setting", "AllowPlainListFile", appModel.AllowPlainList);

                profile.SetValue("Machine Setting", "SyncDateTime", appModel.IsSync);
                flag = true;
                if (flag == true)
                {
                    profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                    System.Web.HttpContext.Current.Session["Setting"] = this.LoadDataFromRegistry();
                    var task = Task.Run(async () => await RestartService());
                    task.Wait();
                }
            }

            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task RestartService()
        {

            try
            {
                WinServiceController service = new WinServiceController();
                service.Stop();
                await Task.Delay(TimeSpan.FromSeconds(10));
                service.Start();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

        }

        [HttpPost]
        public JsonResult OrclTestConn(AppSettingModel appModel)
        {
            bool flag = false;
            //string msg = "";
            try
            {
                OracleConnectionStringBuilder csb = new OracleConnectionStringBuilder
                {
                    DataSource = appModel.OrclServerName + "/" + appModel.OrclServicName,
                    UserID = appModel.OrclUserId,
                    Password = appModel.OrclPassword
                };
                string conString = csb.ConnectionString;

                OracleDbAccess OracleDB = new OracleDbAccess(profile);
                if (OracleDB.TestDBConnectivity(conString))
                {
                    // string msg = "Connection Successfull!!";
                    flag = true;
                }
                //else
                //{
                //   // string msg = "Connection Failed!!";
                //}
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SQLTestConn(AppSettingModel appModel)
        {
            bool flag = false;
            try
            {

                string strConnectionString =
                "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
                string mstrConnectionString = string.Format(
                    strConnectionString,
                         appModel.SQLServerName, appModel.SQLDBName, appModel.SQLUserId, appModel.SQLPassword);

                SQLDbAccess ObjClsDatabaseSQL = new SQLDbAccess(mstrConnectionString);
                flag = ObjClsDatabaseSQL.TestDBConnectivity(
                    mstrConnectionString);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult MySqlTestConn(AppSettingModel appModel)
        {
            bool flag = false;
            try
            {

                string strConnectionString =
                "Data Source={0};Port={1}; Initial Catalog={2};User ID={3};Password={4};";
                string mstrConnectionString = string.Format(
                    strConnectionString,
                         appModel.MySqlServerName, appModel.MySqlPort, appModel.MySqlDBName, appModel.MySqlUserId, appModel.MySqlPassword);

                MySQLDbAccess ObjClsDatabaseSQL = new MySQLDbAccess(mstrConnectionString);
                flag = ObjClsDatabaseSQL.TestDBConnectivity(
                    mstrConnectionString);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult TISTestConn(AppSettingModel appModel)
        {
            bool flag = false;
            try
            {

                string mstrConnectionString;
                string strConnectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";
                string strTCPConnectionString = "Data Source={0},{4};Network Library=DBMSSOCN;Initial Catalog={1};User ID={2};Password={3};Packet Size=4096;";

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
                SQLDbAccess ObjClsDatabaseSQL = new SQLDbAccess(mstrConnectionString);
                flag = ObjClsDatabaseSQL.TestDBConnectivity(
                    mstrConnectionString);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }

        public AppSettingModel LoadDataFromRegistry()
        {
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);

                app.WebTimeout = profile.GetValue("Machine Setting", "WebTimeout", "");
                app.HiddenFilePath = profile.GetValue("File Setting", "HiddenPath", "");
                app.BackUpPath = profile.GetValue("DB Setting", "DBBackupPath", "");
                bool IsDup = bool.Parse(profile.GetValue("File Setting", "DuplicateEntriesAllowed", "").ToString());
                bool IsSync = bool.Parse(profile.GetValue("Machine Setting", "SyncDateTime", "").ToString());
                app.IsDup = IsDup;
                app.IsSync = IsSync;
                app.AllowPlainList = profile.GetValue("File Setting", "AllowPlainListFile", "").ToLower() == "true" ? true : false;

                //file Setting
                app.PollingFolderName = profile.GetValue("File Setting", "PollingFolderName").ToString();
                app.UseFixedPollingFile = profile.GetValue("File Setting", "UseFixedPollingFile").ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
                app.UseDateTimePollingFile = profile.GetValue("File Setting", "UseDateTimePollingFile").ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
                app.PollingFileName = profile.GetValue("File Setting", "PollingFileName").ToString();

                //Polling Setting
                // app.IsPollEvery = (profile.GetValue("Polling Setting", "PollEveryEnable").ToString() == "True" ? true : false);
                app.EnableAutoPolling = (profile.GetValue("Polling Setting", "EnableAutoPolling").ToString() == "True" ? true : false);

                //switch (app.IsPollEvery)
                //{
                //    case true:
                //        app.IsPollEvery = true;
                //        app.EnableScheduling = false;
                //        break;
                //    case false:
                //        app.IsPollEvery = false;
                //        app.EnableScheduling = true;
                //        break;
                //}

                app.PollEverySpan = profile.GetValue("Polling Setting", "PollEverySpan").ToString();

                // License Lic = new License();
                // app.LDAT = Lic.LDAT;
                // app.IntAuPoAct = Lic.AutoPolling;
                // app.TISInt = Lic.IntTis;
                //// app.OrclInt = true;
                // if (Lic.IntDatabType == "Oracle Integration")
                //     app.OrclInt = true;
                // else if (Lic.IntDatabType == "SQL Integration")
                //     app.SQLInt = true;


                License Lic = new License();
                app.LDAT = Lic.LDAT;
                app.IntAuPoAct = Lic.AutoPolling;
                app.TISInt = Lic.IntTis;
                if (Lic.IntOracle)
                    app.OrclInt = true;
                if (Lic.IntSql)
                    app.SQLInt = true;

                app.MySqlInt = true;

                app.SQLInQr = profile.GetValue("Setting", "SQLInQr").ToString();
                app.ORCLInQr = profile.GetValue("Setting", "ORCLInQr").ToString();

                //app.SQLInQr = profile.GetValue("Setting", "SQLInQr").ToString();
                //app.ORCLInQr = profile.GetValue("Setting", "ORCLInQr").ToString();

                //app.LDAT = (profile.GetValue("Setting", "LDAT").ToString() == "True" ? true : false);
                //app.SQLInt = (profile.GetValue("Setting", "SQLInt").ToString() == "True" ? true : false);
                //app.OrclInt = (profile.GetValue("Setting", "OrclInt").ToString() == "True" ? true : false);
                //app.TISInt = (profile.GetValue("Setting", "TISInt").ToString() == "True" ? true : false);
                //app.InQr = profile.GetValue("Setting", "InQr").ToString();

                //app.LDAT = true;
                //app.SQLInt = false;
                //app.OrclInt = false;
                //app.TISInt = false;
                //app.IntAuPoAct = (profile.GetValue("Setting", "IntAuPoAct").ToString() == "True" ? true : false);



                //Email Configuration
                try
                {
                    app.SendingEmail = (profile.GetValue("Email Configuration", "SendingEmail").ToString() == "True" ? true : false);
                    app.SMTPServerIP = profile.GetValue("Email Configuration", "SMTPServerIP").ToString();
                    app.UserName = profile.GetValue("Email Configuration", "UserName").ToString();
                    app.Password = profile.GetValue("Email Configuration", "Password").ToString();
                    app.SupervisorEmail = profile.GetValue("Email Configuration", "SupervisorEmail").ToString();
                    app.MailPort = profile.GetValue("Email Configuration", "MailPort").ToString();
                    app.SenderEmail = profile.GetValue("Email Configuration", "SenderEmail").ToString();
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "Email");
                    LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                }

                //app.AuthenticationMode = profile.GetValue("Email Configuration", "AuthenticationMode").ToString();

                try
                {
                    LoadDBIntegrationSettings();
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P> list = new List<Att_Status_P>();
                    list = objBLL.GetAllStatus();
                    app.ListAttendenceStatus = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P.Department_P> list = new List<Att_Status_P.Department_P>();
                    list = objDepBLL.GetAllDepartments();
                    app.ListDepartments = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P.Location_P> list = new List<Att_Status_P.Location_P>();
                    list = objLocBLL.GetAllLocations();

                    app.ListLocations = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P.EmployeeType_P> list = new List<Att_Status_P.EmployeeType_P>();
                    list = objETBLL.GetAllEmployeeTypes();

                    app.ListEmployeeTypes = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P.Designation_P> list = new List<Att_Status_P.Designation_P>();
                    list = objDesBLL.GetAllDesignations();

                    app.ListDesignations = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                try
                {
                    List<Att_Status_P.WorkCode_P> list = new List<Att_Status_P.WorkCode_P>();
                    list = objWcBLL.GetAllWorkCodes();

                    app.ListWorkCodes = list;
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                }
                //try
                //{
                //    List<Att_Status_P.TimeZone_P> list = new List<Att_Status_P.TimeZone_P>();
                //    list = objTZBLL.GetAllTimeZones();
                //    app.ListTimeZones = list;
                //}
                //catch (Exception ex)
                //{
                //    LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
                //}
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return app;
        }

        public AppSettingModel LoadDBIntegrationSettings()
        {
            try
            {
                if (app.SQLInt)
                    app.EnableSQLIntegration = (profile.GetValue("SQL DB Setting", "EnableSqlIntegration").ToString() == "True" ? true : false);
                else
                    app.EnableSQLIntegration = false;
                app.SQLServerName = profile.GetValue("SQL DB Setting", "SqlServerName").ToString();
                app.SQLDBName = profile.GetValue("SQL DB Setting", "SqlDatabaseName").ToString();
                app.SQLUserId = profile.GetValue("SQL DB Setting", "SqlUserId").ToString();
                app.SQLPassword = profile.GetValue("SQL DB Setting", "SqlPassword").ToString();

                if (app.MySqlInt)
                    app.EnableMySqlIntegration = (profile.GetValue("MySql DB Setting", "EnableMySqlIntegration").ToString() == "True" ? true : false);
                else
                    app.EnableMySqlIntegration = false;
                app.MySqlServerName = profile.GetValue("MySql DB Setting", "mySqlServerName").ToString();
                app.MySqlDBName = profile.GetValue("MySql DB Setting", "mySqlDatabaseName").ToString();
                app.MySqlUserId = profile.GetValue("MySql DB Setting", "mySqlUserId").ToString();
                app.MySqlPassword = profile.GetValue("MySql DB Setting", "mySqlPassword").ToString();
                app.MySqlPort = profile.GetValue("MySql DB Setting", "mySqlPort").ToString();

                if (app.TISInt)
                    app.EnableTISIntegration = (profile.GetValue("TIS DB Setting", "EnableTISIntegration").ToString() == "True" ? true : false);
                else
                    app.EnableTISIntegration = false;
                //app.TISDBType = profile.GetValue("TIS DB Setting", "DBTypeForTIS").ToString();

                app.TISSQLServerName = profile.GetValue("TIS DB Setting", "ServerForSQL").ToString();
                app.TISSQLDBName = profile.GetValue("TIS DB Setting", "DataBaseForSQL").ToString();
                app.TISSQLUserID = profile.GetValue("TIS DB Setting", "UserNameForSQL").ToString();
                app.TISSQLPassword = profile.GetValue("TIS DB Setting", "PasswordForSQL").ToString();
                app.TISSQLPort = profile.GetValue("TIS DB Setting", "PortForSQL").ToString();
                app.UseSQLTcp = (profile.GetValue("TIS DB Setting", "UseTCPOnlyForSQL").ToString() == "True" ? true : false);

                if (app.OrclInt)
                    app.EnableOrclIntegration = (profile.GetValue("Oracle DB Setting", "EnableOracleIntegration").ToString() == "True" ? true : false);
                else
                    app.EnableOrclIntegration = false;
                app.OrclServicName = profile.GetValue("Oracle DB Setting", "OracleServiceName").ToString();
                app.OrclServerName = profile.GetValue("Oracle DB Setting", "OracleServerName").ToString();
                app.OrclUserId = profile.GetValue("Oracle DB Setting", "OracleUserId").ToString();
                app.OrclPassword = profile.GetValue("Oracle DB Setting", "OraclePassword").ToString();

                //app.TISOracleServerName = profile.GetValue("TIS DB Setting", "ServerForOracle").ToString();
                //app.TISOracleUserId = profile.GetValue("TIS DB Setting", "UserNameForOracle").ToString();
                //app.TISOraclePassword = profile.GetValue("TIS DB Setting", "PasswordForOracle").ToString();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return app;
        }


        [HttpPost]
        public JsonResult SetDuplicateEntries(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("File Setting", "DuplicateEntriesAllowed", appModel.IsDup);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SetSynchorinzedDateTime(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("Machine Setting", "SyncDateTime", appModel.IsSync);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EnableAutoPolling(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("Polling Setting", "EnableAutoPolling", appModel.EnableAutoPolling);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public JsonResult SetFileType(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            //string msg = "";
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("File Setting", "UseFixedPollingFile", appModel.UseFixedPollingFile);
                profile.SetValue("File Setting", "UseDateTimePollingFile", appModel.UseDateTimePollingFile);
                profile.SetValue("File Setting", "AllowPlainListFile", appModel.AllowPlainList);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetTISIntegration(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("TIS DB Setting", "EnableTISIntegration", appModel.EnableTISIntegration);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SetTISSQLUseTCP(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("TIS DB Setting", "UseTCPOnlyForSQL", appModel.UseSQLTcp);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SetORCLIntegration(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("Oracle DB Setting", "EnableOracleIntegration", appModel.EnableOrclIntegration);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SetSQLIntegration(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("SQL DB Setting", "EnableSqlIntegration", appModel.EnableSQLIntegration);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SetMYSQLIntegration(AppSettingModel appModel, bool isChecked)
        {
            bool flag = false;
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                profile.SetValue("MySql DB Setting", "EnableMySqlIntegration", appModel.EnableMySqlIntegration);
                profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JsonResult SetSendingEmail(AppSettingModel appModel, bool isChecked)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
        //        profile.SetValue("Email Configuration", "SendingEmail", appModel.SendingEmail);
        //        profile = xmlReader.GetProfile("Write", ProjectPath.FILE_PATH);
        //        flag = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    return Json(new
        //    {
        //        result = flag
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult UpdateAttStatus(int code, string name)
        {
            bool flag = false;
            try
            {
                Att_Status_P list = new Att_Status_P();
                flag = objBLL.SetAttendenceStatus(code, name);
                //app.ListAttendenceStatus = list;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateWorkCode(int code, string name)
        {
            bool flag = false;
            try
            {
                //WorkCode_P wc = new WorkCode_P();
                //Att_Status_P list = new Att_Status_P();
                flag = objWcBLL.SetWorkCode(code, name);
                //app.ListAttendenceStatus = list;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddLocation(string name)
        {
            Location_P location = new Location_P();
            bool flag = false;
            try
            {
                location.Description = name;
                flag = objLocBLL.AddUpdateLocation(location);
                location = objLocBLL.GetLocationByName(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                loc_id = location.Code
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteLocation(int code)
        {
            bool flag = false;
            try
            {
                flag = objLocBLL.DeleteLocation(Formatter.SetValidValueToInt(code));
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateLocation(int code, string name)
        {
            Location_P location = new Location_P();
            bool flag = false;
            try
            {
                location.Code = code;
                location.Description = name;
                flag = objLocBLL.AddUpdateLocation(location);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEmployeeType(string name)
        {
            EmployeeType_P employeetype = new EmployeeType_P();
            bool flag = false;
            try
            {
                employeetype.Description = name;
                flag = objETBLL.AddUpdateEmployeeType(employeetype);
                employeetype = objETBLL.GetEmployeeTypeByName(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                et_id = employeetype.Code
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEmployeeType(int code)
        {
            bool flag = false;
            try
            {
                flag = objETBLL.DeleteEmployeeType(Formatter.SetValidValueToInt(code));
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UpdateEmployeeType(int code, string name)
        {
            EmployeeType_P employeetype = new EmployeeType_P();
            bool flag = false;
            try
            {
                employeetype.Code = code;
                employeetype.Description = name;
                flag = objETBLL.AddUpdateEmployeeType(employeetype);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddDesignation(string name)
        {
            Designation_P designation = new Designation_P();
            bool flag = false;
            try
            {
                designation.Description = name;
                flag = objDesBLL.AddUpdateDesignation(designation);
                designation = objDesBLL.GetDesignationByName(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                des_id = designation.Code
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteDesignation(int code)
        {
            bool flag = false;
            try
            {
                flag = objDesBLL.DeleteDesignation(Formatter.SetValidValueToInt(code));
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UpdateDesignation(int code, string name)
        {
            Designation_P designation = new Designation_P();
            bool flag = false;
            try
            {
                designation.Code = code;
                designation.Description = name;
                flag = objDesBLL.AddUpdateDesignation(designation);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddDepartment(string name)
        {
            Department_P department = new Department_P();
            bool flag = false;
            try
            {
                department.Description = name;
                flag = objDepBLL.AddUpdateDepartment(department);
                department = objDepBLL.GetDepartmentByName(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                dept_id = department.Code
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateDepartment(int code, string name)
        {
            Department_P department = new Department_P();
            bool flag = false;
            try
            {
                department.Code = code;
                department.Description = name;
                flag = objDepBLL.AddUpdateDepartment(department);
                // department = objDepBLL.GetDepartmentByName(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                //dept_id = department.Code
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteDepartment(int code)
        {
            bool flag = false;
            try
            {
                flag = objDepBLL.DeleteDepartment(Formatter.SetValidValueToInt(code));
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);

        }

    }
}