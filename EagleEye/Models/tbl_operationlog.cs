//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EagleEye.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_operationlog
    {
        public int Code { get; set; }
        public string Trans_ID { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UserName { get; set; }
        public Nullable<int> Device_Status { get; set; }
    }
}
