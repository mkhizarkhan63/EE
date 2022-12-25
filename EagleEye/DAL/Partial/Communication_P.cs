using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Communication_P
    {
        public int Code { get; set; }
        public string Server_IP { get; set; }
        public string SignalR_Port { get; set; }

        public string Server_Port { get; set; }
    }
}