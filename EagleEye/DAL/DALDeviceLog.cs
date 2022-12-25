using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using Common;

namespace EagleEye.DAL
{
    public class DALDeviceLog
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Device_OfflineLog_P> GetAllLogs()
        {
            List<Device_OfflineLog_P> list = new List<Device_OfflineLog_P>();
            try
            {
                list = (from d in objModel.tbl_fkcmd_trans_offline
                        join dev in objModel.tbl_device on d.device_id equals dev.Device_ID into de
                        from dev in de.DefaultIfEmpty()
                        join tz in objModel.tbl_timezone on d.timezone_no equals tz.Timezone_No into t
                        from tz in t.DefaultIfEmpty()
                        select new Device_OfflineLog_P
                        {
                            trans_id = d.trans_id,
                            device_id = d.device_id,
                            user_id = d.user_id,
                            cmd_code = d.cmd_code,
                            return_code = d.return_code,
                            status = d.status,
                            update_time = d.update_time.ToString(),
                            timezone_no = d.timezone_no,
                            timezone_name = tz.Timezone_Name,
                            Device_Name = dev.Device_Name,
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

        public bool DeleteLog(string Code)
        {
            bool flag = false;
            try
            {
                tbl_fkcmd_trans_offline e = objModel.tbl_fkcmd_trans_offline.Where(x => x.trans_id == Code).FirstOrDefault();
                objModel.tbl_fkcmd_trans_offline.Remove(e);

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

        public bool DeleteFKCmd(string Code)
        {
            bool flag = false;
            try
            {
                tbl_fkcmd_trans_cmd_param_offline e = objModel.tbl_fkcmd_trans_cmd_param_offline.Where(x => x.trans_id == Code).FirstOrDefault();
                objModel.tbl_fkcmd_trans_cmd_param_offline.Remove(e);

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