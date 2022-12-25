using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class AwaitingDevice_P
    {
        public int Code { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Device_Info { get; set; }
        public string Device_Status_Info { get; set; }
        public string Device_Type { get; set; }
        public Nullable<bool> IsConnected { get; set; }
    }
}