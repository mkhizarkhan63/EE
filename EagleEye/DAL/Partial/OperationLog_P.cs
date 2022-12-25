using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class OperationLog_P
    {
        public int Code { get; set; }
        public string Trans_ID { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Nullable<int> Device_Status { get; set; }
        public string DateTime { get; set; }
        public string UserName { get; set; }

        // public DateTime orderByDateTime { get; set; }
        public string Empty { get; set; }
        public List<Device_P> ListDevices { get; set; }
    }
}