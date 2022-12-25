using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.DTO
{
    public class AttendanceDTO
    {
        public int Code { get; set; }
        public DateTime? Attendance_DateTime { get; set; }
        public DateTime? Polling_DateTime { get; set; }
        public string Device_ID { get; set; }

        public string Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public string Status { get; set; }
        public string DoorStatus { get; set; }
        public string VerifyMode { get; set; }
    }
}