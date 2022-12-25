using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.DTO
{
    public class WorkhourPolicyDTO
    {

        //public string Day { get; set; }
        //public string Workhour { get; set; }
        //public string Overtime { get; set; }
        //public string Breakhour { get; set; }
        //public bool isOvertimeActive { get; set; }

        public string Empty { get; set; }
        public int workHour_PolicyCode { get; set; }
        public string workHour_PolicyName { get; set; }
        public bool workHour_PolicyIsActive { get; set; }
        public System.DateTime workHour_PolicyCreatedOn { get; set; }

        //public List<PolicyDetail_P> policyDetailList { get; set; }
    }
}