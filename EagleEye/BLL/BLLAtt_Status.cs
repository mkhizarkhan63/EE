using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL;
using Common;

namespace EagleEye.BLL
{
    public class BLLAtt_Status
    {
        DALAtt_Status objDAL = new DALAtt_Status();

        public List<Att_Status_P> GetAllStatus()
        {

            List<Att_Status_P> list = new List<Att_Status_P>();
            try
            {
                list = objDAL.GetAllStatus();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public List<Att_Status_P> GetAllReportStatus()
        {

            List<Att_Status_P> list = new List<Att_Status_P>();
            try
            {
                list = objDAL.GetAllReportStatus();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool SetAttendenceStatus(int code, string name)
        {
            bool flag = false;
            try
            {
                flag = objDAL.SetAttendenceStatus(code, name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }
        public Att_Status_P GetStatusCode(string name)
        {
            Att_Status_P att = new Att_Status_P();
            try
            {
                att = objDAL.GetStatusCode(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
        public Att_Status_P GetStatusName(int code)
        {
            Att_Status_P att = new Att_Status_P();
            try
            {
                att = objDAL.GetStatusName(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }

    }
}