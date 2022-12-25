using EagleEye.BLL;
using System;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Models;
using System.Web.Security;
using System.Security.Principal;
using EagleEye.DAL.Partial;
using Common;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using EagleEye.Common;
using System.Diagnostics;
using System.Data.SqlClient;

namespace EagleEye.Controllers
{
    public class LoginController : Controller
    {

        #region Variables

        BLLAccount objBLL = new BLLAccount();
       // BLLCommunication objComm = new BLLCommunication();

        #endregion

        [HttpGet]
        public ActionResult Index(string ReturnUrl)
        {
            var userinfo = new Account_P();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.ReturnURL = ReturnUrl;

                return View(userinfo);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                return View(userinfo);
            }
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account_P entity)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[10];

            try
            {

                // Ensure we have a valid viewModel to work with
                if (!ModelState.IsValid)
                    return View(entity);

                //Retrive Stored HASH Value From Database According To Username (one unique field)
                var userInfo = objBLL.GetAccountByUser(entity.UserName);

                //Assign HASH Value
                if (userInfo != null)
                {
                    OldHASHValue = userInfo.Hash;
                    //SALT = userInfo.Salt;
                }


                License Lic = new License();

                //if (!Debugger.IsAttached)
                //{
                //    if (!Lic.CheckProductLicense(DateTime.Now))
                //    {
                //        TempData["ErrorMSG"] = "981 Object Reference not set to an instance of an object!";
                //        return View(entity);
                //    }
                //    //  LogService.WriteLog("Product licence is OKAY");
                //}
                bool isLogin = CompareHashValue(entity.Password, entity.UserName, OldHASHValue, SALT);
               // LogService.WriteLog("Login: " + isLogin);
                if (isLogin)
                {
                 //   LogService.WriteLog("Login Successfull");
                    //Login Success
                    //For Set Authentication in Cookie (Remeber ME Option)
                    SignInRemember(entity.UserName, entity.IsRemember);

                    //Set A Unique ID in session
                    //Session["UserID"] = userInfo.Code;
                    Session["User"] = userInfo;
                   // LogService.WriteLog("userInfo");

                    SettingsController settingC = new SettingsController();
                    Session["Setting"] = settingC.LoadDataFromRegistry();
                   // LogService.WriteLog("LoadDataFromRegistry");

                    BLLMenu objMenu = new BLLMenu();
                    Session["Menu"] = objMenu.GetMenuGen(userInfo.Code);
                   // LogService.WriteLog("GetMenuGen");

                    BLLCommunication objBLLCom = new BLLCommunication();
                    Communication_P comm = new Communication_P();
                    comm = objBLLCom.GetData();
                    comm.Server_IP = HelpingMethod.RemoveZeros(comm.Server_IP);
                    Session[SessionVariables.Session_Communication] = comm;
                    //LogService.WriteLog("Session_Communication");

                    BLLSetting objsetting = new BLLSetting();
                    Setting_P setting = new Setting_P {
                        AppSetting_Path = ProjectPath.FILE_PATH
                    };
                    objsetting.AddUpdateSetting(setting);


                    // If we got this far, something failed, redisplay form
                    //return RedirectToAction("Index", "Home");
                    return RedirectToLocal(entity.ReturnURL);
                }
                else
                {
                    //Login Fail
                    TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                    return View(entity);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                return View(entity);
            }

        }

        [HttpPost]
        public JsonResult TestConnection()
        {
            string msg = "";
            bool flag = false;
            try
            {

                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["testCon"].ConnectionString);
                con.Open();

                if (con.State == System.Data.ConnectionState.Open) { flag = true; }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { msg, flag }, JsonRequestBehavior.AllowGet);
        }

        public static bool CompareHashValue(string password, string username, string OldHASHValue, byte[] SALT)
        {
            try
            {
                string expectedHashString = Security.Get_HASH_SHA512(password, username, SALT);

                return (OldHASHValue == expectedHashString);
            }
            catch
            {
                return false;
            }
        }

        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        //GET: Logout
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();
                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }

        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);
                //LogService.WriteLog("RedirectToLocal");
                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }

        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }


        [HttpPost]
        public JsonResult CreateDatabase()
        {
            string msg = "";
            bool flag = false;
            try
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["testCon"].ConnectionString;
                connection = connection.Replace("database=EagleEye", "");
                SqlConnection con = new SqlConnection(connection);
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {

                    string path = Server.MapPath("~") + @"\DB\eagle-eye.sql";
                    string script = System.IO.File.ReadAllText(path);

                    SqlCommand cmd = new SqlCommand(script, con);
                    cmd.ExecuteNonQuery();
                    flag = true;

                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { msg, flag }, JsonRequestBehavior.AllowGet);
        }
    }
}
