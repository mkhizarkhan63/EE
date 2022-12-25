using System;
using System.IO;

namespace LncSlaMang
{
    public class LogService
    {
        //private static string PATH = Application.StartupPath + @"\logs\";
        private static string PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)+ @"\logs\";
        private static object lockObj = new object();

        public static void WriteLog(string content)
        {
            try
            {
                PATH = PATH.Replace("file:\\", "");
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }
                DateTime getDate = DateTime.Now;
                //FileStream fs = new FileStream(Environment.CurrentDirectory + @"\Log_" + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);


                FileStream fs = new FileStream(PATH + @"Lnc-config.log." + getDate.Month + "_" + getDate.Year + ".txt", FileMode.OpenOrCreate);
                {
                    
                    lock (lockObj)
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.BaseStream.Seek(0, SeekOrigin.End);
                            sw.WriteLine(DateTime.Now + ": " + content);
                            sw.Flush();
                            sw.Close();
                            fs.Close();
                        }
                    }
                }
            }
            catch (Exception x)
            { }
        }

        
    }
}
