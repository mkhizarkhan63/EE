using EagleEye.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.Partial;

namespace EagleEye.BLL
{
    public class BLLAccount
    {
        DALAccount objDAL = new DALAccount();

        public Account_P GetAccountByUser(string username)
        {
            Account_P acccount = new Account_P();
            try
            {
                acccount = objDAL.GetAccountByUser(username);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return acccount;
        }

        public List<Account_P> GetAllAccountUser()
        {

            List<Account_P> list = new List<Account_P>();
            try
            {
                list = objDAL.GetAllAccountUser();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        //public bool AddUpdateUserRights(Account_P accounts)
        //{

        //    bool flag = false;
        //    try
        //    {
        //        flag = objDAL.AddUpdateUserRights(accounts);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
        //    }
        //    return flag;

        //}

        public bool AddUpdateAccounts(Account_P accounts)
        {

            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateAccounts(accounts);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }

        public bool UpdateLoginDate(Account_P accounts)
        {

            bool flag = false;
            try
            {
                flag = objDAL.UpdateLoginDate(accounts);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;

        }

        public bool DeleteAccount(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteAccounts(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
    }
}