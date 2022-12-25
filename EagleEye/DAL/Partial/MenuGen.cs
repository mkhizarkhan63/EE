using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class MenuGen
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Controller { get; set; }
        public string Menu_Action { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> Insert { get; set; }
        public Nullable<bool> Update { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> View { get; set; }
    }
}