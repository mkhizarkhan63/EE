using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Models;
using EagleEye.DAL.Partial;
using System.Data.Entity.Validation;

namespace EagleEye.DAL
{
    public class DALExpiredUsers
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<ExpiredUsers_P> GetAllExpiredEmployees()

        {
            List<ExpiredUsers_P> list = new List<ExpiredUsers_P>();
            try
            {

                list = (from e in objModel.tbl_expiredusers
                        join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                        from loc in l.DefaultIfEmpty()
                        join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                        from dep in d.DefaultIfEmpty()
                        join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                        from dev in de.DefaultIfEmpty()
                        where (e.IsDelete == false || e.IsDelete == null)
                        select new ExpiredUsers_P
                        {
                            Code = e.Code,
                            Employee_ID = e.Employee_ID,
                            Employee_Name = e.Employee_Name,
                            Employee_Photo = e.Employee_Photo,
                            Email = e.Email,
                            Card_No = e.Card_No,
                            Active = e.Active,
                            fkLocation_Code = e.fkLocation_Code,
                            Location = loc.Description,
                            fkDepartment_Code = e.fkDepartment_Code,
                            Department = dep.Description,
                            Device_Id = e.Device_Id,
                            Device_Name = dev.Device_Name,
                            Gender = e.Gender,
                            fkDesignation_Code = e.fkDesignation_Code,
                            fkEmployeeType_Code = e.fkEmployeeType_Code,
                            Telephone = e.Telephone,
                            Trans_Id = e.Trans_Id,
                            Update_Date = e.Update_Date,
                            Cmd_Param = e.Cmd_Param,
                            User_Privilege = e.User_Privilege,
                            FingerPrint = e.FingerPrint,
                            Face = e.Face,
                            Palm = e.Palm,
                            Password = e.Password,
                            //finger_0 = e.finger_0,
                            //finger_1 = e.finger_1,
                            //finger_2 = e.finger_2,
                            //finger_3 = e.finger_3,
                            //finger_4 = e.finger_4,
                            //finger_5 = e.finger_5,
                            //finger_6 = e.finger_6,
                            //finger_7 = e.finger_7,
                            //finger_8 = e.finger_8,
                            //finger_9 = e.finger_9,
                            //face_data = e.face_data,
                            //palm_0 = e.palm_0,
                            //palm_1 = e.palm_1,
                            //photo_data = e.photo_data,
                            Valid_DateStart = e.Valid_DateStart,
                            Valid_DateEnd = e.Valid_DateEnd,
                            Sunday = e.Sunday,
                            Monday = e.Monday,
                            Tuesday = e.Tuesday,
                            Wednesday = e.Wednesday,
                            Thursday = e.Thursday,
                            Friday = e.Friday,
                            Saturday = e.Saturday
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

        public bool DeleteEmployee(string Code)
        {
            bool flag = false;
            try
            {
                List<tbl_expiredusers> e = objModel.tbl_expiredusers.Where(x => x.Employee_ID == Code).ToList();
                foreach (var item in e)
                {
                    if (e != null)
                    {
                        objModel.tbl_expiredusers.Remove(item);
                       
                        flag = true;
                    }
                }
                objModel.SaveChanges();
                //e.IsDelete = true;
                //objModel.Entry(e).State = System.Data.Entity.EntityState.Modified;

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
        public bool CheckEmployeeExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                tbl_expiredusers d = objModel.tbl_expiredusers.Where(x => x.Employee_ID == CODE).FirstOrDefault();
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

    }
}