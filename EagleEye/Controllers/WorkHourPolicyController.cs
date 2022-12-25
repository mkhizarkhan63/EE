using EagleEye.Common;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.BLL;
using EagleEye.DAL.DTO;

namespace EagleEye.Controllers
{
    public class WorkHourPolicyController : masterController
    {
        BLLWorkHour objBLL = new BLLWorkHour();
        [CheckAuthorization]
        public ActionResult Index()
        {
            if (!CheckRights("Work Hour Policy"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [CheckAuthorization]
        public ActionResult Add()
        {

            return View();
        }


        [CheckAuthorization]
        public ActionResult Edit(string Code)
        {
            List<PolicyDetail_P> ls = new List<PolicyDetail_P>();
            var code = long.Parse(Code);
            ls = objBLL.getAllPolicyDetail(code);
            var whp = objBLL.getWorkHourByCode(code);
            ViewBag.PolicyName = whp.PolicyName;
            ViewBag.PolicyIsActive = whp.isActive;
            ViewBag.PolicyCode = code;

            return View(ls);
        }

        public JsonResult AddWorkHour(WorkHourPolicy_P workHour, List<PolicyDetail_P> workHourDetail)
        {
            bool flag = false;
            int code = 0;
            try
            {
                if (workHourDetail != null)
                {
                    //add for policy name
                    flag = objBLL.AddUpdateWorkHourPolicy(workHour, out code);

                    foreach (var item in workHourDetail)
                    {
                        item.PolicyCode = code;
                        //add for policy detail
                        flag = objBLL.AddUpdatePolicyDetail(item);
                    }

                }


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }


            return Json(new
            {
                result = flag,
                msg = ""
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditWorkHour(WorkHourPolicy_P workHourPolicy, List<PolicyDetail_P> workHourDetail)
        {
            bool flag = false;
            string msg = string.Empty;
            int code = 0;
            try
            {
                if (workHourDetail != null)
                {
                    //add for policy name
                    flag = objBLL.AddUpdateWorkHourPolicy(workHourPolicy, out code);

                    //Removing detail policy one-many relation with workhour_policy
                    //  flag = objBLL.removingRelationPolicy(code);

                    foreach (var item in workHourDetail)
                    {
                        item.PolicyCode = code;
                        //add for policy detail
                        objBLL.AddUpdatePolicyDetail(item);
                    }
                    flag = true;
                    msg = "Changes Saved Successfully!";
                }
                else
                {
                    //upd for policy name
                    flag = objBLL.AddUpdateWorkHourPolicy(workHourPolicy, out code);
                    objBLL.RemoveAllPolicyDetail(workHourPolicy.Code);
                    msg = "Add Workhour To Proceed...";
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                result = flag,
                msg = ""
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTotalReport(JqueryDatatableParam param)
        {
            int totalRecords = 0;
            List<WorkhourPolicyDTO> list = new List<WorkhourPolicyDTO>();

            try
            {
                list = objBLL.getAllWorkPolicies(param, out totalRecords);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }


            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeletedPolicies(string[] selectedPolicy)
        {
            bool flag = false;
            try
            {
                for (int i = 0; i < selectedPolicy.Length; i++)
                {
                    flag = objBLL.DeletedPolicy(selectedPolicy[i]);
                }
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

    }
}