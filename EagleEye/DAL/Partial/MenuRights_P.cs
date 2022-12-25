using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.DAL.Partial
{
    public class MenuRights_P
    {
        public int Menu_Rights_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<int> Menu_Id { get; set; }
        public Nullable<bool> Insert { get; set; }
        public Nullable<bool> Update { get; set; }
        public Nullable<bool> Delete { get; set; }
        public Nullable<bool> View { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}