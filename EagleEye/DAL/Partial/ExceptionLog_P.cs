using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class ExceptionLog_P
    {
        public int Code { get; set; }
        public string Exception_Layer { get; set; }
        public string Stacktrace { get; set; }
        public string Error_Message { get; set; }
        public string Method { get; set; }
        public string Form { get; set; }
        public Nullable<System.DateTime> Exception_DateTime { get; set; }
    }
}