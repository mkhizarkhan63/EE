using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;
using Common;

namespace EagleEye.DAL
{
    public class DALEmployeeType
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<EmployeeType_P> GetAllEmployeeType()
        {

            List<EmployeeType_P> list = new List<EmployeeType_P>();
            try
            {
                list = (from d in objModel.tbl_employeetype
                        select new EmployeeType_P
                        {
                            Code = d.Code,
                            Description = d.Description
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
            return list;

        }

        public EmployeeType_P GetEmployeeTypeByCode(int Code)
        {

            EmployeeType_P employeetype = new EmployeeType_P();
            try
            {
                employeetype = (from d in objModel.tbl_employeetype
                                where d.Code == Code
                                select new EmployeeType_P
                                {
                                    Code = d.Code,
                                    Description = d.Description,
                                }).FirstOrDefault();
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
            return employeetype;

        }

        public bool AddUpdateEmployeeType(EmployeeType_P employeetype)
        {
            bool flag = false;
            try
            {
                tbl_employeetype d = objModel.tbl_employeetype.Where(x => x.Code == employeetype.Code).FirstOrDefault();

                if (d == null)
                    d = new tbl_employeetype();

                d.Description = employeetype.Description;


                if (d.Code == 0)
                {
                    objModel.tbl_employeetype.Add(d);
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

        public bool DeleteEmployeeType(int Code)
        {
            bool flag = false;
            try
            {
                tbl_employeetype d = objModel.tbl_employeetype.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_employeetype.Remove(d);
                objModel.SaveChanges();
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

        public List<Att_Status_P.EmployeeType_P> GetAllEmployeeTypes()
        {

            List<Att_Status_P.EmployeeType_P> list = new List<Att_Status_P.EmployeeType_P>();
            try
            {
                list = (from d in objModel.tbl_employeetype
                        select new Att_Status_P.EmployeeType_P
                        {
                            Code = d.Code,
                            Description = d.Description
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
            return list;

        }

        public EmployeeType_P GetEmployeeTypeByName(string name)
        {

            EmployeeType_P employeetype = new EmployeeType_P();
            try
            {
                employeetype = (from d in objModel.tbl_employeetype
                                where d.Description == name
                               select new EmployeeType_P
                               {
                                   Code = d.Code,
                                   Description = d.Description,
                               }).FirstOrDefault();
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
            return employeetype;

        }
    }
}