using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.DTO
{
    public class EmployeeDTO
    {
        public int Code { get; set; }
        public string Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_Photo { get; set; }
        public string Card_No { get; set; }
        public Nullable<bool> FingerPrint { get; set; }
        public Nullable<bool> Face { get; set; }
        public Nullable<bool> Palm { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }

        public Nullable<int> fkLocation_Code { get; set; }
        public Nullable<int> Active { get; set; }
        public string Empty { get; set; }
        
    }
}