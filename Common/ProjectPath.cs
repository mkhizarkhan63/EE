using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class ProjectPath
    {
        public static string FILE_PATH = HttpContext.Current.Server.MapPath(@"\AppSettings.xml");
    }
}
