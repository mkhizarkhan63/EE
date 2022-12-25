using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LncSlaMang.Controller
{
    public abstract  class BlackBox : IsConfig
    {
        private const int EXCEPTION_CODE = 65537;
        protected static bool IsWindowService = false;
        protected DateTime currentDate;
        internal static string config_Path = "";

        public  abstract bool Authenticate(DateTime currentDate, string buildNumber);
        public abstract bool Authenticate(DateTime currentDate, string buildNumber, string configPath);
        public abstract string AuthenticateMachine(string serialNo);
        public abstract string AuthenticateMachineWOTrail(string serialNo);
        
        protected BlackBox()
        {
            CultureInfo cl = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cl;            
        }
       
        private string S_KEY()
        {
            return "F9knhLvgshN0pDlMbZm/7Q==";
        }

        protected Exception Error { get; private set; }
        public static string GetAllException(Exception x)
        {
            if (x.InnerException == null)
                return x.Message;

            return x.Message + "\r\nInnerException: " + GetAllException(x.InnerException);
        }

        protected  bool Auth(DateTime currentDate, string buildNumber)
        {
            try
            {
                string mybuildNumber = "20200701";

                if (buildNumber != mybuildNumber)
                {
                    LogService.WriteLog(" Error Code: 200601");
                    CustomErrorBox("Invalid build !", "200601");
                    return false;
                }

                this.currentDate = currentDate;
                CultureInfo cl = new CultureInfo("en-GB");
                Thread.CurrentThread.CurrentCulture = cl;
                string upk; string actKey;
                int ret = 0;

                //Generate Config.ini file
                GenerateConfigFile(GetUPK(), ret);

                // check and getting informatino from mll.dll
                string result = GetMLicFileAndVerify(out actKey, out upk);
                
                if (result != "true")
                {
                    LogService.WriteLog(result+" Error Code: 105");
                    ret = -1;
                }

                if (ret == -1)
                {
                    if (!Properties.Settings.Default.IsPath) // When once is expired/non-validate for any reason it wont run again
                    {
                        if (!Properties.Settings.Default.Path) // Path = system config file installed. Checked
                        {
                            if (!ReadConfigStatus())//&& !helper.IsDExpired)
                            {
                                if (!SysConfigFT(currentDate))
                                {
                                    return false;
                                }
                                //helper.SetIsDExpired(true);
                            }
                        }
                        if (!Properties.Settings.Default.dbPath)//if someone delete config file.It maintain checked
                        {
                            // Validate 7 days Trail License
                            ret = ValidateTrailLicense();
                            //Generate Config.ini file
                            GenerateConfigFile(GetUPK(), ret);

                            if (ret == 1)
                                return true;

                            if (ret == 55)
                            {
                                CustomErrorBox("System Configuration file has been corrupted.", "55");
                                Properties.Settings.Default["IsPath"] = true;
                                Properties.Settings.Default.Save();
                            }
                        }
                    }
                    else
                    {
                        CustomErrorBox("Configuration file has been corrupted. ", "955441");
                    }
                }

                //Validate license after trail day expired   
                if (ret == 0)
                {
                    ret = CheckingLicense(true, actKey, currentDate);
                    if (ret == 1)
                    {
                        if (String.IsNullOrEmpty(upk) || !GetUPK().Equals(upk.Trim().ToString()))
                        {
                            CustomErrorBox("Configuration file has been corrupted", "102");
                            return false;
                        }

                        if (!Properties.Settings.Default.IsPath)
                        {
                            Properties.Settings.Default["IsPath"] = true;
                            Properties.Settings.Default.Save();
                        }
                        return true;
                    }
                    else if (ret == -1)
                    {
                        CustomErrorBox("Configuration file has been corrupted", "103");
                    }
                    else if (ret == -981)
                    {
                        //helper.SetAPL(String.Empty);
                        CustomErrorBox("Exeception Code -981. Object reference not set an instance of an object.", "981");
                    }
                }
                return false;
            }
            catch (Exception x)
            {
                //CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from Authenticate(). Message: " + GetAllException(x));
                return false;
            }
        }
        protected  string AuthMachine(string serial_no)
        {
            try
            {
                Type ClassType = null;
                string result = Execute_Mll(ref ClassType);
                if (result != "true")
                {
                    return result;
                }

                MethodInfo getMacList = ClassType.GetMethod("GetList");
                List<string> returnMacValue = (List<string>)getMacList.Invoke(null, new object[] { });
                if (returnMacValue != null && returnMacValue.Count != 0)
                {
                    List<string> serialList = new List<string>();
                    foreach (string getKey in returnMacValue)
                    {
                        if (CheckMacLic(getKey, serial_no))
                        {
                            return "true";
                        }
                    }
                }
                else
                {
                    return "mll.Class data object is null in mll.dll";
                }

                return "This Device " + serial_no + " is not compatible.";
            }
            catch (Exception x)
            {
                LogService.WriteLog("Exception from AuthenticateMachine(). Message: " + GetAllException(x));
                return x.Message;

            }
        }

        protected string GetProductName()
        {
            return Application.ProductName;
        }

        private int MachineLicense(string lic, string machineSN)
        {
            try
            {
                if (String.IsNullOrEmpty(lic))
                {
                    return 0;
                }

                string[] getList = Decode(lic);
                // on index 0, check secret password. 
                if (getList[1] == machineSN)
                {

                }
                if (getList[0].Equals(S_KEY()) && getList[1].Equals(machineSN))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString().ToString());
                LogService.WriteLog("Exception from MachineLicense(). Message: " + GetAllException(x));
                return -2;
            }
        }

        protected string ExtractCompanyName()
        {
            try
            {
                string actKey; string upk;
                string result = GetMLicFileAndVerify(out actKey, out upk);
                if (result == "true")
                {
                    if (String.IsNullOrEmpty(actKey))
                        return String.Empty;
                    string[] getList = Decode(actKey);
                    return getList.ElementAtOrDefault(8);
                }                
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GetCompanyName(). Message: " + GetAllException(x));                
            }
            return String.Empty;
        }

        protected int CheckingLicense(bool pFlag, string slk, DateTime crndt)
        {
            try
            {
                //pFlag=true
                //slk=UPK
                //crndt=Machine Date Time

                if (!pFlag || String.IsNullOrEmpty(slk))
                {
                    return -1;
                }
                else
                {
                    if (String.IsNullOrEmpty(slk))
                    {
                        return -981;
                    }

                    string[] getList = Decode(slk);

                    if (!getList[0].Equals(Application.ProductName) || !getList[4].Equals(S_KEY()) || GetHId("Win32_Processor", "ProcessorID") != getList[2])
                    {
                        return -981;
                    }

                    string getVol = getList[6];
                    if (!String.IsNullOrEmpty(getVol))
                    {
                        string[] splitDates = getVol.Split('-');
                        DateTime getFromDate = Convert.ToDateTime(splitDates[0]);
                        DateTime getToDate = Convert.ToDateTime(splitDates[1]);

                        if (crndt.Year >= getFromDate.Year && crndt.Year <= getToDate.Year)
                        {
                            if (crndt.Ticks >= getFromDate.Ticks && crndt.Ticks <= getToDate.Ticks)
                            {
                                return 1;
                            }
                        }

                        return -981;
                    }
                    return 1;
                }
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString().ToString());
                LogService.WriteLog("Exception from CheckingLicense(). Message: " + GetAllException(x));
                return EXCEPTION_CODE;
            }
        }

        protected string GetUPK()
        {
            try
            {
                //string uId = string.Concat(GetProductName(), "$&", GetHId("Win32_Processor", "ProcessorID"));
                string uId = string.Concat(GetProductName(), "$&", GetHardwareId("Win32_Processor", "ProcessorID"));


                byte[] _bytesId = Encoding.UTF8.GetBytes(uId);
                return Convert.ToBase64String(_bytesId);
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from UPK(). Message: " + GetAllException(x));
                return String.Empty;
            }
        }
        protected string GetMacAdd()
        {
            string empty = string.Empty;
            try
            {
                foreach (ManagementObject instance in new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances())
                {
                    if ((bool)instance["IPEnabled"] && instance["MacAddress"] != null)
                    {
                        empty = instance["MacAddress"].ToString();
                        break;
                    }
                }
            }
            catch
            {
            }
            return empty;
        }
        private string[] Decode(string slk)
        {
            try
            {
                byte[] t1 = Convert.FromBase64String(slk);
                string getDTxt = Encoding.UTF8.GetString(t1);
                string[] getList = getDTxt.Split('$', '&');
                return getList;
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from Decode(). Message: " + GetAllException(x));
                return null;
            }
        }

        private string GetHId(string key, string value)
        {
            try
            {
                string cpuInfo = String.Empty;
                ManagementClass mangClass = new ManagementClass(key);
                ManagementObjectCollection mangObjCollection = mangClass.GetInstances();
                foreach (ManagementObject managObj in mangObjCollection)
                {
                    cpuInfo = managObj.Properties[value].Value.ToString();
                    if (!String.IsNullOrEmpty(cpuInfo))
                        break;
                }
                
                //return cpuInfo + "#" + GetHDDSN();
                //return cpuInfo + "#" + GetMacAdd();

                if (IsVirtualMachine())
                    return cpuInfo + "#" + GetMacAdd();
                else
                    return cpuInfo + "#" + GetHDDSN();
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GetHId(). Message: " + GetAllException(x));
                return String.Empty;
            }
        }
        private bool IsVirtualMachine()
        {
            try
            {
                using (var searcher = new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
                {
                    using (var items = searcher.Get())
                    {
                        foreach (var item in items)
                        {
                            string manufacturer = item["Manufacturer"].ToString().ToLower();
                            if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                                || manufacturer.Contains("vmware")
                                || item["Model"].ToString() == "VirtualBox")
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from IsVirtualMachine(). Message: " + GetAllException(x));
                return false;
            }
        }

        private string GetHardwareId(string key, string value)
        {
            try
            {
                string cpuInfo = String.Empty;
                string query = string.Format("Select {0} FROM {1}", value, key);

                ManagementObjectSearcher searchar = new ManagementObjectSearcher(query);
                ManagementObjectCollection mangObjCollection = searchar.Get();
                foreach (ManagementObject managObj in mangObjCollection)
                {
                    cpuInfo = managObj.Properties[value].Value.ToString();
                    if (!String.IsNullOrEmpty(cpuInfo))
                        break;
                }
                //return cpuInfo + "#" + GetHDDSN();
                return cpuInfo + "#" + GetMacAdd();
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GetHId(). Message: " + GetAllException(x));
                return String.Empty;
            }
        }

        private string GetHDDSN()
        {
            try
            {
                string hdd_sn = String.Empty;
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    hdd_sn = wmi_HD.GetPropertyValue("SerialNumber").ToString().Trim();
                    string HDSNo = wmi_HD["SerialNumber"].ToString();

                    if (!String.IsNullOrEmpty(hdd_sn))
                        break;
                }
                return hdd_sn;
            }
            catch (Exception x)
            {
                Error = x;
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GetHDDSN(). Message: " + GetAllException(x));
                return String.Empty;
            }
        }
        private string GET_S_KEY(string secret_Key)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] getSKeyByte = Encoding.UTF8.GetBytes(secret_Key);
                return Convert.ToBase64String(md5.ComputeHash(getSKeyByte));
            }
            catch (Exception x)
            {
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GET_S_KEY(). Message: " + GetAllException(x));
                Error = x;
                return String.Empty;
            }
        }
        private string SerialDecode(string key)
        {
            try
            {
                byte[] t1 = Convert.FromBase64String(key);
                string t = Encoding.UTF8.GetString(t1).Split('&')[1];

                return t;
            }
            catch (Exception x)
            {
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from SerialDecode(). Message: " + GetAllException(x));
                return null;
            }
        }
        protected bool ReadConfigStatus()
        {
            try
            {
                ConfigType readCntnt = ReadConfig();
                if (readCntnt == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception x)
            {
                //CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from ReadConfigStatus(). Message: " + GetAllException(x));
                return false;
            }
        }
        protected bool SysConfigFT(DateTime iDT)
        {
            try
            {
                //{0} = Installed Date
                //{1} = Expire Check
                //{2} = Last UpdateDT
                ConfigType readCntnt = ReadConfig();
                if (readCntnt == null)
                {
                    //maintain state of delete file

                    if (!Properties.Settings.Default.Path)
                    {
                        string contnt = String.Format("{0};{1};{2}", iDT.ToShortDateString(), false, currentDate.ToShortDateString());
                        WriteConfig(contnt);
                        Properties.Settings.Default["Path"] = true;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception x)
            {
                if (x.Message.Contains("denied"))
                    SlaLncMdle.CustomErrorBox("System Permission is denied. Please run as administrative mode.", "500");
                else
                    SlaLncMdle.CustomErrorBox(GetAllException(x), "-0000");

                LogService.WriteLog("Exception from SysConfigFT(). Message: " + GetAllException(x));
                return false;
            }
        }
        private void ConfigUpdate()
        {
            try
            {
                ConfigType readCntnt = ReadConfig();
                if (readCntnt == null)
                {
                    //CustomErrorBox("System Configuration file has been corrupted. Read content is missing!", "55");
                    return;
                }
                if (readCntnt.Exp_DT == true)
                    return;

                if (currentDate.Date >= DateTime.Parse(readCntnt.LUpdteDT).Date)
                {
                    string contnt = String.Format("{0};{1};{2}", readCntnt.IDT, false, currentDate.ToShortDateString());
                    WriteConfig(contnt);
                }
            }
            catch (Exception x)
            {
                LogService.WriteLog("Exception from ConfigUpdate(). Message: " + GetAllException(x));
                throw x;
            }
        }
        protected int ValidateTrailLicense()
        {
            try
            {
                CultureInfo cl = new CultureInfo("en-GB");
                Thread.CurrentThread.CurrentCulture = cl;

                ConfigUpdate();
                DateTime crntDT = DateTime.Parse(currentDate.ToShortDateString());
                DateTime iDT;
                DateTime SevenDayDT;
                DateTime LastCurnDT;
                ConfigType readCntnt = ReadConfig();
                if (readCntnt == null || (readCntnt != null && readCntnt.Exp_DT))
                {
                    LogService.WriteLog("System Configuration file has been corrupted.Read content is missing!");
                    //CustomErrorBox("System Configuration file has been corrupted. Read content is missing!", "55");
                    return 55;
                }

                //if (readCntnt.Exp_DT )
                //{
                //    // CustomErrorBox("Configuration file has been corrupted", "55");
                //    Properties.Settings.Default["IsPath"] = true;
                //    Properties.Settings.Default.Save();
                //    return 55;
                //}


                iDT = DateTime.Parse(readCntnt.IDT);
                SevenDayDT = iDT.AddDays(14);
                LastCurnDT = DateTime.Parse((String.IsNullOrEmpty(readCntnt.LUpdteDT) ? currentDate.ToShortDateString() : readCntnt.LUpdteDT));

                if (crntDT.Year >= iDT.Year && crntDT.Year <= SevenDayDT.Year)
                {
                    if (crntDT.Ticks >= iDT.Ticks && crntDT.Ticks <= SevenDayDT.Ticks && crntDT.Date == LastCurnDT.Date)
                    {
                        WriteConfig(String.Format("{0};{1};{2}", readCntnt.IDT, false, currentDate.ToShortDateString()));
                        return 1; // valid for 7 days
                    }
                }

                WriteConfig(String.Format("{0};{1};{2}", readCntnt.IDT, true, readCntnt.LUpdteDT));
                //CustomErrorBox("Configuration file has been corrupted", "55");
                return 55; // 7 day expired
            }
            catch (Exception x)
            {
                if (x.Message.Contains("denied"))
                    SlaLncMdle.CustomErrorBox("System Permission is denied. Please run as administrative mode.", "500");
                else
                    SlaLncMdle.CustomErrorBox(GetAllException(x), EXCEPTION_CODE.ToString().ToString());

                LogService.WriteLog("Exception from ValidateLicense(). Message: " + GetAllException(x));
                return EXCEPTION_CODE;
            }
        }
        protected static void CustomErrorBox(string msg, string code)
        {
            try
            {
                if (!IsWindowService)
                {
                    LogService.WriteLog(String.Format("[Info] Message: {0}. Error Code: {1}", msg, code));
                    ErrorForm r = new ErrorForm(msg, code);
                    r.ShowDialog();
                }
                else
                {
                    LogService.WriteLog(String.Format("[Info] Message: {0}. Error Code: {1}", msg, code));
                }
            }
            catch (Exception x)
            {
                LogService.WriteLog(String.Format("[Exception] Message: {0}. Error Code: {1}", msg, code));
                LogService.WriteLog("Exception from CustomErrorBox(). Message: " + GetAllException(x));
                throw x;
            }

        }
        protected void GenerateConfigFile(string upk, int errorCode)
        {
            try
            {
                //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\config.ini";
                //string path  = Application.StartupPath + @"\config.ini";
                string path = "";

                if (config_Path == "")
                    path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config.ini";
                else
                {
                    //path = config_Path + @"\config.ini";
                    path = Path.GetFullPath(config_Path) + @"\config.ini";
                }

                string getContent = "";

                if (File.Exists(path))
                {
                    getContent = File.ReadAllText(path);
                    getContent = Encoding.ASCII.GetString(Convert.FromBase64String(getContent));
                    if (getContent.Trim().Contains("APP_ID") && getContent.Trim().Contains(upk))
                    {
                        return;
                    }
                }
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    ConfigData data = new ConfigData();
                    data.APP_ID = upk;
                    data.Code = errorCode.ToString();
                    data.Date = currentDate.ToString();
                    data.App_Name = Application.ProductName;
                    data.App_Ver= Application.ProductVersion;
                    data.SystemInformation = new ConfigData.ClientSystemInfo()
                    {
                        Architecture = Environment.Is64BitOperatingSystem?"x64":"x32",
                        OS = Environment.OSVersion.ToString()
                    };

                    string JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                   string content = String.Format("{0}\n{1}", getContent, JsonData);
                    //string content = String.Format("{0}\n[{3}]\nAPP_ID={1}\nCode={2}", getContent, upk, errorCode, currentDate.ToString());
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.Write(Convert.ToBase64String(Encoding.ASCII.GetBytes(content)));
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception x)
            {
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from GenerateConfigFile(). Message: " + GetAllException(x));
                throw x;
            }
        }
        
        #region Machine Lic Check
        protected string Execute_Mll(ref Type ClassType)
        {
            try
            {
                string get_Dll = "";
                if (config_Path == "")
                    get_Dll = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                else
                    get_Dll = Path.GetFullPath(config_Path);

                string path = get_Dll + @"\mll.dll";
                if (!File.Exists(path))
                {
                    return "mll.dll is missing.";
                }

                var dll = Assembly.LoadFile(path);
                if (dll == null)
                {
                    return "mll.dll is missing.";
                }

                ClassType = dll.GetType("mll.Class1");
                if (ClassType == null)
                {
                    return "mll.Class object is null in mll.dll.";
                }
                return "true";
            }
            catch (Exception x)
            {
                if (x.Message.Contains("An attempt was made to load an assembly from a network location which would have caused the assembly to be sandboxed in previous versions of the .NET Framework."))
                {
                    CustomErrorBox("UNBLOCK THE FILE. \r\n" + x.Message, EXCEPTION_CODE.ToString());
                }
                else
                {
                    CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                }
                LogService.WriteLog("Exception from Execute_Mll(). Message: " + GetAllException(x));
                return String.Empty;
            }
        }
        protected string GetMLicFileAndVerify(out string actKey, out string upk)
        {
            actKey = upk = String.Empty;
            try
            {
                Type ClassType = null;
                string result = Execute_Mll(ref ClassType);
                if (result != "true")
                {
                    return result;
                }

                MethodInfo getActMethod = ClassType.GetMethod("GetActKey");
                MethodInfo getUPKMethod = ClassType.GetMethod("GetUPKKey");
                //PropertyInfo getRequestedByProperty = class1Type.GetProperty("REQUESTED_BY");

                string returnAckValue = (string)getActMethod.Invoke(null, new object[] { });
                string returnUPKValue = (string)getUPKMethod.Invoke(null, new object[] { });


                if (!String.IsNullOrEmpty(returnAckValue))
                {
                    actKey = returnAckValue;
                }

                if (!String.IsNullOrEmpty(returnUPKValue))
                {
                    upk = returnUPKValue;
                }

                return "true";
            }
            catch (Exception x)
            {
                //if (x.HResult == -2147024894)
                //{
                //    return "mll.dll is missing.";
                //}
                //else
                {
                    CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                    LogService.WriteLog("Exception from GetMLicFileAndVerify(). Message: " + GetAllException(x));
                    return x.Message;
                }
            }
        }
        protected bool CheckMacLic(string code, string machineSN)
        {
            try
            {
                int ret = MachineLicense(code, machineSN);
                if (ret == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception x)
            {
                CustomErrorBox(x.Message, EXCEPTION_CODE.ToString());
                LogService.WriteLog("Exception from CheckMacLic(). Message: " + GetAllException(x));
                return false;
            }
        }
        #endregion

    }
}
