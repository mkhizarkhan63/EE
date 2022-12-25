using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace EagleEye.BLL
{
    public class BLLWorkCode
    {
        DALWorkCode objDAL = new DALWorkCode();
        public List<Att_Status_P.WorkCode_P> GetAllWorkCodes()
        {
            List<Att_Status_P.WorkCode_P> list = new List<Att_Status_P.WorkCode_P>();
            try
            {
                list = objDAL.GetAllWorkCodes();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }
        public List<WorkCode_P> GetAllWorkCode()
        {

            List<WorkCode_P> list = new List<WorkCode_P>();
            try
            {
                list = objDAL.GetAllWorkCode();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public List<WorkCode_P> GetAllReportWorkCode()
        {

            List<WorkCode_P> list = new List<WorkCode_P>();
            try
            {
                list = objDAL.GetAllReportWorkCode();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }
        public bool SetWorkCode(int code, string name)
        {
            bool flag = false;
            try
            {
                flag = objDAL.SetWorkCode(code, name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }
        public WorkCode_P GetWorkCode(string name)
        {
            WorkCode_P att = new WorkCode_P();
            try
            {
                att = objDAL.GetWorkCode(name);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
        public WorkCode_P GetWorkCodeName(int code)
        {
            WorkCode_P att = new WorkCode_P();
            try
            {
                att = objDAL.GetWorkCodeName(code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return att;

        }
    }
}