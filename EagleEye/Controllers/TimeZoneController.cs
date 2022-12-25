using EagleEye.Common;
using EagleEye.DAL.Partial;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EagleEye.BLL;
using System.Web.Script.Serialization;
using Common;

namespace EagleEye.Controllers
{
    public class TimeZoneController : masterController
    {
        // GET: TimeZone
        BLLTimeZone objBLL = new BLLTimeZone();
        BLLDevice objDevice = new BLLDevice();
        [CheckAuthorization]
        public ActionResult Index()
        {
            List<TimeZone_P> list = new List<TimeZone_P>();
            try
            {
                if (!CheckRights("Time Zone"))
                {
                    return RedirectToAction("Index", "Home");
                }
                list = objBLL.GetAllTimeZone();
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
            TimeZone_P tz = new TimeZone_P();
            try
            {

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View();
        }

        [CheckAuthorization]
        public ActionResult Edit(int Code)
        {
            TimeZone_P tz = new TimeZone_P();
            try
            {
                tz = objBLL.GetTimeZonebyID(Code);

                if (tz != null)
                {

                }
                else
                {
                    return RedirectToAction("Index", "User");
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(tz);
        }

        [HttpPost]
        public JsonResult AddTimeZone(TimeZone_P timezone)
        {
            bool flag = false;
            bool tzflag = false;
            List<Device_P> device = new List<Device_P>();
            string msg = "";
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement hm = new EagleEyeManagement();
                if (timezone.Period_1_Start == null)
                    timezone.Period_1_Start = "00:00";

                if (timezone.Period_1_End == null)
                    timezone.Period_1_End = "00:00";

                if (timezone.Period_2_Start == null)
                    timezone.Period_2_Start = "00:00";

                if (timezone.Period_2_End == null)
                    timezone.Period_2_End = "00:00";

                if (timezone.Period_3_Start == null)
                    timezone.Period_3_Start = "00:00";

                if (timezone.Period_3_End == null)
                    timezone.Period_3_End = "00:00";

                if (timezone.Period_4_Start == null)
                    timezone.Period_4_Start = "00:00";

                if (timezone.Period_4_End == null)
                    timezone.Period_4_End = "00:00";

                if (timezone.Period_5_Start == null)
                    timezone.Period_5_Start = "00:00";

                if (timezone.Period_5_End == null)
                    timezone.Period_5_End = "00:00";

                if (timezone.Period_6_Start == null)
                    timezone.Period_6_Start = "00:00";

                if (timezone.Period_6_End == null)
                    timezone.Period_6_End = "00:00";

                if (timezone.Period_1_Start != "00:00" && timezone.Period_1_End != "00:00"
                    || timezone.Period_2_Start != "00:00" || timezone.Period_2_End != "00:00"
                    || timezone.Period_3_Start != "00:00" || timezone.Period_3_End != "00:00"
                    || timezone.Period_4_Start != "00:00" || timezone.Period_4_End != "00:00"
                    || timezone.Period_5_Start != "00:00" || timezone.Period_5_End != "00:00"
                    || timezone.Period_6_Start != "00:00" || timezone.Period_6_End != "00:00")
                {
                    timezone.Status = true;


                    tzflag = objBLL.CheckTimeZoneExistInDB(timezone.Timezone_No);
                    if (tzflag != true)
                        flag = objBLL.AddUpdateTimeZone(timezone);
                    else
                    {
                        msg = "TimeZone Already Exists";
                        flag = false;
                    }
                    if (flag)
                    {

                        device = objDevice.GetAllDevices();
                        timezone.Period_1_Start = timezone.Period_1_Start.Replace(":", "");
                        timezone.Period_1_End = timezone.Period_1_End.Replace(":", "");
                        timezone.Period_2_Start = timezone.Period_2_Start.Replace(":", "");
                        timezone.Period_2_End = timezone.Period_2_End.Replace(":", "");
                        timezone.Period_3_Start = timezone.Period_3_Start.Replace(":", "");
                        timezone.Period_3_End = timezone.Period_3_End.Replace(":", "");
                        timezone.Period_4_Start = timezone.Period_4_Start.Replace(":", "");
                        timezone.Period_4_End = timezone.Period_4_End.Replace(":", "");
                        timezone.Period_5_Start = timezone.Period_5_Start.Replace(":", "");
                        timezone.Period_5_End = timezone.Period_5_End.Replace(":", "");
                        timezone.Period_6_Start = timezone.Period_6_Start.Replace(":", "");
                        timezone.Period_6_End = timezone.Period_6_End.Replace(":", "");
                        foreach (var d in device)
                        {
                            if (timezone.Status != false)
                                flag = hm.SetTimeZone(d.Device_ID, timezone, Formatter.SetValidValueToInt(d.Device_Status), account.UserName);

                        }
                    }
                }
                else
                {
                    timezone.Status = false;
                    flag = false;
                    msg = "Can't set invalid timezone to device";
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditTimeZone(TimeZone_P timezone)
        {
            bool flag = false;
            string msg = "";
            List<Device_P> device = new List<Device_P>();
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement hm = new EagleEyeManagement();

                if (timezone.Period_1_Start == null)
                    timezone.Period_1_Start = "00:00";

                if (timezone.Period_1_End == null)
                    timezone.Period_1_End = "00:00";

                if (timezone.Period_2_Start == null)
                    timezone.Period_2_Start = "00:00";

                if (timezone.Period_2_End == null)
                    timezone.Period_2_End = "00:00";

                if (timezone.Period_3_Start == null)
                    timezone.Period_3_Start = "00:00";

                if (timezone.Period_3_End == null)
                    timezone.Period_3_End = "00:00";

                if (timezone.Period_4_Start == null)
                    timezone.Period_4_Start = "00:00";

                if (timezone.Period_4_End == null)
                    timezone.Period_4_End = "00:00";

                if (timezone.Period_5_Start == null)
                    timezone.Period_5_Start = "00:00";

                if (timezone.Period_5_End == null)
                    timezone.Period_5_End = "00:00";

                if (timezone.Period_6_Start == null)
                    timezone.Period_6_Start = "00:00";

                if (timezone.Period_6_End == null)
                    timezone.Period_6_End = "00:00";
                if (timezone.Period_1_Start != "00:00" || timezone.Period_1_End != "00:00"
                    || timezone.Period_2_Start != "00:00" || timezone.Period_2_End != "00:00"
                    || timezone.Period_3_Start != "00:00" || timezone.Period_3_End != "00:00"
                    || timezone.Period_4_Start != "00:00" || timezone.Period_4_End != "00:00"
                    || timezone.Period_5_Start != "00:00" || timezone.Period_5_End != "00:00"
                    || timezone.Period_6_Start != "00:00" || timezone.Period_6_End != "00:00")
                {
                    timezone.Status = true;


                    flag = objBLL.AddUpdateTimeZone(timezone);

                    if (flag)
                    {

                        device = objDevice.GetAllDevices();
                        timezone.Period_1_Start = timezone.Period_1_Start.Replace(":", "");
                        timezone.Period_1_End = timezone.Period_1_End.Replace(":", "");
                        timezone.Period_2_Start = timezone.Period_2_Start.Replace(":", "");
                        timezone.Period_2_End = timezone.Period_2_End.Replace(":", "");
                        timezone.Period_3_Start = timezone.Period_3_Start.Replace(":", "");
                        timezone.Period_3_End = timezone.Period_3_End.Replace(":", "");
                        timezone.Period_4_Start = timezone.Period_4_Start.Replace(":", "");
                        timezone.Period_4_End = timezone.Period_4_End.Replace(":", "");
                        timezone.Period_5_Start = timezone.Period_5_Start.Replace(":", "");
                        timezone.Period_5_End = timezone.Period_5_End.Replace(":", "");
                        timezone.Period_6_Start = timezone.Period_6_Start.Replace(":", "");
                        timezone.Period_6_End = timezone.Period_6_End.Replace(":", "");
                        foreach (var d in device)
                        {
                            if (timezone.Status != false)
                                flag = hm.SetTimeZone(d.Device_ID, timezone, Formatter.SetValidValueToInt(d.Device_Status), account.UserName);
                        }
                    }
                }
                else
                {
                    timezone.Status = false;
                    flag = false;
                    msg = "Can't set invalid timezone to device";
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendTimeZoneToDevice(string[] tzCodes, string[] deviceIds)
        {
            bool flag = false;
            bool devflag = false;
            string msg = "";
            try
            {
                Account_P account = (Account_P)Session["User"];
                for (int i = 0; i < deviceIds.Count(); i++)
                {
                    string DeviceID = deviceIds[i];
                    EagleEyeManagement hm = new EagleEyeManagement();
                    Device_P device = objDevice.GetDeviceByDevice_ID(DeviceID);
                    for (int j = 0; j < tzCodes.Count(); j++)
                    {
                        string tzCode = tzCodes[j];
                        BLLTimeZone objtz = new BLLTimeZone();
                        TimeZone_P tz = new TimeZone_P();
                        if (Formatter.SetValidValueToInt(device.Device_Status) != 0)
                        {
                            tz = objtz.GetTimeZonebyID(Formatter.SetValidValueToInt(tzCode));
                            tz.Period_1_Start = tz.Period_1_Start.Replace(":", "");
                            tz.Period_1_End = tz.Period_1_End.Replace(":", "");
                            tz.Period_2_Start = tz.Period_2_Start.Replace(":", "");
                            tz.Period_2_End = tz.Period_2_End.Replace(":", "");
                            tz.Period_3_Start = tz.Period_3_Start.Replace(":", "");
                            tz.Period_3_End = tz.Period_3_End.Replace(":", "");
                            tz.Period_4_Start = tz.Period_4_Start.Replace(":", "");
                            tz.Period_4_End = tz.Period_4_End.Replace(":", "");
                            tz.Period_5_Start = tz.Period_5_Start.Replace(":", "");
                            tz.Period_5_End = tz.Period_5_End.Replace(":", "");
                            tz.Period_6_Start = tz.Period_6_Start.Replace(":", "");
                            tz.Period_6_End = tz.Period_6_End.Replace(":", "");
                            flag = hm.SetTimeZone(DeviceID, tz, Formatter.SetValidValueToInt(device.Device_Status), account.UserName);

                            devflag = true;
                        }
                        else
                        {
                            msg = "Device " + device.Device_Name + " is offline, TimeZone Set when device connect";
                            devflag = false;
                        }
                    }
                }

                flag = true;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                flag = false;
            }
            return Json(new
            {
                result = flag,
                result2 = devflag,
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTimeZones(string[] lstTZCode)
        {
            List<Device_P> dev = new List<Device_P>();
            EagleEyeManagement em = new EagleEyeManagement();
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                BLLDevice objDev = new BLLDevice();
                dev = objDev.GetAllDevices();
                for (int i = 0; i < lstTZCode.Count(); i++)
                {
                    TimeZone_P tz = objBLL.GetTimeZonebyID(Formatter.SetValidValueToInt(lstTZCode[i]));
                    if (tz.Timezone_No != "1")
                    {
                        flag = objBLL.DeleteTimeZone(Formatter.SetValidValueToInt(lstTZCode[i]));

                        if (flag)
                        {
                            foreach (var d in dev)
                            {
                                tz.Period_1_Start = "0000";
                                tz.Period_1_End = "0000";
                                tz.Period_2_Start = "0000";
                                tz.Period_2_End = "0000";
                                tz.Period_3_Start = "0000";
                                tz.Period_3_End = "0000";
                                tz.Period_4_Start = "0000";
                                tz.Period_4_End = "0000";
                                tz.Period_5_Start = "0000";
                                tz.Period_5_End = "0000";
                                tz.Period_6_Start = "0000";
                                tz.Period_6_End = "0000";

                                flag = em.SetTimeZone(d.Device_ID, tz, Formatter.SetValidValueToInt(d.Device_Status), account.UserName);
                            }
                        }
                    }
                }
                // flag = true;

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