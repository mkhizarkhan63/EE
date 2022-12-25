using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using Common;

namespace EagleEye.BLL
{
    public class BLLLocation
    {
        DALLocation objDAL = new DALLocation();

        public List<Att_Status_P.Location_P> GetAllLocations()
        {
            List<Att_Status_P.Location_P> list = new List<Att_Status_P.Location_P>();
            try
            {
                list = objDAL.GetAllLocations();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<Location_P> GetAllLocation()
        {
            List<Location_P> list = new List<Location_P>();
            try
            {
                list = objDAL.GetAllLocation();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public Location_P GetLocationByCode(int Code)
        {
            Location_P loc = new Location_P();
            try
            {
                loc = objDAL.GetLocationByCode(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return loc;
        }

        public bool AddUpdateLocation(Location_P location)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateLocation(location);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteLocation(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteLocation(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public Location_P GetLocationByName(string Name)
        {
            Location_P loc = new Location_P();
            try
            {
                loc = objDAL.GetLocationByName(Name);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return loc;
        }
    }


}