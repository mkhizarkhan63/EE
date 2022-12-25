using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Common;
using EagleEye.BLL;
using EagleEye.DAL.Partial;

namespace EagleEye.Controllers
{
    public class WinServiceController : masterController
    {

        public ServiceController myService;
        //public string ServiceName = string.Empty;

        // GET: WinService
        [CheckAuthorization]
        public ActionResult Index()
        {
            if (!CheckRights("EagleEye Service"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public JsonResult Start()
        {
            BLLSetting bLLSetting = new BLLSetting();
           Setting_P ServiceName = bLLSetting.GetServiceName();
            myService = new ServiceController(ServiceName.ServiceName);
            bool flag = false;
            try
            {
                if (myService.Status == ServiceControllerStatus.Paused)
                    myService.Continue();
                else
                    myService.Start();

                flag = true;
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

        [HttpPost]
        public JsonResult Stop()
        {
            BLLSetting bLLSetting = new BLLSetting();
            Setting_P ServiceName = bLLSetting.GetServiceName();
            // string serv;
            myService = new ServiceController(ServiceName.ServiceName);
            bool flag = false;
            try
            {
                myService.Stop();
                flag = true;
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

        [HttpPost]
        public JsonResult ServiceExists()
        {
            bool flag = false;
            string msg = "";
            string status = ""; ;
            BLLSetting bLLSetting = new BLLSetting();
           
            try
            {
                Setting_P ServiceName = bLLSetting.GetServiceName();
               
                flag = ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName.ServiceName));
                if (flag)
                {
                    myService = new ServiceController(ServiceName.ServiceName);
                    ServiceControllerStatus Servicestatus = myService.Status;
                    status = Servicestatus.ToString();
                    msg = SetServiceStatus(myService.Status, myService);
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
                status
            }, JsonRequestBehavior.AllowGet);

        }


        private string SetServiceStatus(ServiceControllerStatus status, ServiceController service)
        {
            string msg = "";
            try
            {
                while (service.Status != status)
                {
                    service.Refresh();
                }
                switch (status)
                {
                    case ServiceControllerStatus.Running:
                        msg = "Service is running...";
                        break;
                    case ServiceControllerStatus.Paused:
                        msg = "Service is paused...";
                        break;
                    default:
                        msg = "Service is stopped...";
                        break;
                }
            }
            catch (Exception ex)
            { LogException(ex, ExceptionLayer.Controller, GetCurrentMethod()); }

            return msg;
        }

    }
}