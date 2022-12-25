using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service
{
    public class clsWriterLog
    {
        #region Variables
        private static string profilePath = null;
        private static string flag = "";
        private static object myLock = 0;

        static string logDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        static string errDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLogs";

        #endregion Variables


        public static void WriteDevLog(string EventName, string Message)
        {
            string DevFilePath = logDirectoryPath + "\\DevLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";

            try
            {

                lock (myLock)
                {
                    if (!Directory.Exists(logDirectoryPath))
                    {
                        Directory.CreateDirectory(logDirectoryPath);
                    }

                    bool fileCreated = (!File.Exists(DevFilePath));

                    using (StreamWriter sw = new StreamWriter(DevFilePath, true))
                    {
                        string r = "";

                        if (fileCreated)
                        {
                            r = "Event Time,Event Name,Description";
                            sw.WriteLine(r);
                        }

                        r = "{0},{1},{2}";
                        r = string.Format(r, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), EventName, Message);
                        sw.WriteLine(r);
                        sw.Close();
                    }

                }
            }
            catch (Exception ex)
            { WriteError("CommonProject", "clsWriter", MethodBase.GetCurrentMethod().Name, ex.Message); }
        }

        public static void WriteAppLog(string EventName, string Message)
        {
            string logFilePath = logDirectoryPath + "\\AppLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";

            try
            {

                lock (myLock)
                {
                    if (!Directory.Exists(logDirectoryPath))
                    {
                        Directory.CreateDirectory(logDirectoryPath);
                    }

                    bool fileCreated = (!File.Exists(logFilePath));

                    using (StreamWriter sw = new StreamWriter(logFilePath, true))
                    {
                        string r = "";

                        if (fileCreated)
                        {
                            r = "Event Time,Event Name,Description";
                            sw.WriteLine(r);
                        }

                        r = "{0},{1},{2}";
                        r = string.Format(r, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), EventName, Message);
                        sw.WriteLine(r);
                        sw.Close();
                    }

                }
            }
            catch (Exception ex)
            { WriteError("CommonProject", "clsWriter", MethodBase.GetCurrentMethod().Name, ex.Message); }
        }


        public static void WriteProcessLog(string EventName, string Message)
        {
            string ProcFilePath = logDirectoryPath + "\\ProcLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";

            try
            {

                lock (myLock)
                {
                    if (!Directory.Exists(logDirectoryPath))
                    {
                        Directory.CreateDirectory(logDirectoryPath);
                    }

                    bool fileCreated = (!File.Exists(ProcFilePath));

                    using (StreamWriter sw = new StreamWriter(ProcFilePath, true))
                    {
                        string r = "";

                        if (fileCreated)
                        {
                            r = "Event Time,Event Name,Description";
                            sw.WriteLine(r);
                        }

                        r = "{0},{1},{2}";
                        r = string.Format(r, DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"), EventName, Message);
                        sw.WriteLine(r);
                        sw.Close();
                    }

                }
            }
            catch (Exception ex)
            { WriteError("CommonProject", "clsWriter", MethodBase.GetCurrentMethod().Name, ex.Message); }
        }


        public static void WriteError(string Namespace, string ClassName, string Method, string Error)
        {
            string errFilePath = errDirectoryPath + "\\ErrorLogs_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".csv";

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


    }
}
