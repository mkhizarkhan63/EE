using System;
using System.Collections.Generic;
using System.Linq;
using EagleEye.DAL.Partial;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web;
using System.Web.Mvc;
using EagleEye.BLL;
using EagleEye.Common;
using Common;

namespace EagleEye.Controllers
{
    public class OperationLogController : masterController
    {
        BLLOperationLog objBLL = new BLLOperationLog();
        [CheckAuthorization]
        // GET: OperationLog
        public ActionResult Index()
        {
            List<OperationLog_P> list = new List<OperationLog_P>();
            try
            {
                if (!CheckRights("Operation Log"))
                {
                    return RedirectToAction("Index", "Home");
                }
                //string dt = DateTime.Now.ToString("yyyy-MM-dd");
                //list = objBLL.GetAllLogs(dt);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(list);
        }


        [HttpPost]
        public JsonResult ConfirmPassword(string Password, string[] lstLogs)
        {
            bool flag = false;

            try
            {
                if (Password == "eagle123")
                {
                    for (int i = 0; i < lstLogs.Count(); i++)
                    {
                        flag = objBLL.DeleteLogs(Formatter.SetValidValueToInt(lstLogs[i]));
                    }
                }
                else
                {
                    flag = false;
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

        public JsonResult GetAllLogs(JqueryDatatableParam param, string start, string end, string status, string devices)
        {
            int totalRecords = 0;
            List<OperationLog_P> list = new List<OperationLog_P>();
            try
            {
                list = objBLL.GetAllLogs(param, start, end, status, devices, out totalRecords);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            //string start, string end, string status, string devices
            //List<OperationLog_P> list = new List<OperationLog_P>();
            //try
            //{
            //    // string dt = DateTime.Now.ToString("yyyy-MM-dd");

            //    list = objBLL.GetAllLogs(start, end);

            //    if (status != "-1")
            //        list = list.Where(x => x.Status == status).ToList();

            //    if (devices != "0")
            //        list = list.Where(x => x.Device_ID == devices).ToList();

            //    list = list.ToList().OrderBy(x => x.UpdateTime).ToList();
            //    //int index = 0;
            //    //foreach(var l in list)
            //    //{
            //    //    list[0].UpdateTime = 
            //    //}

            //    }
            //    catch (Exception ex)
            //    {
            //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            //    }



            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

    }
}