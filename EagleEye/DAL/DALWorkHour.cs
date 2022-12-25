using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;
using EagleEye.Models;
using EagleEye.DAL.Partial;
using EagleEye.Common;
using EagleEye.DAL.DTO;
using Common;

namespace EagleEye.DAL
{
    public class DALWorkHour
    {

        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<WorkHourPolicy_P> getAllWorkHour()
        {
            List<WorkHourPolicy_P> ls = new List<WorkHourPolicy_P>();
            try
            {
                IQueryable<WorkHourPolicy_P> query = (from wh in objModel.tbl_workHourPolicy
                                                      select new WorkHourPolicy_P
                                                      {
                                                          Code = wh.Code,
                                                          PolicyName = wh.PolicyName,
                                                          CreatedOn = wh.CreatedOn,
                                                          isActive = wh.isActive
                                                      });

                ls = query.ToList();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return ls;
        }

        public bool removingPolicy(int code)
        {
            bool flag = false;
            try
            {
                var ls = objModel.tbl_policyDetail.Where(x => x.PolicyCode == code).ToList();
                foreach (var item in ls)
                {
                    objModel.tbl_policyDetail.Remove(item);

                }
                int res = objModel.SaveChanges();
                if (res > 0)
                    flag = true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return flag;
        }

        public bool AddUpdatePolicyDetail(PolicyDetail_P obj)
        {
            bool flag = false;
            int res = 0;
            try
            {
                var query = objModel.tbl_policyDetail.Where(x => x.Day == obj.Day && x.PolicyCode == obj.PolicyCode).FirstOrDefault();
                tbl_policyDetail policy = new tbl_policyDetail
                {
                    Day = obj.Day == null ? "" : obj.Day,
                    Workhour = obj.Workhour == null ? "" : obj.Workhour,
                    Overtime = obj.Overtime == null ? "" : obj.Overtime,
                    Breakhour = obj.Breakhour == null ? "" : obj.Breakhour,
                    isOvertimeActive = obj.isOvertimeActive,
                    PolicyCode = obj.PolicyCode,
                    DayCheck = obj.DayCheck
                };


                if (query != null)
                {
                    //if (obj.DayCheck == false)
                    //    query.Day = "";
                    //else
                    query.DayCheck = obj.DayCheck;
                    query.Day = obj.Day;
                    query.Workhour = obj.Workhour == null ? "" : obj.Workhour;
                    query.Overtime = obj.Overtime == null ? "" : obj.Overtime;
                    query.Breakhour = obj.Breakhour == null ? "" : obj.Breakhour;
                    query.isOvertimeActive = obj.isOvertimeActive;
                    query.PolicyCode = obj.PolicyCode;

                    objModel.Entry(query).State = System.Data.Entity.EntityState.Modified;
                    res = objModel.SaveChanges();

                }
                else
                {
                    objModel.tbl_policyDetail.Add(policy);
                    res = objModel.SaveChanges();
                }


                if (res > 0)
                {
                    flag = true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return flag;

        }

        public bool AddUpdateWorkHourPolicy(WorkHourPolicy_P workHour, out int code)
        {
            code = 0;
            bool flag = false;
            try
            {
                var foundRow = objModel.tbl_workHourPolicy.Where(x => x.Code == workHour.Code).FirstOrDefault();
                //***************************************
                //For ADD
                if (foundRow == null)
                {
                    tbl_workHourPolicy obj = new tbl_workHourPolicy();
                    obj.PolicyName = workHour.PolicyName;
                    obj.CreatedOn = DateTime.Now;
                    obj.isActive = workHour.isActive;

                    objModel.tbl_workHourPolicy.Add(obj);

                    int res = objModel.SaveChanges();
                    if (res > 0)
                    {
                        code = obj.Code;
                        flag = true;
                    }
                }
                //***************************************
                //For UPDATE
                else
                {
                    foundRow.Code = workHour.Code;
                    foundRow.PolicyName = workHour.PolicyName;
                    foundRow.isActive = workHour.isActive;
                    foundRow.CreatedOn = DateTime.Now;

                    objModel.Entry(foundRow).State = System.Data.Entity.EntityState.Modified;
                    int res = objModel.SaveChanges();
                    if (res > 0)
                    {
                        flag = true;
                        code = workHour.Code;
                    }
                }


            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return flag;
        }

        public List<PolicyDetail_P> getAllPolicyDetail(long Code)
        {
            List<PolicyDetail_P> ls = new List<PolicyDetail_P>();
            try
            {
                IQueryable<PolicyDetail_P> query = (from pd in objModel.tbl_policyDetail
                                                    where pd.PolicyCode == Code
                                                    select new PolicyDetail_P
                                                    {
                                                        Code = pd.Code,
                                                        Day = pd.Day,
                                                        Workhour = pd.Workhour,
                                                        Overtime = pd.Overtime,
                                                        Breakhour = pd.Breakhour,
                                                        isOvertimeActive = pd.isOvertimeActive,
                                                        PolicyCode = pd.PolicyCode,
                                                        DayCheck = pd.DayCheck

                                                    });
                ls = query.ToList();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return ls;
        }

        public WorkHourPolicy_P getWorkHourByCode(long code)
        {

            WorkHourPolicy_P obj = new WorkHourPolicy_P();

            try
            {
                var query = objModel.tbl_workHourPolicy.Where(x => x.Code == code).FirstOrDefault();

                obj.Code = query.Code;
                obj.PolicyName = query.PolicyName;
                obj.isActive = query.isActive;
                obj.CreatedOn = query.CreatedOn;

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return obj;
        }
        public List<WorkhourPolicyDTO> getAllWorkPolicies(JqueryDatatableParam param, out int totalRecords)
        {
            List<WorkhourPolicyDTO> ls = new List<WorkhourPolicyDTO>();
            totalRecords = 0;
            try
            {
                IQueryable<WorkhourPolicyDTO> query = (from wrkh in objModel.tbl_workHourPolicy

                                                       select new WorkhourPolicyDTO
                                                       {
                                                           Empty = "",
                                                           workHour_PolicyCode = wrkh.Code,
                                                           workHour_PolicyName = wrkh.PolicyName,
                                                           workHour_PolicyIsActive = wrkh.isActive,
                                                           workHour_PolicyCreatedOn = wrkh.CreatedOn,
                                                           //policyDetailList = (from p in objModel.tbl_policyDetail
                                                           //                where p.PolicyCode == wrkh.Code
                                                           //                select new PolicyDetail_P
                                                           //                {
                                                           //                    Code = p.Code,
                                                           //                    Workhour = p.Workhour,
                                                           //                    Overtime = p.Overtime,
                                                           //                    Breakhour = p.Breakhour,
                                                           //                    Day =p.Day,
                                                           //                    isOvertimeActive = p.isOvertimeActive
                                                           //                }).ToList()
                                                       }).OrderBy(x => x.workHour_PolicyCode);

                totalRecords = query.Count();
                //skipping
                if (param.iDisplayLength != -1)
                {
                    if (totalRecords > param.iDisplayStart)
                    {
                        ls = query.Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();
                    }
                }



            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return ls;
        }


        public bool RemoveAllPolicyDetail(int code)
        {

            bool flag = false;
            try
            {
                var wh = objModel.tbl_policyDetail.Where(x => x.PolicyCode == code).ToList();

                foreach (var item in wh)
                {
                    objModel.tbl_policyDetail.Remove(item);
                }
                int res = objModel.SaveChanges();
                if (res > 0)
                {
                    flag = true;
                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeletedPolicy(string code)
        {
            bool flag = false;
            tbl_workHourPolicy wh = new tbl_workHourPolicy();
            List<tbl_policyDetail> policies = new List<tbl_policyDetail>();
            try
            {
                var parsedCode = long.Parse(code);
                //getting all workCode
                wh = objModel.tbl_workHourPolicy.Where(x => x.Code == parsedCode).FirstOrDefault();
                if (wh != null)
                {
                    objModel.tbl_workHourPolicy.Remove(wh);
                    //getting all policy
                    policies = objModel.tbl_policyDetail.Where(x => x.PolicyCode == parsedCode).ToList();
                    foreach (var item in policies)
                    {
                        objModel.tbl_policyDetail.Remove(item);
                    }
                }
                int res = objModel.SaveChanges();
                if (res > 0)
                {
                    flag = true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return flag;
        }

        public List<ReportDTO> GetPolicyEmployeeByDate(Employee_P emp, string dt)
        {
            List<ReportDTO> ls = new List<ReportDTO>();
            DateTime TodaysDate = Formatter.SetValidValueToDateTime(dt);
            try
            {
                string DayWithName = TodaysDate.DayOfWeek.ToString();
                int value = (int)Enum.Parse(typeof(Enumeration.workHourPolicyEnum), DayWithName);
                ls = (from e in objModel.tbl_employee
                      join pd in objModel.tbl_policyDetail on e.WorkHourPolicyCode equals pd.PolicyCode
                      into policy
                      from pd in policy.DefaultIfEmpty()
                      where e.WorkHourPolicyCode == emp.WorkHourPolicyCode && pd.Day == value.ToString()
                      select new ReportDTO
                      {
                          ActualWorkHour = pd.Workhour,
                          OverTime = pd.Overtime,
                          BreakHour = pd.Breakhour

                      }).ToList();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return ls;
        }

    }
}