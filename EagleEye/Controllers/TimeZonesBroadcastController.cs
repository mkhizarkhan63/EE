using System;
using System.Collections.Generic;
using System.Linq;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web;
using System.Web.Mvc;
using EagleEye.BLL;
using EagleEye.Common;
using EagleEye.DAL.Partial;

namespace EagleEye.Controllers
{
    public class TimeZonesBroadcastController : masterController
    {
        BLLDeviceLog objBLL = new BLLDeviceLog();
        [CheckAuthorization]
        // GET: DeviceOfflineLog
        public ActionResult Index()
        {

            try
            {
                if (!CheckRights("TimeZones Broadcast Status"))
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetAllLogs()
        {
            List<Device_OfflineLog_P> list = new List<Device_OfflineLog_P>();
            try
            {
                list = objBLL.GetAllLogs();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = list
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLog(string[] lstLogCode)
        {
            List<Device_P> dev = new List<Device_P>();
            EagleEyeManagement em = new EagleEyeManagement();
            bool flag = false;
            try
            {
                BLLDevice objDev = new BLLDevice();
                dev = objDev.GetAllDevices();
                for (int i = 0; i < lstLogCode.Count(); i++)
                {
                    flag = objBLL.DeleteLog(lstLogCode[i]);
                    if (flag)
                    {
                        flag = objBLL.DeleteFKCmd(lstLogCode[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag
            }, JsonRequestBehavior.AllowGet);

        }

    }
}