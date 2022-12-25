using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using Common;

namespace EagleEye.BLL
{
    public class BLLTimeZone
    {
        DALTimeZone objDAL = new DALTimeZone();
        public List<Att_Status_P.TimeZone_P> GetAllTimeZones()
        {

            List<Att_Status_P.TimeZone_P> list = new List<Att_Status_P.TimeZone_P>();
            try
            {
                list = objDAL.GetAllTimeZones();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<TimeZone_P> GetAllTimeZone()
        {

            List<TimeZone_P> list = new List<TimeZone_P>();
            try
            {
                list = objDAL.GetAllTimeZone();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<TimeZone_P> GetAllValidTimeZone()
        {

            List<TimeZone_P> list = new List<TimeZone_P>();
            try
            {
                list = objDAL.GetAllValidTimeZone();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool AddUpdateTimeZone(TimeZone_P tz)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateTimeZone(tz);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public TimeZone_P GetTimeZonebyID(int Code)
        {
            TimeZone_P tz = new TimeZone_P();
            try
            {
                tz = objDAL.GetTimeZonebyID(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return tz;
        }

        public bool CheckTimeZoneExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckTimeZoneExistInDB(CODE);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteTimeZone(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteTimeZone(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

    }
}