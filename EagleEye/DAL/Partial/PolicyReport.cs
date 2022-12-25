using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class PolicyReport
    {
        public int Code { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }

        public DateTime Dt { get; set; }
        public string Workhour { get; set; }
        public string Overtime { get; set; }
        public string Breakhour { get; set; }
        public string Extrahour { get; set; }
        public string Policycode { get; set; }
        public string Policyname { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Empty { get; set; }

       

    }
}