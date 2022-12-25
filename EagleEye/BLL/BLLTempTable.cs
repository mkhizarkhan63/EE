using System;
using System.Collections.Generic;
using System.Linq;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web;
using EagleEye.DAL;

namespace EagleEye.BLL
{
    public class BLLTempTable
    {
        DALTempTable objDAL = new DALTempTable();
        public bool InsertCommand(string deviceID, string cmd,string empID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.InsertCommand(deviceID, cmd, empID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

    }
}