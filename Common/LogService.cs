using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LogService
    {
        private static string PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\logs\";
        private static string listPath = String.Empty;
        /// <summary>
        public static void WriteQuery(string content)
        {
            try
            {
                PATH = PATH.Replace(@"file:\", "");
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                DateTime getDate = DateTime.Now;
                //FileStream fs = new FileStream(Application.StartupPath + @"\Query_" + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream fs = new FileStream(PATH + @"DMU.Query." + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now + ": " + content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }

        public static void WriteLog(string content)
        {
            try
            {
                PATH = PATH.Replace(@"file:\", "");
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                DateTime getDate = DateTime.Now;
                //FileStream fs = new FileStream(Application.StartupPath + @"\Log_" + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream fs = new FileStream(PATH + @"EagleEye.Log." + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now + ": " + content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }

        public static void WriteServiceLog(string content)
        {
            try
            {
                PATH = PATH.Replace(@"file:\", "");
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                DateTime getDate = DateTime.Now;
                //FileStream fs = new FileStream(Application.StartupPath + @"\Log_" + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream fs = new FileStream(PATH + @"EagleEyeService.Log." + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now + ": " + content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception) { }
        }
    }
}
