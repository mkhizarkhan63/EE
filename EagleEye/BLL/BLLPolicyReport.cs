using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL;
using EagleEye.DAL.Partial;
using EagleEye.Common;

namespace EagleEye.BLL
{

    public class BLLPolicyReport
    {
        DALPolicyReport obj = new DALPolicyReport();

        public List<PolicyReport> GetAllPolicyReport(JqueryDatatableParam param, string emp, string st, string et, out int totalRecords)
        {
            totalRecords = 0;
            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                list = obj.GetAllPolicyReport(param, emp, st, et, out totalRecords);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<PolicyReport> GetPolicyReportByUserID(int[] lstUserCode)
        {

            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                list = obj.GetPolicyReportByUserID(lstUserCode);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<PolicyReport> GetPolicyReportByDateRange(string emp, string startDt, string endDt)
        {
            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                list = obj.GetPolicyReportByDateRange(emp, startDt, endDt);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

    }
}