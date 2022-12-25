using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Device_P 
    {
        public Communication_P comm { get; set; }
        public int Code { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Device_Type { get; set; }
        public string Device_Info { get; set; }
        public string Device_Location { get; set; }
        public string  LocationDescription { get; set; }
        public string Face_Data_Ver { get; set; }
        public string Firmware { get; set; }
        public string Firmware_Filename { get; set; }
        public string Fk_Bin_Data_Lib { get; set; }
        public string Fp_Data_Ver { get; set; }
        public string Supported_Enroll_Data { get; set; }
        public string Device_Status_Info { get; set; }
        public string Max_Record { get; set; }
        public string Real_FaceReg { get; set; }
        public string Max_FaceReg { get; set; }
        public string Real_FPReg { get; set; }
        public string Max_FPReg { get; set; }
        public string Real_IDCardReg { get; set; }
        public string Max_IDCardReg { get; set; }
        public string Real_Manager { get; set; }
        public string Max_Manager { get; set; }
        public string Real_PasswordReg { get; set; }
        public string Max_PasswordReg { get; set; }
        public string Real_PvReg { get; set; }
        public string Max_PvReg { get; set; }
        public string Total_log_Count { get; set; }
        public string Total_log_Max { get; set; }
        public string Real_Employee { get; set; }
        public string Max_Employee { get; set; }
        public Nullable<System.DateTime> Device_LastStatusTime { get; set; }
        public string Alarm_Delay { get; set; }
        public string Allow_EarlyTime { get; set; }
        public string Allow_LateTime { get; set; }
        public string Anti_back { get; set; }
        public string DoorMagnetic_Delay { get; set; }
        public string DoorMagnetic_Type { get; set; }
        public string Glog_Warning { get; set; }
        public string OpenDoor_Delay { get; set; }
        public string Receive_Interval { get; set; }
        public string Reverify_Time { get; set; }
        public string Show_ResultTime { get; set; }
        public string Screensavers_Time { get; set; }
        public string Sleep_Time { get; set; }
        public string Use_Alarm { get; set; }
        public string Volume { get; set; }
        public string Wiegand_Input { get; set; }
        public string Wiegand_Output { get; set; }
        public string Wiegand_Type { get; set; }
        public Nullable<int> Device_Group { get; set; }
        public string Server_Address { get; set; }
        public string Server_Port { get; set; }
        public Nullable<System.DateTime> Sys_Time { get; set; }
        public Nullable<int> Device_Status { get; set; }
        public Nullable<int> Reader_ID { get; set; }
        public bool Active { get; set; }

        public Nullable<System.DateTime> Last_Polled_Record { get; set; }
        public string Multi_Users { get; set; }
        public int WebTimeout { get; set; }

        public int FingerCount { get; set; }
        public int FaceCount { get; set; }
        public int CardCount { get; set; }
        public int PalmCount { get; set; }
        public int PwdCount { get; set; }
        //public bool alarmFlag { get; set; }
        //public bool doorFlag { get; set; }
        //public bool antiFlag { get; set; }

        public bool? isSlave { get; set; }
        public string Device_IP { get; set; }
    }

   
}