using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;
using System.Linq;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;

namespace EagleEye.DAL
{
    public class DALSetting
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public Setting_P GetSetting()
        {

            Setting_P setting = new Setting_P();
            try
            {
                setting = (from d in objModel.tbl_setting
                           select new Setting_P
                           {
                               Code = d.Code,
                               AppSetting_Path = d.AppSetting_Path
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
            return setting;

        }

        public bool AddUpdateSetting(Setting_P Setting)
        {
            bool flag = false;
            try
            {
                tbl_setting l = objModel.tbl_setting.FirstOrDefault();


                if (l == null)
                    l = new tbl_setting();

                l.AppSetting_Path = Setting.AppSetting_Path;


                if (l.Code == 0)
                {
                    objModel.tbl_setting.Add(l);
                }
                else
                {
                    objModel.Entry(l).State = System.Data.Entity.EntityState.Modified;
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

        public Setting_P GetServiceName()
        {
            Setting_P serviceName = new Setting_P();

            try
            {
                serviceName = (from s_name in objModel.tbl_setting
                               select new Setting_P
                               {
                                   ServiceName = s_name.ServiceName

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
            return serviceName;
        }

    }
}