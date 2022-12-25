
using Common;
using EagleEye_Service.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALCommunication : DataAccess
    {
        public Communication GetCommunication()
        {
            Communication c = new Communication();
            try
            {
                query = @"Select * from tbl_communication";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    c.Server_IP = HelpingMethod.RemoveZeros(dt.Rows[i]["Server_IP"].ToString());
                    c.SignalR_Port = dt.Rows[i]["SignalR_Port"].ToString();
                    c.Server_Port = Formatter.SetValidValueToInt(dt.Rows[i]["Server_Port"]);

                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }

            return c;

        }

    }
}
