
using Decdr;
using LncSlaMang.Controller;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace LncSlaMang
{
    public sealed class SlaLncMdle : BlackBox
    {
        private SlaLncMdle(Type baseType)
        {
            CultureInfo cl = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cl;
            baseAssembly = baseType.Assembly;
            var file_watcher = new FileSystemWatcher();
            file_watcher.Deleted += File_watcher_Deleted;
            file_watcher.Created += File_watcher_Created;
            file_watcher.Path = SysPath;
            file_watcher.Filter = GetInheritAppId();
            file_watcher.EnableRaisingEvents = true;
        }
        private void File_watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name == GetInheritAppId())
                {
                    LogService.WriteLog("System configuration file has been Created.");
                }
            }
            catch (Exception x)
            {
                //CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from File_watcher_Deleted(). Message: " + x.Message);
            }
        }
        private void File_watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name == GetInheritAppId())
                {
                    LogService.WriteLog("System configuration file has been deleted.");
                    Properties.Settings.Default["dbPath"] = true;
                    Properties.Settings.Default["IsPath"] = true;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception x)
            {
                //CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from File_watcher_Deleted(). Message: " + x.Message);
            }
        }
        public static SlaLncMdle CreateInstance(string key, Type type, bool WindowService)
        {
            try
            {
                string pwd = "LIS@$L@LnCMdle2O!9";
                string getPwd = "";

                try
                {
                    LICDE lic = new LICDE();
                    getPwd = lic.DecryptData(key, "*99L!mT0n|$uprem@|@p!99*");
                }
                catch (Exception x)
                {
                    throw new Exception("Key is invalid.",x);
                }

                if (pwd.Equals(getPwd))
                {
                    IsWindowService = WindowService;
                    return new SlaLncMdle(type);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        //public override bool Authenticate(DateTime currentDate)
        //{
        //    return Auth(currentDate);
        //}
        /// <summary>
        /// To authenticate middleware software
        /// Modified by M. Saqib Khan on 09-Jun-2020
        /// Build number verification added
        /// </summary>
        /// <param name="currentDate">dd-MMM-yyyy HH:mm:ss</param>
        /// <param name="buildNumber">abc12345</param>
        /// <returns></returns>
        public override bool Authenticate(DateTime currentDate, string buildNumber)
        {
            return Auth(currentDate, buildNumber);
        }

        /// <summary>
        /// To authenticate middleware software
        /// Modified by M. Saqib Khan on 09-Jun-2020
        /// Build number verification added
        /// </summary>
        /// <param name="currentDate">dd-MMM-yyyy HH:mm:ss</param>
        /// <param name="buildNumber">abc12345</param>
        /// /// <param name="configPath">C:\Program Files\Limton Innovative Systems\DMU</param>
        /// <returns></returns>
        public override bool Authenticate(DateTime currentDate, string buildNumber, string configPath)
        {
            config_Path = configPath;
            return Auth(currentDate, buildNumber);
        }

        public override string AuthenticateMachine(string serialNo)
        {
            if (ValidateTrailLicense() == 1)
            {
                return "true";
            }
            return AuthMachine(serialNo);
        }

        public override string AuthenticateMachine(string serialNo,string path)
        {
            return AuthMachine(serialNo, path);
        }

        public override string AuthenticateMachineWOTrail(string serialNo)
        {            
            return AuthMachine(serialNo);
        }

        public string GetCompanyName()
        {
            return ExtractCompanyName();
        }
    }
}