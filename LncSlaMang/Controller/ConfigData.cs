using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LncSlaMang.Controller
{
    public class ConfigData
    {
        public string Date { get; set; }
        public string APP_ID { get; set; }
        public string Code { get; set; }
        public string App_Name { get; set; }
        public string App_Ver { get; set; }
        public ClientSystemInfo SystemInformation { get; set; }


        public class ClientSystemInfo
        {
            public string OS { get; set; }
            public string Architecture { get; set; }
        }
      
    }
}