
using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Linq;
using System.Web;
using Common;

namespace EagleEye.DAL
{
    public class DALAtt_Status
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Att_Status_P> GetAllStatus()
        {

            List<Att_Status_P> list = new List<Att_Status_P>();
            try
            {
                list = (from d in objModel.tbl_att_status
                        where d.Code != 0
                        select new Att_Status_P
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
        public List<Att_Status_P> GetAllReportStatus()
        {

            List<Att_Status_P> list = new List<Att_Status_P>();
            try
            {
                list = (from d in objModel.tbl_att_status
                        select new Att_Status_P
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
        public bool SetAttendenceStatus(int code, string name)
        {
            bool flag = false;
            try
            {
                tbl_att_status d = objModel.tbl_att_status.Where(x => x.Code == code).FirstOrDefault();
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
        public Att_Status_P GetStatusCode(string name)
        {
            Att_Status_P att = new Att_Status_P();
            try
            {
                tbl_att_status d = objModel.tbl_att_status.Where(x => x.Name == name).FirstOrDefault();
                att.Code = d.Code;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
        public Att_Status_P GetStatusName(int code)
        {
            Att_Status_P att = new Att_Status_P();
            try
            {
                tbl_att_status d = objModel.tbl_att_status.Where(x => x.Code == code).FirstOrDefault();
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