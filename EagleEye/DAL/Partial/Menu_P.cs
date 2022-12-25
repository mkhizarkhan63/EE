using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Menu_P
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Controller { get; set; }
        public string Menu_Action { get; set; }
        public string Parent { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string Icon { get; set; }
    }
}