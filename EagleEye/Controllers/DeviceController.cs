using EagleEye.BLL;
using EagleEye.DAL.Partial;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web.Script.Serialization;
using EagleEye.Hubs;
using System.Threading;
using Newtonsoft.Json.Linq;
using EagleEye.Common;
using Common;
using System.Threading.Tasks;

namespace EagleEye.Controllers
{

    public class DeviceController : masterController
    {
        #region Variables

        AppSettingModel app = new AppSettingModel();
        AMS.Profile.Xml profile = null;
        BLLDevice objBLL = new BLLDevice();
        BLLEmployee objempBLL = new BLLEmployee();
        BLLLocation objlocation = new BLLLocation();

        #endregion

        #region Actions

        [CheckAuthorization]
        public ActionResult Index()
        {
            bool flag = true;
            EagleEyeManagement hm = new EagleEyeManagement();
            List<Device_P> list = new List<Device_P>();
            List<Location_P> list_loc = new List<Location_P>();
            List<Device_P> newList = new List<Device_P>();

            try
            {

                Account_P account = (Account_P)Session["User"];
                if (!CheckRights("Device"))
                {
                    return RedirectToAction("Index", "Home");
                }
                list_loc = objlocation.GetAllLocation();
                list = objBLL.GetAllDevices();
                foreach (var i in list)
                {
                    if (i.Device_Status == 1)
                    {
                        DateTime dt = DateTime.Now;
                        //flag = hm.SetDeviceTIme(i.Device_ID, dt, account.UserName);
                    }
                    //string location = i.Device_Location;
                    if (i.Device_Location != null)
                    {
                        i.LocationDescription = list_loc.Where(x => x.Code == Formatter.SetValidValueToInt(i.Device_Location)).Select(x => x.Description).FirstOrDefault();
                    }
                    newList.Add(i);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            ViewBag.Loc_list = list_loc;
            return View(newList);
        }

        [CheckAuthorization]
        public ActionResult Edit(int code)
        {
            Device_P device = new Device_P();
            //EagleEyeManagement hm = new EagleEyeManagement();
            List<Location_P> list_loc = new List<Location_P>();
            bool sflag = false;
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                List<Employee_P> empList = new List<Employee_P>();
                Device_P deviceInfo = new Device_P();

                //EagleEyeManagement hm = new EagleEyeManagement();
                device = objBLL.GetDeviceByCode(code);
                list_loc = objlocation.GetAllLocation();
                //DateTime dt = DateTime.Now;
                //flag = hm.SetDeviceTIme(device.Device_ID, dt, account.UserName);
                //app = LoadTimeout();
                //device.WebTimeout = Formatter.SetValidValueToInt(app.WebTimeout) * 1000;
                //list_loc = objlocation.GetAllLocation();
                //if (device != null && device.Device_Status != 0)
                //{
                //    sflag = hm.GetDeviceStatus(device.Device_ID, account.UserName);
                //    flag = hm.GetDeviceSettings(device.Device_ID, account.UserName);
                //}
                empList = objempBLL.GetEmployeeBydevID(device.Device_ID);
                foreach (var emp in empList)
                {
                    if (emp.Face == true)
                        device.FaceCount++;

                    if (emp.FingerPrint == true)
                        device.FingerCount++;

                    if (emp.Palm == true)
                        device.PalmCount++;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            ViewBag.Loc_list = list_loc;


            return View(device);
        }


        public JsonResult getDeviceSettingAfterSignalR(int code)
        {
            Device_P device = new Device_P();
            List<Location_P> list_loc = new List<Location_P>();
            Account_P account = (Account_P)Session["User"];
            bool sflag = false;
            bool flag = false;
            try
            {
                EagleEyeManagement hm = new EagleEyeManagement();
                device = objBLL.GetDeviceByCode(code);
                app = LoadTimeout();
                device.WebTimeout = Formatter.SetValidValueToInt(app.WebTimeout) * 1000;
                list_loc = objlocation.GetAllLocation();
                if (device != null && device.Device_Status != 0)
                {
                    sflag = hm.GetDeviceStatus(device.Device_ID, account.UserName);
                    flag = hm.GetDeviceSettings(device.Device_ID, account.UserName);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
            });
        }

        public AppSettingModel LoadTimeout()
        {
            try
            {
                profile = xmlReader.GetProfile("Read", ProjectPath.FILE_PATH);
                app.WebTimeout = profile.GetValue("Machine Setting", "WebTimeout", "");
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return app;
        }



        [CheckAuthorization]
        public ActionResult Devices()
        {

            List<Device_P> list = new List<Device_P>();

            try
            {
                list = objBLL.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return PartialView(list);

        }

        #endregion

        #region Methods

        [HttpPost]
        public JsonResult ConfirmPassword(string UserName, string Password, int Action, int devID)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[10];
            bool flag = false;
            Device_P device = new Device_P();
            BLLAccount objacc = new BLLAccount();
            try
            {
                if (UserName == "admin")
                {
                    var acc = objacc.GetAccountByUser(UserName);

                    //Assign HASH Value
                    if (acc != null)
                    {
                        OldHASHValue = acc.Hash;
                    }

                    bool isTrue = CompareHashValue(Password, UserName, OldHASHValue, SALT);

                    if (isTrue)
                    {
                        device = objBLL.GetDeviceByCode(devID);
                        switch (Action)
                        {
                            case 4:
                                flag = ResetFK(device);
                                break;
                        }
                        // flag = true;
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
        [HttpPost]
        public JsonResult ConfirmDelete(string UserName, string Password, int Action, string[] deviceIds)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[10];
            bool flag = false;
            Device_P device = new Device_P();
            BLLAccount objacc = new BLLAccount();
            try
            {
                if (UserName == "admin")
                {
                    var acc = objacc.GetAccountByUser(UserName);

                    //Assign HASH Value
                    if (acc != null)
                    {
                        OldHASHValue = acc.Hash;
                    }

                    bool isTrue = CompareHashValue(Password, UserName, OldHASHValue, SALT);

                    for (int i = 0; i < deviceIds.Count(); i++)
                    {
                        if (isTrue)
                        {
                            device = objBLL.GetDeviceByDevice_ID(deviceIds[i]);
                            switch (Action)
                            {
                                case 1:
                                    flag = InitDeviceUsers(device);
                                    break;
                                case 2:
                                    flag = InitDeviceAdmin(device);
                                    break;
                                case 3:
                                    flag = InitDeviceLogs(device);
                                    break;
                            }
                        }
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


        [HttpPost]
        public JsonResult SearchDevice(string deviceid)
        {
            bool flag = false;
            BLLAwaitingDevice obj = new BLLAwaitingDevice();
            AwaitingDevice_P device_P = new AwaitingDevice_P();

            try
            {
                device_P = obj.GetAwaitingDeviceByDeviceID(deviceid);

                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                info = device_P
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Location()
        {
            BLLLocation objLocBLL = new BLLLocation();
            List<Att_Status_P.Location_P> list = new List<Att_Status_P.Location_P>();
            try
            {

                list = objLocBLL.GetAllLocations();


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, "LoadDBIntegrationSettings");
            }
            return Json(new
            {
                result = list

            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddDevice(AwaitingDevice_P deviceInfo, string name, string type, string loc)
        {

            bool flag = false;
            string msg = "";
            EagleEyeManagement hm = new EagleEyeManagement();
            DateTime dt;
            try
            {
                Account_P account = (Account_P)Session["User"];
                License Lic = new License();
                //if (!Lic.CheckDeviceLicense(deviceInfo.Device_ID))
                //{
                //    msg = "Invalid Device Serial Number!";
                //}
                //else
                if (objBLL.CheckDeviceExistInDB(deviceInfo.Device_ID))
                {
                    msg = "Device Already Exist!!";
                }
                else
                {
                    Device_P d = new Device_P
                    {
                        Device_ID = deviceInfo.Device_ID,
                        Device_Name = name,
                        Device_Info = deviceInfo.Device_Info,
                        Device_Status_Info = deviceInfo.Device_Status_Info,
                        Device_Location = loc

                    };

                    d.Active = true;
                    d.Device_Status = (int)DeviceStatus.Connected;
                    List<string> list = new List<string>();

                    string rem = d.Device_Info.Replace("{", "").Replace("}", "").Replace("\"", "").Replace("[", "").Replace("]", "");
                    string[] device_Info = rem.Split(',');
                    for (int i = 0; i < device_Info.Length; i++)
                    {
                        string[] ds = device_Info[i].Split(':');
                        for (int j = 0; j < ds.Length; j++)
                        {
                            list.Add(ds[j]);
                        }
                    }
                    d.Face_Data_Ver = list[1].ToString();
                    d.Firmware = list[3].ToString();
                    d.Firmware_Filename = list[5].ToString();
                    d.Fk_Bin_Data_Lib = list[7].ToString();
                    d.Fp_Data_Ver = list[9].ToString();
                    d.Supported_Enroll_Data = list[11].ToString() + "," + list[12].ToString() + "," + list[13].ToString() + "," + list[14].ToString();

                    flag = hm.SetDeviceName(deviceInfo.Device_ID, d.Device_Name, account.UserName);
                    flag = hm.GetDeviceStatus(d.Device_ID, account.UserName);
                    flag = hm.GetDeviceSettings(d.Device_ID, account.UserName);
                    dt = DateTime.Now;
                    flag = hm.SyncDevice(deviceInfo.Device_ID, account.UserName);
                    flag = hm.SetDeviceTIme(deviceInfo.Device_ID, dt, account.UserName);
                    if (flag)
                    {
                        flag = objBLL.AddDevice(d);
                    }


                    if (flag)
                    {
                        BLLAwaitingDevice objAwaiting = new BLLAwaitingDevice();
                        objAwaiting.DeleteAwaitingDevice(d.Device_ID);
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
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDevice(Device_P deviceInfo)
        {
            bool flag = false;
            DateTime dt;
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement hm = new EagleEyeManagement();
                flag = objBLL.UpdateDeviceInfo(deviceInfo);
                if (flag)
                {
                    if (!string.IsNullOrEmpty(deviceInfo.Device_Name))
                    {

                        flag = hm.SetDeviceName(deviceInfo.Device_ID, deviceInfo.Device_Name, account.UserName);
                    }
                    switch (Convert.ToString(deviceInfo.Use_Alarm))
                    {
                        case "false":
                            deviceInfo.Use_Alarm = "no";
                            break;
                        case "true":
                            deviceInfo.Use_Alarm = "yes";
                            break;
                        default:
                            deviceInfo.Use_Alarm = "NO ACCESS";
                            break;
                    }
                    switch (Convert.ToString(deviceInfo.DoorMagnetic_Type))
                    {
                        case "0":
                            deviceInfo.DoorMagnetic_Type = "no";
                            break;
                        case "1":
                            deviceInfo.DoorMagnetic_Type = "open";
                            break;
                        case "2":
                            deviceInfo.DoorMagnetic_Type = "close";
                            break;
                        default:
                            deviceInfo.DoorMagnetic_Type = "NO ACCESS";
                            break;
                    }
                    switch (deviceInfo.Anti_back)
                    {
                        case "false":
                            deviceInfo.Anti_back = "no";
                            break;
                        case "true":
                            deviceInfo.Anti_back = "yes";
                            break;
                        default:
                            deviceInfo.Anti_back = "NO ACCESS";
                            break;
                    }
                    dt = DateTime.Now;
                    flag = hm.SetDeviceSetting(deviceInfo, account.UserName);
                    flag = hm.SetDeviceTIme(deviceInfo.Device_ID, dt, account.UserName);
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


        [HttpPost]
        public JsonResult DeleteAwaitingDevices(string device_ID)
        {
            bool flag = false;
            try
            {
                BLLAwaitingDevice objAwaiting = new BLLAwaitingDevice();
                flag = objAwaiting.DeleteAwaitingDevice(device_ID);
                flag = objAwaiting.DeleteAwaitingDeviceC(device_ID);
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

        [HttpPost]
        public JsonResult SetDeviceTime(Device_P deviceInfo, string dt)
        {

            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                //DateTime datetime = DateTime.Now;
                DateTime datetime = Formatter.SetValidValueToDateTime(dt);
                EagleEyeManagement hm = new EagleEyeManagement();
                flag = hm.SetDeviceTIme(deviceInfo.Device_ID, datetime, account.UserName);
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

        [HttpPost]
        public JsonResult SetDeviceName(Device_P deviceInfo, string dev_name)
        {
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement hm = new EagleEyeManagement();
                flag = hm.SetDeviceName(deviceInfo.Device_ID, dev_name, account.UserName);
                if (flag)
                {
                    deviceInfo.Device_Name = dev_name;
                    objBLL.UpdateDeviceName(deviceInfo);
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

        [HttpPost]
        public JsonResult SetNetworkInfo(Device_P deviceInfo, string ipAddress, string port)
        {
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement hm = new EagleEyeManagement();
                flag = hm.SetNetworkInfo(deviceInfo.Device_ID, ipAddress, port, account.UserName);
                if (flag)
                {
                    deviceInfo.Server_Address = HelpingMethod.RemoveZeros(ipAddress);
                    deviceInfo.Server_Port = port;
                    objBLL.UpdateServerSetting(deviceInfo);
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

        [HttpPost]
        public JsonResult SyncDevice(int deviceCode)
        {
            bool flag = false;
            string deviceName = "";
            Device_P deviceInfo = new Device_P();
            Employee_P employee = new Employee_P();
            EagleEyeManagement hm = new EagleEyeManagement();
            try
            {
                Account_P account = (Account_P)Session["User"];
                deviceInfo = objBLL.GetDeviceByCode(deviceCode);
                flag = hm.SyncDevice(deviceInfo.Device_ID, account.UserName);
                if (deviceInfo != null)
                {
                    deviceName = deviceInfo.Device_Name;
                    DateTime dt = DateTime.Now;
                    flag = hm.SetDeviceTIme(deviceInfo.Device_ID, dt, account.UserName);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                device = deviceName
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SyncTime(int deviceCode)
        {
            bool flag = false;
            string deviceName = "";
            Device_P deviceInfo = new Device_P();
            EagleEyeManagement hm = new EagleEyeManagement();
            try
            {
                Account_P account = (Account_P)Session["User"];
                deviceInfo = objBLL.GetDeviceByCode(deviceCode);
                if (deviceInfo != null)
                {
                    deviceName = deviceInfo.Device_Name;
                    DateTime dt = DateTime.Now;
                    flag = hm.SetDeviceTIme(deviceInfo.Device_ID, dt, account.UserName);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                device = deviceName
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteDevice(string[] deviceCodes)
        {
            Device_P device = new Device_P();
            bool flag = false;
            try
            {
                for (int i = 0; i < deviceCodes.Count(); i++)
                {
                    device = objBLL.GetDeviceByCode(Formatter.SetValidValueToInt(deviceCodes[i]));
                    flag = objBLL.DeleteDevice(Formatter.SetValidValueToInt(deviceCodes[i]));
                    flag = objBLL.DeleteDeviceFK(device.Device_ID);
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

        [HttpPost]
        public JsonResult GeAllAwaitingDevices()
        {

            List<AwaitingDevice_P> List = new List<AwaitingDevice_P>();
            try
            {
                BLLAwaitingDevice obj = new BLLAwaitingDevice();
                List = obj.GetAllAwaitingDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = List
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GeAllDevices()
        {

            List<Device_P> List = new List<Device_P>();
            try
            {

                List = objBLL.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = List
            }, JsonRequestBehavior.AllowGet);

        }

        public bool InitDeviceAdmin(Device_P deviceInfo)
        {

            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                if (deviceInfo != null)
                {
                    EagleEyeManagement hm = new EagleEyeManagement();
                    flag = hm.InitDeviceAdmin(deviceInfo.Device_ID, account.UserName);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
            //return Json(new
            //{
            //    result = flag
            //}, JsonRequestBehavior.AllowGet);

        }

        public bool InitDeviceUsers(Device_P deviceInfo)
        {

            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                if (deviceInfo != null)
                {
                    EagleEyeManagement hm = new EagleEyeManagement();
                    flag = hm.InitDeviceUsers(deviceInfo.Device_ID, account.UserName);
                    if (flag)
                        flag = hm.SyncDevice(deviceInfo.Device_ID, account.UserName);

                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
            //return Json(new
            //{
            //    result = flag
            //}, JsonRequestBehavior.AllowGet);

        }

        public bool InitDeviceLogs(Device_P deviceInfo)
        {
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                if (deviceInfo != null)
                {
                    EagleEyeManagement hm = new EagleEyeManagement();
                    flag = hm.InitDeviceLogs(deviceInfo.Device_ID, account.UserName);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
            //return Json(new
            //{
            //    result = flag
            //}, JsonRequestBehavior.AllowGet);

        }

        public bool ResetFK(Device_P device)
        {

            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                if (device != null)
                {
                    EagleEyeManagement hm = new EagleEyeManagement();
                    flag = hm.ResetFK(device.Device_ID, account.UserName);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
            //return Json(new
            //{
            //    result = flag
            //}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetCurrentTime()
        {
            string dt = "";
            try
            {
                dt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = dt
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetAllDevices()
        {

            List<Device_P> List = new List<Device_P>();
            try
            {
                BLLDevice obj = new BLLDevice();
                List = obj.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = List,
                count = List.Count
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetEmployees(int deviceCode)
        {
            Device_P deviceInfo = new Device_P();
            bool flag = false;
            bool flag2 = false;
            //List<Employee_P> list = new List<Employee_P>();
            //var serializer = new JavaScriptSerializer
            //{
            //    MaxJsonLength = Int32.MaxValue
            //};
            try
            {
                Account_P account = (Account_P)Session["User"];
                deviceInfo = objBLL.GetDeviceByCode(deviceCode);
                if (deviceInfo != null)
                {
                    if (deviceInfo.Device_Status != 0)
                    {
                        EagleEyeManagement hm = new EagleEyeManagement();
                        flag = hm.GetEmployees(deviceInfo.Device_ID, account.UserName);
                        flag2 = true;
                    }
                    else
                    {
                        flag2 = false;
                    }

                    //list = hm.GetEmployees(deviceInfo.Device_ID);

                    //IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<WebHub>();
                    //hubContext.Clients.All.employeeids(list);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            //var resultdata = new
            //{
            //    result = list
            //};
            return Json(new
            {
                result = flag,
                result2 = flag2
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadUsers(string[] listSelected, string device_ID)
        {

            BLLEmployee objBll = new BLLEmployee();
            bool flag = false;
            string msg = "";
            EagleEyeManagement em = new EagleEyeManagement();
            List<Employee_P> empList = new List<Employee_P>();

            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            try
            {
                Device_P deviceInfo = objBLL.GetDeviceByCode(Formatter.SetValidValueToInt(device_ID));
                empList = objBll.GetEmployeeList(listSelected, deviceInfo.Device_ID);

                foreach (var emp in empList)
                {
                    Employee_P employee = new Employee_P();
                    em.DecryptEmployees(emp.Cmd_Param, deviceInfo.Device_ID, out employee);
                    flag = objBll.UploadEmployee(employee, emp);
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
        public JsonResult DeleteUsersFromMachine(string[] listSelected, string device_ID)
        {
            bool flag = false;
            Device_P deviceInfo = new Device_P();
            try
            {
                Account_P account = (Account_P)Session["User"];
                deviceInfo = objBLL.GetDeviceByCode(Formatter.SetValidValueToInt(device_ID));
                if (deviceInfo != null)
                {
                    EagleEyeManagement hm = new EagleEyeManagement();

                    for (int i = 0; i < listSelected.Length; i++)
                    {
                        flag = hm.DeleteUserfromMachine(listSelected[i], deviceInfo.Device_ID, account.UserName);
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
                code = deviceInfo.Code
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetDeviceSetting(string machineInfo, string deviceCode)
        {
            JObject vResultJson;
            Device_P deviceInfo = new Device_P();
            bool flag = false;
            try
            {
                vResultJson = JObject.Parse(machineInfo);
                if (vResultJson.Property("OpenDoor_Delay") != null)
                    deviceInfo.OpenDoor_Delay = vResultJson["OpenDoor_Delay"].ToString();
                if (vResultJson.Property("DoorMagnetic_Delay") != null)
                    deviceInfo.DoorMagnetic_Delay = vResultJson["DoorMagnetic_Delay"].ToString();
                if (vResultJson.Property("Alarm_Delay") != null)
                    deviceInfo.Alarm_Delay = vResultJson["Alarm_Delay"].ToString();
                if (vResultJson.Property("Sleep_Time") != null)
                    deviceInfo.Sleep_Time = vResultJson["Sleep_Time"].ToString();
                if (vResultJson.Property("Screensavers_Time") != null)
                    deviceInfo.Screensavers_Time = vResultJson["Screensavers_Time"].ToString();
                if (vResultJson.Property("Reverify_Time") != null)
                    deviceInfo.Reverify_Time = vResultJson["Reverify_Time"].ToString();
                if (vResultJson.Property("DoorMagnetic_Type") != null)
                    deviceInfo.DoorMagnetic_Type = vResultJson["DoorMagnetic_Type"].ToString();
                if (vResultJson.Property("Anti-back") != null)
                    deviceInfo.Anti_back = vResultJson["Anti-back"].ToString();
                if (vResultJson.Property("Use_Alarm") != null)
                    deviceInfo.Use_Alarm = vResultJson["Use_Alarm"].ToString();
                if (vResultJson.Property("Wiegand_Type") != null)
                    deviceInfo.Wiegand_Type = vResultJson["Wiegand_Type"].ToString();
                if (vResultJson.Property("Glog_Warning") != null)
                    deviceInfo.Glog_Warning = vResultJson["Glog_Warning"].ToString();
                if (vResultJson.Property("Volume") != null)
                    deviceInfo.Volume = vResultJson["Volume"].ToString();
                if (vResultJson.Property("Allow_EarlyTime") != null)
                    deviceInfo.Allow_EarlyTime = vResultJson["Allow_EarlyTime"].ToString();
                if (vResultJson.Property("Allow_LateTime") != null)
                    deviceInfo.Allow_LateTime = vResultJson["Allow_LateTime"].ToString();
                if (vResultJson.Property("Receive_Interval") != null)
                    deviceInfo.Receive_Interval = vResultJson["Receive_Interval"].ToString();
                if (vResultJson.Property("Wiegand_Input") != null)
                    deviceInfo.Wiegand_Input = vResultJson["Wiegand_Input"].ToString();
                if (vResultJson.Property("Wiegand_Output") != null)
                    deviceInfo.Wiegand_Output = vResultJson["Wiegand_Output"].ToString();
                if (vResultJson.Property("Show_ResultTime") != null)
                    deviceInfo.Show_ResultTime = vResultJson["Show_ResultTime"].ToString();
                if (vResultJson.Property("MutiUser") != null)
                    deviceInfo.Multi_Users = vResultJson["MutiUser"].ToString();


                if (deviceInfo.Anti_back == null)
                {
                    deviceInfo.Anti_back = "NO ACCESS";
                    deviceInfo.Device_Type = "ATTENDENCE";
                }
                else
                {
                    deviceInfo.Device_Type = "ACCESS CONTROL";
                    deviceInfo.Anti_back = deviceInfo.Anti_back;
                }

                if (deviceInfo.DoorMagnetic_Type == null)
                {
                    deviceInfo.DoorMagnetic_Type = "NO ACCESS";
                    deviceInfo.Device_Type = "ATTENDENCE";
                }
                else
                {
                    deviceInfo.Device_Type = "ACCESS CONTROL";
                    deviceInfo.DoorMagnetic_Type = deviceInfo.DoorMagnetic_Type;
                }

                if (deviceInfo.Use_Alarm == null)
                {
                    deviceInfo.Use_Alarm = "NO ACCESS";
                    deviceInfo.Device_Type = "ATTENDENCE";
                }
                else
                {
                    deviceInfo.Device_Type = "ACCESS CONTROL";
                    deviceInfo.Use_Alarm = deviceInfo.Use_Alarm;
                }


                deviceInfo.Device_ID = deviceCode;

                flag = objBLL.UpdateMachineInfo(deviceInfo);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = deviceInfo,
                flag = flag
                //  code = deviceInfo.Code
            }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public JsonResult GetDeviceStatusInfo(string statusInfo, string deviceCode)
        {
            JObject vResultJson;
            Device_P deviceInfo = new Device_P();
            try
            {
                vResultJson = JObject.Parse(statusInfo);
                if (vResultJson.Property("face_count") != null)
                    deviceInfo.Real_FaceReg = vResultJson["face_count"].ToString();

                if (vResultJson.Property("face_max") != null)
                    deviceInfo.Max_FaceReg = vResultJson["face_max"].ToString();

                if (vResultJson.Property("fp_count") != null)
                    deviceInfo.Real_FPReg = vResultJson["fp_count"].ToString();

                if (vResultJson.Property("fp_max") != null)
                    deviceInfo.Max_FPReg = vResultJson["fp_max"].ToString();

                if (vResultJson.Property("idcard_count") != null)
                    deviceInfo.Real_IDCardReg = vResultJson["idcard_count"].ToString();

                if (vResultJson.Property("idcard_max") != null)
                    deviceInfo.Max_IDCardReg = vResultJson["idcard_max"].ToString();

                if (vResultJson.Property("manager_count") != null)
                    deviceInfo.Real_Manager = vResultJson["manager_count"].ToString();

                if (vResultJson.Property("manager_max") != null)
                    deviceInfo.Max_Manager = vResultJson["manager_max"].ToString();

                if (vResultJson.Property("password_count") != null)
                    deviceInfo.Real_PasswordReg = vResultJson["password_count"].ToString();

                if (vResultJson.Property("password_max") != null)
                    deviceInfo.Max_PasswordReg = vResultJson["password_max"].ToString();

                if (vResultJson.Property("pv_count") != null)
                    deviceInfo.Real_PvReg = vResultJson["pv_count"].ToString();

                if (vResultJson.Property("pv_max") != null)
                    deviceInfo.Max_PvReg = vResultJson["pv_max"].ToString();

                if (vResultJson.Property("total_log_count") != null)
                    deviceInfo.Total_log_Count = vResultJson["total_log_count"].ToString();

                if (vResultJson.Property("total_log_max") != null)
                    deviceInfo.Total_log_Max = vResultJson["total_log_max"].ToString();

                if (vResultJson.Property("user_count") != null)
                    deviceInfo.Real_Employee = vResultJson["user_count"].ToString();

                if (vResultJson.Property("user_max") != null)
                    deviceInfo.Max_Employee = vResultJson["user_max"].ToString();

                if (vResultJson.Property("all_log_count") != null)
                    deviceInfo.Max_Record = vResultJson["all_log_count"].ToString();

                deviceInfo.Device_Status_Info = statusInfo;
                deviceInfo.Device_ID = deviceCode;

                objBLL.UpdateDeviceStatus(deviceInfo);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = deviceInfo
            }, JsonRequestBehavior.AllowGet);
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
        #endregion
    }
}