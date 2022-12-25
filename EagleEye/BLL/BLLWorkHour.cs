using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;
using EagleEye.DAL;
using EagleEye.DAL.Partial;
using EagleEye.Common;
using EagleEye.DAL.DTO;

namespace EagleEye.BLL
{
    public class BLLWorkHour
    {
        DALWorkHour objDAL = new DALWorkHour();


        public List<WorkHourPolicy_P> getAllWorkHour()
        {
            List<WorkHourPolicy_P> ls = new List<WorkHourPolicy_P>();
            try
            {
                ls = objDAL.getAllWorkHour();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return ls;
        }

        public List<ReportDTO> GetPolicyEmployeeByDate(Employee_P emp, string dt)
        {
            List<ReportDTO> ls = new List<ReportDTO>();
            try
            {
                ls = objDAL.GetPolicyEmployeeByDate(emp, dt);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return ls;
        }

        //This method is for updating policyDetail
        public bool removingRelationPolicy(int code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.removingPolicy(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool AddUpdateWorkHourPolicy(WorkHourPolicy_P workHour, out int code)
        {
            bool flag = false;
            code = 0;
            try
            {
                flag = objDAL.AddUpdateWorkHourPolicy(workHour, out code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool AddUpdatePolicyDetail(PolicyDetail_P policyDetail)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdatePolicyDetail(policyDetail);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }


        public List<PolicyDetail_P> getAllPolicyDetail(long Code)
        {
            List<PolicyDetail_P> ls = new List<PolicyDetail_P>();
            try
            {
                ls = objDAL.getAllPolicyDetail(Code);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return ls;
        }

        public List<WorkhourPolicyDTO> getAllWorkPolicies(JqueryDatatableParam param, out int totalRecords)
        {
            totalRecords = 0;
            List<WorkhourPolicyDTO> ls = new List<WorkhourPolicyDTO>();
            try
            {
                ls = objDAL.getAllWorkPolicies(param, out totalRecords);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return ls;
        }


        public WorkHourPolicy_P getWorkHourByCode(long code)
        {
            WorkHourPolicy_P obj = new WorkHourPolicy_P();
            try
            {
                obj = objDAL.getWorkHourByCode(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return obj;

        }

        public bool RemoveAllPolicyDetail(int code) {
            bool flag = false;
            try
            {
                flag = objDAL.RemoveAllPolicyDetail(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeletedPolicy(string code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeletedPolicy(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

    }
}