using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALSetting : DataAccess
    {
        public string GetSettingPath()
        {
            string path = "";
            try
            {
                query = @"Select * from tbl_setting";
                DataTable res = ExecuteDataTable();
                if (res.Rows.Count > 0)
                    path = res.Rows[0]["AppSetting_Path"].ToString();
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return path;
        }
    }
}
