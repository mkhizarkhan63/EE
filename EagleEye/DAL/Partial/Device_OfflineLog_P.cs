using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Device_OfflineLog_P
    {
        public string trans_id { get; set; }
        public string device_id { get; set; }
        public string user_id { get; set; }
        public string timezone_no { get; set; }
        public string cmd_code { get; set; }
        public string return_code { get; set; }
        public string status { get; set; }
        public string update_time { get; set; }
        public string timezone_name { get; set; }
        public string Device_Name { get; set; }

    }
}