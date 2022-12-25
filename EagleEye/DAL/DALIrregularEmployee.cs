using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EagleEye.DAL.Partial;
using EagleEye.Models;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;
using EagleEye.Common;

namespace EagleEye.DAL
{
    public class DALIrregularEmployee
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<IrregularEmployee_P> GetAllEmployees()
        {

            List<IrregularEmployee_P> list = new List<IrregularEmployee_P>();
            try
            {
                var result = objModel.tbl_irregularemployee.Distinct().ToList();
                list = (from e in objModel.tbl_irregularemployee
                        join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                        from loc in l.DefaultIfEmpty()
                        join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                        from dep in d.DefaultIfEmpty()
                        join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                        from dev in de.DefaultIfEmpty()
                        where (e.IsDelete == false || e.IsDelete == null)
                        select new IrregularEmployee_P
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
                            finger_0 = e.finger_0,
                            finger_1 = e.finger_1,
                            finger_2 = e.finger_2,
                            finger_3 = e.finger_3,
                            finger_4 = e.finger_4,
                            finger_5 = e.finger_5,
                            finger_6 = e.finger_6,
                            finger_7 = e.finger_7,
                            finger_8 = e.finger_8,
                            finger_9 = e.finger_9,
                            face_data = e.face_data,
                            palm_0 = e.palm_0,
                            palm_1 = e.palm_1,
                            photo_data = e.photo_data,
                            Msg = e.Msg
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
        public bool DeleteEmployee(int Code)
        {
            bool flag = false;
            try
            {
                var checkByID = objModel.tbl_irregularemployee.Where(x => x.Employee_ID == Code.ToString()).FirstOrDefault();

                if (checkByID != null)
                {

                    tbl_irregularemployee e = objModel.tbl_irregularemployee.Where(x => x.Code == checkByID.Code).FirstOrDefault();
                    objModel.tbl_irregularemployee.Remove(e);
                    objModel.SaveChanges();
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

        public IrregularEmployee_P GetIrrEmployee(string Code)
        {

            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {
                // var result = objModel.tbl_irregularemployee.Distinct().ToList();
                emp = (from e in objModel.tbl_irregularemployee
                       join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                       from loc in l.DefaultIfEmpty()
                       join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                       from dep in d.DefaultIfEmpty()
                       join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                       from dev in de.DefaultIfEmpty()
                       where e.Employee_ID == Code && (e.IsDelete == false || e.IsDelete == null)
                       select new IrregularEmployee_P
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
                           finger_0 = e.finger_0,
                           finger_1 = e.finger_1,
                           finger_2 = e.finger_2,
                           finger_3 = e.finger_3,
                           finger_4 = e.finger_4,
                           finger_5 = e.finger_5,
                           finger_6 = e.finger_6,
                           finger_7 = e.finger_7,
                           finger_8 = e.finger_8,
                           finger_9 = e.finger_9,
                           face_data = e.face_data,
                           palm_0 = e.palm_0,
                           palm_1 = e.palm_1,
                           photo_data = e.photo_data,
                           Msg = e.Msg
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
            return emp;

        }

        public bool AddUpdateIrrEmployee(IrregularEmployee_P employee)
        {
            bool flag = false;
            try
            {
                //int id = Formatter.SetValidValueToInt(employee.Employee_ID);
                tbl_irregularemployee e = objModel.tbl_irregularemployee.Where(x => x.Code == employee.Code).FirstOrDefault();

                if (e == null)
                    e = new tbl_irregularemployee();

                e.Msg = employee.Msg;

                if (e.Code == 0)
                {
                    objModel.tbl_irregularemployee.Add(e);
                }
                else
                {
                    objModel.Entry(e).State = System.Data.Entity.EntityState.Modified;
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


        public List<IrregularEmployee_P> GetDataTable(JqueryDatatableParam param, out int TotalRecords)
        {
            TotalRecords = 0;

            List<IrregularEmployee_P> list = new List<IrregularEmployee_P>();
            try
            {

                objModel.Database.CommandTimeout = 86400;
                IQueryable<IrregularEmployee_P> query = (from e in objModel.tbl_irregularemployee
                                                         join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                                                         from loc in l.DefaultIfEmpty()
                                                             //join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                                                             //from dep in d.DefaultIfEmpty()
                                                         join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                                                         from dev in de.DefaultIfEmpty()
                                                         where (e.IsDelete == false || e.IsDelete == null)
                                                         select new IrregularEmployee_P
                                                         {
                                                             Code = e.Code,
                                                             Employee_ID = e.Employee_ID,
                                                             Employee_Name = e.Employee_Name,
                                                             // Employee_Photo = e.Employee_Photo,
                                                             // Email = e.Email,
                                                             Card_No = e.Card_No,
                                                             Active = e.Active,
                                                             // fkLocation_Code = e.fkLocation_Code,
                                                             Location = loc.Description,
                                                             // fkDepartment_Code = e.fkDepartment_Code,
                                                             // Department = dep.Description,
                                                             Device_Id = e.Device_Id,
                                                             Device_Name = dev.Device_Name,
                                                             DeviceComb = e.Device_Id + "," + dev.Device_Name,
                                                             // Gender = e.Gender,
                                                             //  fkDesignation_Code = e.fkDesignation_Code,
                                                             //  fkEmployeeType_Code = e.fkEmployeeType_Code,
                                                             //  Telephone = e.Telephone,
                                                             // Trans_Id = e.Trans_Id,
                                                             // Update_Date = e.Update_Date,
                                                             //  Cmd_Param = e.Cmd_Param,
                                                             //  User_Privilege = e.User_Privilege,
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
                                                             Msg = e.Msg
                                                         });

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x => x.Employee_ID.Contains(param.sSearch)
                    || x.Employee_Name.Contains(param.sSearch)
                    || x.Card_No.Contains(param.sSearch)
                    || x.Location.Contains(param.sSearch));
                }

                TotalRecords = query.Count();
                list = query.ToList().OrderBy(x => Convert.ToInt32(x.Employee_ID)).ToList();
                ///skipping
                if (param.iDisplayLength != -1)
                {

                    if (TotalRecords > param.iDisplayStart)
                        list = list.Skip(param.iDisplayStart)
                           .Take(param.iDisplayLength).ToList();
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

            return list;
        }

        public IrregularEmployee_P GetIrrEmployeeByEmpID(string emp_id)
        {

            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {
                emp = (from irrEmp in objModel.tbl_irregularemployee
                       join dev in objModel.tbl_device on irrEmp.Device_Id equals dev.Device_ID into device
                       from dev in device.DefaultIfEmpty()
                       join loc in objModel.tbl_location on irrEmp.fkLocation_Code equals loc.Code into l
                       from loc in l.DefaultIfEmpty()

                       where irrEmp.Employee_ID == emp_id
                       select new IrregularEmployee_P()
                       {
                           Employee_ID = irrEmp.Employee_ID,
                           Employee_Name = irrEmp.Employee_Name,
                           Employee_Photo = irrEmp.Employee_Photo,
                           Location = loc.Description,
                           Device_Name = dev.Device_Name,
                           Active = irrEmp.Active
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

            return emp;
        }


    }
}