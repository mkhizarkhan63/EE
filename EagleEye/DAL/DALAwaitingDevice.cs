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

namespace EagleEye.DAL
{
    public class DALAwaitingDevice
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<AwaitingDevice_P> GetAllAwaitingDevices()
        {

            List<AwaitingDevice_P> list = new List<AwaitingDevice_P>();
            try
            {
                list = (from d in objModel.tbl_awaitingdevice
                        select new AwaitingDevice_P
                        {
                            Code = d.Code,
                            Device_ID = d.Device_ID,
                            Device_Type = d.Device_Type,
                            Device_Name = d.Device_Name,
                            IsConnected = d.IsConnected,
                            Device_Info = d.Device_Info,
                            Device_Status_Info = d.Device_Status_Info
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

        public AwaitingDevice_P GetAwaitingDeviceByCode(int Code)
        {
            AwaitingDevice_P device = new AwaitingDevice_P();
            try
            {
                device = (from d in objModel.tbl_awaitingdevice
                          where d.Code == Code
                          select new AwaitingDevice_P
                          {
                              Code = d.Code,
                              Device_ID = d.Device_ID,
                              Device_Type = d.Device_Type,
                              Device_Name = d.Device_Name,
                              IsConnected = d.IsConnected,
                              Device_Info = d.Device_Info,
                              Device_Status_Info = d.Device_Status_Info
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

            return device;
        }

        public AwaitingDevice_P GetAwaitingDeviceByDeviceID(string Code)
        {
            AwaitingDevice_P device = new AwaitingDevice_P();
            try
            {
                device = (from d in objModel.tbl_awaitingdevice
                          where d.Device_ID == Code
                          select new AwaitingDevice_P
                          {
                              Code = d.Code,
                              Device_ID = d.Device_ID,
                              Device_Type = d.Device_Type,
                              Device_Name = d.Device_Name,
                              IsConnected = d.IsConnected,
                              Device_Info = d.Device_Info,
                              Device_Status_Info = d.Device_Status_Info
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

            return device;
        }

        //public AwaitingDevice_P GetFKDevices(string Code)
        //{
        //    AwaitingDevice_P device = new AwaitingDevice_P();
        //    try
        //    {
        //        device = (from d in objModel.tbl_fkdevice_status
        //                  where d.device_id == Code
        //                  select new AwaitingDevice_P
        //                  {
        //                      //Code = d.c,
        //                      Device_ID = d.device_id,
        //                     // Device_Type = d.Device_Type,
        //                      Device_Name = d.device_name,
        //                      IsConnected = true,
        //                      Device_Info = d.device_info,
        //                      Device_Status_Info = d.dev_status_info
        //                  }).FirstOrDefault();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var eve in ex.EntityValidationErrors)
        //        {
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
        //    }

        //    return device;
        //}
        
        public bool DeleteAwaitingDevice(string Device_ID)
        {
            bool flag = false;
            try
            {
                tbl_awaitingdevice d = objModel.tbl_awaitingdevice.Where(x => x.Device_ID == Device_ID).FirstOrDefault();
                if (d != null)
                {
                    objModel.tbl_awaitingdevice.Remove(d);
                    objModel.SaveChanges();
                }
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
        public bool DeleteAwaitingDeviceC(string Device_ID)
        {
            bool flag = false;
            try
            {
                tbl_fkdevice_status d = objModel.tbl_fkdevice_status.Where(x => x.device_id == Device_ID).FirstOrDefault();
                if (d != null)
                {
                    objModel.tbl_fkdevice_status.Remove(d);
                    objModel.SaveChanges();
                }
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