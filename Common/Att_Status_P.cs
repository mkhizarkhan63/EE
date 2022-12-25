using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class Att_Status_P
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public class Location_P
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }
        public class Department_P
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }
        public class Designation_P
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }
        public class EmployeeType_P
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }
        public class WorkCode_P
        {
            public int Code { get; set; }
            public string Name { get; set; }
        }
        public class TimeZone_P
        {
            public int Code { get; set; }
            public string Timezone_No { get; set; }
            public string Timezone_Name { get; set; }
            public string Period_1_Start { get; set; }
            public string Period_1_End { get; set; }
            public string Period_2_Start { get; set; }
            public string Period_2_End { get; set; }
            public string Period_3_Start { get; set; }
            public string Period_3_End { get; set; }
            public string Period_4_Start { get; set; }
            public string Period_4_End { get; set; }
            public string Period_5_Start { get; set; }
            public string Period_5_End { get; set; }
            public string Period_6_Start { get; set; }
            public string Period_6_End { get; set; }
            public Nullable<bool> Status { get; set; }
        }
    }
    
    

    
}