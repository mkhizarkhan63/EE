using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class TempTable_P
    {
        public int Code { get; set; }
        public string Device_ID { get; set; }
        public string Employee_ID { get; set; }
        public string Cmd { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    }
}