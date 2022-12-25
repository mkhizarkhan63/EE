using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Common;
using EagleEye.DAL.Partial;
using EagleEye.Models;
using System.Data.Entity.Validation;
using Common;

namespace EagleEye.DAL
{
    public class DALPolicyReport
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<PolicyReport> GetAllPolicyReport(JqueryDatatableParam param, string empID, string st, string et, out int totalRecords)
        {
            totalRecords = 0;
            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                var StartDate = Formatter.SetValidValueToDateTime(st);
                var EndDate = Formatter.SetValidValueToDateTime(et);

                IQueryable<PolicyReport> query = (from emp in objModel.tbl_employee
                                                  join pr in objModel.tbl_policyWithEmployee on emp.Employee_ID equals pr.Emp_id
                                                  join wh in objModel.tbl_workHourPolicy on pr.Policycode equals wh.Code.ToString()
                                                  where emp.IsDelete != true && pr.Dt >= StartDate && pr.Dt <= EndDate
                                                  select new PolicyReport
                                                  {

                                                      Code = pr.Code,
                                                      EmpID = emp.Employee_ID,
                                                      EmpName = emp.Employee_Name,
                                                      Dt = pr.Dt.Value,
                                                      Workhour = pr.Workhour,
                                                      Overtime = pr.Overtime,
                                                      Breakhour = pr.Breakhour,
                                                      Extrahour = pr.Extrahour,
                                                      //  Policycode = pr.Policycode,
                                                      Policyname = wh.PolicyName,
                                                      CreatedOn = pr.CreatedOn,
                                                      Empty = ""

                                                  }).Distinct().OrderByDescending(x => x.Dt);

                //if (!string.IsNullOrEmpty(dt))
                //{
                //    var filterDateTime = Formatter.SetValidValueToDateTime(dt);
                //    query = query.Where(x => x.Dt == filterDateTime);
                //}

                if (!string.IsNullOrEmpty(empID) && empID != "-1")
                {
                    string[] empArr = empID.Split(',');

                    query = query.Where(x => empArr.Contains(x.EmpID));
                }


                totalRecords = query.Count();
                ///skipping
                if (param.iDisplayLength != -1)
                {
                    if (totalRecords > param.iDisplayStart)
                        list = query.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength).ToList();
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
            return list;

        }


        public List<PolicyReport> GetPolicyReportByUserID(int[] lstUserCode)
        {
            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                list = (from emp in objModel.tbl_employee
                        join pr in objModel.tbl_policyWithEmployee on emp.Employee_ID equals pr.Emp_id
                        join wh in objModel.tbl_workHourPolicy on pr.Policycode equals wh.Code.ToString()
                        where emp.IsDelete != true

                        select new PolicyReport
                        {

                            Code = pr.Code,
                            EmpID = emp.Employee_ID,
                            EmpName = emp.Employee_Name,
                            Dt = pr.Dt.Value,
                            Workhour = pr.Workhour,
                            Overtime = pr.Overtime,
                            Breakhour = pr.Breakhour,
                            Extrahour = pr.Extrahour,
                            //  Policycode = pr.Policycode,
                            Policyname = wh.PolicyName,
                            CreatedOn = pr.CreatedOn,
                            Empty = ""

                        }).Distinct().OrderBy(x => x.EmpID).Where(x => lstUserCode.Contains(x.Code)).ToList();


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
            return list;
        }

        public List<PolicyReport> GetPolicyReportByDateRange(string empIds, string startDt, string endDt)
        {
            List<PolicyReport> list = new List<PolicyReport>();
            try
            {
                var StartDate = Formatter.SetValidValueToDateTime(startDt);
                var EndDate = Formatter.SetValidValueToDateTime(endDt);
                string[] emp_ID = empIds.Split(',');


                list = (from emp in objModel.tbl_employee
                        join pr in objModel.tbl_policyWithEmployee on emp.Employee_ID equals pr.Emp_id
                        join wh in objModel.tbl_workHourPolicy on pr.Policycode equals wh.Code.ToString()
                        where emp.IsDelete != true && pr.Dt >= StartDate && pr.Dt <= EndDate

                        select new PolicyReport
                        {

                            Code = pr.Code,
                            EmpID = emp.Employee_ID,
                            EmpName = emp.Employee_Name,
                            Dt = pr.Dt.Value,
                            Workhour = pr.Workhour,
                            Overtime = pr.Overtime,
                            Breakhour = pr.Breakhour,
                            Extrahour = pr.Extrahour,
                            //  Policycode = pr.Policycode,
                            Policyname = wh.PolicyName,
                            CreatedOn = pr.CreatedOn,
                            Empty = ""

                        }).Distinct().OrderBy(x => x.EmpID).ToList();

                if (!string.IsNullOrEmpty(empIds))
                {
                    list = list.Where(x => emp_ID.Contains(x.EmpID)).ToList();
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
            return list;
        }

    }

}