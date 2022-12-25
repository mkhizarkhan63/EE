using EagleEye.BLL;
using EagleEye.Common;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Models;
using Common;

namespace EagleEye.Controllers
{
    public class AccountsController : masterController
    {
        BLLAccount objBLL = new BLLAccount();
        BLLMenu objmenuBLL = new BLLMenu();
        // GET: Accounts

        [CheckAuthorization]
        public ActionResult Index()
        {
            List<Account_P> list = new List<Account_P>();
            try
            {
                if (!CheckRights("Accounts"))
                {
                    return RedirectToAction("Index", "Home");
                }

                list = objBLL.GetAllAccountUser();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(list);
        }

        [CheckAuthorization]
        public ActionResult Add()
        {
            Account_P accounts = new Account_P();
            return View();
        }

        [CheckAuthorization]
        public ActionResult Edit(string username)
        {

            Account_P accounts = new Account_P();
            try
            {
                accounts = objBLL.GetAccountByUser(username);
                if (accounts == null)
                {
                    return RedirectToAction("Index", "Accounts");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(accounts);

        }

        [HttpPost]
        public JsonResult AddAccount(Account_P accounts)
        {
            bool flag = false;
            string msg = "";
            try
            {
                flag = objBLL.AddUpdateAccounts(accounts);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMenuTable()
        {
            List<Menu_P> menus = new List<Menu_P>();
            try
            {
                menus = objmenuBLL.GetAllMenu();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { result = menus }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMenuRights(int id)
        {
            List<MenuRights_P> rights = new List<MenuRights_P>();
            try
            {
                rights = objmenuBLL.GetMenuRights(id);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { result = rights }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateMenuRights(List<MenuRights_P> menuRights, string[] lstAccountCode)
        {
            bool flag = false;
            string msg = "";
            try
            {
                for (int i = 0; i < lstAccountCode.Count(); i++)
                {
                    flag = objmenuBLL.DeleteMenus(Formatter.SetValidValueToInt(lstAccountCode[i]));
                }

                for (int i = 0; i < lstAccountCode.Count(); i++)
                {
                    flag = objmenuBLL.AddUpdateMenuRights(menuRights, Formatter.SetValidValueToInt(lstAccountCode[i]));
                }
            }

            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteAccount(string[] lstAccountCode)
        {

            bool flag = false;
            string msg = "";
            try
            {
                for (int i = 0; i < lstAccountCode.Count(); i++)
                {
                    flag = objBLL.DeleteAccount(Formatter.SetValidValueToInt(lstAccountCode[i]));
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditAccount(Account_P accounts)
        {
            bool flag = false;
            string msg = "";
            try
            {
                flag = objBLL.AddUpdateAccounts(accounts);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);
        }

    }
}