using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class Account_P
    {
        public int Code { get; set; }
        public string UserName { get; set; }
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string ReturnURL { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}