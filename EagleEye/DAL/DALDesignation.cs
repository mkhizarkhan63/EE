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
    public class DALDesignation
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<Designation_P> GetAllDesignation()
        {

            List<Designation_P> list = new List<Designation_P>();
            try
            {
                list = (from d in objModel.tbl_designation
                        select new Designation_P
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

        public Designation_P GetDesignationByCode(int Code)
        {

            Designation_P designation = new Designation_P();
            try
            {
                designation = (from d in objModel.tbl_designation
                               where d.Code == Code
                              select new Designation_P
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
            return designation;

        }

        public bool AddUpdateDesignation(Designation_P designation)
        {
            bool flag = false;
            try
            {
                tbl_designation d = objModel.tbl_designation.Where(x => x.Code == designation.Code).FirstOrDefault();

                if (d == null)
                    d = new tbl_designation();

                d.Description = designation.Description;


                if (d.Code == 0)
                {
                    objModel.tbl_designation.Add(d);
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

        public bool DeleteDesignation(int Code)
        {
            bool flag = false;
            try
            {
                tbl_designation d = objModel.tbl_designation.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_designation.Remove(d);
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

        public List<Att_Status_P.Designation_P> GetAllDesignations()
        {

            List<Att_Status_P.Designation_P> list = new List<Att_Status_P.Designation_P>();
            try
            {
                list = (from d in objModel.tbl_designation
                        select new Att_Status_P.Designation_P
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

        public Designation_P GetDesignationByName(string name)
        {

            Designation_P designation = new Designation_P();
            try
            {
                designation = (from d in objModel.tbl_designation
                              where d.Description == name
                              select new Designation_P
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
            return designation;

        }
    }
}