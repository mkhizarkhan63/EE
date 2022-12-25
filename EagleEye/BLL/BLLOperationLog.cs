using EagleEye.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.Partial;
using EagleEye.Common;

namespace EagleEye.BLL
{
    public class BLLOperationLog
    {
        DALOperationLog objDAL = new DALOperationLog();

        public List<OperationLog_P> GetAllLogs(JqueryDatatableParam param, string dt1, string dt2, string status, string devices, out int totalRecords)
        {
            totalRecords = 0;
            List<OperationLog_P> list = new List<OperationLog_P>();
            try
            {

                list = objDAL.GetAllLogs(param, dt1, dt2, status, devices, out totalRecords).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool AddUpdateOperationLog(OperationLog_P olog)
        {

            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateOperationLog(olog);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }
        public bool DeleteLogs(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteLogs(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

    }

}