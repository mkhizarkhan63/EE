using AMS.Profile;
using Decdr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LncSlaMang.Controller
{
    public abstract class IsConfig
    {
        protected Assembly baseAssembly { get; set; }
        private LICDE decrdr = new LICDE();
        protected readonly string SysPath = string.Empty;
        //private string FILE_PATH = Path.Combine(Environment.SystemDirectory, "lsconfig.FILE");

        protected IsConfig()
        {
            SysPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
           
        }
       
        protected string GetInheritAppId()
        {
            GuidAttribute GuidAttr = (GuidAttribute)GuidAttribute.GetCustomAttribute(baseAssembly, typeof(GuidAttribute));
            return GuidAttr.Value;
        }

        private string FILE_PATH()
        {            
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "kmt2dll.file");
            return Path.Combine(SysPath, GetInheritAppId());
        }

        protected internal void WriteConfig(string content)
        {
            try
            {
                string PASS = "$L!mT0N-Lnc$l@M@ng$";
                string encryptContent = decrdr.EncryptData(content, PASS);
                DateTime getDate = DateTime.Now;
                //FileStream fs = new FileStream(Environment.CurrentDirectory + @"\Log_" + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream fs = new FileStream(FILE_PATH(), FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(encryptContent);
                sw.Flush();
                sw.Close();                
            }
            catch (Exception x)
            {
                if (x.Message.Contains("denied"))
                {
                    throw x;
                }
                else
                    LogService.WriteLog("Exception from WriteConfig(). Message: " + SlaLncMdle.GetAllException(x));
            }
        }

        protected internal ConfigType ReadConfig()
        {
            try
            {
                string PASS = "$L!mT0N-Lnc$l@M@ng$";
                if (!File.Exists(FILE_PATH()))
                {
                    return null;
                }
                var content = File.ReadAllText(FILE_PATH());
                if (String.IsNullOrEmpty(content))
                {
                    LogService.WriteLog("System Configuration file is corrupted.");
                    MessageBox.Show("System Configuration file is corrupted.");
                    return null;
                }
                                
                                
                string decryptContent = decrdr.DecryptData(content, PASS);
                ConfigType config = new ConfigType();
                string[] getConfig = decryptContent.Split(';');
                config.IDT = getConfig[0];
                config.Exp_DT = getConfig[1].ToLower() =="true"?true:false;
                config.LUpdteDT = getConfig[2];                
                return config;
            }
            catch (Exception x)
            {
                LogService.WriteLog("Exception from ReadConfig(). Message: "+ SlaLncMdle.GetAllException(x));
                return null;
            }
        }

     

        public class ConfigType
        {
            public string IDT { get; set; }
            public string LUpdteDT { get; set; }
            public bool Exp_DT { get; set; }            
        }
    }
}
