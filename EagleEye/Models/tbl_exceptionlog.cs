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
    
    public partial class tbl_exceptionlog
    {
        public int Code { get; set; }
        public string Exception_Layer { get; set; }
        public string Method { get; set; }
        public string Stacktrace { get; set; }
        public string Error_Message { get; set; }
        public string Form { get; set; }
        public Nullable<System.DateTime> Exception_DateTime { get; set; }
    }
}
