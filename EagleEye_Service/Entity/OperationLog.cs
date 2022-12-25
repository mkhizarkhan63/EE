using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.Entity
{
   public class OperationLog
    {
        public int Code { get; set; }
        public string Trans_ID { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UpdateTime { get; set; }
        public string Device_Status { get; set; }
        public string DateTime { get; set; }
        public string UserName { get; set; }
    }
}
