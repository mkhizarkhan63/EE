using Common;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using NetFwTypeLib;
using EagleEye_Service.Entity;
using EagleEye_Service.DAL;
using FKWeb;

namespace EagleEye_Service
{
    public class ServiceUtility
    {

        #region Variables
        AMS.Profile.Xml profile = new AMS.Profile.Xml();
        AppSettingModel app = new AppSettingModel();
        string MllPath, ConfigPath = "";
        // HFaceGo.Controller.PTS.HFaceGoPTS pts;
        private static System.Timers.Timer Timer_AwaitingDevice;
        string AwaitingRefreshTime = "10000";//10 sec
        public delegate void AwaitingDeviceJOB_Completed();
        public event AwaitingDeviceJOB_Completed awaitingDeviceJOB_Completed;

        private static System.Timers.Timer Timer_SearchDevice;
        string SearchRefreshTime = "10000";//10 sec
        public delegate void SearchDeviceJOB_Completed();
        public event SearchDeviceJOB_Completed searchDeviceJOB_Completed;

        private static System.Timers.Timer Timer_DateChanged;
        string SearchCurrentTime = "10000";//10 sec
        public delegate void DateChangedJOB_Completed();
        public event DateChangedJOB_Completed dateChangedJOB_Completed;

        private static System.Timers.Timer Timer_Operationlog;
        string OperationLogTime = "5000";//5 sec
        public delegate void OperationLogJOB_Completed();
        public event OperationLogJOB_Completed operationLogJOB_Completed;

        private static System.Timers.Timer Timer_Intigeration;
        // string IntigerationTime = (Formatter.SetValidValueToInt(app.PollEverySpan) * 1000).ToString();

        public delegate void IntigerationProcess_Completed();
        public event IntigerationProcess_Completed intigerationProcessJob_Completed;

        private IDisposable SignalR { get; set; }
        List<string> Devices = new List<string>();
        Communication Com = new Communication();


        HttpServer Server;


        #endregion

        #region Events

        public void Start()
        {
            try
            {
                LoadSettings();
                GetSetting();
                //License Lic = new License();
                //if (Lic.CheckProductLicense(DateTime.Now, ConfigPath))
                //{
                LogService.WriteServiceLog("SERVICE STARTED..!");
                CreateSignalRServer();
                CreateHttpServer(Com.Server_IP, Com.Server_Port, app);

                //  GetSetting();

                searchDeviceJOB_Completed += ServiceUtility_searchDeviceJOB_Completed;
                // Create a timer with a five second interval.
                Timer_SearchDevice = new System.Timers.Timer(Convert.ToInt64(SearchRefreshTime));
                // Hook up the Elapsed event for the timer.
                Timer_SearchDevice.Elapsed += new ElapsedEventHandler(SearchDevice_Elapsed);
                // Set the Interval to 5 seconds (5000 milliseconds).
                Timer_SearchDevice.Interval = Convert.ToInt64(SearchRefreshTime);
                Timer_SearchDevice.Start();


                dateChangedJOB_Completed += ServiceUtility_dateChangedJOB_Completed;
                Timer_DateChanged = new System.Timers.Timer(Convert.ToInt64(SearchCurrentTime));
                Timer_DateChanged.Elapsed += new ElapsedEventHandler(DateChanged_Elapsed);
                Timer_DateChanged.Interval = Convert.ToInt64(SearchCurrentTime);
                Timer_DateChanged.Start();

                operationLogJOB_Completed += ServiceUtility_operationLogJOB_Completed;
                Timer_Operationlog = new System.Timers.Timer(Convert.ToInt64(OperationLogTime));
                Timer_Operationlog.Elapsed += new ElapsedEventHandler(OperationLog_Elapsed);
                Timer_Operationlog.Interval = Convert.ToInt64(OperationLogTime);
                Timer_Operationlog.Start();

                string s = AppDomain.CurrentDomain.BaseDirectory + @"\CurrentDate.txt";
                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                using (StreamWriter write = new StreamWriter(s))
                {
                    write.Write(dt);
                }


                awaitingDeviceJOB_Completed += ServiceUtility_awaitingDeviceJOB_Completed;
                // Create a timer with a five second interval.
                Timer_AwaitingDevice = new System.Timers.Timer(Convert.ToInt64(AwaitingRefreshTime));
                // Hook up the Elapsed event for the timer.
                Timer_AwaitingDevice.Elapsed += new ElapsedEventHandler(AWaitingDevices_Elapsed);
                // Set the Interval to 5 seconds (5000 milliseconds).
                Timer_AwaitingDevice.Interval = Convert.ToInt64(AwaitingRefreshTime);
                Timer_AwaitingDevice.Start();


                //if (app.IntAuPoAct)
                //{
                    intigerationProcessJob_Completed += ServiceUtility_intigerationProcessJob_Completed;
                    string IntigerationTime = (Formatter.SetValidValueToInt(app.PollEverySpan) * 1000).ToString();

                    Timer_Intigeration = new System.Timers.Timer(Convert.ToInt64(IntigerationTime));
                    Timer_Intigeration.Elapsed += new ElapsedEventHandler(IntigertationProcess_Elapsed);
                    Timer_Intigeration.Interval = Convert.ToInt64(IntigerationTime);
                    Timer_Intigeration.Start();
                //}

                var a = Assembly.GetCallingAssembly();

                string name = a.FullName.Split(',')[0];
                string location = a.Location;

                AddRuleInFirewall(name, location);
                //}
                //else
                //{
                //    clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "981 Object Reference not set to an instance of an object!");
                //}
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ServiceUtility_operationLogJOB_Completed()
        {
            try
            {
                if (Timer_Operationlog != null)
                {
                    Timer_Operationlog.Start();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ServiceUtility_dateChangedJOB_Completed()
        {
            try
            {
                if (Timer_DateChanged != null)
                {
                    Timer_DateChanged.Start();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ServiceUtility_searchDeviceJOB_Completed()
        {
            try
            {
                if (Timer_SearchDevice != null)
                {
                    Timer_SearchDevice.Start();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                StopHttpServer();

                //if (pts != null)
                //    pts.Dispose();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void ServiceUtility_awaitingDeviceJOB_Completed()
        {
            try
            {
                if (Timer_AwaitingDevice != null)
                {
                    Timer_AwaitingDevice.Start();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }


        private void AWaitingDevices_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Timer_AwaitingDevice.Stop();

                Thread th = new Thread(new ThreadStart(checkDeviceAvailability));
                th.Start();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }


        private void SearchDevice_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Timer_SearchDevice.Stop();

                Thread th = new Thread(new ThreadStart(SearchDevice));
                th.Start();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }

        private void DateChanged_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Timer_DateChanged.Stop();

                Thread th = new Thread(new ThreadStart(DateChanged));
                th.Start();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }
        private void OperationLog_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {


                //Thread th = new Thread(new ThreadStart(OperationLog));
                //th.Start();
                OperationLog();


            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }
        private void IntigertationProcess_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // clsWriterLog.WriteAppLog("OnStart", "Sms Sending started");
                Timer_Intigeration.Stop();
                DALLog ObjLog = new DALLog();
                DALDevice objDevice = new DALDevice();

                #region TIS Integeration

                if (app.EnableTISIntegration && app.TISInt)
                {
                    List<EventLogs> List = ObjLog.GetLog("TIS");

                    foreach (var log in List)
                    {
                        if (log.UserID != "0" && log.UserName != "Status Monitoring Log" && log.Status != "0" && log.DoorStatus != "CloseDoor")
                        {
                            TISIntigeration TIS = new TISIntigeration(app);
                            bool flag = TIS.InsertData(log, objDevice.GetReaderID(log.DeviceID));
                            if (flag)
                            {
                                ObjLog.UpdateLogStatus("TIS", log.Code);
                                clsWriterLog.WriteProcessLog("TIS DB", "Inserted User ID: " + log.UserID);
                            }
                            else
                                clsWriterLog.WriteProcessLog("TIS DB", "Not Inserted User ID: " + log.UserID);
                        }
                    }

                }

                #endregion

                #region SQL Integeration

                if (app.EnableSQLIntegration && app.SQLInt)
                {
                    List<EventLogs> List = ObjLog.GetLog("SQL");
                    foreach (var log in List)
                    {
                        if (log.UserID != "0" && log.UserName != "Status Monitoring Log" && log.Status != "0" && log.DoorStatus != "CloseDoor")
                        {
                            SQLIntigeration Sql = new SQLIntigeration(app);
                            bool flag = Sql.InsertData(log, objDevice.GetReaderID(log.DeviceID), objDevice.GetDeviceName(log.DeviceID), app);
                            if (flag)
                            {
                                ObjLog.UpdateLogStatus("SQL", log.Code);
                                clsWriterLog.WriteProcessLog("SQL Intigeration DB", "Inserted User ID: " + log.UserID);
                            }
                            else
                                clsWriterLog.WriteProcessLog("SQL DB", "Not Inserted User ID: " + log.UserID);
                        }
                    }
                }

                #endregion

                #region Oracle Integeration

                if (app.EnableOrclIntegration && app.OrclInt)
                {
                    List<EventLogs> List = ObjLog.GetLog("Oracle");
                    foreach (var log in List)
                    {
                        if (log.UserID != "0" && log.UserName != "Status Monitoring Log" && log.Status != "0" && log.DoorStatus != "CloseDoor")
                        {
                            OracleIntigeration Oracle = new OracleIntigeration();
                            string query = "";
                            bool flag = Oracle.InsertData(log, objDevice.GetReaderID(log.DeviceID), objDevice.GetDeviceName(log.DeviceID), app);
                            clsWriterLog.WriteProcessLog("Oracle DB", "Query: " + query);

                            if (flag)
                            {
                                ObjLog.UpdateLogStatus("Oracle", log.Code);
                                clsWriterLog.WriteProcessLog("Oracle Intigeration DB", "Inserted User ID: " + log.UserID);
                            }
                            else
                            {
                                clsWriterLog.WriteProcessLog("Oracle DB", "Not Inserted User ID: " + log.UserID);
                            }
                        }
                    }
                }

                #endregion

                #region MySql Integration

                if (app.EnableMySqlIntegration && app.SQLInt)
                {
                    List<EventLogs> List = ObjLog.GetLog("MySQL");
                    foreach (var log in List)
                    {
                        if (log.UserID != "0" && log.UserName != "Status Monitoring Log" && log.Status != "0" && log.DoorStatus != "CloseDoor")
                        {
                            MySQLIntegration Sql = new MySQLIntegration(app);
                            bool flag = Sql.InsertData(log, objDevice.GetReaderID(log.DeviceID), objDevice.GetDeviceName(log.DeviceID), app);
                            if (flag)
                            {
                                ObjLog.UpdateLogStatus("MySQL", log.Code);
                                clsWriterLog.WriteProcessLog("MySQL Intigeration DB", "Inserted User ID: " + log.UserID);
                            }
                            else
                                clsWriterLog.WriteProcessLog("MySQL DB", "Not Inserted User ID: " + log.UserID);
                        }
                    }
                }

                #endregion

                intigerationProcessJob_Completed?.Invoke();


            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }

        private void ServiceUtility_intigerationProcessJob_Completed()
        {
            try
            {
                if (Timer_Intigeration != null)
                {
                    Timer_Intigeration.Start();
                    //clsWriterLog.WriteAppLog("OnStart", "Awaiting Timer Started again");
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region Methods

        private void GetSetting()
        {
            try
            {
                DALSetting setting = new DALSetting();


                string path = setting.GetSettingPath();


                MllPath = path.Replace("AppSettings.xml", @"bin\mll.dll");
                ConfigPath = path.Replace("AppSettings.xml", @"bin");

                profile = xmlReader.GetProfile("Read", path);

                app.ProfilePath = path.Replace("AppSettings.xml", @"profiles");
                app.irregularPath = path.Replace("AppSettings.xml", @"irregularProfiles");

                //file Setting
                app.PollingFolderName = profile.GetValue("File Setting", "PollingFolderName").ToString();
                app.UseFixedPollingFile = profile.GetValue("File Setting", "UseFixedPollingFile").ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
                app.UseDateTimePollingFile = profile.GetValue("File Setting", "UseDateTimePollingFile").ToString().Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
                app.PollingFileName = profile.GetValue("File Setting", "PollingFileName").ToString();
                app.PollEverySpan = profile.GetValue("Polling Setting", "PollEverySpan").ToString();


                app.HiddenFilePath = profile.GetValue("File Setting", "HiddenPath", "");
                bool IsDup = bool.Parse(profile.GetValue("File Setting", "DuplicateEntriesAllowed", "").ToString());
                bool IsSync = bool.Parse(profile.GetValue("Machine Setting", "SyncDateTime", "").ToString());
                bool IsSms = bool.Parse(profile.GetValue("Setting", "Sms", "").ToString());
                app.IsDup = IsDup;
                app.IsSync = IsSync;
                app.AllowPlainList = profile.GetValue("File Setting", "AllowPlainListFile", "").ToLower() == "true" ? true : false;


                License Lic = new License();
                app.LDAT = Lic.LDAT;
                app.IntAuPoAct = Lic.AutoPolling;
                app.TISInt = Lic.IntTis;
                if (Lic.IntOracle)
                    app.OrclInt = true;
                if (Lic.IntSql)
                    app.SQLInt = true;

                app.SQLInQr = profile.GetValue("Setting", "SQLInQr").ToString();
                app.ORCLInQr = profile.GetValue("Setting", "ORCLInQr").ToString();

                app.EnableOrclIntegration = (profile.GetValue("Oracle DB Setting", "EnableOracleIntegration").ToString() == "True" ? true : false);
                app.OrclServicName = profile.GetValue("Oracle DB Setting", "OracleServiceName").ToString();
                app.OrclServerName = profile.GetValue("Oracle DB Setting", "OracleServerName").ToString();
                app.OrclUserId = profile.GetValue("Oracle DB Setting", "OracleUserId").ToString();
                app.OrclPassword = profile.GetValue("Oracle DB Setting", "OraclePassword").ToString();

                app.EnableTISIntegration = (profile.GetValue("TIS DB Setting", "EnableTISIntegration").ToString() == "True" ? true : false);
                app.TISSQLServerName = profile.GetValue("TIS DB Setting", "ServerForSQL").ToString();
                app.TISSQLDBName = profile.GetValue("TIS DB Setting", "DataBaseForSQL").ToString();
                app.TISSQLUserID = profile.GetValue("TIS DB Setting", "UserNameForSQL").ToString();
                app.TISSQLPassword = profile.GetValue("TIS DB Setting", "PasswordForSQL").ToString();
                app.TISSQLPort = profile.GetValue("TIS DB Setting", "PortForSQL").ToString();
                app.UseSQLTcp = (profile.GetValue("TIS DB Setting", "UseTCPOnlyForSQL").ToString() == "True" ? true : false);

                app.EnableSQLIntegration = (profile.GetValue("SQL DB Setting", "EnableSqlIntegration").ToString() == "True" ? true : false);
                app.SQLServerName = profile.GetValue("SQL DB Setting", "SqlServerName").ToString();
                app.SQLDBName = profile.GetValue("SQL DB Setting", "SqlDatabaseName").ToString();
                app.SQLUserId = profile.GetValue("SQL DB Setting", "SqlUserId").ToString();
                app.SQLPassword = profile.GetValue("SQL DB Setting", "SqlPassword").ToString();

                app.EnableMySqlIntegration = (profile.GetValue("MySql DB Setting", "EnableMySqlIntegration").ToString() == "True" ? true : false);
                app.MySqlServerName = profile.GetValue("MySql DB Setting", "mySqlServerName").ToString();
                app.MySqlDBName = profile.GetValue("MySql DB Setting", "mySqlDatabaseName").ToString();
                app.MySqlUserId = profile.GetValue("MySql DB Setting", "mySqlUserId").ToString();
                app.MySqlPassword = profile.GetValue("MySql DB Setting", "mySqlPassword").ToString();
                app.MySqlPort = profile.GetValue("MySql DB Setting", "mySqlPort").ToString();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                if (ex.InnerException != null)
                    clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.InnerException.Message);
            }
        }

        private void checkDeviceAvailability()
        {
            try
            {
                DALAwaitingDevice obj = new DALAwaitingDevice();
                DALDevice objDevice = new DALDevice();
                // EagleEyeManagment em = new EagleEyeManagment();
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();

                DataTable dtConnected = objDevice.GetAllDevices();
                DataTable dtFKDevice = new DataTable();
                if (dtConnected.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConnected.Rows.Count; i++)
                    {
                        string Device_ID = dtConnected.Rows[i]["Device_Id"].ToString();
                        string Device_Name = dtConnected.Rows[i]["Device_Name"].ToString();
                        string Code = dtConnected.Rows[i]["Code"].ToString();
                        dtFKDevice = obj.GetFkWebServerDevice(Device_ID);
                        // LogService.WriteServiceLog("Method: [checkDeviceAvailability] - Get Fk Device..: " + dtFKDevice.Rows[0][0].ToString());

                        bool flag = false;
                        objDevice.UpdateConnectedDeviceStatus(Code, dtFKDevice, out flag);

                        clsWriterLog.WriteDevLog("Device Status", Device_Name + " " + flag);

                        //objDevice.UpdateFKDeviceStatus(Code, dtFKDevice);
                        hubContext.Clients.All.refreshConnectedDevices(Code, flag);
                    }
                    DataTable dt = new DataTable();
                    dt = objDevice.GetAllConnectedDevices();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Device_ID = dt.Rows[i]["Device_Id"].ToString();
                            string Cmd = "DELETE_USER";
                            dt = objDevice.GetCommand(Device_ID, Cmd);
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    bool flag = objDevice.DeleteUserfromMachine(dt.Rows[j]["Employee_ID"].ToString(), dt.Rows[j]["Device_ID"].ToString(), app);
                                    if (flag)
                                        flag = objDevice.DeleteTempEmployee(dt.Rows[j]["Device_ID"].ToString(), dt.Rows[j]["Employee_ID"].ToString());
                                }
                            }
                        }

                    }
                    DataTable dt2 = new DataTable();
                    dt2 = objDevice.GetAllConnectedDevices();
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            string Device_ID = dt2.Rows[i]["Device_Id"].ToString();
                            dt = objDevice.GetFkCommands(Device_ID);
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    bool flag = objDevice.InsertFkCommands(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][3].ToString());
                                    if (flag)
                                        flag = objDevice.DeleteFkCommand(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString());
                                }
                            }
                        }
                    }
                }

                LoadSettings();

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                awaitingDeviceJOB_Completed?.Invoke();
            }
        }


        private void OperationLog()
        {
            try
            {
                Timer_Operationlog.Stop();

                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();
                DataTable dt = new DataTable();
                List<OperationLog> olog = new List<OperationLog>();
                DALOperationLog log = new DALOperationLog();
                DALDevice objDevice = new DALDevice();
                DataTable dtConnected = objDevice.GetAllDevices();
                DataTable dtFKDevice = new DataTable();

                if (dtConnected.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConnected.Rows.Count; i++)
                    {
                        string Device_ID = dtConnected.Rows[i]["Device_Id"].ToString();
                        dtFKDevice = objDevice.GetFkWebServerDevice(Device_ID);
                        string status = dtFKDevice.Rows[0]["connected"].ToString();

                        bool flag = log.UpdateOperationDeviceStatus(Device_ID, status);
                    }
                }

                olog = log.GetOperationLog();
                foreach (var operation in olog)
                {
                    dt = objDevice.GetFkCmdTrans(operation.Trans_ID);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string status = dt.Rows[i]["return_code"].ToString();
                            string msg = dt.Rows[i]["return_code"].ToString();
                            string deviceStatus = operation.Device_Status.ToString();
                            string response = "";
                            bool flag = log.UpdateOperationLog(operation.Trans_ID, status, deviceStatus, msg, out response);

                            hubContext.Clients.All.sendOperationLog(operation.Trans_ID, response, status, deviceStatus);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                operationLogJOB_Completed?.Invoke();
            }
        }

        private void DateChanged()
        {
            try
            {
                List<Att_Status_P.TimeZone_P> tz = new List<Att_Status_P.TimeZone_P>();
                List<Employee> employees = new List<Employee>();
                DALDevice objDevice = new DALDevice();
                DataTable dtDevice = objDevice.GetAllDevices();
                string s = AppDomain.CurrentDomain.BaseDirectory + @"\CurrentDate.txt";
                string date = File.ReadAllText(s);
                string currDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (date != currDate)
                {
                    DALEmployee emp = new DALEmployee();
                    employees = emp.GetInvalidEmployee(currDate);
                    foreach (var employee in employees)
                    {
                        Employee e = new Employee();
                        e.Employee_ID = employee.Employee_ID;
                        e.Employee_Name = employee.Employee_Name;
                        e.Employee_Photo = employee.Employee_Photo;
                        e.Email = employee.Email;
                        e.Password = employee.Password;
                        e.Card_No = employee.Card_No;
                        e.Active = employee.Active;
                        e.Gender = employee.Gender;
                        e.Address = employee.Address;
                        e.fkDepartment_Code = employee.fkDepartment_Code;
                        e.fkLocation_Code = employee.fkLocation_Code;
                        e.fkDesignation_Code = employee.fkDesignation_Code;
                        e.fkEmployeeType_Code = employee.fkEmployeeType_Code;
                        e.Telephone = employee.Telephone;
                        e.User_Privilege = employee.User_Privilege;
                        e.FingerPrint = employee.FingerPrint;
                        e.Face = employee.Face;
                        e.Palm = employee.Palm;
                        e.Password = employee.Password;

                        e.Trans_Id = employee.Trans_Id;
                        e.Update_Date = DateTime.Now;
                        e.Cmd_Param = employee.Cmd_Param;
                        e.Device_Id = employee.Device_Id;

                        e.Valid_DateStart = employee.Valid_DateStart;
                        e.Valid_DateEnd = employee.Valid_DateEnd;
                        e.Sunday = employee.Sunday;
                        e.Monday = employee.Monday;
                        e.Tuesday = employee.Tuesday;
                        e.Wednesday = employee.Wednesday;
                        e.Thursday = employee.Thursday;
                        e.Friday = employee.Friday;
                        e.Saturday = employee.Saturday;
                        e.IsDelete = employee.IsDelete;
                        emp.InsertExpiredEmployee(e);
                    }
                }
                if (date != currDate)
                {
                    DALTimeZones timeZones = new DALTimeZones();
                    tz = timeZones.GetAllTimeZones();
                    foreach (var t in tz)
                    {
                        Att_Status_P.TimeZone_P e = new Att_Status_P.TimeZone_P();
                        e.Code = t.Code;
                        e.Timezone_No = t.Timezone_No;
                        e.Timezone_Name = t.Timezone_Name;
                        e.Period_1_Start = t.Period_1_Start.Replace(":", "");
                        e.Period_1_End = t.Period_1_End.Replace(":", "");
                        e.Period_2_Start = t.Period_2_Start.Replace(":", "");
                        e.Period_2_End = t.Period_2_End.Replace(":", "");
                        e.Period_3_Start = t.Period_3_Start.Replace(":", "");
                        e.Period_3_End = t.Period_3_End.Replace(":", "");
                        e.Period_4_Start = t.Period_4_Start.Replace(":", "");
                        e.Period_4_End = t.Period_4_End.Replace(":", "");
                        e.Period_5_Start = t.Period_5_Start.Replace(":", "");
                        e.Period_5_End = t.Period_5_End.Replace(":", "");
                        e.Period_6_Start = t.Period_6_Start.Replace(":", "");
                        e.Period_6_End = t.Period_6_End.Replace(":", "");
                        e.Status = t.Status;
                        for (int i = 0; i < dtDevice.Rows.Count; i++)
                        {
                            string Device_ID = dtDevice.Rows[i]["Device_Id"].ToString();
                            string status = dtDevice.Rows[i]["Device_Status"].ToString();
                            if (status == "1")
                                timeZones.SetTimeZone(Device_ID, e, Formatter.SetValidValueToInt(status), app);
                        }
                    }
                }
                using (StreamWriter write = new StreamWriter(s))
                {
                    write.Write(currDate);
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                dateChangedJOB_Completed?.Invoke();
            }
        }

        private void SearchDevice()
        {
            try
            {
                DataTable dt = new DataTable();
                DALAwaitingDevice objAwaitingDevice = new DALAwaitingDevice();
                DALDevice objDevice = new DALDevice();
                dt = objAwaitingDevice.GetAllFkWebServerDevices();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string deviceID = dt.Rows[i][0].ToString();

                    // LogService.WriteServiceLog("Method: [SearchDevice] - Search Device..: " + deviceID);
                    if (!objDevice.CheckDeviceExistInDB(deviceID))
                    {
                        dt = objAwaitingDevice.GetFkWebServerDevice(deviceID);
                        objDevice.GetFKStatusInfo(dt.Rows[0][0].ToString());
                        if (dt.Rows.Count != 0)
                            objAwaitingDevice.AddAwaitingDevice(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][7].ToString());
                    }
                    IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotifierHub>();
                    hubContext.Clients.All.refreshAwaitingDevices();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                searchDeviceJOB_Completed?.Invoke();
            }
        }

        private void CreateSignalRServer()
        {
            try
            {
                if (Com.Server_IP == "" || Com.Server_IP == null)
                {
                    Com.Server_IP = "localhost";
                }
                if (HelpingMethod.PortInUse(Convert.ToInt32(Com.SignalR_Port)) == false)
                {
                    if (SignalR == null)
                    {
                        LogService.WriteServiceLog("Method: [CreateSignalRServer] - SignalR URL: " + "http://" + Com.Server_IP + ":" + Com.SignalR_Port);
                        SignalR = WebApp.Start<Startup>("http://" + Com.Server_IP + ":" + Com.SignalR_Port);//Com.Server_IP

                        clsWriterLog.WriteAppLog("CreateSignalRServer", "Signal R server created.");
                    }
                }
                else
                {
                    clsWriterLog.WriteAppLog("CreateSignalRServer", "Already Created Port is in use!!");
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message + ex.InnerException.Message);
            }
        }

        private void CreateHttpServer(string IP, int Port, AppSettingModel setting)
        {

            try
            {
                app = setting;
                int WorkerCount = 1;
                DALDevice device = new DALDevice();
                int NoOfDevices = device.GetAllDevices().Rows.Count;
                if (NoOfDevices > 0)
                    WorkerCount = NoOfDevices;


                Server = new HttpServer(WorkerCount, setting);
                Server.Start(IP, Port);
                clsWriterLog.WriteAppLog("CreateHttpServer", "Http server created on " + IP + ":" + Port);
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

        }

        private void StopHttpServer()
        {

            try
            {
                if (Server != null)
                    Server.Stop();
                clsWriterLog.WriteAppLog("StopHttpServer", "Http server stopped.");
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void CreatePTSServer()
        {
            try
            {
                string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";
                string conStr = File.ReadAllText(s);

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }


        }

        private void LoadSettings()
        {
            try
            {
                DALCommunication comm = new DALCommunication();
                Com = comm.GetCommunication();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void AddRuleInFirewall(string AppName, string Location)
        {
            try
            {
                INetFwRule fwRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                INetFwPolicy2 fwPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

                foreach (INetFwRule2 item in fwPolicy.Rules)
                {
                    if (item.Name.ToLower() == AppName.ToLower())
                    {
                        return;
                    }
                }

                if (1 == 1)
                {
                    fwRule.ApplicationName = Location;

                    fwRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                    fwRule.Description = AppName;
                    fwRule.Enabled = true;

                    fwRule.InterfaceTypes = "All";

                    fwRule.Name = AppName;

                    fwPolicy.Rules.Add(fwRule);

                    clsWriterLog.WriteAppLog("Application Rule added & allowed in Windows Firewall with name: ", AppName);
                }

                //var powershell = PowerShell.Create();


                //return true;
            }
            catch (Exception ex)
            {
                string ErrMsg = "Error while AddRuleInFirewall: " + ex.Message;

                if (ex.InnerException != null)
                    ErrMsg += ex.InnerException.Message;

                //throw new Exception(ErrMsg);
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }

        #endregion

    }
}
