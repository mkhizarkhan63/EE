using System;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EagleEye.Models;
using EagleEye.DAL.Partial;
using System.Data.Entity.Validation;
using Common;

namespace EagleEye.DAL
{
    public class DALWorkCode
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Att_Status_P.WorkCode_P> GetAllWorkCodes()
        {

            List<Att_Status_P.WorkCode_P> list = new List<Att_Status_P.WorkCode_P>();
            try
            {
                list = (from d in objModel.tbl_workcode
                        select new Att_Status_P.WorkCode_P
                        {
                            Code = d.Code,
                            Name = d.Name
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

        public List<WorkCode_P> GetAllWorkCode()
        {
            List<WorkCode_P> list = new List<WorkCode_P>();
            try
            {
                list = (from d in objModel.tbl_workcode
                        where d.Code != 0
                        select new WorkCode_P
                        {
                            Code = d.Code,
                            Name = d.Name
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
        public List<WorkCode_P> GetAllReportWorkCode()
        {
            List<WorkCode_P> list = new List<WorkCode_P>();
            try
            {
                list = (from d in objModel.tbl_workcode
                        select new WorkCode_P
                        {
                            Code = d.Code,
                            Name = d.Name
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
        public bool SetWorkCode(int code, string name)
        {
            bool flag = false;
            try
            {
                tbl_workcode d = objModel.tbl_workcode.Where(x => x.Code == code).FirstOrDefault();
                d.Name = name;

                objModel.Entry(d).State = System.Data.Entity.EntityState.Modified;
                int res = objModel.SaveChanges();

                if (res > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }
        public WorkCode_P GetWorkCode(string name)
        {
            WorkCode_P att = new WorkCode_P();
            try
            {
                tbl_workcode d = objModel.tbl_workcode.Where(x => x.Name == name).FirstOrDefault();
                att.Code = d.Code;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
        public WorkCode_P GetWorkCodeName(int code)
        {
            WorkCode_P att = new WorkCode_P();
            try
            {
                tbl_workcode d = objModel.tbl_workcode.Where(x => x.Code == code).FirstOrDefault();
                att.Name = d.Name;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
    }
}