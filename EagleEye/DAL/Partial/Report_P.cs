using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Report_P
    {
        public List<Attendance_P> attendencelist { get; set; }
        public Attendance_P attendance { get; set; }
        public int att_count { get; set; }
        public string TimeInDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string TimeOutDate { get; set; }
        public string dt_workedhours { get; set; }
        public string curr_datetime { get; set; }
        //public string selected_datetime { get; set; }
        public string Employee_ID { get; set; }
        public string Employee_Photo { get; set; }
        public string Employee_Name { get; set; }
        public List<Att_Status_P> Att_Status_List { get; set; }
        public List<Employee_P> Employee_List { get; set; }


        public Nullable<int> fkDepartment_Code { get; set; }
        public Nullable<int> fkLocation_Code { get; set; }
        public Nullable<int> fkDesignation_Code { get; set; }
        public Nullable<int> fkEmployeeType_Code { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string EmployeeType { get; set; }
        public string Designation { get; set; }
        public string Device { get; set; }

        //*********For WorkHour************
        public int workHourPolicy__Code { get; set; }
        public string rpt_ActualWorkHour { get; set; }
        public string rpt_OverTime { get; set; }
        public string rpt_BreakHour { get; set; }

    }

    public class ReportDTO
    {

        public string ActualWorkHour { get; set; }
        public string OverTime { get; set; }
        public string BreakHour { get; set; }

    }
}