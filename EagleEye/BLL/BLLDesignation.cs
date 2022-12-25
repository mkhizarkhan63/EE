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
    public class BLLDesignation
    {
        DALDesignation objDAL = new DALDesignation();

        public List<Att_Status_P.Designation_P> GetAllDesignations()
        {
            List<Att_Status_P.Designation_P> list = new List<Att_Status_P.Designation_P>();
            try
            {
                list = objDAL.GetAllDesignations();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<Designation_P> GetAllDesignation()
        {
            List<Designation_P> list = new List<Designation_P>();
            try
            {
                list = objDAL.GetAllDesignation();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public Designation_P GetDesignationByCode(int Code)
        {
            Designation_P dep = new Designation_P();
            try
            {
                dep = objDAL.GetDesignationByCode(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return dep;
        }

        public bool AddUpdateDesignation(Designation_P Designation)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateDesignation(Designation);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteDesignation(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteDesignation(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public Designation_P GetDesignationByName(string Name)
        {
            Designation_P des = new Designation_P();
            try
            {
                des = objDAL.GetDesignationByName(Name);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return des;
        }

    }
}