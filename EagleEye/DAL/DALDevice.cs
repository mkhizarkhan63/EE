using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;

namespace EagleEye.DAL
{
    public class DALDevice
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<Device_P> GetAllDevices()
        {

            List<Device_P> list = new List<Device_P>();
            try
            {
                list = (from d in objModel.tbl_device
                        select new Device_P
                        {
                            Code = d.Code,
                            Device_ID = d.Device_ID,
                            Device_Name = d.Device_Name,
                            Device_Info = d.Device_Info,
                            Device_Location = d.Device_Location,
                            Device_Type = d.Device_Type,
                            Device_Status_Info = d.Device_Status_Info,
                            Max_Record = (d.Max_Record == null ? "0" : d.Max_Record),
                            Real_FaceReg = (d.Real_FaceReg == null ? "0" : d.Real_FaceReg),
                            Max_FaceReg = (d.Max_FaceReg == null ? "0" : d.Max_FaceReg),
                            Real_FPReg = (d.Real_FPReg == null ? "0" : d.Real_FPReg),
                            Max_FPReg = (d.Max_FPReg == null ? "0" : d.Max_FPReg),
                            Real_IDCardReg = (d.Real_IDCardReg == null ? "0" : d.Real_IDCardReg),
                            Max_IDCardReg = (d.Max_IDCardReg == null ? "0" : d.Max_IDCardReg),
                            Real_Manager = (d.Real_Manager == null ? "0" : d.Real_Manager),
                            Max_Manager = (d.Max_Manager == null ? "0" : d.Max_Manager),
                            Real_PasswordReg = (d.Real_PasswordReg == null ? "0" : d.Real_PasswordReg),
                            Max_PasswordReg = (d.Max_PasswordReg == null ? "0" : d.Max_PasswordReg),
                            Real_PvReg = (d.Real_PvReg == null ? "0" : d.Real_PvReg),
                            Max_PvReg = (d.Max_PvReg == null ? "0" : d.Max_PvReg),
                            Total_log_Count = (d.Total_log_Count == null ? "0" : d.Total_log_Count),
                            Total_log_Max = (d.Total_log_Max == null ? "0" : d.Total_log_Max),
                            Real_Employee = (d.Real_Employee == null ? "0" : d.Real_Employee),
                            Max_Employee = (d.Max_Employee == null ? "0" : d.Max_Employee),
                            //Device_LastStatusTime = d.Device_LastStatusTime,
                            Device_Status = d.Device_Status,
                            Face_Data_Ver = d.Face_Data_Ver,
                            Firmware = d.Firmware,
                            Firmware_Filename = d.Firmware_Filename,
                            Fk_Bin_Data_Lib = d.Fk_Bin_Data_Lib,
                            Fp_Data_Ver = d.Fp_Data_Ver,
                            Supported_Enroll_Data = d.Supported_Enroll_Data,
                            Active = d.Active,
                            Alarm_Delay = d.Alarm_Delay,
                            Allow_EarlyTime = d.Allow_EarlyTime,
                            Allow_LateTime = d.Allow_LateTime,
                            Anti_back = d.Anti_back,
                            Show_ResultTime = d.Show_ResultTime,
                            DoorMagnetic_Delay = d.DoorMagnetic_Delay,
                            DoorMagnetic_Type = d.DoorMagnetic_Type,
                            Glog_Warning = d.Glog_Warning,
                            OpenDoor_Delay = d.OpenDoor_Delay,
                            Receive_Interval = d.Receive_Interval,
                            Reverify_Time = d.Reverify_Time,
                            Screensavers_Time = d.Screensavers_Time,
                            Sleep_Time = d.Sleep_Time,
                            Use_Alarm = d.Use_Alarm,
                            Volume = d.Volume,
                            Wiegand_Input = d.Wiegand_Input,
                            Wiegand_Output = d.Wiegand_Output,
                            Wiegand_Type = d.Wiegand_Type,
                            Device_Group = d.Device_Group,
                            Server_Address = d.Server_Address,
                            Server_Port = d.Server_Port,
                            Sys_Time = d.Sys_Time,
                            Reader_ID = d.Reader_ID,
                            Multi_Users = d.Multi_Users
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

        public Device_P GetDeviceByCode(int Code)
        {
            Device_P device = new Device_P();
            try
            {
                device = (from d in objModel.tbl_device
                          where d.Code == Code
                          select new Device_P
                          {
                              Code = d.Code,
                              Device_ID = d.Device_ID,
                              Device_Name = d.Device_Name,
                              Device_Info = d.Device_Info,
                              Device_Location = d.Device_Location,
                              Device_Type = d.Device_Type,
                              Device_Status_Info = d.Device_Status_Info,
                              Max_Record = (d.Max_Record == null ? "0" : d.Max_Record),
                              Real_FaceReg = (d.Real_FaceReg == null ? "0" : d.Real_FaceReg),
                              Max_FaceReg = (d.Max_FaceReg == null ? "0" : d.Max_FaceReg),
                              Real_FPReg = (d.Real_FPReg == null ? "0" : d.Real_FPReg),
                              Max_FPReg = (d.Max_FPReg == null ? "0" : d.Max_FPReg),
                              Real_IDCardReg = (d.Real_IDCardReg == null ? "0" : d.Real_IDCardReg),
                              Max_IDCardReg = (d.Max_IDCardReg == null ? "0" : d.Max_IDCardReg),
                              Real_Manager = (d.Real_Manager == null ? "0" : d.Real_Manager),
                              Max_Manager = (d.Max_Manager == null ? "0" : d.Max_Manager),
                              Real_PasswordReg = (d.Real_PasswordReg == null ? "0" : d.Real_PasswordReg),
                              Max_PasswordReg = (d.Max_PasswordReg == null ? "0" : d.Max_PasswordReg),
                              Real_PvReg = (d.Real_PvReg == null ? "0" : d.Real_PvReg),
                              Max_PvReg = (d.Max_PvReg == null ? "0" : d.Max_PvReg),
                              Total_log_Count = (d.Total_log_Count == null ? "0" : d.Total_log_Count),
                              Total_log_Max = (d.Total_log_Max == null ? "0" : d.Total_log_Max),
                              Real_Employee = (d.Real_Employee == null ? "0" : d.Real_Employee),
                              Max_Employee = (d.Max_Employee == null ? "0" : d.Max_Employee),
                              Face_Data_Ver = d.Face_Data_Ver,
                              Firmware = d.Firmware,
                              Firmware_Filename = d.Firmware_Filename,
                              Fk_Bin_Data_Lib = d.Fk_Bin_Data_Lib,
                              Fp_Data_Ver = d.Fp_Data_Ver,
                              Supported_Enroll_Data = d.Supported_Enroll_Data,
                              Device_Status = d.Device_Status,
                              //Device_LastStatusTime = d.Device_LastStatusTime,
                              Active = d.Active,
                              Alarm_Delay = d.Alarm_Delay,
                              Allow_EarlyTime = d.Allow_EarlyTime,
                              Allow_LateTime = d.Allow_LateTime,
                              Show_ResultTime = d.Show_ResultTime,
                              Anti_back = d.Anti_back,
                              DoorMagnetic_Delay = d.DoorMagnetic_Delay,
                              DoorMagnetic_Type = d.DoorMagnetic_Type,
                              Glog_Warning = d.Glog_Warning,
                              OpenDoor_Delay = d.OpenDoor_Delay,
                              Receive_Interval = d.Receive_Interval,
                              Reverify_Time = d.Reverify_Time,
                              Screensavers_Time = d.Screensavers_Time,
                              Sleep_Time = d.Sleep_Time,
                              Use_Alarm = d.Use_Alarm,
                              Volume = d.Volume,
                              Wiegand_Input = d.Wiegand_Input,
                              Wiegand_Output = d.Wiegand_Output,
                              Wiegand_Type = d.Wiegand_Type,
                              Device_Group = d.Device_Group,
                              Server_Address = d.Server_Address,
                              Server_Port = d.Server_Port,
                              Sys_Time = d.Sys_Time,
                              Reader_ID = d.Reader_ID,
                              Multi_Users = d.Multi_Users,
                              isSlave = d.IsSlave
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

        public Device_P GetDeviceByDevice_ID(string Device_ID)
        {
            Device_P device = new Device_P();
            try
            {
                device = (from d in objModel.tbl_device
                          where d.Device_ID == Device_ID
                          select new Device_P
                          {
                              Code = d.Code,
                              Device_ID = d.Device_ID,
                              Device_Name = d.Device_Name,
                              Device_Info = d.Device_Info,
                              Device_Type = d.Device_Type,
                              Device_Location = d.Device_Location,
                              Device_Status_Info = d.Device_Status_Info,
                              Max_Record = (d.Max_Record == null ? "0" : d.Max_Record),
                              Real_FaceReg = (d.Real_FaceReg == null ? "0" : d.Real_FaceReg),
                              Max_FaceReg = (d.Max_FaceReg == null ? "0" : d.Max_FaceReg),
                              Real_FPReg = (d.Real_FPReg == null ? "0" : d.Real_FPReg),
                              Max_FPReg = (d.Max_FPReg == null ? "0" : d.Max_FPReg),
                              Real_IDCardReg = (d.Real_IDCardReg == null ? "0" : d.Real_IDCardReg),
                              Max_IDCardReg = (d.Max_IDCardReg == null ? "0" : d.Max_IDCardReg),
                              Real_Manager = (d.Real_Manager == null ? "0" : d.Real_Manager),
                              Max_Manager = (d.Max_Manager == null ? "0" : d.Max_Manager),
                              Real_PasswordReg = (d.Real_PasswordReg == null ? "0" : d.Real_PasswordReg),
                              Max_PasswordReg = (d.Max_PasswordReg == null ? "0" : d.Max_PasswordReg),
                              Real_PvReg = (d.Real_PvReg == null ? "0" : d.Real_PvReg),
                              Max_PvReg = (d.Max_PvReg == null ? "0" : d.Max_PvReg),
                              Total_log_Count = (d.Total_log_Count == null ? "0" : d.Total_log_Count),
                              Total_log_Max = (d.Total_log_Max == null ? "0" : d.Total_log_Max),
                              Real_Employee = (d.Real_Employee == null ? "0" : d.Real_Employee),
                              Max_Employee = (d.Max_Employee == null ? "0" : d.Max_Employee),
                              Face_Data_Ver = d.Face_Data_Ver,
                              Firmware = d.Firmware,
                              Firmware_Filename = d.Firmware_Filename,
                              Fk_Bin_Data_Lib = d.Fk_Bin_Data_Lib,
                              Fp_Data_Ver = d.Fp_Data_Ver,
                              Supported_Enroll_Data = d.Supported_Enroll_Data,
                              Device_Status = d.Device_Status,
                              //Device_LastStatusTime = d.Device_LastStatusTime,
                              Active = d.Active,
                              Alarm_Delay = d.Alarm_Delay,
                              Allow_EarlyTime = d.Allow_EarlyTime,
                              Allow_LateTime = d.Allow_LateTime,
                              Anti_back = d.Anti_back,
                              DoorMagnetic_Delay = d.DoorMagnetic_Delay,
                              DoorMagnetic_Type = d.DoorMagnetic_Type,
                              Glog_Warning = d.Glog_Warning,
                              OpenDoor_Delay = d.OpenDoor_Delay,
                              Receive_Interval = d.Receive_Interval,
                              Reverify_Time = d.Reverify_Time,
                              Screensavers_Time = d.Screensavers_Time,
                              Sleep_Time = d.Sleep_Time,
                              Use_Alarm = d.Use_Alarm,
                              Show_ResultTime = d.Show_ResultTime,
                              Volume = d.Volume,
                              Wiegand_Input = d.Wiegand_Input,
                              Wiegand_Output = d.Wiegand_Output,
                              Wiegand_Type = d.Wiegand_Type,
                              Device_Group = d.Device_Group,
                              Server_Address = d.Server_Address,
                              Server_Port = d.Server_Port,
                              Sys_Time = d.Sys_Time,
                              Reader_ID = d.Reader_ID,
                              Multi_Users = d.Multi_Users
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

        public bool AddDevice(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = new tbl_device
                {
                    Code = device.Code,
                    //  Device_Type = device.Device_Type,
                    Device_ID = device.Device_ID,
                    Device_Name = device.Device_Name,
                    Device_Info = device.Device_Info,
                    Device_Status = device.Device_Status,
                    Active = device.Active,
                    //Device_LastStatusTime = device.Device_LastStatusTime,
                    Device_Location = device.Device_Location,
                    Face_Data_Ver = device.Face_Data_Ver,
                    Firmware = device.Firmware,
                    Firmware_Filename = device.Firmware_Filename,
                    Fk_Bin_Data_Lib = device.Fk_Bin_Data_Lib,
                    Fp_Data_Ver = device.Fp_Data_Ver,
                    Supported_Enroll_Data = device.Supported_Enroll_Data,
                    IsSlave = device.isSlave

                };

                objModel.tbl_device.Add(d);
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

        public bool UpdateDeviceInfo(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Code == device.Code).FirstOrDefault();
                d.Device_ID = device.Device_ID;
                d.Device_Name = device.Device_Name;
                d.Server_Address = device.Server_Address;
                d.Server_Port = device.Server_Port;
                d.Device_Status = device.Device_Status;
                d.Device_Type = device.Device_Type;
                d.Device_Location = device.Device_Location;
                //  d.Device_Info = device.Device_Info;
                d.Max_Record = device.Max_Record;
                d.Real_FaceReg = device.Real_FaceReg;
                d.Max_FaceReg = device.Max_FaceReg;
                d.Real_FPReg = device.Real_FPReg;
                d.Max_FPReg = device.Max_FPReg;
                d.Real_IDCardReg = device.Real_IDCardReg;
                d.Max_IDCardReg = device.Max_IDCardReg;
                d.Real_Manager = device.Real_Manager;
                d.Max_Manager = device.Max_Manager;
                d.Real_PasswordReg = device.Real_PasswordReg;
                d.Max_PasswordReg = device.Max_PasswordReg;
                d.Real_PvReg = device.Real_PvReg;
                d.Max_PvReg = device.Max_PvReg;
                d.Total_log_Count = device.Total_log_Count;
                d.Total_log_Max = device.Total_log_Max;
                d.Real_Employee = device.Real_Employee;
                d.Max_Employee = device.Max_Employee;
                d.Face_Data_Ver = device.Face_Data_Ver;
                d.Firmware = device.Firmware;
                d.Firmware_Filename = device.Firmware_Filename;
                d.Fk_Bin_Data_Lib = device.Fk_Bin_Data_Lib;
                d.Fp_Data_Ver = device.Fp_Data_Ver;
                d.Supported_Enroll_Data = device.Supported_Enroll_Data;
                d.Active = device.Active;
                //d.Device_LastStatusTime = device.Device_LastStatusTime;
                d.Alarm_Delay = device.Alarm_Delay;
                d.Allow_EarlyTime = device.Allow_EarlyTime;
                if (device.Show_ResultTime != null)
                    d.Show_ResultTime = device.Show_ResultTime;

                d.Allow_LateTime = device.Allow_LateTime;
                d.Anti_back = device.Anti_back;
                d.DoorMagnetic_Delay = device.DoorMagnetic_Delay;
                d.DoorMagnetic_Type = device.DoorMagnetic_Type;
                d.Glog_Warning = device.Glog_Warning;
                d.OpenDoor_Delay = device.OpenDoor_Delay;
                d.Receive_Interval = device.Receive_Interval;
                d.Reverify_Time = device.Reverify_Time;
                d.Screensavers_Time = device.Screensavers_Time;
                d.Sleep_Time = device.Sleep_Time;
                d.Use_Alarm = device.Use_Alarm;
                d.Show_ResultTime = device.Show_ResultTime;
                d.Volume = device.Volume;
                d.Wiegand_Input = device.Wiegand_Input;
                d.Wiegand_Output = device.Wiegand_Output;
                d.Wiegand_Type = device.Wiegand_Type;
                d.Reader_ID = device.Reader_ID;
                d.Multi_Users = device.Multi_Users;
                d.IsSlave = device.isSlave;
                objModel.Entry(d).State = System.Data.Entity.EntityState.Modified;
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

        public bool UpdateMachineInfo(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Device_ID == device.Device_ID).FirstOrDefault();
                d.Alarm_Delay = device.Alarm_Delay;
                d.Allow_EarlyTime = device.Allow_EarlyTime;
                // d.Show_ResultTime = device.Show_ResultTime;
                d.Allow_LateTime = device.Allow_LateTime;
                d.Anti_back = device.Anti_back;
                d.DoorMagnetic_Delay = device.DoorMagnetic_Delay;
                d.DoorMagnetic_Type = device.DoorMagnetic_Type;
                d.Glog_Warning = device.Glog_Warning;
                d.OpenDoor_Delay = device.OpenDoor_Delay;
                d.Receive_Interval = device.Receive_Interval;
                d.Reverify_Time = device.Reverify_Time;
                d.Screensavers_Time = device.Screensavers_Time;
                d.Sleep_Time = device.Sleep_Time;
                d.Use_Alarm = device.Use_Alarm;
                d.Volume = device.Volume;
                d.Wiegand_Input = device.Wiegand_Input;
                d.Wiegand_Output = device.Wiegand_Output;
                d.Wiegand_Type = device.Wiegand_Type;
                d.Device_Type = device.Device_Type;
                d.Multi_Users = device.Multi_Users;
                if (device.Show_ResultTime != null)
                {
                    d.Show_ResultTime = device.Show_ResultTime;
                }

                objModel.Entry(d).State = System.Data.Entity.EntityState.Modified;
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

        public bool UpdateDeviceStatus(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Device_ID == device.Device_ID).FirstOrDefault();
                if (d != null)
                {
                    d.Max_Record = device.Max_Record;
                    d.Real_FaceReg = device.Real_FaceReg;
                    d.Max_FaceReg = device.Max_FaceReg;
                    d.Real_FPReg = device.Real_FPReg;
                    d.Max_FPReg = device.Max_FPReg;
                    d.Real_IDCardReg = device.Real_IDCardReg;
                    d.Max_IDCardReg = device.Max_IDCardReg;
                    d.Real_Manager = device.Real_Manager;
                    d.Max_Manager = device.Max_Manager;
                    d.Real_PasswordReg = device.Real_PasswordReg;
                    d.Max_PasswordReg = device.Max_PasswordReg;
                    d.Real_PvReg = device.Real_PvReg;
                    d.Max_PvReg = device.Max_PvReg;
                    d.Total_log_Count = device.Total_log_Count;
                    d.Total_log_Max = device.Total_log_Max;
                    d.Real_Employee = device.Real_Employee;
                    d.Max_Employee = device.Max_Employee;
                    d.Device_Status_Info = device.Device_Status_Info;

                    objModel.Entry(d).State = System.Data.Entity.EntityState.Modified;
                    int res = objModel.SaveChanges();

                    if (res > 0)
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

        public bool DeleteDevice(int Code)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_device.Remove(d);
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
        public bool DeleteDeviceFK(string Code)
        {
            bool flag = false;
            try
            {
                tbl_fkdevice_status d = objModel.tbl_fkdevice_status.Where(x => x.device_id == Code).FirstOrDefault();
                objModel.tbl_fkdevice_status.Remove(d);
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
        public bool CheckDeviceExistInDB(string DeviceID)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Device_ID == DeviceID).FirstOrDefault();
                if (d != null)
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

        public int GetDeviceCountByStatus(int Status)
        {
            int status = 0;
            try
            {
                status = objModel.tbl_device.Where(x => x.Device_Status == Status).ToList().Count();
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

            return status;
        }

        public bool UpdateDeviceName(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Code == device.Code).FirstOrDefault();
                d.Device_Name = device.Device_Name;

                objModel.Entry(d).State = System.Data.Entity.EntityState.Modified;
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

        public bool UpdateServerSetting(Device_P device)
        {
            bool flag = false;
            try
            {
                tbl_device d = objModel.tbl_device.Where(x => x.Code == device.Code).FirstOrDefault();
                d.Server_Address = device.Server_Address;
                d.Server_Port = device.Server_Port;
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


        public List<Device_P> GetAllConnectedDevices()
        {

            List<Device_P> list = new List<Device_P>();
            try
            {
                list = (from d in objModel.tbl_device
                        where d.Device_Status == 1
                        select new Device_P
                        {
                            //SYS INFO
                            Code = d.Code,
                            Device_ID = d.Device_ID,
                            Device_Name = d.Device_Name,
                            Device_Info = d.Device_Info,
                            Device_Type = d.Device_Type,
                            Device_Location = d.Device_Location,
                            Device_Status_Info = d.Device_Status_Info,
                            Max_Record = (d.Max_Record == null ? "0" : d.Max_Record),
                            Real_FaceReg = (d.Real_FaceReg == null ? "0" : d.Real_FaceReg),
                            Max_FaceReg = (d.Max_FaceReg == null ? "0" : d.Max_FaceReg),
                            Real_FPReg = (d.Real_FPReg == null ? "0" : d.Real_FPReg),
                            Max_FPReg = (d.Max_FPReg == null ? "0" : d.Max_FPReg),
                            Real_IDCardReg = (d.Real_IDCardReg == null ? "0" : d.Real_IDCardReg),
                            Max_IDCardReg = (d.Max_IDCardReg == null ? "0" : d.Max_IDCardReg),
                            Real_Manager = (d.Real_Manager == null ? "0" : d.Real_Manager),
                            Max_Manager = (d.Max_Manager == null ? "0" : d.Max_Manager),
                            Real_PasswordReg = (d.Real_PasswordReg == null ? "0" : d.Real_PasswordReg),
                            Max_PasswordReg = (d.Max_PasswordReg == null ? "0" : d.Max_PasswordReg),
                            Real_PvReg = (d.Real_PvReg == null ? "0" : d.Real_PvReg),
                            Max_PvReg = (d.Max_PvReg == null ? "0" : d.Max_PvReg),
                            Total_log_Count = (d.Total_log_Count == null ? "0" : d.Total_log_Count),
                            Total_log_Max = (d.Total_log_Max == null ? "0" : d.Total_log_Max),
                            Real_Employee = (d.Real_Employee == null ? "0" : d.Real_Employee),
                            Max_Employee = (d.Max_Employee == null ? "0" : d.Max_Employee),
                            //Device_LastStatusTime = d.Device_LastStatusTime,
                            Device_Status = d.Device_Status,
                            Face_Data_Ver = d.Face_Data_Ver,
                            Firmware = d.Firmware,
                            Firmware_Filename = d.Firmware_Filename,
                            Fk_Bin_Data_Lib = d.Fk_Bin_Data_Lib,
                            Fp_Data_Ver = d.Fp_Data_Ver,
                            Supported_Enroll_Data = d.Supported_Enroll_Data,
                            Active = d.Active,
                            Alarm_Delay = d.Alarm_Delay,
                            Allow_EarlyTime = d.Allow_EarlyTime,
                            Allow_LateTime = d.Allow_LateTime,
                            Anti_back = d.Anti_back,
                            Show_ResultTime = d.Show_ResultTime,
                            DoorMagnetic_Delay = d.DoorMagnetic_Delay,
                            DoorMagnetic_Type = d.DoorMagnetic_Type,
                            Glog_Warning = d.Glog_Warning,
                            OpenDoor_Delay = d.OpenDoor_Delay,
                            Receive_Interval = d.Receive_Interval,
                            Reverify_Time = d.Reverify_Time,
                            Screensavers_Time = d.Screensavers_Time,
                            Sleep_Time = d.Sleep_Time,
                            Use_Alarm = d.Use_Alarm,
                            Volume = d.Volume,
                            Wiegand_Input = d.Wiegand_Input,
                            Wiegand_Output = d.Wiegand_Output,
                            Wiegand_Type = d.Wiegand_Type,
                            Device_Group = d.Device_Group,
                            Server_Address = d.Server_Address,
                            Server_Port = d.Server_Port,
                            Sys_Time = d.Sys_Time,
                            Reader_ID = d.Reader_ID,
                            Multi_Users = d.Multi_Users
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
        #region NOT IN USE

        public Device_P GetDeviceStatus(string devID)
        {
            List<string> list = new List<string>();
            Device_P device = new Device_P();
            try
            {
                device = (from d in objModel.tbl_fkdevice_status
                          where d.device_id == devID
                          select new Device_P
                          {
                              Device_Status_Info = d.dev_status_info
                          }).FirstOrDefault();


                string device_Status = device.Device_Status_Info;
                // LogService.WriteLog("[GetDeviceStatus]: " + device_Status);

                if (device_Status != "" && device_Status != null)
                {
                    string rem = device_Status.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("[", "").Replace("]", "").Replace("\n", "");
                    string[] dev_Status = rem.Split(',');
                    for (int i = 0; i < dev_Status.Length; i++)
                    {
                        string[] ds = dev_Status[i].Split(':');
                        for (int j = 0; j < ds.Length; j++)
                        {
                            list.Add(ds[j]);
                        }
                    }
                }
                device.Max_Record = list[1].ToString();
                device.Real_FaceReg = list[3].ToString();
                device.Max_FaceReg = list[5].ToString();
                device.Real_FPReg = list[7].ToString();
                device.Max_FPReg = list[9].ToString();
                device.Real_IDCardReg = list[11].ToString();
                device.Max_IDCardReg = list[13].ToString();
                device.Real_Manager = list[15].ToString();
                device.Max_Manager = list[17].ToString();
                device.Real_PasswordReg = list[19].ToString();
                device.Max_PasswordReg = list[21].ToString();
                device.Real_PvReg = list[23].ToString();
                device.Max_PvReg = list[25].ToString();
                device.Total_log_Count = list[27].ToString();
                device.Total_log_Max = list[29].ToString();
                device.Real_Employee = list[31].ToString();
                device.Max_Employee = list[33].ToString();

                //LogService.WriteLog("[Max_Record]: " + device.Max_Record +
                //    "[Real_FaceReg]: " + device.Real_FaceReg +
                //    "[Max_FaceReg]: " + device.Max_FaceReg +
                //    "[Real_FPReg]: " + device.Real_FPReg +
                //    "[Max_FPReg]: " + device.Max_FPReg +
                //    "[Real_IDCardReg]: " + device.Real_IDCardReg +
                //    "[Max_IDCardReg]: " + device.Max_IDCardReg +
                //    "[Real_Manager]: " + device.Real_Manager +
                //    "[Real_PasswordReg]: " + device.Real_PasswordReg +
                //    "[Max_PasswordReg]: " + device.Max_PasswordReg +
                //    "[Total_log_Count]: " + device.Total_log_Count);
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


        #endregion


        public string GetDeviceName(string devID)
        {
            string deviceName = "";
            Device_P device = new Device_P();
            try
            {
                device = (from d in objModel.tbl_device
                          where d.Device_ID == devID
                          select new Device_P
                          {
                              Device_Name = d.Device_Name
                          }).FirstOrDefault();

                deviceName = device.Device_Name;
            }
           
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return deviceName;
        }


    }
}