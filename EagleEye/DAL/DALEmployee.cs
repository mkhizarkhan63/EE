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
using EagleEye.DAL.DTO;
using EagleEye.Common;

namespace EagleEye.DAL
{
    public class DALEmployee
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Employee_P> GetEmployeeByCodes(string[] code)
        {

            List<Employee_P> employee = new List<Employee_P>();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            from loc in l.DefaultIfEmpty()
                            join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            from dep in d.DefaultIfEmpty()
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where code.Contains(e.Code.ToString()) && (e.IsDelete == false || e.IsDelete == null)
                            select new Employee_P
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
                                photo_data = e.photo_data
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
            return employee;

        }
        public List<Employee_P> GetAllEmployees()
        {
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = (from e in objModel.tbl_employee
                            //join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            //from loc in l.DefaultIfEmpty()
                            //join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            //from dep in d.DefaultIfEmpty()
                            //join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            //from dev in de.DefaultIfEmpty()
                            //join des in objModel.tbl_designation on e.fkDesignation_Code equals des.Code into desg
                            //from des in desg.DefaultIfEmpty()
                            //join et in objModel.tbl_employeetype on e.fkEmployeeType_Code equals et.Code into ety
                            //from et in ety.DefaultIfEmpty()
                        where (e.IsDelete == false || e.IsDelete == null)
                        select new Employee_P
                        {
                            Code = e.Code,
                            Employee_ID = e.Employee_ID,
                            Employee_Name = e.Employee_Name,
                            Employee_Photo = e.Employee_Photo,
                            Email = e.Email,
                            Card_No = e.Card_No,
                            Active = e.Active,
                            fkLocation_Code = e.fkLocation_Code,
                            Location = "",
                            fkDepartment_Code = e.fkDepartment_Code,
                            Department = "",
                            Device_Id = e.Device_Id,
                            Device_Name = "",
                            Gender = e.Gender,
                            fkDesignation_Code = e.fkDesignation_Code,
                            Designation = "",
                            EmployeeType = "",
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

        public List<EmployeeDTO> GetAllEmployeeDTO(JqueryDatatableParam param, string locationFilter, string statusFilter, out int totalRecords)
        {
            totalRecords = 0;
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            try
            {
                objModel.Database.CommandTimeout = 86400;
                IEnumerable<EmployeeDTO> query = (from e in objModel.tbl_employee
                                                  join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                                                  from loc in l.DefaultIfEmpty()
                                                      //join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                                                      //from dep in d.DefaultIfEmpty()
                                                      //join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                                                      //from dev in de.DefaultIfEmpty()
                                                      //join des in objModel.tbl_designation on e.fkDesignation_Code equals des.Code into desg
                                                      //from des in desg.DefaultIfEmpty()
                                                      //join et in objModel.tbl_employeetype on e.fkEmployeeType_Code equals et.Code into ety
                                                      //from et in ety.DefaultIfEmpty()
                                                  where (e.IsDelete == false || e.IsDelete == null)
                                                  select new EmployeeDTO
                                                  {
                                                      Code = e.Code,
                                                      Employee_ID = e.Employee_ID,
                                                      Employee_Name = e.Employee_Name,
                                                      Employee_Photo = e.Employee_Photo,
                                                      Card_No = e.Card_No,
                                                      Active = e.Active,
                                                      Location = loc.Description,
                                                      FingerPrint = e.FingerPrint,
                                                      Face = e.Face,
                                                      Palm = e.Palm,
                                                      Password = e.Password,
                                                      fkLocation_Code = e.fkLocation_Code,
                                                      Empty = ""

                                                  }).OrderBy(x => x.Employee_ID);





                if (!string.IsNullOrEmpty(locationFilter))
                {
                    string[] s = locationFilter.Split(',');
                    query = query.Where(x => s.Contains(x.fkLocation_Code.ToString()));
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    string[] s = statusFilter.Split(',');
                    query = query.Where(x => s.Contains(x.Active.ToString()));
                }

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x =>
                    x.Employee_ID.Contains(param.sSearch)
                    || x.Employee_Name.Contains(param.sSearch));

                }
                list = query.ToList().OrderBy(x => long.Parse(x.Employee_ID)).ToList();
                totalRecords = list.Count();
                ///skipping
                if (param.iDisplayLength != -1)
                {

                    if (totalRecords > param.iDisplayStart)
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

        public int RestoreUsersList()
        {
            int count = 0;
            try
            {
                var ls = objModel.tbl_employee.Where(x => x.IsDelete == true).ToList();
                if (ls.Count() > 0)
                {
                    count = ls.Count();
                }

            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return count;
        }


        public List<Employee_P> GetEmployeeList(string[] list, string deviceID)
        {
            Employee_P employee = new Employee_P();
            List<Employee_P> employeeList = new List<Employee_P>();
            try
            {
                foreach (var eList in list)
                {
                    string userID = eList.ToString();

                    employee = (from d in objModel.tbl_realtime_enroll_data
                                join dev in objModel.tbl_device on d.device_id equals dev.Device_ID into l
                                from dev in l.DefaultIfEmpty()
                                where d.device_id == deviceID && d.user_id == userID
                                select new Employee_P
                                {
                                    Device_Id = d.device_id,
                                    Update_Date = d.update_time,
                                    Cmd_Param = d.user_data,
                                    Employee_ID = d.user_id,
                                    Device_Name = dev.Device_Name

                                }).FirstOrDefault();

                    //tbl_device device = objModel.tbl_device.Where(x => x.Device_ID == deviceID).FirstOrDefault();
                    //if (device != null)
                    //{
                    //    employee.fkLocation_Code = !string.IsNullOrEmpty(device.Device_Location) ? Formatter.SetValidValueToInt(device.Device_Location) : 1;
                    //}
                    employeeList.Add(employee);
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

            return employeeList;
        }
        public Employee_P GetEmployeeByCode(int Code)
        {

            Employee_P employee = new Employee_P();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            from loc in l.DefaultIfEmpty()
                            join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            from dep in d.DefaultIfEmpty()
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where e.Code == Code && (e.IsDelete == false || e.IsDelete == null)
                            select new Employee_P
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
                                Valid_DateStart = e.Valid_DateStart,
                                Valid_DateEnd = e.Valid_DateEnd,
                                Sunday = e.Sunday,
                                Monday = e.Monday,
                                Tuesday = e.Tuesday,
                                Wednesday = e.Wednesday,
                                Thursday = e.Thursday,
                                Friday = e.Friday,
                                Saturday = e.Saturday
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
            return employee;

        }
        public List<Employee_P> GetAllUserIds(string dev_ID)
        {
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = (from d in objModel.tbl_fkcmd_trans_cmd_result_user_id_list
                        select new Employee_P
                        {
                            Trans_Id = d.trans_id,
                            Device_Id = d.device_id,
                            Employee_ID = d.user_id,
                            Backup_Number = d.backup_number
                        }).Where(d => d.Device_Id == dev_ID).ToList();

                list = list.GroupBy(p => p.Employee_ID).Select(g => g.First()).ToList();
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
        public int GetEmployeeCount()
        {

            int Count = 0;
            try
            {
                Count = objModel.tbl_employee.Where(x => x.IsDelete != true).Count();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return Count;

        }
        public Employee_P GetEmployeeByID(string Id)
        {

            Employee_P employee = new Employee_P();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            from loc in l.DefaultIfEmpty()
                            join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            from dep in d.DefaultIfEmpty()
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            join gp in objModel.tbl_workHourPolicy on e.WorkHourPolicyCode equals gp.Code into g_policy
                            from gp in g_policy.DefaultIfEmpty()
                            where e.Employee_ID == Id && (e.IsDelete == false || e.IsDelete == null)
                            select new Employee_P
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
                                Valid_DateStart = e.Valid_DateStart,
                                Valid_DateEnd = e.Valid_DateEnd,
                                Sunday = e.Sunday,
                                Monday = e.Monday,
                                Tuesday = e.Tuesday,
                                Wednesday = e.Wednesday,
                                Thursday = e.Thursday,
                                Friday = e.Friday,
                                Saturday = e.Saturday,
                                WorkHourPolicyCode = e.WorkHourPolicyCode
                                
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
            return employee;

        }
        public Employee_P GetEmployeeByID(string Id, string empName)
        {

            Employee_P employee = new Employee_P();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            from loc in l.DefaultIfEmpty()
                            join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            from dep in d.DefaultIfEmpty()
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where e.Employee_ID == Id && e.Employee_Name == empName && (e.IsDelete == false || e.IsDelete == null)
                            select new Employee_P
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
                                Valid_DateStart = e.Valid_DateStart,
                                Valid_DateEnd = e.Valid_DateEnd,
                                Sunday = e.Sunday,
                                Monday = e.Monday,
                                Tuesday = e.Tuesday,
                                Wednesday = e.Wednesday,
                                Thursday = e.Thursday,
                                Friday = e.Friday,
                                Saturday = e.Saturday
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
            return employee;

        }
        public Employee_P GetEmployeeByIDIsDeleted(string Id)
        {

            Employee_P employee = new Employee_P();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                            from loc in l.DefaultIfEmpty()
                            join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                            from dep in d.DefaultIfEmpty()
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where e.Employee_ID == Id && e.IsDelete == true
                            select new Employee_P
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
                                Valid_DateStart = e.Valid_DateStart,
                                Valid_DateEnd = e.Valid_DateEnd,
                                Sunday = e.Sunday,
                                Monday = e.Monday,
                                Tuesday = e.Tuesday,
                                Wednesday = e.Wednesday,
                                Thursday = e.Thursday,
                                Friday = e.Friday,
                                Saturday = e.Saturday
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
            return employee;

        }
        public List<Employee_P> GetEmployeeBydevID(string Id)
        {

            List<Employee_P> employee = new List<Employee_P>();
            try
            {
                employee = (from e in objModel.tbl_employee
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where e.Device_Id == Id && (e.IsDelete == false || e.IsDelete == null)
                            select new Employee_P
                            {
                                Code = e.Code,
                                Employee_ID = e.Employee_ID,
                                Active = e.Active,
                                Device_Id = e.Device_Id,
                                FingerPrint = e.FingerPrint,
                                Face = e.Face,
                                Palm = e.Palm
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
            return employee;

        }



        public bool AddUpdateEmployee(Employee_P employee)
        {
            bool flag = false;
            try
            {
                //int id = Formatter.SetValidValueToInt(employee.Employee_ID);
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == employee.Employee_ID && x.IsDelete != true).FirstOrDefault();

                if (e == null)
                    e = new tbl_employee();

                e.Employee_ID = employee.Employee_ID;
                e.Employee_Name = employee.Employee_Name;
                e.Employee_Photo = employee.Employee_Photo;
                e.Email = employee.Email;
                e.Password = employee.Password;
                e.Card_No = employee.Card_No;
                e.Active = employee.Active;
                e.Gender = employee.Gender;
                e.Address = employee.Address;
                e.fkDepartment_Code = employee.fkDepartment_Code;
                e.fkLocation_Code = employee.fkLocation_Code;
                e.fkDesignation_Code = employee.fkDesignation_Code;
                e.fkEmployeeType_Code = employee.fkEmployeeType_Code;
                e.Telephone = employee.Telephone;
                e.User_Privilege = employee.User_Privilege;
                e.FingerPrint = employee.FingerPrint;
                e.Face = employee.Face;
                e.Palm = employee.Palm;
                e.Password = employee.Password;

                e.Trans_Id = employee.Trans_Id;
                e.Update_Date = DateTime.Now;
                e.Cmd_Param = employee.Cmd_Param;
                e.Device_Id = employee.Device_Id;

                e.finger_0 = employee.finger_0;
                e.finger_1 = employee.finger_1;
                e.finger_2 = employee.finger_2;
                e.finger_3 = employee.finger_3;
                e.finger_4 = employee.finger_4;
                e.finger_5 = employee.finger_5;
                e.finger_6 = employee.finger_6;
                e.finger_7 = employee.finger_7;
                e.finger_8 = employee.finger_8;
                e.finger_9 = employee.finger_9;
                e.face_data = employee.face_data;
                e.palm_0 = employee.palm_0;
                e.palm_1 = employee.palm_1;
                e.photo_data = employee.photo_data;
                e.Valid_DateStart = employee.Valid_DateStart;
                e.Valid_DateEnd = employee.Valid_DateEnd;
                e.Sunday = employee.Sunday;
                e.Monday = employee.Monday;
                e.Tuesday = employee.Tuesday;
                e.Wednesday = employee.Wednesday;
                e.Thursday = employee.Thursday;
                e.Friday = employee.Friday;
                e.Saturday = employee.Saturday;
                e.IsDelete = false;
                e.WorkHourPolicyCode = employee.WorkHourPolicyCode;
                if (e.Code == 0)
                {
                    objModel.tbl_employee.Add(e);
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

        public bool TransferByMachine(Employee_P employee)
        {
            bool flag = false;
            try
            {
                //int id = Formatter.SetValidValueToInt(employee.Employee_ID);
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == employee.Employee_ID).FirstOrDefault();

                if (e == null)
                    e = new tbl_employee();

                e.Employee_ID = employee.Employee_ID;
                e.Employee_Name = employee.Employee_Name;
                e.Employee_Photo = employee.Employee_Photo;
                e.Email = employee.Email;
                e.fkDepartment_Code = employee.fkDepartment_Code;
                e.fkLocation_Code = employee.fkLocation_Code;
                e.Password = employee.Password;
                e.Card_No = employee.Card_No;
                e.Active = employee.Active;
                e.Employee_ID = employee.Employee_ID;
                e.Employee_Name = employee.Employee_Name;
                e.Employee_Photo = employee.Employee_Photo;
                e.Email = employee.Email;
                e.Card_No = employee.Card_No;
                e.Active = employee.Active;
                e.Gender = employee.Gender;
                e.Address = employee.Address;
                e.fkDesignation_Code = employee.fkDesignation_Code;
                e.fkEmployeeType_Code = employee.fkEmployeeType_Code;
                e.Telephone = employee.Telephone;
                e.User_Privilege = employee.User_Privilege;
                e.FingerPrint = employee.FingerPrint;
                e.Face = employee.Face;
                e.Palm = employee.Palm;
                e.Password = employee.Password;

                e.Trans_Id = employee.Trans_Id;
                e.Update_Date = DateTime.Now;
                e.Cmd_Param = employee.Cmd_Param;
                e.Device_Id = employee.Device_Id;

                e.finger_0 = employee.finger_0;
                e.finger_1 = employee.finger_1;
                e.finger_2 = employee.finger_2;
                e.finger_3 = employee.finger_3;
                e.finger_4 = employee.finger_4;
                e.finger_5 = employee.finger_5;
                e.finger_6 = employee.finger_6;
                e.finger_7 = employee.finger_7;
                e.finger_8 = employee.finger_8;
                e.finger_9 = employee.finger_9;
                e.face_data = employee.face_data;
                e.palm_0 = employee.palm_0;
                e.palm_1 = employee.palm_1;
                e.photo_data = employee.photo_data;
                e.Valid_DateStart = employee.Valid_DateStart;
                e.Valid_DateEnd = employee.Valid_DateEnd;
                e.Sunday = employee.Sunday;
                e.Monday = employee.Monday;
                e.Tuesday = employee.Tuesday;
                e.Wednesday = employee.Wednesday;
                e.Thursday = employee.Thursday;
                e.Friday = employee.Friday;
                e.Saturday = employee.Saturday;
                e.IsDelete = false;

                if (e.Code == 0)
                {
                    objModel.tbl_employee.Add(e);
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

        public bool UploadEmployee(Employee_P employee, Employee_P encemployee)
        {
            bool flag = false;
            try
            {
                //foreach (var emp in employee)
                //{
                string id = employee.Employee_ID;
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == id).FirstOrDefault();

                if (e == null)
                    e = new tbl_employee();

                e.Employee_ID = employee.Employee_ID;
                e.Employee_Name = employee.Employee_Name;
                if (employee.Employee_Photo != null)
                    e.Employee_Photo = employee.Employee_Photo;
                else
                    e.Employee_Photo = "";
                //e.Email = employee.Email;
                if (employee.Card_No != null)
                    e.Card_No = employee.Card_No;
                else
                    e.Card_No = "";

                e.Active = 1;
                //e.fkLocation_Code = employee.fkLocation_Code;
                //e.fkDepartment_Code = e.fkDepartment_Code;
                //e.Gender = employee.Gender;
                //e.fkDesignation_Code = employee.fkDesignation_Code;
                //e.fkEmployeeType_Code = employee.fkEmployeeType_Code;
                //e.Telephone = employee.Telephone;
                e.User_Privilege = employee.User_Privilege;
                e.FingerPrint = employee.FingerPrint;
                e.Face = employee.Face;
                e.Palm = employee.Palm;
                if (employee.Password != null)
                    e.Password = employee.Password;
                else
                    e.Password = "";


                e.Trans_Id = encemployee.Trans_Id;
                e.Update_Date = encemployee.Update_Date;
                e.Cmd_Param = encemployee.Cmd_Param;
                e.Device_Id = encemployee.Device_Id;

                e.finger_0 = employee.finger_0;
                e.finger_1 = employee.finger_1;
                e.finger_2 = employee.finger_2;
                e.finger_3 = employee.finger_3;
                e.finger_4 = employee.finger_4;
                e.finger_5 = employee.finger_5;
                e.finger_6 = employee.finger_6;
                e.finger_7 = employee.finger_7;
                e.finger_8 = employee.finger_8;
                e.finger_9 = employee.finger_9;
                e.face_data = employee.face_data;
                e.palm_0 = employee.palm_0;
                e.palm_1 = employee.palm_1;
                e.photo_data = employee.photo_data;
                e.fkLocation_Code = employee.fkLocation_Code;
                e.IsDelete = false;
                if (e.Code == 0)
                {
                    objModel.tbl_employee.Add(e);
                }

                int res = objModel.SaveChanges();
                if (res > 0)
                    flag = true;

                // }
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


        public bool DeleteEmployeeByCode(string deviceCode)
        {
            bool flag = false;
            try
            {
                List<tbl_realtime_enroll_data> e = objModel.tbl_realtime_enroll_data.Where(y => y.device_id == deviceCode).ToList();
                foreach (var detail in e)
                {
                    objModel.tbl_realtime_enroll_data.Remove(detail);
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
        public bool DeleteEmployee(string Code)
        {
            bool flag = false;
            try
            {
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == Code).FirstOrDefault();
                objModel.tbl_employee.Remove(e);

                //e.IsDelete = true;
                //objModel.Entry(e).State = System.Data.Entity.EntityState.Modified;
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
        public bool DeleteEmployeeByDevice_ID(string deviceCode)
        {
            bool flag = false;
            try
            {
                List<tbl_fkcmd_trans_cmd_result_user_id_list> e = objModel.tbl_fkcmd_trans_cmd_result_user_id_list.Where(y => y.device_id == deviceCode).ToList();
                foreach (var detail in e)
                {
                    objModel.tbl_fkcmd_trans_cmd_result_user_id_list.Remove(detail);
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
        public List<Employee_P> GetAllDeletedUsers()
        {

            List<Employee_P> list = new List<Employee_P>();
            try
            {
                list = (from e in objModel.tbl_employee
                        where e.IsDelete == true
                        select new Employee_P
                        {
                            Code = e.Code,
                            Employee_ID = e.Employee_ID,
                            Employee_Name = e.Employee_Name,
                            Card_No = e.Card_No,
                            Gender = e.Gender,
                            Device_Id = e.Device_Id,
                            Active = e.Active
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
        public bool IsDeleteEmployee(int Code)
        {
            bool flag = false;
            try
            {
                tbl_employee e = objModel.tbl_employee.Where(x => x.Code == Code).FirstOrDefault();
                e.IsDelete = true;
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
        public bool RestoreUserFromDB(string Code, int action)
        {
            bool flag = false;
            try
            {
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == Code).FirstOrDefault();
                e.IsDelete = false;

                if (action == 1)
                    e.Active = 0;
                else if (action == 2 && e.Active == 0)
                    e.Active = 1;

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
        public bool RestoreUserFromDB(string Code, string empName, int action)
        {
            bool flag = false;
            try
            {
                tbl_employee e = objModel.tbl_employee.Where(x => x.Employee_ID == Code && x.Employee_Name == empName).FirstOrDefault();
                e.IsDelete = false;

                //if (action == 1)
                //    e.Active = 0;
                //else if (action == 2 && e.Active == 0)
                //    e.Active = 1;

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

        public bool CheckEmployeeExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Employee_ID == CODE && x.IsDelete != true).FirstOrDefault();
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
        public bool CheckEmployeeExistInDB(string CODE, out string Employee_Name)
        {
            bool flag = false;
            Employee_Name = "";
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Employee_ID == CODE).FirstOrDefault();
                if (d != null)
                {
                    flag = true;
                    Employee_Name = d.Employee_Name;

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
        public bool CheckCardExistInDB(string Card, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Card_No == Card).FirstOrDefault();
                if (d != null)
                {
                    flag = true;
                    employee.Code = d.Code;
                    employee.Employee_Name = d.Employee_Name;
                    employee.Card_No = d.Card_No;
                    employee.Password = d.Password;
                    employee.Employee_ID = d.Employee_ID;

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
        public bool CheckPasswordExistInDB(string PWD, string CODE)
        {
            bool flag = false;
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Password == PWD && x.Employee_ID != CODE).FirstOrDefault();
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
        public bool CheckPwdExistInDB(string PWD, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Password == PWD).FirstOrDefault();
                if (d != null)
                {
                    flag = true;
                    employee.Code = d.Code;
                    employee.Employee_Name = d.Employee_Name;
                    employee.Card_No = d.Card_No;
                    employee.Password = d.Password;
                    employee.Employee_ID = d.Employee_ID;

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
        public bool CheckCardExistInDB(string CardNo, string CODE)
        {
            bool flag = false;
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Card_No == CardNo && x.Employee_ID != CODE && x.IsDelete != true).FirstOrDefault();
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
        public Employee_P GetIrregularEmployeeByCode(int Code)
        {

            Employee_P employee = new Employee_P();
            try
            {
                employee = (from e in objModel.tbl_irregularemployee
                            join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                            from dev in de.DefaultIfEmpty()
                            where e.Code == Code
                            select new Employee_P
                            {
                                IrregularCode = e.Code.ToString(),
                                Employee_ID = e.Employee_ID,
                                Employee_Name = e.Employee_Name,
                                Employee_Photo = e.Employee_Photo,
                                Email = e.Email,
                                Card_No = e.Card_No,
                                Active = e.Active,
                                fkLocation_Code = e.fkLocation_Code,
                                fkDepartment_Code = e.fkDepartment_Code,
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
                                photo_data = e.photo_data
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
            return employee;

        }

        //Using in Restore method in User Controller
        public bool CheckCardExistInDB(string emp_id, out int existingEmpID)
        {
            bool flag = false;
            existingEmpID = 0;
            var employee = new Employee_P();
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Employee_ID == emp_id).FirstOrDefault();
                if (d.Card_No != "" )
                {
                    //check how many time this card exist in db
                    List<tbl_employee> checkingCard = objModel.tbl_employee.Where(x => x.Card_No == d.Card_No).ToList();
                    if (checkingCard.Count() == 1)
                    {
                        tbl_employee userManagement = objModel.tbl_employee.Where(x => x.Card_No == d.Card_No && x.IsDelete == false).FirstOrDefault();
                        existingEmpID = Formatter.SetValidValueToInt(userManagement.Employee_ID);
                        if (d.Employee_ID == checkingCard.Select(x => x.Employee_ID).FirstOrDefault())
                        {
                            flag = false;

                        }

                    }
                    else if (checkingCard.Count() > 1)
                    {
                        //check user in User Management is there any user from this same id card no.
                        tbl_employee userManagement = objModel.tbl_employee.Where(x => x.Card_No == d.Card_No && x.IsDelete == false).FirstOrDefault();
                        existingEmpID = Formatter.SetValidValueToInt(userManagement.Employee_ID);
                        if (userManagement == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            foreach (var item in checkingCard)
                            {
                                if (item.Employee_ID != d.Employee_ID)
                                    flag = true;
                            }
                        }

                    }
                    else
                    {
                        flag = false;
                    }

                }
                else
                {
                    flag = false;
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

        public bool CheckUserExistInDB(string CODE, out Employee_P employee)
        {
            bool flag = false;
            employee = new Employee_P();
            try
            {
                tbl_employee d = objModel.tbl_employee.Where(x => x.Employee_ID == CODE && x.IsDelete == true).FirstOrDefault();
                if (d != null)
                {
                    flag = true;
                    employee.Code = d.Code;
                    employee.Employee_Name = d.Employee_Name;
                    employee.Card_No = d.Card_No;
                    employee.Password = d.Password;
                    employee.Employee_ID = d.Employee_ID;

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

        public List<Employee_P> GetExistingEmployee(string user_id, string Card_No, string Password)
        {
            List<Employee_P> employee = new List<Employee_P>();
            try
            {
                employee = (from e in objModel.tbl_employee
                            where e.Employee_ID == user_id || e.Card_No == Card_No || e.Password == Password
                            select new Employee_P
                            {
                                Code = e.Code,
                                Employee_ID = e.Employee_ID,
                                Employee_Name = e.Employee_Name,
                                Employee_Photo = e.Employee_Photo,
                                Email = e.Email,
                                Card_No = e.Card_No,
                                Active = e.Active,
                                fkLocation_Code = e.fkLocation_Code,
                                fkDepartment_Code = e.fkDepartment_Code,
                                Device_Id = e.Device_Id,
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
                                photo_data = e.photo_data
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
            return employee;

        }

        public bool CheckRealEmployeeExistInDB(string CODE, string Device_ID)
        {
            bool flag = false;
            try
            {
                tbl_realtime_enroll_data d = objModel.tbl_realtime_enroll_data.Where(x => x.user_id == CODE && x.device_id == Device_ID).FirstOrDefault();
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

        public bool checkEmployeeIsNotDeleted(string empID)
        {
            bool flag = false;
            try
            {
                var isDeletedEmp = objModel.tbl_employee.Where(x => x.Employee_ID == empID && x.IsDelete == true).FirstOrDefault();
                if (isDeletedEmp != null)
                {
                    if (isDeletedEmp.IsDelete == true)
                        flag = true;
                    else
                        flag = false;
                }
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return flag;
        }


        //report

        public List<Employee_P> GetEmployeesWithAttCount(string dt)
        {
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt + " 23:59:59");


                list = (from e in objModel.tbl_employee
                        join loc in objModel.tbl_location on e.fkLocation_Code equals loc.Code into l
                        from loc in l.DefaultIfEmpty()
                        join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                        from dep in d.DefaultIfEmpty()
                        join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                        from dev in de.DefaultIfEmpty()
                        join des in objModel.tbl_designation on e.fkDesignation_Code equals des.Code into desg
                        from des in desg.DefaultIfEmpty()
                        join et in objModel.tbl_employeetype on e.fkEmployeeType_Code equals et.Code into ety
                        from et in ety.DefaultIfEmpty()
                        where (e.IsDelete == false || e.IsDelete == null)
                        select new Employee_P
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
                            Designation = des.Description,
                            EmployeeType = et.Description,
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
                            Valid_DateStart = e.Valid_DateStart,
                            Valid_DateEnd = e.Valid_DateEnd,
                            Sunday = e.Sunday,
                            Monday = e.Monday,
                            Tuesday = e.Tuesday,
                            Wednesday = e.Wednesday,
                            Thursday = e.Thursday,
                            Friday = e.Friday,
                            Saturday = e.Saturday,
                            attcount = objModel.tbl_attendence.Where(x => x.Employee_ID == e.Employee_ID && x.Attendance_DateTime >= StartDate && x.Attendance_DateTime <= EndDate).Count()
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

        public List<Employee_P> GetEmployeesWParam(JqueryDatatableParam param, string devices, out int TotalRecords, out int FilteredRecords)
        {
            TotalRecords = 0;
            FilteredRecords = 0;
            List<Employee_P> list = new List<Employee_P>();
            try
            {
                objModel.Database.CommandTimeout = 86400;

                IQueryable<Employee_P> query =
                    objModel.tbl_employee
                    .Where(x => x.IsDelete != true)
                    .Select(e => new Employee_P
                    {
                        Code = e.Code,
                        Employee_ID = e.Employee_ID,
                        Employee_Name = e.Employee_Name,
                        Employee_Photo = e.Employee_Photo,
                        Email = e.Email,
                        Card_No = e.Card_No,
                        Active = e.Active,
                        fkLocation_Code = e.fkLocation_Code,
                        fkDepartment_Code = e.fkDepartment_Code,
                        Device_Id = e.Device_Id,
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
                        Valid_DateStart = e.Valid_DateStart,
                        Valid_DateEnd = e.Valid_DateEnd,
                        Sunday = e.Sunday,
                        Monday = e.Monday,
                        Tuesday = e.Tuesday,
                        Wednesday = e.Wednesday,
                        Thursday = e.Thursday,
                        Friday = e.Friday,
                        Saturday = e.Saturday,
                        WorkHourPolicyCode = e.WorkHourPolicyCode,
                    }).OrderByDescending(x => x.Employee_ID);


                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x => x.Employee_ID.ToLower().Contains(param.sSearch.ToLower())
                    || x.Employee_ID.ToLower().Contains(param.sSearch.ToLower())
                    );
                }

                if (!string.IsNullOrEmpty(devices))
                {
                    string[] d = devices.Split(',');
                    if (d.Count() > 0)
                    {
                        query = query.Where(x => d.Contains(x.Device_Id));
                    }
                }



                TotalRecords = query.Count();

                if (param.iDisplayLength != -1)
                {
                    query = query.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength);
                }

                list = query.ToList();

                int index = 0;
                foreach (var item in list)
                {

                    tbl_device device = objModel.tbl_device.Where(x => x.Device_ID == item.Device_Id).FirstOrDefault();
                    if (device != null)
                        item.Device_Name = device.Device_Name;

                    tbl_location location = objModel.tbl_location.Where(x => x.Code == item.fkLocation_Code).FirstOrDefault();
                    if (location != null)
                        item.Location = location.Description;

                    tbl_designation designation = objModel.tbl_designation.Where(x => x.Code == item.fkDesignation_Code).FirstOrDefault();
                    if (designation != null)
                        item.Designation = designation.Description;

                    tbl_department department = objModel.tbl_department.Where(x => x.Code == item.fkDesignation_Code).FirstOrDefault();
                    if (department != null)
                        item.Department = department.Description;

                    tbl_employeetype type = objModel.tbl_employeetype.Where(x => x.Code == item.fkEmployeeType_Code).FirstOrDefault();
                    if (type != null)
                        item.EmployeeType = type.Description;


                    index++;
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

        public List<string> GetRecordsByFilter(string loc, string status)

        {

            List<EmployeeDTO> list = new List<EmployeeDTO>();
            List<string> Codes = new List<string>();
            try
            {
                objModel.Database.CommandTimeout = 86400;
                IEnumerable<EmployeeDTO> query = (from e in objModel.tbl_employee
                                                  join location in objModel.tbl_location on e.fkLocation_Code equals location.Code into l
                                                  from location in l.DefaultIfEmpty()
                                                      //join dep in objModel.tbl_department on e.fkDepartment_Code equals dep.Code into d
                                                      //from dep in d.DefaultIfEmpty()
                                                      //join dev in objModel.tbl_device on e.Device_Id equals dev.Device_ID into de
                                                      //from dev in de.DefaultIfEmpty()
                                                      //join des in objModel.tbl_designation on e.fkDesignation_Code equals des.Code into desg
                                                      //from des in desg.DefaultIfEmpty()
                                                      //join et in objModel.tbl_employeetype on e.fkEmployeeType_Code equals et.Code into ety
                                                      //from et in ety.DefaultIfEmpty()
                                                  where (e.IsDelete == false || e.IsDelete == null)
                                                  select new EmployeeDTO
                                                  {
                                                      Code = e.Code,
                                                      Employee_ID = e.Employee_ID,
                                                      Employee_Name = e.Employee_Name,
                                                      Employee_Photo = e.Employee_Photo,
                                                      Card_No = e.Card_No,
                                                      Active = e.Active,
                                                      Location = location.Description,
                                                      FingerPrint = e.FingerPrint,
                                                      Face = e.Face,
                                                      Palm = e.Palm,
                                                      Password = e.Password,
                                                      fkLocation_Code = e.fkLocation_Code,
                                                      Empty = ""

                                                  }).OrderBy(x => x.Employee_ID);





                if (!string.IsNullOrEmpty(loc))
                {
                    string[] s = loc.Split(',');
                    query = query.Where(x => s.Contains(x.fkLocation_Code.ToString()));
                }

                if (!string.IsNullOrEmpty(status))
                {
                    string[] s = status.Split(',');
                    query = query.Where(x => s.Contains(x.Active.ToString()));
                }
                list = query.OrderBy(x => Int32.Parse(x.Employee_ID)).ToList();

                Codes = list.Select(x => x.Code.ToString()).ToList();


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
            return Codes;






        }

    }
}