
using EagleEye.BLL;
using EagleEye.DAL.Partial;
using System;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static Common.HelpingMethod;
using static EagleEye.Common.ExceptionLogger;
using EagleEye.Common;
using Common;

namespace EagleEye.Controllers
{
    public class PortController : masterController
    {
        BLLCommunication objBLL = new BLLCommunication();

        // GET: Port
        [CheckAuthorization]
        public ActionResult Index()
        {

            Communication_P data = new Communication_P();
            try
            {
                if (!CheckRights("Port"))
                {
                    return RedirectToAction("Index", "Home");
                }

                data = objBLL.GetData();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(data);
        }

        public JsonResult Save(Communication_P data)
        {

            bool flag = false;
            try
            {
                flag = objBLL.SaveData(data);
                if (flag)
                    Session[SessionVariables.Session_Communication] = data;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CheckPort(string port)
        {

            bool flag = false;
            try
            {
                if (!string.IsNullOrEmpty(port))
                    flag = CheckPortAvailability(Formatter.SetValidValueToInt(port));
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                result = flag,
            }, JsonRequestBehavior.AllowGet);

        }
    }
}