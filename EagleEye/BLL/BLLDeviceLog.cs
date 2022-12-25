using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL;
using EagleEye.DAL.Partial;

namespace EagleEye.BLL
{
    public class BLLDeviceLog
    {
        DALDeviceLog objDAL = new DALDeviceLog();

        public List<Device_OfflineLog_P> GetAllLogs()
        {
            List<Device_OfflineLog_P> list = new List<Device_OfflineLog_P>();
            try
            {
                list = objDAL.GetAllLogs();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool DeleteLog(string Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteLog(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool DeleteFKCmd(string Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteFKCmd(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        
    }
}