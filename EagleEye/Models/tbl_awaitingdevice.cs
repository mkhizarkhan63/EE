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
    
    public partial class tbl_awaitingdevice
    {
        public int Code { get; set; }
        public string Device_ID { get; set; }
        public string Device_Name { get; set; }
        public string Device_Info { get; set; }
        public string Device_Status_Info { get; set; }
        public string Device_Type { get; set; }
        public Nullable<bool> IsConnected { get; set; }
    }
}
