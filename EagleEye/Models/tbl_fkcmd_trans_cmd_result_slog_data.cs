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
    
    public partial class tbl_fkcmd_trans_cmd_result_slog_data
    {
        public string trans_id { get; set; }
        public string device_id { get; set; }
        public string kind { get; set; }
        public string tobackup_number { get; set; }
        public string user_id { get; set; }
        public string touser_id { get; set; }
        public Nullable<System.DateTime> operation_time { get; set; }
    }
}
