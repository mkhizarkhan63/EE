using EagleEye.BLL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.DTO;
using System.Threading.Tasks;
using System.Text;
using EagleEye.Common;
using Common;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace EagleEye.Controllers
{
    public class APIController : masterController
    {

        private string Apikey = "E@gleEyeAp!Ki";
        BLLEmployee EmpBLL = new BLLEmployee();

        [HttpGet]
        public JsonResult GetAllDevices(string Key)
        {

            if (Key != Apikey)
            {
                return Json(new { error = "Invalid Key" }, JsonRequestBehavior.AllowGet);
            }


            List<DeviceInfo> List = new List<DeviceInfo>();
            try
            {
                BLLDevice objDevice = new BLLDevice();
                var L = objDevice.GetAllDevices().Where(x => x.Active == true);
                foreach (var item in L)
                {
                    DeviceInfo info = new DeviceInfo { Device_ID = item.Device_ID, Device_Name = item.Device_Name };
                    List.Add(info);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { result = List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetConnectedDevices(string Key)
        {
            if (Key != Apikey)
            {
                return Json(new { error = "Invalid Key" }, JsonRequestBehavior.AllowGet);
            }

            List<DeviceInfo> List = new List<DeviceInfo>();
            try
            {
                BLLDevice objDevice = new BLLDevice();
                var L = objDevice.GetAllConnectedDevices().Where(x => x.Device_Status == 1);
                foreach (var item in L)
                {
                    DeviceInfo info = new DeviceInfo { Device_ID = item.Device_ID, Device_Name = item.Device_Name };
                    List.Add(info);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { result = List }, JsonRequestBehavior.AllowGet);
        }

        // GET: API
        //Get Employee
        [HttpGet]
        public JsonResult GetRecords(string Key, string[] DeviceIds, DateTime? FromDate, DateTime? ToDate)
        {
            if (Key != Apikey)
            {
                return Json(new { error = "Invalid Key" }, JsonRequestBehavior.AllowGet);
            }
            List<string> error = new List<string>();
            List<AttendanceDTO> List = new List<AttendanceDTO>();
            var deviceCheck = false;
            try
            {

                BLLDevice objDevice = new BLLDevice();
                BLLAttendance objAtt = new BLLAttendance();


                if (DeviceIds.Count() > 0)
                {
                    List<Device_P> Devices = new List<Device_P>();
                    if (DeviceIds.Contains("-1"))
                    {
                        Devices = objDevice.GetAllDevices().Where(x => x.Active == true).ToList();
                        foreach (var elem in Devices)
                        {
                            List.AddRange(objAtt.GetAttendanceByDATEandDEVICE(FromDate?.ToString("yyyy-MM-dd HH:mm:ss"), ToDate?.ToString("yyyy-MM-dd HH:mm:ss"), elem.Device_ID));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DeviceIds.Length; i++)
                        {
                            Devices = objDevice.GetAllDevices().Where(x => DeviceIds[i].Contains(x.Device_ID)).ToList();
                            if (Devices.Count() > 0)
                            {
                                foreach (var item in Devices)
                                {
                                    deviceCheck = true;
                                    if (item.Device_Status == 0)
                                    {
                                        error.Add("Device Disconnected: " + DeviceIds[i]);
                                        deviceCheck = false;
                                    }


                                    if (deviceCheck)
                                    {

                                        Devices = Devices.Where(x => x.Device_Status == 1 && DeviceIds.Contains(x.Device_ID)).ToList();

                                        foreach (var elem in Devices)
                                        {
                                            List.AddRange(objAtt.GetAttendanceByDATEandDEVICE(FromDate?.ToString("yyyy-MM-dd HH:mm:ss"), ToDate?.ToString("yyyy-MM-dd HH:mm:ss"), elem.Device_ID));
                                        }

                                    }
                                }



                            }
                            else
                            {
                                error.Add("Invalid Parmeters For Device: " + DeviceIds[i]);
                            }

                        }
                    }

                    //if (Devices.Count() == 0)
                    //{

                    //        if (item.Device_Status == 1)
                    //            return Json(new { error = "Invalid Parameters" }, JsonRequestBehavior.AllowGet);
                    //        else if (item.Device_Status == 0)
                    //            return Json(new { error = "Device Disconnected" }, JsonRequestBehavior.AllowGet);

                    //}




                }


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new { result = List, error, }, JsonRequestBehavior.AllowGet);
        }


        //POST : API 
        //Add or Update Employee With Transfer into Device
        [HttpPost]
        public JsonResult SendEmployee(string Key, string ID, string Name, HttpPostedFileBase Picture, string[] DeviceIds, string Pin, string CardNo, bool Active)
        {
            EagleEyeManagement hm = new EagleEyeManagement();
            StringBuilder res = new StringBuilder();
            bool empflag = false;
            bool cardflag = false;
            bool flag = false;
            string msg = "";
            bool check = false;
            List<string> error = new List<string>();
            Employee_P employee = new Employee_P();
            BLLDevice objDevice = new BLLDevice();
            List<Device_P> Devices = new List<Device_P>();
            bool deviceCheck = false;
            string[] deviceArray = new string[DeviceIds.Length];
            try
            {
                //Devices = objDevice.GetAllDevices().Where(x => DeviceIds.Contains(x.Device_ID)).ToList();
                for (int i = 0; i < DeviceIds.Length; i++)
                {
                    Devices = objDevice.GetAllDevices().Where(x => DeviceIds[i].Contains(x.Device_ID)).ToList();
                    if (Devices.Count() > 0)
                    {
                        foreach (var item in Devices)
                        {
                            deviceCheck = true;
                            //checking for device
                            if (item.Device_Status == 0)
                            {
                                error.Add("Device Disconnected: " + item.Device_ID);
                                deviceCheck = false;
                            }
                            if (deviceCheck)
                            {
                                employee.Employee_ID = ID;
                                empflag = EmpBLL.CheckEmployeeExistInDB(employee.Employee_ID);

                                employee.Card_No = CardNo;
                                if (employee.Card_No != "")
                                    cardflag = EmpBLL.CheckCardExistInDB(employee.Card_No, employee.Employee_ID);


                                employee.Code = 0;
                                employee.Employee_Name = Name;

                                employee.IrregularCode = "";
                                employee.Email = "";
                                employee.Address = "";
                                employee.Gender = "0";
                                employee.Device_Id = "";


                                employee.Cmd_Param = new byte[0];
                                employee.finger_0 = new byte[0];
                                employee.finger_1 = new byte[0];
                                employee.finger_2 = new byte[0];
                                employee.finger_3 = new byte[0];
                                employee.finger_4 = new byte[0];
                                employee.finger_5 = new byte[0];
                                employee.finger_6 = new byte[0];
                                employee.finger_7 = new byte[0];
                                employee.finger_8 = new byte[0];
                                employee.finger_9 = new byte[0];
                                employee.face_data = new byte[0];
                                employee.palm_0 = new byte[0];
                                employee.palm_1 = new byte[0];
                                employee.photo_data = new byte[0];



                                employee.Valid_DateEnd = "";
                                employee.Valid_DateStart = "";
                                employee.Saturday = "";
                                employee.Sunday = "";
                                employee.Thursday = "";
                                employee.Tuesday = "";
                                employee.Monday = "";
                                employee.Friday = "";
                                employee.Wednesday = "";

                                if (employee.Saturday == "null" || employee.Saturday == "" || employee.Saturday == "0")
                                    employee.Saturday = "1";

                                if (employee.Sunday == "null" || employee.Sunday == "" || employee.Sunday == "0")
                                    employee.Sunday = "1";

                                if (employee.Thursday == "null" || employee.Thursday == "" || employee.Thursday == "0")
                                    employee.Thursday = "1";

                                if (employee.Tuesday == "null" || employee.Tuesday == "" || employee.Tuesday == "0")
                                    employee.Tuesday = "1";

                                if (employee.Monday == "null" || employee.Monday == "" || employee.Monday == "0")
                                    employee.Monday = "1";

                                if (employee.Friday == "null" || employee.Friday == "" || employee.Friday == "0")
                                    employee.Friday = "1";

                                if (employee.Wednesday == "null" || employee.Wednesday == "" || employee.Wednesday == "0")
                                    employee.Wednesday = "1";

                                string DepartmentCode = "";
                                string LocationCode = "";
                                string DesignationCode = "";
                                string EmpTypeCode = "";

                                if (!string.IsNullOrEmpty(DesignationCode) && DesignationCode != "null")
                                    employee.fkDesignation_Code = Formatter.SetValidValueToInt(DesignationCode);

                                if (!string.IsNullOrEmpty(EmpTypeCode) && EmpTypeCode != "null")
                                    employee.fkEmployeeType_Code = Formatter.SetValidValueToInt(EmpTypeCode);

                                if (!string.IsNullOrEmpty(DepartmentCode) && DepartmentCode != "null")
                                    employee.fkDepartment_Code = Formatter.SetValidValueToInt(DepartmentCode);

                                if (!string.IsNullOrEmpty(LocationCode) && LocationCode != "null")
                                    employee.fkLocation_Code = Formatter.SetValidValueToInt(LocationCode);


                                employee.Telephone = "+92";

                                employee.Password = Pin;
                                employee.User_Privilege = "USER";

                                string face = "false";
                                if (!string.IsNullOrEmpty(face) && face != "null")
                                    employee.Face = Formatter.SetValidValueToBool(face);

                                string fp = "false";
                                if (!string.IsNullOrEmpty(fp) && fp != "null")
                                    employee.FingerPrint = Formatter.SetValidValueToBool(fp);

                                string palm = "false";
                                if (!string.IsNullOrEmpty(palm) && palm != "null")
                                    employee.Palm = Formatter.SetValidValueToBool(palm);

                                string Actives = Active.ToString();
                                if (!string.IsNullOrEmpty(Actives) && Actives != "null")
                                {
                                    switch (Actives.ToLower().ToString())
                                    {
                                        case "true":
                                            employee.Active = 1;
                                            break;
                                        case "false":
                                            employee.Active = 0;
                                            break;
                                    }
                                }


                                string MainPath = "";
                                Stream photoStream = null;
                                string httpURL = "";
                                if (Request.Files.Count > 0)
                                {
                                    HttpFileCollectionBase files = Request.Files;

                                    HttpPostedFileBase file = files[0];
                                    string fileName = "Profile_" + employee.Employee_ID + "." + Picture.FileName.Split('.')[1];

                                    Directory.CreateDirectory(Server.MapPath("~/profiles/"));
                                    MainPath = Path.Combine(Server.MapPath("~/profiles/"), fileName);


                                    photoStream = file.InputStream;

                                    string url = HttpContext.Request.Url.Authority;
                                    employee.Employee_Photo = "http://" + url + "/profiles/" + fileName;


                                    if (photoStream != null)
                                        ReduceImageSize(photoStream, MainPath);

                                    httpURL = employee.Employee_Photo;



                                    byte[] bytes = new byte[0];

                                    if (employee.Employee_Photo.Contains(httpURL))
                                    {
                                        using (WebClient client = new WebClient())
                                        {
                                            bytes = client.DownloadData(employee.Employee_Photo);
                                        }
                                    }
                                    employee.photo_data = bytes;
                                    // create the uploads folder if it doesn't exist

                                    photoStream = new MemoryStream(bytes);
                                    employee.Employee_Photo = "/profiles/" + fileName;

                                }
                                else
                                {
                                    employee.Employee_Photo = "";
                                }
                                if (photoStream != null)
                                    ReduceImageSize(photoStream, MainPath);


                                byte[] encEmployee = new byte[0];
                                hm.EncryptEmployee(employee, out encEmployee);
                                employee.Cmd_Param = encEmployee;

                                flag = EmpBLL.AddUpdateEmployee(employee);

                                if (flag)
                                {

                                    employee = EmpBLL.GetEmployeeByID(employee.Employee_ID);
                                    SendUserToDevice(employee.Code.ToString(), item.Device_ID);


                                    bool exist = Directory.Exists(Server.MapPath("~/Temp"));
                                    if (exist)
                                    {
                                        Directory.Delete(Server.MapPath("/Temp"), true);
                                    }
                                    deviceArray[i] = item.Device_ID;

                                    if (empflag)
                                    {
                                        msg = "User Updated Successfully on Device: " + string.Join(",", deviceArray);
                                    }
                                    else
                                    {
                                        msg = "User Added Successfully";
                                    }


                                }

                            } // device check end

                        }

                    }
                    else
                    {
                        error.Add("Invalid Parmeters For Device: " + DeviceIds[i]);
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
                msg,
                error,
            }, JsonRequestBehavior.AllowGet);
        }


        //POST : API
        //Delete Employee

        [HttpPost]
        public JsonResult DeleteEmployee(string Key, string ID, string[] DeviceIds)
        {
            bool flag = false;
            string msg = "";
            bool exflag = false;
            bool isEmpDeleted = false;
            BLLExpiredUsers objexUser = new BLLExpiredUsers();
            EagleEyeManagement em = new EagleEyeManagement();
            BLLTempTable objTempBLL = new BLLTempTable();
            BLLDevice objDev = new BLLDevice();
            List<Device_P> Devices = new List<Device_P>();
            BLLDevice objDevice = new BLLDevice();
            List<string> error = new List<string>();
            bool deviceCheck = false;
            Employee_P emp = EmpBLL.GetEmployeeByID(ID);
            string[] deviceArray = new string[DeviceIds.Length];
            try
            {
                //x.Device_Status == 1 &&
                // Devices = objDevice.GetAllDevices().Where(x => DeviceIds.Contains(x.Device_ID)).ToList();
                for (int i = 0; i < DeviceIds.Length; i++)
                {

                    Devices = objDevice.GetAllDevices().Where(x => DeviceIds[i].Contains(x.Device_ID)).ToList();
                    //main if start
                    if (Devices.Count > 0)
                    {
                        foreach (var item in Devices)
                        {
                            deviceCheck = true;
                            if (item.Device_Status == 0)
                            {
                                error.Add("Device Disconnected: " + item.Device_ID);
                                deviceCheck = false;
                            }
                            //check for device if it is online
                            if (deviceCheck)
                            {


                                // flag = EmpBLL.IsDeleteEmployee(emp.Code);
                                //emp not equal to null
                                if (emp != null)
                                {


                                    //  if (isEmpDeleted)
                                    // {

                                    //for (int j = 0; i < DeviceIds.Length; j++)
                                    //{
                                    Device_P d = objDev.GetDeviceByDevice_ID(DeviceIds[i]);
                                    if (d.Device_Status != 0)
                                    {
                                        flag = em.DeleteUserfromMachine(emp.Employee_ID, d.Device_ID, emp.Employee_Name);
                                    }
                                    else
                                    {
                                        flag = objTempBLL.InsertCommand(d.Device_ID, "DELETE_USER", emp.Employee_ID);
                                    }
                                    //   }
                                    // }
                                    deviceArray[i] = item.Device_ID;
                                    if (flag)
                                    {
                                        msg = "User(s) Deleted Successfully on Device: " + string.Join(",", deviceArray);
                                    }
                                }
                                else
                                {
                                    deviceArray[i] = item.Device_ID;
                                    if (error.Count() == 0)
                                        msg = "User(s) Not Found on Database";

                                    flag = em.DeleteUserfromMachine(ID, item.Device_ID, "User");
                                    if (flag)
                                    {
                                        msg += " / User(s) Deleted Successfully on Device: " + string.Join(",", deviceArray);
                                    }
                                }



                            }

                        }


                    }
                    else
                    {
                        error.Add("Invalid Parmeters For Device: " + DeviceIds[i]);
                    }
                    // main if end
                }

                exflag = objexUser.DeleteExEmployee(emp.Employee_ID);
                isEmpDeleted = EmpBLL.IsDeleteEmployee(emp.Code);


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());

            }

            return Json(new
            {
                result = flag,
                msg,
                error,


            }, JsonRequestBehavior.AllowGet);

        }




        #region Adding or Updating Employee

        private bool SendUserToDevice(string userCodes, string deviceIds)
        {

            bool flag = false;
            bool userflag = false;
            try
            {
                string DeviceID = deviceIds;
                BLLDevice objDevice = new BLLDevice();
                EagleEyeManagement hm = new EagleEyeManagement();
                Device_P device = objDevice.GetDeviceByDevice_ID(DeviceID);

                BLLEmployee objEmployee = new BLLEmployee();
                Employee_P emp = new Employee_P();
                emp = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCodes));
                if (device != null)
                {
                    if (emp.Active == 1)
                    {
                        if (string.IsNullOrEmpty(emp.Device_Id))
                            emp.Device_Id = device.Device_ID;

                        userflag = EmpBLL.CheckRealEmployeeExistInDB(emp.Employee_ID, emp.Device_Id);
                        if (userflag)
                        {
                            if (emp.Password == "")
                                flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, emp.Employee_Name);

                            else if (emp.Card_No == "")
                                flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, emp.Employee_Name);
                        }

                        flag = hm.SendEmployeeToDevice(device.Device_ID, emp, emp.Employee_Name);
                        hm.SyncDevice(device.Device_ID, emp.Employee_Name);
                    }
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                flag = false;
            }
            return flag;

        }

        //private bool SendUserToDevice(string userCodes, string[] deviceIds)
        //{
        //    bool flag = false;
        //    bool userflag = false;
        //    try
        //    {
        //        for (int i = 0; i < deviceIds.Count(); i++)
        //        {
        //            string DeviceID = deviceIds[i];
        //            BLLDevice objDevice = new BLLDevice();
        //            EagleEyeManagement hm = new EagleEyeManagement();
        //            Device_P device = objDevice.GetDeviceByDevice_ID(DeviceID);

        //            BLLEmployee objEmployee = new BLLEmployee();
        //            Employee_P emp = new Employee_P();
        //            emp = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCodes));
        //            if (device != null)
        //            {
        //                if (emp.Active == 1)
        //                {
        //                    if (string.IsNullOrEmpty(emp.Device_Id))
        //                        emp.Device_Id = device.Device_ID;

        //                    userflag = EmpBLL.CheckRealEmployeeExistInDB(emp.Employee_ID, emp.Device_Id);
        //                    if (userflag)
        //                    {
        //                        if (emp.Password == "")
        //                            flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, emp.Employee_Name);

        //                        else if (emp.Card_No == "")
        //                            flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, emp.Employee_Name);
        //                    }

        //                    flag = hm.SendEmployeeToDevice(device.Device_ID, emp, emp.Employee_Name);
        //                    hm.SyncDevice(device.Device_ID, emp.Employee_Name);
        //                }
        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //        flag = false;
        //    }
        //    return flag;
        //}

        private void ReduceImageSize(Stream sourcePath, string targetPath)
        {
            Image image = Image.FromStream(sourcePath);
            SaveJpeg(targetPath, image, 50);

        }
        private static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
            {
                throw new ArgumentOutOfRangeException("quality exceed");
            }
            EncoderParameter qual = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParameter = new EncoderParameters(1);
            encoderParameter.Param[0] = qual;
            img.Save(path, jpegCodec, encoderParameter);
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codesc = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < codesc.Length; i++)
                if (codesc[i].MimeType == mimeType)
                    return codesc[i];

            return null;
        }
        #endregion

    }
}