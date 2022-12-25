using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EventLogs
    {
        public int Code { get; set; }
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string DateTime { get; set; }
        public string WorkCode { get; set; }
        public string Authority { get; set; }
        public string CheckType { get; set; }
        public string Status { get; set; }
        public string DoorStatus { get; set; }
        public string Photo { get; set; }
        public string Polling_DateTime { get; set; }

        public string VerifyMode { get; set; }
        public bool NotDuplicate { get; set; }
        public string ExtraStatus { get; set; }

    }
}
