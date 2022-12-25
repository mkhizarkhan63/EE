using System;
using System.Collections.Generic;
using System.Linq;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;
using System.Data.Entity;
using EagleEye.Models;
using EagleEye.DAL.Partial;
using static EagleEye.Common.ExceptionLogger;
using static EagleEye.Common.Enumeration;
using Common;
using EagleEye.BLL;
using EagleEye.Common;
using EagleEye.DAL.DTO;

namespace EagleEye.DAL
{
    public class DALAttendance
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        // Att_Status_P att_status = new Att_Status_P();
        BLLAtt_Status objBLLAtt_Status = new BLLAtt_Status();
        BLLWorkCode objBLLWorkCode = new BLLWorkCode();

        public List<Attendance_P> GetAllAttendance()
        {

            List<Attendance_P> list = new List<Attendance_P>();
            try
            {
                list = (from d in objModel.tbl_attendence
                        join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                        from emp in employee.DefaultIfEmpty()
                        select new Attendance_P
                        {
                            Code = d.Code,
                            Attendance_DateTime = d.Attendance_DateTime,
                            Attendance_Photo = emp.Employee_Photo,
                            Device_ID = d.Device_ID,
                            Employee_ID = d.Employee_ID,
                            Polling_DateTime = d.Polling_DateTime,
                            Status = d.Status,
                            WorkCode = d.WorkCode,
                            DoorStatus = d.DoorStatus,
                            Verify_Mode = d.Verify_Mode,
                            Status_TIS = d.Status_TIS,
                            Status_Oracle = d.Status_Oracle,
                            Status_SQL = d.Status_SQL
                        }).AsEnumerable().Select(x => new Attendance_P
                        {
                            Code = x.Code,
                            Attendance_DateTime = x.Attendance_DateTime,
                            Attendance_Photo = x.Attendance_Photo,
                            Device_ID = x.Device_ID,
                            Employee_ID = x.Employee_ID,
                            Polling_DateTime = x.Polling_DateTime,
                            Status = objBLLAtt_Status.GetStatusName(Formatter.SetValidValueToInt(x.Status)).Name,
                            WorkCode = x.WorkCode_Description,
                            DoorStatus = x.DoorStatus,
                            Device = x.Device,
                            Employee = x.Employee,
                            DateTime = x.Attendance_DateTime.ToString(),
                            Status_TIS = x.Status_TIS,
                            Status_Oracle = x.Status_Oracle,
                            Status_SQL = x.Status_SQL
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

        public List<Attendance_P> GetAttendanceByUser(string Employee_ID)
        {

            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = (from d in objModel.tbl_attendence
                       join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                       from emp in employee.DefaultIfEmpty()
                       where d.Employee_ID == Employee_ID
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Attendance_Photo = emp.Employee_Photo,
                           Device_ID = d.Device_ID,
                           Employee_ID = d.Employee_ID,
                           Polling_DateTime = d.Polling_DateTime,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Verify_Mode = d.Verify_Mode,
                           Status_TIS = d.Status_TIS,
                           Status_Oracle = d.Status_Oracle,
                           Status_SQL = d.Status_SQL
                       }).AsEnumerable().Select(x => new Attendance_P
                       {
                           Code = x.Code,
                           Attendance_DateTime = x.Attendance_DateTime,
                           Attendance_Photo = x.Attendance_Photo,
                           Device_ID = x.Device_ID,
                           Employee_ID = x.Employee_ID,
                           Polling_DateTime = x.Polling_DateTime,
                           WorkCode = objBLLWorkCode.GetWorkCodeName(Formatter.SetValidValueToInt(x.WorkCode)).Name,
                           Status = objBLLAtt_Status.GetStatusName(Formatter.SetValidValueToInt(x.Status)).Name,
                           //Status = x.Status_Description,
                           //WorkCode = x.WorkCode_Description,
                           DoorStatus = x.DoorStatus,
                           Device = x.Device,
                           Employee = x.Employee,
                           DateTime = x.Attendance_DateTime.ToString(),
                           Status_TIS = x.Status_TIS,
                           Status_Oracle = x.Status_Oracle,
                           Status_SQL = x.Status_SQL
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
            return att;

        }

        public List<Attendance_P> GetAttendanceByDevice(string DeviceID)
        {

            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                att = (from d in objModel.tbl_attendence
                       join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                       from emp in employee.DefaultIfEmpty()
                       where d.Device_ID == DeviceID
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Attendance_Photo = emp.Employee_Photo,
                           Device_ID = d.Device_ID,
                           Employee_ID = d.Employee_ID,
                           Polling_DateTime = d.Polling_DateTime,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Verify_Mode = d.Verify_Mode,
                           Status_TIS = d.Status_TIS,
                           Status_Oracle = d.Status_Oracle,
                           Status_SQL = d.Status_SQL
                       }).AsEnumerable().Select(x => new Attendance_P
                       {
                           Code = x.Code,
                           Attendance_DateTime = x.Attendance_DateTime,
                           Attendance_Photo = x.Attendance_Photo,
                           Device_ID = x.Device_ID,
                           Employee_ID = x.Employee_ID,
                           Polling_DateTime = x.Polling_DateTime,
                           Status = objBLLAtt_Status.GetStatusName(Formatter.SetValidValueToInt(x.Status)).Name,
                           // Status = x.Status_Description,
                           WorkCode = objBLLWorkCode.GetWorkCodeName(Formatter.SetValidValueToInt(x.WorkCode)).Name,
                           DoorStatus = x.DoorStatus,
                           Device = x.Device,
                           Employee = x.Employee,
                           DateTime = x.Attendance_DateTime.ToString(),
                           Status_TIS = x.Status_TIS,
                           Status_Oracle = x.Status_Oracle,
                           Status_SQL = x.Status_SQL
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
            return att;

        }

        public Attendance_P GetAttendanceByMINDate(string employee_id, string dt)
        {
            Attendance_P att = new Attendance_P();
            List<Attendance_P> att2 = new List<Attendance_P>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt + " 23:59:59");
                string status = "1";
                att2 = (from d in objModel.tbl_attendence
                        join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID.ToString() into device
                        from dev in device.DefaultIfEmpty()
                        where d.Employee_ID == employee_id && d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate
                        && d.Status == status
                        select new Attendance_P
                        {
                            Code = d.Code,
                            Attendance_DateTime = d.Attendance_DateTime,
                            Employee_ID = d.Employee_ID,
                            Status = d.Status,
                            WorkCode = d.WorkCode,
                            DoorStatus = d.DoorStatus,
                            Verify_Mode = d.Verify_Mode,
                            Device = dev.Device_Name
                        }).ToList();

                att.Attendance_DateTime = att2.Min(a => a.Attendance_DateTime);
                att.Device = att2.Min(a => a.Device);
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
            return att;
        }

        public Attendance_P GetAttendanceByMAXDate(string employee_id, string dt)
        {
            Attendance_P att = new Attendance_P();
            List<Attendance_P> att2 = new List<Attendance_P>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt + " 23:59:59");
                string status = "2";

                att2 = (from d in objModel.tbl_attendence
                        join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID.ToString() into device
                        from dev in device.DefaultIfEmpty()
                        where d.Employee_ID == employee_id && d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate
                        && d.Status == status
                        select new Attendance_P
                        {
                            Code = d.Code,
                            Attendance_DateTime = d.Attendance_DateTime,
                            Employee_ID = d.Employee_ID,
                            Status = d.Status,
                            WorkCode = d.WorkCode,
                            DoorStatus = d.DoorStatus,
                            Verify_Mode = d.Verify_Mode,
                            Device = dev.Device_Name
                        }).ToList();

                att.Attendance_DateTime = att2.Max(a => a.Attendance_DateTime);
                att.Device = att2.Max(a => a.Device);
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
            return att;
        }

        public List<Attendance_P> GetAttendanceByUSERandDATE(string employee_id, string dt)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt + " 23:59:59");

                att = (from d in objModel.tbl_attendence
                       join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                       from emp in employee.DefaultIfEmpty()
                       where d.Employee_ID == employee_id && d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Employee_ID = d.Employee_ID,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Status_Description = d.Status_Description,
                           WorkCode_Description = d.WorkCode_Description
                       }).AsEnumerable().Select(x => new Attendance_P
                       {
                           Code = x.Code,
                           Attendance_DateTime = x.Attendance_DateTime,
                           Employee_ID = x.Employee_ID,
                           Status = x.Status,
                           WorkCode = x.WorkCode,
                           WorkCode_Description = x.WorkCode_Description,
                           DoorStatus = x.DoorStatus,
                           Status_Description = x.Status_Description,
                           DateTime = Formatter.SetValidValueToDateTime(x.Attendance_DateTime).ToString("HH:mm:ss")
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
            return att;

        }

        public List<Attendance_P> GetAttendanceByUSERandDATE(string employee_id, string dt, string[] devices)
        {
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt + " 23:59:59");

                att = (from d in objModel.tbl_attendence
                       where d.Employee_ID == employee_id && d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Employee_ID = d.Employee_ID,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Status_Description = d.Status_Description,
                           WorkCode_Description = d.WorkCode_Description,
                           Device_ID = d.Device_ID
                       }).ToList();
                //.AsEnumerable().Select(x => new Attendance_P
                // {
                //     Code = x.Code,
                //     Attendance_DateTime = x.Attendance_DateTime,
                //     Employee_ID = x.Employee_ID,
                //     Status = x.Status,
                //     WorkCode = x.WorkCode,
                //     WorkCode_Description = x.WorkCode_Description,
                //     DoorStatus = x.DoorStatus,
                //     Status_Description = x.Status_Description,
                //     DateTime = Formatter.SetValidValueToDateTime(x.Attendance_DateTime).ToString("HH:mm:ss"),
                //     Device_ID = x.Device_ID

                // })
                if (devices != null)
                {
                    devices = devices.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    if (devices.Count() > 0)
                    {
                        att = att.Where(x => devices.Contains(x.Device_ID)).ToList();
                    }
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
            return att;

        }

        public List<Attendance_P> GetAttendanceByDateForList(string Start, string End)
        {
            List<Attendance_P> ls = new List<Attendance_P>();
            DateTime StartDate = Formatter.SetValidValueToDateTime(Start + " 00:00:00");
            DateTime EndDate = Formatter.SetValidValueToDateTime(End + " 23:59:59");
            try
            {

                ls = (from d in objModel.tbl_attendence
                      join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                      from emp in employee.DefaultIfEmpty()
                      join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                      from dev in device.DefaultIfEmpty()
                          //join st in objModel.tbl_att_status on d.Status equals st.Code into status
                          //from st in status.DefaultIfEmpty()
                      where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate && emp.IsDelete == false
                      select new Attendance_P
                      {
                          Code = d.Code,
                          Attendance_DateTime = d.Attendance_DateTime,
                          Attendance_Photo = emp.Employee_Photo,
                          Verify_Mode = d.Verify_Mode,
                          Device_ID = d.Device_ID,
                          Employee_ID = d.Employee_ID,
                          Polling_DateTime = d.Polling_DateTime,
                          Status = d.Status,
                          WorkCode = d.WorkCode,
                          DoorStatus = d.DoorStatus,
                          Device = dev.Device_Name,
                          Employee = d.Employee_Name,
                          DateTime = d.Attendance_DateTime.ToString(),
                          Status_TIS = d.Status_TIS,
                          WorkCode_Description = d.WorkCode_Description,
                          Status_Description = d.Status_Description,
                          Status_Oracle = d.Status_Oracle,
                          Status_SQL = d.Status_SQL,

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

            return ls;
        }


        public List<Attendance_P> GetAttendanceByDate(JqueryDatatableParam param, string Start, string End, out int TotalRecords)
        {
            TotalRecords = 0;
            List<Attendance_P> att = new List<Attendance_P>();
            try
            {

                DateTime StartDate = Formatter.SetValidValueToDateTime(Start + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(End + " 23:59:59");

                objModel.Database.CommandTimeout = 86400;
                IQueryable<Attendance_P> query = (from d in objModel.tbl_attendence
                                                  join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                                                  from emp in employee.DefaultIfEmpty()
                                                  join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                                                  from dev in device.DefaultIfEmpty()
                                                      //join st in objModel.tbl_att_status on d.Status equals st.Code into status
                                                      //from st in status.DefaultIfEmpty()
                                                  where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate && emp.IsDelete == false
                                                  select new Attendance_P
                                                  {
                                                      Code = d.Code,
                                                      Attendance_DateTime = d.Attendance_DateTime,
                                                      Attendance_Photo = emp.Employee_Photo,
                                                      Verify_Mode = d.Verify_Mode,
                                                      Device_ID = d.Device_ID,
                                                      Employee_ID = d.Employee_ID,
                                                      Polling_DateTime = d.Polling_DateTime,
                                                      Status = d.Status,
                                                      WorkCode = d.WorkCode,
                                                      DoorStatus = d.DoorStatus,
                                                      Device = dev.Device_Name,
                                                      Employee = d.Employee_Name,
                                                      DateTime = d.Attendance_DateTime.ToString(),
                                                      // DateTime = Formatter.SetValidValueToDateTime(d.Attendance_DateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                                                      Status_TIS = d.Status_TIS,
                                                      WorkCode_Description = d.WorkCode_Description,
                                                      Status_Description = d.Status_Description,
                                                      Status_Oracle = d.Status_Oracle,
                                                      Status_SQL = d.Status_SQL,

                                                  }).OrderByDescending(x => x.Attendance_DateTime);

                //att = query.Take(10).ToList();
                //}).OrderByDescending(x => x.DateTime).AsEnumerable().Select(x => new Attendance_P
                //{
                //    Code = x.Code,
                //    Attendance_DateTime = x.Attendance_DateTime,
                //    Attendance_Photo = x.Attendance_Photo,
                //    Verify_Mode = x.Verify_Mode,
                //    Device_ID = x.Device_ID,
                //    Employee_ID = x.Employee_ID,
                //    Polling_DateTime = x.Polling_DateTime,
                //    Status = x.Status,
                //    WorkCode = x.WorkCode,
                //    DoorStatus = x.DoorStatus,
                //    WorkCode_Description = x.WorkCode_Description,
                //    Status_Description = x.Status_Description,
                //    Device = x.Device,
                //    Employee = x.Employee,
                //    DateTime = Formatter.SetValidValueToDateTime(x.Attendance_DateTime).ToString("dd-MM-yyyy HH:mm:ss"),
                //    Status_TIS = x.Status_TIS,
                //    Status_Oracle = x.Status_Oracle,
                //    Status_SQL = x.Status_SQL
                //});

                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x => x.Employee_ID.Contains(param.sSearch)
                    || x.Employee.ToString().ToLower().Contains(param.sSearch.ToLower())
                    || x.Device_ID.ToString().ToLower().Contains(param.sSearch.ToLower())
                    || x.Status.ToString().ToLower().Contains(param.sSearch.ToLower()));
                }

                //int count = (from d in objModel.tbl_attendence
                //                 //join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                //                 //from emp in employee.DefaultIfEmpty()
                //                 //join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                //                 //from dev in device.DefaultIfEmpty()
                //             where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate && emp.IsDelete == false
                //             select d).Count();

                TotalRecords = query.Count();
                // att = query.ToList().OrderByDescending(x => x.Attendance_DateTime).ToList();
                ///skipping

                if (param.iDisplayLength != -1)
                {

                    if (TotalRecords > param.iDisplayStart)
                        query = query.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength);
                }
                att = query.ToList();
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
            return att;

        }

        public List<Attendance_P> GetAttendanceByDate(string Start, string End)
        {

            List<Attendance_P> att = new List<Attendance_P>();
            try
            {

                DateTime StartDate = Formatter.SetValidValueToDateTime(Start + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(End + " 23:59:59");

                objModel.Database.CommandTimeout = 86400;
                att = (from d in objModel.tbl_attendence
                       join emp in objModel.tbl_employee on d.Employee_ID equals emp.Employee_ID.ToString() into employee
                       from emp in employee.DefaultIfEmpty()
                       join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                       from dev in device.DefaultIfEmpty()

                           //join st in objModel.tbl_att_status on d.Code equals st.Code into status
                           //from st in status.DefaultIfEmpty()
                       where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate && emp.IsDelete != true
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Attendance_Photo = emp.Employee_Photo,
                           Verify_Mode = d.Verify_Mode,
                           Device_ID = d.Device_ID,
                           Employee_ID = d.Employee_ID,
                           Polling_DateTime = d.Polling_DateTime,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Device = dev.Device_Name,
                           Employee = d.Employee_Name,
                           DateTime = d.Attendance_DateTime.Value.ToString(),
                           Status_TIS = d.Status_TIS,
                           WorkCode_Description = d.WorkCode_Description,
                           Status_Description = d.Status_Description,
                           Status_Oracle = d.Status_Oracle,
                           Status_SQL = d.Status_SQL,
                           ExtraStatus = d.Ext_Status,
                           CheckIsSlave = dev.IsSlave

                       }).OrderByDescending(x => x.Attendance_DateTime).Take(30).ToList();

                foreach (var item in att)
                {
                    item.DateTime = item.Attendance_DateTime.Value.ToString("dd-MM-yyyy HH:mm:ss");
                    if (item.Employee == "Unregistered")
                    {
                        item.Attendance_Photo = "";
                    }
                }

                //.OrderByDescending(x => x.DateTime).AsEnumerable().Select(x => new Attendance_P
                // {
                //     Code = x.Code,
                //     Attendance_DateTime = x.Attendance_DateTime,
                //     Attendance_Photo = x.Attendance_Photo,
                //     Verify_Mode = x.Verify_Mode,
                //     Device_ID = x.Device_ID,
                //     Employee_ID = x.Employee_ID,
                //     Polling_DateTime = x.Polling_DateTime,
                //     Status = x.Status,
                //     WorkCode = x.WorkCode,
                //     DoorStatus = x.DoorStatus,
                //     WorkCode_Description = x.WorkCode_Description,
                //     Status_Description = x.Status_Description,
                //     Device = x.Device,
                //     Employee = x.Employee,
                //     DateTime = Formatter.SetValidValueToDateTime(x.Attendance_DateTime).ToString("dd-MM-yyyy HH:mm:ss"),
                //     Status_TIS = x.Status_TIS,
                //     Status_Oracle = x.Status_Oracle,
                //     Status_SQL = x.Status_SQL
                // })

                //TotalRecords = query.Count();
                //att = query.ToList().OrderBy(x => Convert.ToInt32(x.Employee_ID)).ToList();
                ///skipping
                //if (param.iDisplayLength != -1)
                //{

                //    if (TotalRecords > param.iDisplayStart)
                //        att = att.Skip(param.iDisplayStart)
                //           .Take(param.iDisplayLength).ToList();
                //}

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
            return att;

        }

        public Attendance_P GetUnRegisteredRecordByDate(DateTime Date)
        {

            Attendance_P att = new Attendance_P();
            try
            {
                att = (from d in objModel.tbl_attendence
                       join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                       from dev in device.DefaultIfEmpty()
                       where d.Attendance_DateTime == Date
                       select new Attendance_P
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Attendance_Photo = d.Attendance_Photo,
                           Verify_Mode = d.Verify_Mode,
                           Device_ID = d.Device_ID,
                           Employee_ID = d.Employee_ID,
                           Status = d.Status,
                           WorkCode = d.WorkCode,
                           DoorStatus = d.DoorStatus,
                           Device = dev.Device_Name,
                           Employee = d.Employee_Name,
                           DateTime = d.Attendance_DateTime.ToString()
                       }).OrderByDescending(x => x.DateTime).AsEnumerable().Select(x => new Attendance_P
                       {
                           Code = x.Code,
                           Attendance_DateTime = x.Attendance_DateTime,
                           Attendance_Photo = x.Attendance_Photo,
                           Verify_Mode = x.Verify_Mode,
                           Device_ID = x.Device_ID,
                           Employee_ID = x.Employee_ID,
                           Status = x.Status,
                           Status_Description = x.Status_Description,
                           WorkCode = x.WorkCode,
                           DoorStatus = x.DoorStatus,
                           WorkCode_Description = x.WorkCode_Description,
                           Device = x.Device,
                           Employee = x.Employee,
                           DateTime = Formatter.SetValidValueToDateTime(x.Attendance_DateTime).ToString("dd-MM-yyyy HH:mm:ss")

                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return att;
        }

        //public List<Attendance_P> GetAttendanceByDateWStranger(string Start, string End)
        //{

        //    List<Attendance_P> att = new List<Attendance_P>();
        //    try
        //    {

        //        DateTime StartDate = Formatter.SetValidValueToDateTime(Start + " 00:00:00");
        //        DateTime EndDate = Formatter.SetValidValueToDateTime(End + " 23:59:59");
        //        //setting s = objModel.settings.FirstOrDefault();

        //        //string CustomMsg = "";
        //        //if (s != null)
        //        //    CustomMsg = s.CustomMsg;
        //        var DateNow = DateTime.Now;

        //        att = (from d in objModel.attendances
        //               join emp in objModel.employees on d.Custom_ID equals emp.Custom_ID.ToString() into employee
        //               from emp in employee.DefaultIfEmpty()
        //               join dev in objModel.devices on d.Device_ID equals dev.Device_ID into device
        //               from dev in device.DefaultIfEmpty()
        //               where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate
        //               select new Attendance_P
        //               {
        //                   Code = d.Code,
        //                   Attendance_DateTime = d.Attendance_DateTime,
        //                   Attendance_Photo = d.Attendance_Photo,
        //                   OType = d.OType,
        //                   Device_ID = d.Device_ID,
        //                   Custom_ID = d.Custom_ID,
        //                   Temperature = d.Temperature,
        //                   Polling_DateTime = d.Polling_DateTime,
        //                   Status = d.Status,
        //                   Device = dev.Device_Name,
        //                   Employee = emp.Employee_Name,
        //                   DateTime = d.Attendance_DateTime.ToString(),
        //                   IsTemp_High = d.IsTemp_High,
        //                   ProfilePhoto = emp.Employee_Photo,
        //                   Transaction_ID = d.Transaction_ID,
        //                   Status_TIS = d.Status_TIS,
        //                   Status_Oracle = d.Status_Oracle,
        //                   Status_SQL = d.Status_SQL
        //               }).Union(from d in objModel.strangerlogs
        //                        join dev in objModel.devices on d.Device_ID equals dev.Device_ID into device
        //                        from dev in device.DefaultIfEmpty()
        //                        where d.DateTime >= StartDate && d.DateTime <= EndDate
        //                        select new Attendance_P
        //                        {
        //                            Code = d.Code,
        //                            Attendance_DateTime = d.DateTime,
        //                            Attendance_Photo = d.Photo,
        //                            OType = "0",
        //                            Device_ID = d.Device_ID,
        //                            Custom_ID = "0",
        //                            Temperature = d.Temperature,
        //                            Polling_DateTime = null,
        //                            Status = d.Status,
        //                            Device = dev.Device_Name,
        //                            Employee = d.Name,
        //                            DateTime = d.DateTime.ToString(),
        //                            IsTemp_High = d.IsTemp_High,
        //                            ProfilePhoto = "",
        //                            Transaction_ID = "",
        //                            Status_TIS = false,
        //                            Status_Oracle = false,
        //                            Status_SQL = false
        //                        }).AsEnumerable().Select(x => new Attendance_P
        //                        {
        //                            Code = x.Code,
        //                            Attendance_DateTime = x.Attendance_DateTime,
        //                            Attendance_Photo = x.Attendance_Photo,
        //                            OType = x.OType,
        //                            Device_ID = x.Device_ID,
        //                            Custom_ID = x.Custom_ID,
        //                            Temperature = x.Temperature,
        //                            Polling_DateTime = null,
        //                            Status = DeviceHelper.GetStatus(x.Status),
        //                            Device = x.Device,
        //                            Employee = x.Employee,
        //                            DateTime = x.DateTime.ToString(),
        //                            IsTemp_High = x.IsTemp_High,
        //                            ProfilePhoto = x.ProfilePhoto,
        //                            Transaction_ID = x.Transaction_ID,
        //                            Status_TIS = x.Status_TIS,
        //                            Status_Oracle = x.Status_Oracle,
        //                            Status_SQL = x.Status_SQL
        //                        }).ToList();
        //        att = att.OrderByDescending(x => x.Attendance_DateTime).Take(25).ToList();

        //        foreach (var item in att)
        //        {
        //            EmployeeMsg_P msg = (from m in objModel.employeemsgs
        //                                 where m.Custom_ID == item.Custom_ID && m.Active == true
        //                                 select new EmployeeMsg_P
        //                                 {
        //                                     Code = m.Code,
        //                                     Message = m.Message
        //                                 }
        //                               ).OrderByDescending(x => x.Code).FirstOrDefault();

        //            if (msg != null)
        //            {
        //                item.CustomMsg = msg.Message;
        //            }
        //        }


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
        //    return att;

        //}

        //public Attendance_P GetStrangerRecordByDate(DateTime Date)
        //{

        //    Attendance_P att = new Attendance_P();
        //    try
        //    {
        //        att = (from d in objModel.strangerlogs
        //               join dev in objModel.devices on d.Device_ID equals dev.Device_ID into device
        //               from dev in device.DefaultIfEmpty()
        //               where d.DateTime == Date
        //               select new Attendance_P
        //               {
        //                   Code = d.Code,
        //                   Attendance_DateTime = d.DateTime,
        //                   Attendance_Photo = d.Photo,
        //                   OType = "0",
        //                   Device_ID = d.Device_ID,
        //                   Custom_ID = "0",
        //                   Temperature = d.Temperature,
        //                   Polling_DateTime = null,
        //                   Status = d.Status,
        //                   Device = dev.Device_Name,
        //                   Employee = d.Name,
        //                   DateTime = d.DateTime.ToString(),
        //                   IsTemp_High = d.IsTemp_High,
        //                   Status_TIS = false,
        //                   Status_Oracle = false,
        //                   Status_SQL = false
        //               }).AsEnumerable().Select(x => new Attendance_P
        //               {
        //                   Code = x.Code,
        //                   Attendance_DateTime = x.Attendance_DateTime,
        //                   Attendance_Photo = x.Attendance_Photo,
        //                   OType = x.OType,
        //                   Device_ID = x.Device_ID,
        //                   Custom_ID = x.Custom_ID,
        //                   Temperature = x.Temperature,
        //                   Polling_DateTime = null,
        //                   Status = DeviceHelper.GetStatus(x.Status),
        //                   Device = x.Device,
        //                   Employee = x.Employee,
        //                   DateTime = x.DateTime.ToString(),
        //                   IsTemp_High = x.IsTemp_High,
        //                   Status_TIS = x.Status_TIS,
        //                   Status_Oracle = x.Status_Oracle,
        //                   Status_SQL = x.Status_SQL
        //               }).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
        //    }
        //    return att;
        //}

        public bool AddAttendance(Attendance_P Attendance)
        {
            bool flag = false;
            try
            {
                tbl_attendence att = new tbl_attendence
                {
                    Attendance_DateTime = Attendance.Attendance_DateTime,
                    Attendance_Photo = Attendance.Attendance_Photo,
                    Verify_Mode = Attendance.Verify_Mode,
                    Device_ID = Attendance.Device_ID,
                    Employee_ID = Attendance.Employee_ID,
                    Polling_DateTime = Attendance.Polling_DateTime,
                    Status = Attendance.Status,
                    WorkCode = Attendance.WorkCode,
                    DoorStatus = Attendance.DoorStatus
                };

                objModel.tbl_attendence.Add(att);
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

        public bool UpdateAttendance(string Device_ID, string Transaction_ID, int status)
        {
            bool flag = false;
            try
            {
                tbl_attendence att = new tbl_attendence();
                int code = 0;
                if (!string.IsNullOrEmpty(Transaction_ID))
                    code = Formatter.SetValidValueToInt(Transaction_ID);

                att = objModel.tbl_attendence.Where(x => x.Device_ID == Device_ID && x.Code == code).FirstOrDefault();
                att.Status = status.ToString();
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

        public bool AddAttendanceWODuplication(Attendance_P Attendance)
        {
            bool flag = false;
            try
            {
                tbl_attendence att = objModel.tbl_attendence.Where(x => x.Employee_ID == Attendance.Employee_ID
                && x.Attendance_DateTime == Attendance.Attendance_DateTime && x.Status == Attendance.Status).FirstOrDefault();

                if (att == null)
                {

                    att = new tbl_attendence
                    {
                        Attendance_DateTime = Attendance.Attendance_DateTime,
                        Attendance_Photo = Attendance.Attendance_Photo,
                        Verify_Mode = Attendance.Verify_Mode,
                        Device_ID = Attendance.Device_ID,
                        Employee_ID = Attendance.Employee_ID,
                        Polling_DateTime = Attendance.Polling_DateTime,
                        Status = Attendance.Status,
                        WorkCode = Attendance.WorkCode,
                        DoorStatus = Attendance.DoorStatus
                    };

                    objModel.tbl_attendence.Add(att);

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

        public List<Att_Graph> GetAttendanceByMonths()
        {
            List<Att_Graph> data = new List<Att_Graph>();
            try
            {
                List<tbl_attendence> att = objModel.tbl_attendence.Where(x => ((DateTime)x.Attendance_DateTime).Year == DateTime.Now.Year).ToList();

                string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };


                for (int i = 1; i < 13; i++)
                {
                    Att_Graph graph = new Att_Graph
                    {
                        In = att.Where(x => ((DateTime)x.Attendance_DateTime).Month == i && x.Status == "1").Count(),
                        Out = att.Where(x => ((DateTime)x.Attendance_DateTime).Month == i && x.Status == "2").Count(),
                        Period = months[i - 1]
                    };
                    data.Add(graph);
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

            return data;
        }

        public bool DeleteAttendance(int Code)
        {
            bool flag = false;
            try
            {
                tbl_attendence d = objModel.tbl_attendence.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_attendence.Remove(d);
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

        public List<AttendanceDTO> GetAttendanceByDATEandDEVICE(string FromDate, string ToDate, string Device_ID)
        {
            List<AttendanceDTO> att = new List<AttendanceDTO>();
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(FromDate);
                DateTime EndDate = Formatter.SetValidValueToDateTime(ToDate);

                objModel.Database.CommandTimeout = 86400;
                att = (from d in objModel.tbl_attendence
                       join dev in objModel.tbl_device on d.Device_ID equals dev.Device_ID into device
                       from dev in device.DefaultIfEmpty()
                       join e in objModel.tbl_employee on d.Employee_ID equals e.Employee_ID into emp
                       from e in emp.DefaultIfEmpty()
                       where d.Attendance_DateTime >= StartDate && d.Attendance_DateTime <= EndDate && dev.Device_ID == Device_ID
                       select new AttendanceDTO
                       {
                           Code = d.Code,
                           Attendance_DateTime = d.Attendance_DateTime,
                           Device_ID = d.Device_ID,
                           Employee_ID = d.Employee_ID,
                           Employee_Name = e.Employee_Name,
                           Polling_DateTime = d.Polling_DateTime,
                           Status = d.Status,
                           DoorStatus = d.DoorStatus,
                           VerifyMode = d.Verify_Mode,


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
            return att;
        }
    }
}