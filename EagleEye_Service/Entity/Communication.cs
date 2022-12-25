using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.Entity
{
    public class Communication
    {
        public string Server_IP { get; set; }
        public string SignalR_Port { get; set; }
        public int Server_Port { get; set; }
    }
}
