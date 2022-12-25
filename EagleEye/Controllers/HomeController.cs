
using EagleEye.BLL;
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
using Common;
using EagleEye.Models;
using Newtonsoft.Json;

namespace EagleEye.Controllers
{
    [CheckAuthorization]
    public class HomeController : masterController
    {
        public ActionResult Index()
        {
            Attendance_P att = new Attendance_P();
            try
            {
                BLLDevice objDevice = new BLLDevice();
                att.DeviceList = objDevice.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(att);
        }
        public ActionResult MultiView()
        {
            Attendance_P att = new Attendance_P();
            BLLDevice objDevice = new BLLDevice();
            att.DeviceList = objDevice.GetAllDevices();
            return View(att);
        }
        [HttpPost]
        public JsonResult GetValues()
        {
            int connected = 0;
            int disconnected = 0;
            int empCount = 0;
            try
            {
                BLLDevice objDevice = new BLLDevice();
                connected = objDevice.GetDeviceCountByStatus(1);
                disconnected = objDevice.GetDeviceCountByStatus(0);

                BLLEmployee objEmp = new BLLEmployee();
                empCount = objEmp.GetEmployeeCount();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                connected,
                disconnected,
                empCount
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetGraph()
        {
            List<Att_Graph> list = new List<Att_Graph>();
            try
            {
                BLLAttendance objAtt = new BLLAttendance();
                list = objAtt.GetAttendanceByMonths();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                res = JsonConvert.SerializeObject(list),
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetLogs()
        {
            List<Attendance_P> list = new List<Attendance_P>();
            try
            {
                BLLAttendance objAtt = new BLLAttendance();
                list = objAtt.GetAttendanceByDate(DateTime.Now.ToString("dd-MMM-yyyy"), DateTime.Now.ToString("dd-MMM-yyyy")).OrderBy(x => x.Attendance_DateTime).ToList();
              
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                res = list,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetExUsers()
        {
            List<ExpiredUsers_P> list = new List<ExpiredUsers_P>();
            try
            {
                BLLExpiredUsers objuser = new BLLExpiredUsers();
                list = objuser.GetAllExpiredEmployees();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                res = list,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AddUser(string Date)
        {
            bool flag = false;
            Attendance_P att = new Attendance_P();
            EagleEyeManagement em = new EagleEyeManagement();
            try
            {
                Account_P account = (Account_P)Session["User"];
                string[] d = Date.Split(' ');
                string[] d1 = d[0].Split('-');
                string d2 = d1[2].ToString() + "-" + d1[1].ToString() + "-" + d1[0].ToString() + " " + d[1].ToString();
                //  DateTime dt = Convert.ToDateTime(Date);
                DateTime dt = Formatter.SetValidValueToDateTime(d2);
                // Date = dt.ToString("dd-MM-yyyy HH:mm:ss");
                BLLAttendance objAtt = new BLLAttendance();
                att = objAtt.GetUnRegisteredRecordByDate(dt);
                // att = objAtt.GetUnRegisteredRecordByDate(Date);
                flag = em.GetUserInfo(att.Device_ID, att.Employee_ID, account.UserName);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return Json(new
            {
                res = flag,
            }, JsonRequestBehavior.AllowGet);

        }


    }
}