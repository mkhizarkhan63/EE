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
    public class DALDepartment
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<Department_P> GetAllDepartment()
        {

            List<Department_P> list = new List<Department_P>();
            try
            {
                list = (from d in objModel.tbl_department
                        select new Department_P
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

        public List<Att_Status_P.Department_P> GetAllDepartments()
        {

            List<Att_Status_P.Department_P> list = new List<Att_Status_P.Department_P>();
            try
            {
                list = (from d in objModel.tbl_department
                        select new Att_Status_P.Department_P
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

        public Department_P GetDepartmentByName(string name)
        {

            Department_P department = new Department_P();
            try
            {
                department = (from d in objModel.tbl_department
                              where d.Description == name
                              select new Department_P
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
            return department;

        }

        public Department_P GetDepartmentByCode(int Code)
        {

            Department_P department = new Department_P();
            try
            {
                department = (from d in objModel.tbl_department
                              where d.Code == Code
                              select new Department_P
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
            return department;

        }

        public bool AddUpdateDepartment(Department_P department)
        {
            bool flag = false;
            try
            {
                tbl_department d = objModel.tbl_department.Where(x => x.Code == department.Code).FirstOrDefault();

                if (d == null)
                    d = new tbl_department();

                d.Description = department.Description;

                if (d.Code == 0)
                {
                    objModel.tbl_department.Add(d);
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

        public bool DeleteDepartment(int Code)
        {
            bool flag = false;
            try
            {
                tbl_department d = objModel.tbl_department.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_department.Remove(d);
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
    }
}