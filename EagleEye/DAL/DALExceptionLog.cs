using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;

namespace EagleEye.DAL
{
    public class DALExceptionLog
    {

        EagleEyeEntities objModel = new EagleEyeEntities();

        public void SaveException(ExceptionLog_P ex)
        {

            try
            {
                tbl_exceptionlog log = new tbl_exceptionlog
                {
                    Exception_Layer = ex.Exception_Layer,
                    Stacktrace = ex.Stacktrace,
                    Error_Message = ex.Error_Message,
                    Method = ex.Method,
                    Form = ex.Form,
                    Exception_DateTime = ex.Exception_DateTime
                };

                objModel.tbl_exceptionlog.Add(log);
                objModel.SaveChanges();

            }
            catch (Exception)
            {

            }


        }

    }
}