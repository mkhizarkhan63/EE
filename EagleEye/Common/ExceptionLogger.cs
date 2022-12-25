using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;

namespace EagleEye.Common
{
    public class ExceptionLogger
    {

        public static void LogException(Exception ex, ExceptionLayer el,string Method)
        {
            DALExceptionLog objDAL = new DALExceptionLog();

            try
            {
                
                ExceptionLog_P log = new ExceptionLog_P
                {
                    Exception_Layer = el.ToString(),
                    Stacktrace = ex.StackTrace,
                    Error_Message = ex.Message
                };
                if (ex.InnerException != null)
                {
                    log.Error_Message += " InnerException=" + ex.InnerException.Message;
                }
                log.Method = Method;
                log.Form = ex.TargetSite.DeclaringType.Name;
                log.Exception_DateTime = DateTime.Now;

                objDAL.SaveException(log);
            }
            catch (Exception)
            {

            }
        }

        public static void LogValidationException(string msg, ExceptionLayer el, string Method)
        {
            DALExceptionLog objDAL = new DALExceptionLog();

            try
            {

                ExceptionLog_P log = new ExceptionLog_P
                {
                    Exception_Layer = el.ToString(),
                    Error_Message = msg,
                    Method = Method,
                    Exception_DateTime = DateTime.Now
                };

                objDAL.SaveException(log);
            }
            catch (Exception)
            {

            }
        }
    }
}