using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class WorkHourPolicy_P
    {
        public int Code { get; set; }
        public string PolicyName { get; set; }
        public bool isActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}