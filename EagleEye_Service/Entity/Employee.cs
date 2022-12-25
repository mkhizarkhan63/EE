﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.Entity
{
    public class Employee
    {
        public int Code { get; set; }
        public string Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_Photo { get; set; }
        public string Card_No { get; set; }
        public string User_Privilege { get; set; }
        public string Gender { get; set; }
        public Nullable<bool> FingerPrint { get; set; }
        public Nullable<bool> Face { get; set; }
        public Nullable<bool> Palm { get; set; }
        public string Password { get; set; }
        public string Device_Id { get; set; }
        public Nullable<int> fkDepartment_Code { get; set; }
        public Nullable<int> fkLocation_Code { get; set; }
        public Nullable<int> fkDesignation_Code { get; set; }
        public Nullable<int> fkEmployeeType_Code { get; set; }
       // public Nullable<int> fkTimeZone_Code { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> Active { get; set; }
        public string Telephone { get; set; }
        public string Trans_Id { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public byte[] Cmd_Param { get; set; }
        public byte[] finger_0 { get; set; }
        public byte[] finger_1 { get; set; }
        public byte[] finger_2 { get; set; }
        public byte[] finger_3 { get; set; }
        public byte[] finger_4 { get; set; }
        public byte[] finger_5 { get; set; }
        public byte[] finger_6 { get; set; }
        public byte[] finger_7 { get; set; }
        public byte[] finger_8 { get; set; }
        public byte[] finger_9 { get; set; }
        public byte[] face_data { get; set; }
        public byte[] palm_0 { get; set; }
        public byte[] palm_1 { get; set; }
        public byte[] photo_data { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string Valid_DateStart { get; set; }
        public string Valid_DateEnd { get; set; }
        public string Sunday { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        //public int Code { get; set; }
        //public string Employee_ID { get; set; }
        //public string Employee_Name { get; set; }
        //public string Employee_Photo { get; set; }
        //public string Card_No { get; set; }
        //public string User_Privilege { get; set; }
        //public string Gender { get; set; }
        //public Nullable<bool> FingerPrint { get; set; }
        //public Nullable<bool> Face { get; set; }
        //public Nullable<bool> Palm { get; set; }
        //public string Password { get; set; }
        //public string Device_Id { get; set; }
        //public Nullable<int> fkDepartment_Code { get; set; }
        //public Nullable<int> fkLocation_Code { get; set; }
        //public Nullable<int> fkDesignation_Code { get; set; }
        //public Nullable<int> fkEmployeeType_Code { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public Nullable<int> Active { get; set; }
        //public string Telephone { get; set; }
        //public string Trans_Id { get; set; }
        //public Nullable<System.DateTime> Update_Date { get; set; }
        //public byte[] Cmd_Param { get; set; }
        //public byte[] finger_0 { get; set; }
        //public byte[] finger_1 { get; set; }
        //public byte[] finger_2 { get; set; }
        //public byte[] finger_3 { get; set; }
        //public byte[] finger_4 { get; set; }
        //public byte[] finger_5 { get; set; }
        //public byte[] finger_6 { get; set; }
        //public byte[] finger_7 { get; set; }
        //public byte[] finger_8 { get; set; }
        //public byte[] finger_9 { get; set; }
        //public byte[] face_data { get; set; }
        //public byte[] palm_0 { get; set; }
        //public byte[] palm_1 { get; set; }
        //public byte[] photo_data { get; set; }
        //public Nullable<bool> IsDelete { get; set; }


        //public Nullable<int> Backup_Number { get; set; }
        //public string Device_Name { get; set; }
    }
}
