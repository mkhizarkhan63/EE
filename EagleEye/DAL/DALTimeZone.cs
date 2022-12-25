using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Models;
using System.Data.Entity.Validation;
using EagleEye.DAL.Partial;
using Common;

namespace EagleEye.DAL
{
    public class DALTimeZone
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Att_Status_P.TimeZone_P> GetAllTimeZones()
        {
            List<Att_Status_P.TimeZone_P> list = new List<Att_Status_P.TimeZone_P>();
            try
            {
                list = (from d in objModel.tbl_timezone
                        where d.Code != 0
                        select new Att_Status_P.TimeZone_P
                        {
                            Code = d.Code,
                            Timezone_No = d.Timezone_No,
                            Timezone_Name = d.Timezone_Name,
                            Period_1_Start = d.Period_1_Start,
                            Period_1_End = d.Period_1_End,
                            Period_2_Start = d.Period_2_Start,
                            Period_2_End = d.Period_2_End,
                            Period_3_Start = d.Period_3_Start,
                            Period_3_End = d.Period_3_End,
                            Period_4_Start = d.Period_4_Start,
                            Period_4_End = d.Period_4_End,
                            Period_5_Start = d.Period_5_Start,
                            Period_5_End = d.Period_5_End,
                            Period_6_Start = d.Period_6_Start,
                            Period_6_End = d.Period_6_End,
                            Status = d.Status
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
        public List<TimeZone_P> GetAllTimeZone()
        {
            List<TimeZone_P> list = new List<TimeZone_P>();
            try
            {
                list = (from d in objModel.tbl_timezone
                        where d.Code != 0
                        select new TimeZone_P
                        {
                            Code = d.Code,
                            Timezone_No = d.Timezone_No,
                            Timezone_Name = d.Timezone_Name,
                            Period_1_Start = d.Period_1_Start,
                            Period_1_End = d.Period_1_End,
                            Period_2_Start = d.Period_2_Start,
                            Period_2_End = d.Period_2_End,
                            Period_3_Start = d.Period_3_Start,
                            Period_3_End = d.Period_3_End,
                            Period_4_Start = d.Period_4_Start,
                            Period_4_End = d.Period_4_End,
                            Period_5_Start = d.Period_5_Start,
                            Period_5_End = d.Period_5_End,
                            Period_6_Start = d.Period_6_Start,
                            Period_6_End = d.Period_6_End,
                            Status = d.Status
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

        public List<TimeZone_P> GetAllValidTimeZone()
        {
            List<TimeZone_P> list = new List<TimeZone_P>();
            try
            {
                list = (from d in objModel.tbl_timezone
                        where d.Code != 0 && d.Status !=false
                        select new TimeZone_P
                        {
                            Code = d.Code,
                            Timezone_No = d.Timezone_No,
                            Timezone_Name = d.Timezone_Name,
                            Period_1_Start = d.Period_1_Start,
                            Period_1_End = d.Period_1_End,
                            Period_2_Start = d.Period_2_Start,
                            Period_2_End = d.Period_2_End,
                            Period_3_Start = d.Period_3_Start,
                            Period_3_End = d.Period_3_End,
                            Period_4_Start = d.Period_4_Start,
                            Period_4_End = d.Period_4_End,
                            Period_5_Start = d.Period_5_Start,
                            Period_5_End = d.Period_5_End,
                            Period_6_Start = d.Period_6_Start,
                            Period_6_End = d.Period_6_End,
                            Status = d.Status
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
        public TimeZone_P GetTimeZonebyID(int Code)
        {
            TimeZone_P list = new TimeZone_P();
            try
            {
                //int code = Convert.ToInt32(Code);
                list = (from d in objModel.tbl_timezone
                        where d.Code == Code
                        select new TimeZone_P
                        {
                            Code = d.Code,
                            Timezone_No = d.Timezone_No,
                            Timezone_Name = d.Timezone_Name,
                            Period_1_Start = d.Period_1_Start,
                            Period_1_End = d.Period_1_End,
                            Period_2_Start = d.Period_2_Start,
                            Period_2_End = d.Period_2_End,
                            Period_3_Start = d.Period_3_Start,
                            Period_3_End = d.Period_3_End,
                            Period_4_Start = d.Period_4_Start,
                            Period_4_End = d.Period_4_End,
                            Period_5_Start = d.Period_5_Start,
                            Period_5_End = d.Period_5_End,
                            Period_6_Start = d.Period_6_Start,
                            Period_6_End = d.Period_6_End,
                            Status = d.Status
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
            return list;

        }
        public bool AddUpdateTimeZone(TimeZone_P tz)
        {
            bool flag = false;
            try
            {
                //int id = Formatter.SetValidValueToInt(employee.Employee_ID);
                tbl_timezone e = objModel.tbl_timezone.Where(x => x.Code == tz.Code).FirstOrDefault();

                if (e == null)
                    e = new tbl_timezone();

                e.Timezone_No = tz.Timezone_No;
                e.Timezone_Name = tz.Timezone_Name;
                e.Period_1_Start = tz.Period_1_Start;
                e.Period_1_End = tz.Period_1_End;
                e.Period_2_Start = tz.Period_2_Start;
                e.Period_2_End = tz.Period_2_End;
                e.Period_3_Start = tz.Period_3_Start;
                e.Period_3_End = tz.Period_3_End;
                e.Period_4_Start = tz.Period_4_Start;
                e.Period_4_End = tz.Period_4_End;
                e.Period_5_Start = tz.Period_5_Start;
                e.Period_5_End = tz.Period_5_End;
                e.Period_6_Start = tz.Period_6_Start;
                e.Period_6_End = tz.Period_6_End;
                e.Status = tz.Status;
                if (e.Code == 0)
                {
                    objModel.tbl_timezone.Add(e);
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
        public bool CheckTimeZoneExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                tbl_timezone d = objModel.tbl_timezone.Where(x => x.Timezone_No == CODE).FirstOrDefault();
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
        public bool DeleteTimeZone(int Code)
        {
            bool flag = false;
            try
            {
                tbl_timezone e = objModel.tbl_timezone.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_timezone.Remove(e);

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