using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web.Security;
using EagleEye.DAL.Partial;
using System.Data.Entity.Validation;
using Common;

namespace EagleEye.DAL
{
    public class DALAccount
    {
        EagleEyeEntities objModel = new EagleEyeEntities();

        public Account_P GetAccountByUser(string username)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[10];
            Account_P acccount = new Account_P();
            try
            {
                acccount = (from d in objModel.tbl_account
                            where d.UserName == username
                            select new Account_P
                            {
                                Code = d.Code,
                                UserName = d.UserName,
                                Hash = d.Hash,
                                Salt = d.Salt,
                                LastLogin = d.LastLogin
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
            return acccount;
        }

        public List<Account_P> GetAllAccountUser()
        {

            List<Account_P> list = new List<Account_P>();
            try
            {
                list = (from d in objModel.tbl_account
                        select new Account_P
                        {
                            Code = d.Code,
                            UserName = d.UserName,
                            Hash = d.Hash,
                            Salt = d.Salt,
                            LastLogin = d.LastLogin
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

        public bool AddUpdateAccounts(Account_P accounts)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[10];
            bool flag = false;
            try
            {
               
                string expectedHashString = Security.Get_HASH_SHA512(accounts.Hash, accounts.UserName, SALT);
                int id = Formatter.SetValidValueToInt(accounts.Code);
                tbl_account a = objModel.tbl_account.Where(x => x.Code == id).FirstOrDefault();

                if (a == null)
                {
                    a = new tbl_account();
                }
                a.UserName = Formatter.SetValidValueToString(accounts.UserName);
                ///  a.UserName = accounts.UserName;
                a.Hash = expectedHashString;
                a.Salt = SALT;

                if (a.Code == 0)
                {
                    tbl_account checkUsernameExist = objModel.tbl_account.Where(x => x.UserName == accounts.UserName).FirstOrDefault();
                    if (checkUsernameExist != null)
                        return flag = false;
                    objModel.tbl_account.Add(a);
                }
                else
                {
                    objModel.Entry(a).State = System.Data.Entity.EntityState.Modified;
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

        public bool UpdateLoginDate(Account_P accounts)
        {
            bool flag = false;
            try
            {
                string id = Formatter.SetValidValueToString(accounts.UserName);
                tbl_account a = objModel.tbl_account.Where(x => x.UserName == id).FirstOrDefault();

                a.LastLogin = DateTime.Now;

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

        public bool DeleteAccounts(int Code)
        {
            bool flag = false;
            try
            {
                tbl_account a = objModel.tbl_account.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_account.Remove(a);
                List<tbl_menurights> mr = objModel.tbl_menurights.Where(x => x.User_Id == Code).ToList();
                foreach (var menu in mr)
                {
                    objModel.tbl_menurights.Remove(menu);
                }
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
    }
}