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
    public class BLLEmployeeType
    {
        DALEmployeeType objDAL = new DALEmployeeType();

        public List<Att_Status_P.EmployeeType_P> GetAllEmployeeTypes()
        {
            List<Att_Status_P.EmployeeType_P> list = new List<Att_Status_P.EmployeeType_P>();
            try
            {
                list = objDAL.GetAllEmployeeTypes();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<EmployeeType_P> GetAllEmployeeType()
        {
            List<EmployeeType_P> list = new List<EmployeeType_P>();
            try
            {
                list = objDAL.GetAllEmployeeType();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public EmployeeType_P GetEmployeeTypeByCode(int Code)
        {
            EmployeeType_P dep = new EmployeeType_P();
            try
            {
                dep = objDAL.GetEmployeeTypeByCode(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return dep;
        }

        public bool AddUpdateEmployeeType(EmployeeType_P EmployeeType)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateEmployeeType(EmployeeType);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteEmployeeType(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteEmployeeType(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public EmployeeType_P GetEmployeeTypeByName(string Name)
        {
            EmployeeType_P et = new EmployeeType_P();
            try
            {
                et = objDAL.GetEmployeeTypeByName(Name);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return et;
        }

    }
}