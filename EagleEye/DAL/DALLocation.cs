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

namespace EagleEye.DAL
{
    public class DALLocation
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public List<Location_P> GetAllLocation()
        {

            List<Location_P> list = new List<Location_P>();
            try
            {
                list = (from d in objModel.tbl_location
                        select new Location_P
                        {
                            Code = d.Code,
                            Description = d.Description
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

        public Location_P GetLocationByCode(int Code)
        {

            Location_P Location = new Location_P();
            try
            {
                Location = (from d in objModel.tbl_location
                            where d.Code == Code
                            select new Location_P
                            {
                                Code = d.Code,
                                Description = d.Description,
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
            return Location;

        }

        public bool AddUpdateLocation(Location_P Location)
        {
            bool flag = false;
            try
            {
                tbl_location l = objModel.tbl_location.Where(x => x.Code == Location.Code).FirstOrDefault();

                if (l == null)
                    l = new tbl_location();

                l.Description = Location.Description;


                if (l.Code == 0)
                {
                    objModel.tbl_location.Add(l);
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

        public bool DeleteLocation(int Code)
        {
            bool flag = false;
            try
            {
                tbl_location d = objModel.tbl_location.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_location.Remove(d);
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


        public List<Att_Status_P.Location_P> GetAllLocations()
        {

            List<Att_Status_P.Location_P> list = new List<Att_Status_P.Location_P>();
            try
            {
                list = (from d in objModel.tbl_location
                        select new Att_Status_P.Location_P
                        {
                            Code = d.Code,
                            Description = d.Description
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

        public Location_P GetLocationByName(string name)
        {

            Location_P location = new Location_P();
            try
            {
                location = (from d in objModel.tbl_location
                            where d.Description == name
                                select new Location_P
                                {
                                    Code = d.Code,
                                    Description = d.Description,
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
            return location;

        }
    }
}