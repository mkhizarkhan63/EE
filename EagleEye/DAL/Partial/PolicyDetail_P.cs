using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class PolicyDetail_P
    {
        public int Code { get; set; }
        public string Day { get; set; }
        public string Workhour { get; set; }
        public string Overtime { get; set; }
        public string Breakhour { get; set; }
        public bool? isOvertimeActive { get; set; }
        public int? PolicyCode { get; set; }
        public bool? DayCheck { get; set; }


    }

   

}