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
    
    public partial class tbl_menu
    {
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public string Menu_Controller { get; set; }
        public string Menu_Action { get; set; }
        public string Parent { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
