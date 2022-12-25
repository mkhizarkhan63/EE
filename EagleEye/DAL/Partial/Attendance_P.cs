using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Attendance_P
    {
        public int Code { get; set; }
        public Nullable<System.DateTime> Attendance_DateTime { get; set; }
        public string Attendance_Photo { get; set; }
        public Nullable<System.DateTime> Polling_DateTime { get; set; }
        public string Device_ID { get; set; }
        public string Device { get; set; }
        public string Employee_ID { get; set; }
        public string Employee { get; set; }
        public string Status { get; set; }
        public string Status_Description { get; set; }
        public string WorkCode { get; set; }
        public string WorkCode_Description { get; set; }
        public string DoorStatus { get; set; }
        public string Verify_Mode { get; set; }
        public string ExtraStatus { get; set; }
        public Nullable<bool> Status_TIS { get; set; }
        public Nullable<bool> Status_Oracle { get; set; }
        public Nullable<bool> Status_SQL { get; set; }
        public string Employee_Name { get; set; }
        public string DateTime { get; set; }
        public List<Device_P> DeviceList { get; set; }
        public List<Employee_P> EmployeeList { get; set; }

        public List<Att_Status_P> Att_Status_List { get; set; }
        public int EmployeeID { get; set; }
        public bool? CheckIsSlave { get; set; }
    }
}