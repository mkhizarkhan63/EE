using System;
using System.Collections.Generic;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Linq;
using System.Web;
using EagleEye.DAL;
using EagleEye.DAL.Partial;

namespace EagleEye.BLL
{
    public class BLLExpiredUsers
    {
        DALExpiredUsers objDAL = new DALExpiredUsers();
        public List<ExpiredUsers_P> GetAllExpiredEmployees()
        {

            List<ExpiredUsers_P> list = new List<ExpiredUsers_P>();
            try
            {
                list = objDAL.GetAllExpiredEmployees();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public bool CheckEmployeeExistInDB(string CODE)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckEmployeeExistInDB(CODE);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        
        public bool DeleteExEmployee(string Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteEmployee(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        //public bool DeleteExEmployeebyID(string Code)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        flag = objDAL.DeleteEmployee(Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
        //    }
        //    return flag;
        //}
    }
}