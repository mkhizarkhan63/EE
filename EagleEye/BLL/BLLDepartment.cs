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
    public class BLLDepartment
    {
        DALDepartment objDAL = new DALDepartment();
       public List<Att_Status_P.Department_P> GetAllDepartments()
        {
            List<Att_Status_P.Department_P> list = new List<Att_Status_P.Department_P>();
            try
            {
                list = objDAL.GetAllDepartments();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public List<Department_P> GetAllDepartment()
        {
            List<Department_P> list = new List<Department_P>();
            try
            {
                list = objDAL.GetAllDepartment();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;
        }

        public Department_P GetDepartmentByCode(int Code)
        {
            Department_P dep = new Department_P();
            try
            {
                dep = objDAL.GetDepartmentByCode(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return dep;
        }

        public Department_P GetDepartmentByName(string Name)
        {
            Department_P dep = new Department_P();
            try
            {
                dep = objDAL.GetDepartmentByName(Name);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return dep;
        }

        public bool AddUpdateDepartment(Department_P department)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateDepartment(department);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteDepartment(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteDepartment(Code);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

    }
}