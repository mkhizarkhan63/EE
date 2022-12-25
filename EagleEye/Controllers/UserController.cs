using EagleEye.BLL;
using EagleEye.Common;
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
using System.Threading;
using System.Threading.Tasks;
using EagleEye.Hubs;
using System.IO;
using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Common;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using EagleEye.DAL.DTO;
using EagleEye_Service;
using System.ServiceProcess;

namespace EagleEye.Controllers
{
    public class UserController : AsyncController
    {
        #region Variables
        int Progress = 0;
        double percent = 0;
        BLLEmployee objBLL = new BLLEmployee();
        BLLDevice objDevBLL = new BLLDevice();
        BLLLocation objLocBLL = new BLLLocation();
        BLLDepartment objDeptBLL = new BLLDepartment();
        BLLEmployeeType objETBLL = new BLLEmployeeType();
        BLLDesignation objDesBLL = new BLLDesignation();
        BLLTempTable objTempBLL = new BLLTempTable();
        BLLExpiredUsers objExUserBLL = new BLLExpiredUsers();

        #endregion

        #region Actions

        [CheckAuthorization]
        public ActionResult Index()
        {
            if (!CheckRights("Users"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetTotalReport(JqueryDatatableParam param, string locationFilter, string statusFilter)
        {
            int totalRecords = 0;
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            try
            {
                list = objBLL.GetAllEmployeeDTO(param, locationFilter, statusFilter, out totalRecords);
            }
            catch (Exception ex)
            {

                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            //var displayResult = list.ToList();
            //try
            //{
            //    var loc = locationFilter.Split(',');
            //    var status = statusFilter.Split(',');

            //    list = objBLL.GetAllEmployeeDTO();

            //    if (loc != null && loc.Count() > 0 && loc.Length != 0 && loc[0] != "")
            //    {
            //        list = list.Where(x => loc.Contains(x.fkLocation_Code.ToString())).ToList();
            //    }
            //    if (status != null && status.Count() > 0 && status.Length != 0 && status[0] != "")
            //    {
            //        list = list.Where(x => status.Contains(x.Active.ToString())).ToList();

            //    }


            //    if (!string.IsNullOrEmpty(param.sSearch))
            //    {
            //        list = list.Where(x => x.Employee_ID.ToString().ToLower() == param.sSearch.ToLower() || x.Employee_Name.ToLower() == param.sSearch.ToLower() || x.Location?.ToString().ToLower() == param.sSearch.ToString().ToLower()).ToList();
            //    }

            //    displayResult = list.ToList();
            //    totalRecords = displayResult.Count();

            //    if (param.iDisplayLength != -1)
            //    {
            //        displayResult = list.Skip(param.iDisplayStart)
            //           .Take(param.iDisplayLength).ToList();
            //    }
            //    var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            //    var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            //    if (sortColumnIndex == 0 && sortDirection == "asc")
            //    {
            //        displayResult = displayResult.OrderBy(x => x.Employee_ID).ToList();
            //    }
            //    else if (sortColumnIndex == 0 && sortDirection == "desc")
            //    {
            //        displayResult = displayResult.OrderByDescending(x => x.Employee_ID).ToList();

            //    }

            //}
            //catch (Exception ex)
            //{
            //    LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            //}
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }
        //location

        public JsonResult Location()
        {
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



        //[CheckAuthorization]
        //public ActionResult Add()
        //{

        //    Employee_P employee = new Employee_P();
        //    try
        //    {
        //        BLLLocation objLocation = new BLLLocation();
        //        employee.ListLocation = objLocation.GetAllLocation();
        //        BLLDepartment objDepartment = new BLLDepartment();
        //        employee.ListDepartment = objDepartment.GetAllDepartment();
        //        BLLDesignation objDesignation = new BLLDesignation();
        //        employee.ListDesignation = objDesignation.GetAllDesignation();
        //        BLLEmployeeType objEmployeeType = new BLLEmployeeType();
        //        employee.ListEmployeeType = objEmployeeType.GetAllEmployeeType();
        //        employee.Telephone = employee.Telephone.Replace("+92", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    return View(employee);
        //}

        [CheckAuthorization]
        public ActionResult Add(int Code = 0)
        {
            Attendance_P att = new Attendance_P();
            Employee_P employee = new Employee_P();
            try
            {
                string url = "http://" + HttpContext.Request.Url.Authority;
                if (Code != 0)
                {
                    employee = objBLL.GetIrregularEmployeeByCode(Code);
                    if (employee.Employee_Photo != "")
                        employee.Employee_Photo = url + employee.Employee_Photo;
                }

                BLLLocation objLocation = new BLLLocation();
                employee.ListLocation = objLocation.GetAllLocation();
                BLLDepartment objDepartment = new BLLDepartment();
                employee.ListDepartment = objDepartment.GetAllDepartment();
                BLLDesignation objDesignation = new BLLDesignation();
                employee.ListDesignation = objDesignation.GetAllDesignation();

                BLLEmployeeType objEmployeeType = new BLLEmployeeType();
                employee.ListEmployeeType = objEmployeeType.GetAllEmployeeType();
                BLLTimeZone objTimeZone = new BLLTimeZone();
                employee.ListTimeZones = objTimeZone.GetAllValidTimeZone();
                BLLWorkHour objWorkHour = new BLLWorkHour();
                employee.ListWorkHour = objWorkHour.getAllWorkHour();

                if (employee.Telephone != null)
                    employee.Telephone = employee.Telephone.Replace("+92", "");

                //switch (employee.Active)
                //{
                //    case 1:
                //        employee.Active = true;
                //        break;
                //    case 2:
                //        employee.Active = false;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View(employee);
        }


        [CheckAuthorization]
        public ActionResult Edit(string Code)
        {
            Employee_P employee = new Employee_P();
            try
            {
                employee = objBLL.GetEmployeeByID(Code);

                if (employee != null)
                {
                    BLLLocation objLocation = new BLLLocation();
                    employee.ListLocation = objLocation.GetAllLocation();
                    BLLDepartment objDepartment = new BLLDepartment();
                    employee.ListDepartment = objDepartment.GetAllDepartment();
                    BLLDesignation objDesignation = new BLLDesignation();
                    employee.ListDesignation = objDesignation.GetAllDesignation();
                    BLLEmployeeType objEmployeeType = new BLLEmployeeType();
                    employee.ListEmployeeType = objEmployeeType.GetAllEmployeeType();
                    if (employee.Telephone != null)
                        employee.Telephone = employee.Telephone.Replace("+92", "");

                    BLLTimeZone objTimeZone = new BLLTimeZone();
                    employee.ListTimeZones = objTimeZone.GetAllValidTimeZone();

                    BLLWorkHour objWorkHour = new BLLWorkHour();
                    employee.ListWorkHour = objWorkHour.getAllWorkHour();
                    if (employee.Valid_DateEnd == "" && employee.Valid_DateStart == "")
                    {
                        employee.Valid_DateStart = "2022-01-01";
                        employee.Valid_DateEnd = "2042-01-01";

                    }
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
            return View(employee);

        }

        //[CheckAuthorization]
        //public ActionResult Edit(int Code, string EmployeeID)
        //{
        //    Employee_P employee = new Employee_P();
        //    try
        //    {
        //        employee = objBLL.GetEmployeeByID(EmployeeID);

        //        if (employee != null)
        //        {
        //            BLLLocation objLocation = new BLLLocation();
        //            employee.ListLocation = objLocation.GetAllLocation();
        //            BLLDepartment objDepartment = new BLLDepartment();
        //            employee.ListDepartment = objDepartment.GetAllDepartment();
        //            BLLDesignation objDesignation = new BLLDesignation();
        //            employee.ListDesignation = objDesignation.GetAllDesignation();
        //            BLLEmployeeType objEmployeeType = new BLLEmployeeType();
        //            employee.ListEmployeeType = objEmployeeType.GetAllEmployeeType();
        //            if (employee.Telephone != null)
        //                employee.Telephone = employee.Telephone.Replace("+92", "");

        //            BLLTimeZone objTimeZone = new BLLTimeZone();
        //            employee.ListTimeZones = objTimeZone.GetAllTimeZone();
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "User");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    return View(employee);

        //}

        #endregion

        #region Methods
        public bool CheckRights(string menu)
        {
            bool flag = false;
            try
            {
                List<MenuGen> ListMenu = (List<MenuGen>)Session["Menu"];
                MenuGen m = ListMenu.Where(x => x.Menu_Name == menu).FirstOrDefault();
                flag = Formatter.SetValidValueToBool(m.View);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }

        //[HttpPost]
        //public JsonResult DeleteUsers(string[] lstUserCode)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        for (int i = 0; i < lstUserCode.Count(); i++)
        //        {
        //            flag = objBLL.DeleteEmployee(Formatter.SetValidValueToInt(lstUserCode[i]));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    return Json(new
        //    {
        //        result = flag
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult SyncUsers(string deviceCode)
        {
            bool flag = false;
            string deviceName = "";
            Device_P deviceInfo = new Device_P();
            EagleEyeManagement hm = new EagleEyeManagement();
            try
            {
                Account_P account = (Account_P)Session["User"];
                deviceInfo = objDevBLL.GetDeviceByDevice_ID(deviceCode);
                if (deviceInfo != null)
                {
                    deviceName = deviceInfo.Device_Name;
                    flag = hm.SyncDevice(deviceInfo.Device_ID, account.UserName);
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
        public async Task<JsonResult> DeleteUsers(string[] lstUserCode, string[] deviceIds)
        {
            int Progress = 0;
            double percent = 0;
            bool flag = false;
            string msg = "";
            try
            {
                Account_P account = (Account_P)Session["User"];
                EagleEyeManagement m = new EagleEyeManagement();
                BLLDevice objDev = new BLLDevice();

                List<Employee_P> List = objBLL.GetEmployeeByCodes(lstUserCode).ToList();
                for (int i = 0; i < deviceIds.Count(); i++)
                {
                    string DeviceID = deviceIds[i];
                    Device_P device = objDev.GetDeviceByDevice_ID(DeviceID);
                    string DeviceName = device.Device_Name;
                    Progress = 1;
                    percent = 0;
                    int TotalUsers = List.Count();
                    foreach (var emp in List)
                    {
                        flag = m.DeleteUserfromMachine(emp.Employee_ID, device.Device_ID, account.UserName);

                        if (flag)
                        {

                            flag = objBLL.IsDeleteEmployee(emp.Code);
                            percent = (Progress * 100) / TotalUsers;

                            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<WebHub>();
                            hubContext.Clients.All.sendUserDeleteStatus(emp, flag, DeviceName, TotalUsers, Progress, percent, msg);
                            Progress++;
                        }

                    }
                }
                //    for (int i = 0; i < lstUserCode.Count(); i++)
                //{
                //    string res = "";
                //    Employee_P emp = objBLL.GetEmployeeByCode(Formatter.SetValidValueToInt(lstUserCode[i]));
                //    foreach (Device_P dev in ListDev)
                //    {
                //        m.DeleteEmployee(dev.Device_ID, emp.Custom_ID, out res);
                //    }
                //    flag = objBLL.DeleteEmployee(Formatter.SetValidValueToInt(lstUserCode[i]));
                //}
                flag = true;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteFromRDB(string[] lstUserCode)
        {

            bool flag = false;
            string msg = "";
            try
            {
                for (int i = 0; i < lstUserCode.Count(); i++)
                {
                    //Employee_P emp = objBLL.GetEmployeeByCode(Formatter.SetValidValueToInt(lstUserCode[i]));
                    flag = objBLL.DeleteEmployee(lstUserCode[i]);
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
                msg,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult RestoreUserFromDB(string[] lstUserCode, string[] lstUserWithNames)
        {

            bool flag = false;
            string msg = "";
            int action = 1;
            int count = 0;
            bool cardflag = false;

            List<string> successUsers = new List<string>();
            try
            {
                count = objBLL.RestoreUsersList();
                if (count > 0)
                {
                    BLLDevice objDev = new BLLDevice();
                    for (int i = 0; i < lstUserCode.Count(); i++)
                    {
                        Employee_P emp = objBLL.GetEmployeeByID(lstUserCode[i]);
                        int existingEmpID = 0;
                        cardflag = objBLL.CheckCardExistInDB(lstUserCode[i], out existingEmpID);

                        if (emp == null)
                        {
                            if (!cardflag)
                            {
                                flag = objBLL.RestoreUserFromDB(lstUserCode[i], lstUserWithNames[i], action);
                                flag = true;
                                if (flag)
                                {
                                    var isDeletedEmp = objBLL.GetEmployeeByIDIsDeleted(lstUserCode[i]);
                                    RevertingPictureName(isDeletedEmp);
                                    successUsers.Add(lstUserCode[i]);
                                }

                            }
                            else
                            {
                                msg = "Error Restoring Card Already Exists Employee ID: " + existingEmpID + "";
                                flag = false;
                            }
                        }
                        else
                        {
                            msg = "Error Restoring Duplicate Records";
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                    msg = "There Is No User To Proceed";
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                successUsers,
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteUsersFromDB(string[] lstUserCode, string[] lstDeviceCode)
        {
            BLLExpiredUsers objexUser = new BLLExpiredUsers();


            EagleEyeManagement em = new EagleEyeManagement();
            bool flag = false; bool exflag = false;
            string msg = "";
            try
            {
                Account_P account = (Account_P)Session["User"];
                BLLDevice objDev = new BLLDevice();
                for (int i = 0; i < lstUserCode.Count(); i++)
                {
                    Employee_P emp = objBLL.GetEmployeeByCode(Formatter.SetValidValueToInt(lstUserCode[i]));
                    ChangingPictureName(emp);
                    //string empPhoto = emp.Employee_Photo;
                    //string fileName = "Profile_" + emp.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                    //string mainPath = Server.MapPath("~/profiles/");
                    //DirectoryInfo directoryInfo = new DirectoryInfo(mainPath);
                    //FileInfo[] fileInfo = directoryInfo.GetFiles();
                    //string oldFile = Server.MapPath("~/oldFile/");
                    //foreach (var item in fileInfo)
                    //{
                    //    var itemname = item.Name.Split('.');
                    //    var changingFilename = fileName.Split('.');
                    //    if (itemname[0] == changingFilename[0])
                    //    {
                    //        if (!Directory.Exists(oldFile))
                    //            Directory.CreateDirectory(oldFile);
                    //        System.IO.File.Move(item.FullName, oldFile);
                    //    }
                    //}

                    flag = objBLL.IsDeleteEmployee(Formatter.SetValidValueToInt(lstUserCode[i]));
                    exflag = objexUser.DeleteExEmployee(emp.Employee_ID);
                    if (flag && lstDeviceCode != null)
                    {

                        foreach (var id in lstDeviceCode)
                        {
                            Device_P d = objDev.GetDeviceByDevice_ID(id);
                            if (d.Device_Status != 0)
                            {
                                flag = em.DeleteUserfromMachine(emp.Employee_ID, d.Device_ID, account.UserName);
                            }
                            else
                            {
                                objTempBLL.InsertCommand(d.Device_ID, "DELETE_USER", emp.Employee_ID);
                            }
                        }
                    }

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
                msg,
            }, JsonRequestBehavior.AllowGet);

        }

        private void RevertingPictureName(Employee_P emp)
        {
            BLLEmployee bLLEmployee = new BLLEmployee();
            try
            {
                if (emp.Employee_Photo != "")
                {
                    string fileName = "Profile_" + emp.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                    string mainPath = Server.MapPath("~/profiles/");
                    DirectoryInfo directoryInfo = new DirectoryInfo(mainPath);
                    FileInfo[] fileInfo = directoryInfo.GetFiles();
                    //string oldFile = Server.MapPath("~/oldFile/");
                    var employee = bLLEmployee.GetEmployeeByID(emp.Employee_ID);
                    string empPhoto = employee.Employee_Photo;

                    string pathCombine = mainPath + empPhoto.Split('/')[2];
                    if (System.IO.File.Exists(pathCombine))
                    {
                        System.IO.File.Copy(pathCombine, mainPath + fileName);
                        System.IO.File.Delete(pathCombine);
                        emp.Employee_Photo = "/profiles/" + fileName;
                        bLLEmployee.AddUpdateEmployee(emp);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
        }


        private void ChangingPictureName(Employee_P emp)
        {
            BLLEmployee bLLEmployee = new BLLEmployee();
            try
            {
                //string empPhoto = emp.Employee_Photo;
                string fileName = "Profile_" + emp.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                string mainPath = Server.MapPath("~/profiles/");
                DirectoryInfo directoryInfo = new DirectoryInfo(mainPath);
                FileInfo[] fileInfo = directoryInfo.GetFiles();
                //string oldFile = Server.MapPath("~/oldFile/");

                var itemname = emp.Employee_Photo.Split('.');
                var changingFilename = fileName.Split('.');
                var dtTime = DateTime.Now.ToString("HH-mm-ss");
                var newFileName = mainPath + changingFilename[0] + "_" + dtTime + "." + changingFilename[1];
                var fullPath = mainPath + changingFilename[0] + "." + changingFilename[1];
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Copy(fullPath, newFileName);
                    System.IO.File.Delete(fullPath);
                    emp.Employee_Photo = @"/profiles/" + changingFilename[0] + "_" + dtTime + "." + changingFilename[1];
                    bLLEmployee.AddUpdateEmployee(emp);
                }


            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
        }


        [HttpPost]
        public JsonResult GetDeletedUsers()
        {
            bool flag = false;
            int total = 0;
            int count = 1;
            List<Employee_P> List = new List<Employee_P>();
            //var serializer = new JavaScriptSerializer
            //{
            //    MaxJsonLength = Int32.MaxValue
            //};
            try
            {
                BLLEmployee obj = new BLLEmployee();
                List = obj.GetAllDeletedUsers();
                total = List.Count;
                foreach (var emp in List)
                {
                    IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<WebHub>();
                    hubContext.Clients.All.deletedUsers(emp, count, total);

                    count++;
                    flag = true;
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
            //return Json(new
            //{
            //    result = List
            //   // count = List.Count
            //}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AddUser()
        {
            EagleEyeManagement hm = new EagleEyeManagement();
            bool empflag = false;
            bool cardflag = false;
            bool flag = false;
            bool isDeletedEmp = false;
            string msg = "";
            Employee_P employee = new Employee_P();
            Stream photoStream = null;
            string title = "";
            try
            {


                employee.Employee_ID = Request.Form["Employee_ID"];

                empflag = objBLL.CheckEmployeeExistInDB(employee.Employee_ID);
                //Check Emp Deleted
                isDeletedEmp = objBLL.checkEmployeeIsNotDeleted(employee.Employee_ID);
                employee.Card_No = Request.Form["Card_No"];
                if (employee.Card_No != "")
                    cardflag = objBLL.CheckCardExistInDB(employee.Card_No, employee.Employee_ID);

                // employee.Password = Request.Form["Password"];
                //if (employee.Password != "")
                //    pwdflag = objBLL.CheckPasswordExistInDB(employee.Password, employee.Employee_ID);


                employee.Code = Formatter.SetValidValueToInt(Request.Form["Code"]);
                employee.Employee_Name = Request.Form["Employee_Name"];

                employee.IrregularCode = Request.Form["IrregularCode"];
                employee.Email = Request.Form["Email"];
                employee.Address = Request.Form["Address"];
                employee.Gender = Request.Form["Gender"];
                employee.Device_Id = Request.Form["Device_Id"];

                employee.Cmd_Param = Convert.FromBase64String(Request.Form["Cmd_Param"]);
                employee.finger_0 = Convert.FromBase64String(Request.Form["finger_0"]);
                employee.finger_1 = Convert.FromBase64String(Request.Form["finger_1"]);
                employee.finger_2 = Convert.FromBase64String(Request.Form["finger_2"]);
                employee.finger_3 = Convert.FromBase64String(Request.Form["finger_3"]);
                employee.finger_4 = Convert.FromBase64String(Request.Form["finger_4"]);
                employee.finger_5 = Convert.FromBase64String(Request.Form["finger_5"]);
                employee.finger_6 = Convert.FromBase64String(Request.Form["finger_6"]);
                employee.finger_7 = Convert.FromBase64String(Request.Form["finger_7"]);
                employee.finger_8 = Convert.FromBase64String(Request.Form["finger_8"]);
                employee.finger_9 = Convert.FromBase64String(Request.Form["finger_9"]);
                employee.face_data = Convert.FromBase64String(Request.Form["face_data"]);
                employee.palm_0 = Convert.FromBase64String(Request.Form["palm_0"]);
                employee.palm_1 = Convert.FromBase64String(Request.Form["palm_1"]);
                employee.photo_data = Convert.FromBase64String(Request.Form["photo_data"]);

                employee.Valid_DateEnd = Request.Form["Valid_DateEnd"];
                employee.Valid_DateStart = Request.Form["Valid_DateStart"];
                if (employee.Valid_DateStart == "" && employee.Valid_DateEnd == "")
                {
                    employee.Valid_DateStart = "2022-01-01";
                    employee.Valid_DateEnd = "2042-01-01";

                }
                employee.Saturday = Request.Form["Saturday"];
                employee.Sunday = Request.Form["Sunday"];
                employee.Thursday = Request.Form["Thursday"];
                employee.Tuesday = Request.Form["Tuesday"];
                employee.Monday = Request.Form["Monday"];
                employee.Friday = Request.Form["Friday"];
                employee.Wednesday = Request.Form["Wednesday"];

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

                string DepartmentCode = Request.Form["fkDepartment_Code"];
                string LocationCode = Request.Form["fkLocation_Code"];
                string DesignationCode = Request.Form["fkDesignation_Code"];
                string EmpTypeCode = Request.Form["fkEmployeeType_Code"];

                if (!string.IsNullOrEmpty(DesignationCode) && DesignationCode != "null")
                    employee.fkDesignation_Code = Formatter.SetValidValueToInt(DesignationCode);

                if (!string.IsNullOrEmpty(EmpTypeCode) && EmpTypeCode != "null")
                    employee.fkEmployeeType_Code = Formatter.SetValidValueToInt(EmpTypeCode);

                if (!string.IsNullOrEmpty(DepartmentCode) && DepartmentCode != "null")
                    employee.fkDepartment_Code = Formatter.SetValidValueToInt(DepartmentCode);

                if (!string.IsNullOrEmpty(LocationCode) && LocationCode != "null")
                    employee.fkLocation_Code = Formatter.SetValidValueToInt(LocationCode);

                //  employee.Card_No = Request.Form["Card_No"];
                employee.Telephone = "+92" + Request.Form["Telephone"];

                employee.Password = Request.Form["Password"];
                employee.User_Privilege = Request.Form["User_Privilege"];

                string face = Request.Form["Face"];
                if (!string.IsNullOrEmpty(face) && face != "null")
                    employee.Face = Formatter.SetValidValueToBool(face);

                string fp = Request.Form["FingerPrint"];
                if (!string.IsNullOrEmpty(fp) && fp != "null")
                    employee.FingerPrint = Formatter.SetValidValueToBool(fp);

                string palm = Request.Form["Palm"];
                if (!string.IsNullOrEmpty(palm) && palm != "null")
                    employee.Palm = Formatter.SetValidValueToBool(palm);

                string Active = Request.Form["Active"];
                if (!string.IsNullOrEmpty(Active) && Active != "null")
                {
                    switch (Active)
                    {
                        case "true":
                            employee.Active = 1;
                            break;
                        case "false":
                            employee.Active = 0;
                            break;
                    }
                }
                string workHourPolicyCode = Request.Form["WorkHourPolicyCode"];
                if (!string.IsNullOrEmpty(workHourPolicyCode) && workHourPolicyCode != "null")
                    employee.WorkHourPolicyCode = Formatter.SetValidValueToInt(Request.Form["WorkHourPolicyCode"]);

                string MainPath = "";


                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;

                    HttpPostedFileBase file = files[0];
                    string fileName = "Profile_" + employee.Employee_ID + "." + file.FileName.Split('.')[1];
                    if (!Directory.Exists(Server.MapPath("~/profiles/")))
                        Directory.CreateDirectory(Server.MapPath("~/profiles/"));
                    MainPath = Path.Combine(Server.MapPath("~/profiles/"), fileName);


                    photoStream = file.InputStream;

                    string url = HttpContext.Request.Url.Authority;
                    employee.Employee_Photo = "http://" + url + "/profiles/" + fileName;
                    //Directory.CreateDirectory(Server.MapPath("~/profiles/"));
                    //MainPath = Path.Combine(Server.MapPath("~/profiles/"), fileName);

                    if (photoStream != null)
                        ReduceImageSize(photoStream, MainPath);

                    byte[] bytes = new byte[0];

                    if (employee.Employee_Photo.Contains("http"))
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
                    employee.Employee_Photo = Request.Form["Employee_Photo"];
                }
                //if (photoStream != null)
                //    ReduceImageSize(photoStream, MainPath);


                byte[] encEmployee = new byte[0];
                hm.EncryptEmployee(employee, out encEmployee);
                employee.Cmd_Param = encEmployee;
                if (cardflag != true && empflag != true)
                {
                    flag = objBLL.AddUpdateEmployee(employee);
                    if (flag)
                    {
                        bool exist = Directory.Exists(Server.MapPath("~/Temp"));
                        if (exist)
                        {
                            Directory.Delete(Server.MapPath("/Temp"), true);
                        }


                        if (employee.IrregularCode != "0" && !string.IsNullOrEmpty(employee.IrregularCode))
                        {
                            BLLIrregularEmployee objIrr = new BLLIrregularEmployee();
                            objIrr.DeleteEmployee(Formatter.SetValidValueToInt(employee.IrregularCode));
                        }
                        employee = objBLL.GetEmployeeByID(employee.Employee_ID);
                    }

                }
                //check wether employee is deleted
                else if (isDeletedEmp)
                {
                    if (!empflag)
                    {
                        flag = objBLL.AddUpdateEmployee(employee);
                        if (flag)
                        {
                            bool exist = Directory.Exists(Server.MapPath("~/Temp"));
                            if (exist)
                            {
                                Directory.Delete(Server.MapPath("/Temp"), true);
                            }


                            if (employee.IrregularCode != "0" && !string.IsNullOrEmpty(employee.IrregularCode))
                            {
                                BLLIrregularEmployee objIrr = new BLLIrregularEmployee();
                                objIrr.DeleteEmployee(Formatter.SetValidValueToInt(employee.IrregularCode));
                            }
                            employee = objBLL.GetEmployeeByID(employee.Employee_ID);
                        }
                    }
                    else
                    {
                        title = "Warning!";
                        msg = "User ID Already Exists";
                        flag = false;
                        return Json(new
                        {
                            result = false,
                            title,
                            msg,
                            code = employee.Code
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (empflag == true)
                    {
                        title = "Warning!";
                        msg = "User ID Already Exists";
                        flag = false;
                        return Json(new
                        {
                            result = false,
                            title,
                            msg,
                            code = employee.Code
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //else if (pwdflag == true)
                    //{
                    //    msg = "Pin Already Exists";
                    //    flag = false;
                    //}
                    else if (cardflag == true)
                    {
                        title = "Warning!";
                        msg = "Card Already Exists";
                        flag = false;
                        return Json(new
                        {
                            result = false,
                            title,
                            msg,
                            code = employee.Code
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Request.Form["Active"] == "false")
                {

                    title = "User Added Successfully!";
                    msg = "Unable to Send InActive User to Device.";
                    flag = false;
                    return Json(new
                    {
                        result = false,
                        title,
                        msg,
                        code = employee.Code
                    }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                title,
                msg,
                code = employee.Code
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EditUser()
        {
            EagleEyeManagement hm = new EagleEyeManagement();
            bool flag = false;
            bool empflag = false;
            bool exEmpflag = false;
            bool cardflag = false;
            string msg = "";
            string title = "";
            Employee_P employee = new Employee_P();
            Stream photoStream = null;
            try
            {
                Account_P account = (Account_P)Session["User"];
                employee.Employee_ID = Request.Form["Employee_ID"];


                empflag = objBLL.CheckEmployeeExistInDB(employee.Employee_ID);

                employee.Card_No = Request.Form["Card_No"];
                if (employee.Card_No != "")
                    cardflag = objBLL.CheckCardExistInDB(employee.Card_No, employee.Employee_ID);

                //employee.Password = Request.Form["Password"];
                //if (employee.Password != "")
                //    pwdflag = objBLL.CheckPasswordExistInDB(employee.Password,employee.Employee_ID);

                if (cardflag != true/* && empflag != true*/)
                {
                    employee.Code = Formatter.SetValidValueToInt(Request.Form["Code"]);
                    employee.Employee_Name = Request.Form["Employee_Name"];
                    // employee.Employee_ID = Request.Form["Employee_ID"];
                    employee.Email = Request.Form["Email"];
                    employee.Address = Request.Form["Address"];
                    employee.Gender = Request.Form["Gender"];
                    employee.Device_Id = Request.Form["Device_Id"];

                    employee.Cmd_Param = Convert.FromBase64String(Request.Form["Cmd_Param"]);
                    employee.finger_0 = Convert.FromBase64String(Request.Form["finger_0"]);
                    employee.finger_1 = Convert.FromBase64String(Request.Form["finger_1"]);
                    employee.finger_2 = Convert.FromBase64String(Request.Form["finger_2"]);
                    employee.finger_3 = Convert.FromBase64String(Request.Form["finger_3"]);
                    employee.finger_4 = Convert.FromBase64String(Request.Form["finger_4"]);
                    employee.finger_5 = Convert.FromBase64String(Request.Form["finger_5"]);
                    employee.finger_6 = Convert.FromBase64String(Request.Form["finger_6"]);
                    employee.finger_7 = Convert.FromBase64String(Request.Form["finger_7"]);
                    employee.finger_8 = Convert.FromBase64String(Request.Form["finger_8"]);
                    employee.finger_9 = Convert.FromBase64String(Request.Form["finger_9"]);
                    employee.face_data = Convert.FromBase64String(Request.Form["face_data"]);
                    employee.palm_0 = Convert.FromBase64String(Request.Form["palm_0"]);
                    employee.palm_1 = Convert.FromBase64String(Request.Form["palm_1"]);
                    employee.photo_data = Convert.FromBase64String(Request.Form["photo_data"]);


                    string DepartmentCode = Request.Form["fkDepartment_Code"];
                    string LocationCode = Request.Form["fkLocation_Code"];
                    string DesignationCode = Request.Form["fkDesignation_Code"];
                    string EmpTypeCode = Request.Form["fkEmployeeType_Code"];

                    if (!string.IsNullOrEmpty(DesignationCode) && DesignationCode != "null")
                        employee.fkDesignation_Code = Formatter.SetValidValueToInt(DesignationCode);

                    if (!string.IsNullOrEmpty(EmpTypeCode) && EmpTypeCode != "null")
                        employee.fkEmployeeType_Code = Formatter.SetValidValueToInt(EmpTypeCode);

                    if (!string.IsNullOrEmpty(DepartmentCode) && DepartmentCode != "null")
                        employee.fkDepartment_Code = Formatter.SetValidValueToInt(DepartmentCode);

                    if (!string.IsNullOrEmpty(LocationCode) && LocationCode != "null")
                        employee.fkLocation_Code = Formatter.SetValidValueToInt(LocationCode);

                    // employee.Card_No = Request.Form["Card_No"];
                    employee.Telephone = "+92" + Request.Form["Telephone"];

                    employee.Password = Request.Form["Password"];
                    employee.User_Privilege = Request.Form["User_Privilege"];

                    string face = Request.Form["Face"];
                    if (!string.IsNullOrEmpty(face) && face != "null")
                        employee.Face = Formatter.SetValidValueToBool(face);

                    string fp = Request.Form["FingerPrint"];
                    if (!string.IsNullOrEmpty(fp) && fp != "null")
                        employee.FingerPrint = Formatter.SetValidValueToBool(fp);

                    string palm = Request.Form["Palm"];
                    if (!string.IsNullOrEmpty(palm) && palm != "null")
                        employee.Palm = Formatter.SetValidValueToBool(palm);

                    string Active = Request.Form["Active"];
                    if (!string.IsNullOrEmpty(Active) && Active != "null")
                    {
                        switch (Active)
                        {
                            case "true":
                                employee.Active = 1;
                                break;
                            case "false":
                                employee.Active = 0;
                                break;
                        }
                    }


                    employee.Valid_DateEnd = Request.Form["Valid_DateEnd"];
                    employee.Valid_DateStart = Request.Form["Valid_DateStart"];
                    employee.Saturday = Request.Form["Saturday"];
                    employee.Sunday = Request.Form["Sunday"];
                    employee.Thursday = Request.Form["Thursday"];
                    employee.Tuesday = Request.Form["Tuesday"];
                    employee.Monday = Request.Form["Monday"];
                    employee.Friday = Request.Form["Friday"];
                    employee.Wednesday = Request.Form["Wednesday"];

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

                    string WorkHourPolicyCode = Request.Form["WorkHourPolicyCode"];
                    if (!string.IsNullOrEmpty(WorkHourPolicyCode) && WorkHourPolicyCode != "null")
                        employee.WorkHourPolicyCode = Formatter.SetValidValueToInt(WorkHourPolicyCode);





                    string MainPath = "";

                    //string SimilarPhoto = "";


                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;

                        HttpPostedFileBase file = files[0];
                        string fileName = "Profile_" + employee.Employee_ID + "." + file.FileName.Split('.')[1];

                        photoStream = file.InputStream;

                        string url = HttpContext.Request.Url.Authority;
                        employee.Employee_Photo = "http://" + url + "/profiles/" + fileName;
                        if (!Directory.Exists(Server.MapPath("~/profiles/")))
                            Directory.CreateDirectory(Server.MapPath("~/profiles/"));
                        MainPath = Path.Combine(Server.MapPath("~/profiles/"), fileName);

                        if (photoStream != null)
                            ReduceImageSize(photoStream, MainPath);
                        byte[] bytes = new byte[0];

                        if (employee.Employee_Photo.Contains("http"))
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
                        employee.Employee_Photo = Request.Form["Employee_Photo"];
                    }


                    byte[] encEmployee = new byte[0];
                    hm.EncryptEmployee(employee, out encEmployee);
                    employee.Cmd_Param = encEmployee;

                    flag = objBLL.AddUpdateEmployee(employee);
                    //if (flag)
                    //if (employee.Active == 0)
                    //    flag = hm.DeleteUserfromMachine(employee.Employee_ID, employee.Device_Id, account.UserName);

                    if (flag)
                    {
                        if (employee.Active == 1)
                            employee = objBLL.GetEmployeeByID(employee.Employee_ID);
                    }

                    if (flag)
                    {
                        exEmpflag = objExUserBLL.CheckEmployeeExistInDB(employee.Employee_ID);

                        if (exEmpflag)
                            exEmpflag = objExUserBLL.DeleteExEmployee(employee.Employee_ID);
                    }

                }
                else
                {
                    //if (empflag == true)
                    //{
                    //    msg = "User ID Already Exists";
                    //    flag = false;
                    //}
                    //if (pwdflag == true)
                    //{
                    //    msg = "Pin Already Exists";
                    //    flag = false;
                    //}
                    if (cardflag == true)
                    {
                        title = "Warning!";
                        msg = "Card Already Exists";
                        flag = false;
                    }
                }

                if (Request.Form["Active"] == "false")
                {
                    title = "User Edited Successfully!";
                    msg = "Unable to Send InActive User to Device.";
                    return Json(new
                    {
                        title,
                        result = false,
                        msg,
                        code = employee.Code
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                title,
                result = flag,
                msg,
                code = employee.Code
            }, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult SendUserToDevice(string[] userCodes, string[] deviceIds, int action, string active)
        //{
        //    bool flag = false;
        //    bool userflag = false;
        //    string msg = "";
        //    Employee_P empForInActive = new Employee_P();
        //    BLLEmployee objEmployee = new BLLEmployee();
        //    BLLDevice objDevice = new BLLDevice();
        //    List<Employee_P> emplCountingList = new List<Employee_P>();
        //    //ServiceController myService;
        //    //WinServiceController winServiceOBJ = new WinServiceController();
        //    try
        //    {

        //        //myService = new ServiceController(winServiceOBJ.ServiceName);
        //        //if (myService.Status == ServiceControllerStatus.Paused || myService.Status == ServiceControllerStatus.Stopped)
        //        //    return Json(new
        //        //    {
        //        //        result = flag,
        //        //        serviceStatus = myService.Status,
        //        //        serviceIssue = 1,
        //        //    }, JsonRequestBehavior.AllowGet);
        //        //for (int i = 0; i < userCodes.Length; i++)
        //        //{
        //        //    empls = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCodes[i]));
        //        //    emplCountingList.Add(empls);
        //        //}
        //        for (int i = 0; i < userCodes.Length; i++)
        //        {
        //            for (int j = 0; j < deviceIds.Length; j++)
        //            {
        //                string DeviceID = deviceIds[j];
        //                Device_P device = objDevice.GetDeviceByDevice_ID(DeviceID);
        //                if (device.Device_Status == 0)
        //                {
        //                    return Json(new
        //                    {
        //                        result = flag,
        //                        disconnect = 1,
        //                    }, JsonRequestBehavior.AllowGet);
        //                }
        //                empForInActive = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCodes[i]));
        //                empForInActive.Device_Name = device.Device_Name;
        //                emplCountingList.Add(empForInActive);
        //            }
        //        }
        //        //check all user either is active or not

        //        var allFlag = emplCountingList.All(x => x.Active == 0);

        //        //check if it is only 1
        //        if (emplCountingList.Count() == 1)
        //        {
        //            if (empForInActive.Active == 0)
        //            {
        //                emplCountingList = new List<Employee_P>();


        //                string ids = empForInActive.Employee_ID;
        //                string username = empForInActive.Employee_Name;
        //                string devicename = empForInActive.Device_Name;

        //                int count = 1;
        //                return Json(new { result = false, ids, username, devicename, count, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }

        //        //check wether it is multiple or not
        //        if (emplCountingList.Count() > 1 && allFlag)
        //        {

        //            int count = emplCountingList.Count();
        //            var ids = emplCountingList.Select(x => x.Employee_ID).ToList();
        //            var username = emplCountingList.Select(x => x.Employee_Name).ToList();
        //            var devicename = emplCountingList.Select(x => x.Device_Name).ToList();
        //            return Json(new { result = false, ids, username, devicename, count, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);


        //        }

        //        if (userCodes.Length >= 1 && !allFlag)
        //        {

        //            int count = emplCountingList.Count();
        //            //not active user
        //            var someUserActiveOrNot = emplCountingList.Where(x => x.Active == 0 || x.Active == 1).ToList();
        //            int activeUsers = emplCountingList.Where(x => x.Active == 1).ToList().Count();

        //            foreach (var item in someUserActiveOrNot)
        //            {
        //                userCodes = userCodes.Where(x => x != item.Code.ToString()).ToArray();
        //                //    int foundNumberIndex = Array.IndexOf(userCodes, item.Code);
        //                //    if (userCodes.Length >= 0)
        //                //    {
        //                //        userCodes = userCodes.Take(foundNumberIndex).Concat(userCodes.Skip(foundNumberIndex + 1)).ToArray();
        //                //    }
        //            }

        //            _sendUserToDevice(userCodes, deviceIds, action);
        //            return Json(new { result = true, count, someUserActiveOrNot, activeUsers, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //        flag = false;
        //    }
        //    return Json(new
        //    {
        //        result = flag
        //    }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult SendUserToDevice(string[] userCodes, string[] deviceIds, int action)
        {

            BLLEmployee objEmployee = new BLLEmployee();
            BLLDevice objDevice = new BLLDevice();
            List<Employee_P> empList = new List<Employee_P>();
            Employee_P empByCode = new Employee_P();
            bool flag = false;
            try
            {
                for (int j = 0; j < deviceIds.Length; j++)
                {
                    for (int i = 0; i < userCodes.Length; i++)
                    {

                        empByCode = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCodes[i]));
                        Device_P device = objDevice.GetDeviceByDevice_ID(deviceIds[j]);
                        empByCode.Device_Name = device.Device_Name;
                        empList.Add(empByCode);


                    }
                }
                if (empList.Count() == 1)
                {
                    var empCheckForSingleUser = empList.Select(x => x.Active).First();
                    //Check Not Active for single user
                    if (empCheckForSingleUser != 1)
                    {

                        string empID = empByCode.Employee_ID;
                        string empUserName = empByCode.Employee_Name;
                        string empDeviceName = empByCode.Device_Name;
                        var count = empList.Count();
                        return Json(new { result = false, empID, empUserName, empDeviceName, count, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);
                    }
                    //check active for single user
                    else if (empCheckForSingleUser == 1)
                    {
                        flag = _sendUserToDevice(userCodes, deviceIds, action);
                    }

                }
                else if (empList.Count() > 1)
                {
                    List<Employee_P> filteringMultiActiveUser = new List<Employee_P>();
                    List<Employee_P> filteringMultiInActiveUser = new List<Employee_P>();
                    foreach (var item in empList)
                    {
                        if (item.Active == 1)
                        {
                            filteringMultiActiveUser.Add(item);
                        }
                        else
                        {
                            filteringMultiInActiveUser.Add(item);
                        }
                    }

                    if (filteringMultiActiveUser.Count() > 0)
                    {
                        string[] multiActiveUsersCodes = filteringMultiActiveUser.Select(x => x.Code.ToString()).Distinct().ToArray();
                        _sendUserToDevice(multiActiveUsersCodes, deviceIds, action);
                    }
                    var count = empList.Count();
                    if (filteringMultiInActiveUser.Count() > 0 && filteringMultiActiveUser.Count() == 0)
                    {
                        count = empList.Count();
                        return Json(new { result = false, filteringMultiInActiveUser, filteringMultiActiveUser, count, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);


                    }


                    return Json(new { result = false, filteringMultiInActiveUser, filteringMultiActiveUser, count, msg = "Error Transferring Inactive User" }, JsonRequestBehavior.AllowGet);



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

        private bool _sendUserToDevice(string[] userCodes, string[] deviceIds, int action)
        {
            string msg = "";
            bool flag = false;
            bool userflag = false;
            BLLEmployee objEmployee = new BLLEmployee();
            try
            {
                Account_P account = (Account_P)Session["User"];
                for (int i = 0; i < deviceIds.Count(); i++)
                {
                    //   LogService.WriteLog("[SendUserToDevice]");
                    string DeviceID = deviceIds[i];
                    BLLDevice objDevice = new BLLDevice();
                    EagleEyeManagement hm = new EagleEyeManagement();
                    Device_P device = objDevice.GetDeviceByDevice_ID(DeviceID);
                    // int Progress = 1;
                    //double percent = 0;

                    for (int j = 0; j < userCodes.Count(); j++)
                    {

                        string userCode = userCodes[j];

                        Employee_P emp = new Employee_P();
                        if (action == 1)
                        {
                            emp = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCode));
                            if (emp.Active == 0)
                            {
                                //flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, account.UserName);

                                //flag = objBLL.RestoreUserFromDB(emp.Employee_ID, 2);
                                // emp.Active = 1;
                            }
                        }
                        else if (action == 2)
                        {
                            flag = objBLL.RestoreUserFromDB(userCode, action);
                            emp = objEmployee.GetEmployeeByID(userCode);

                        }


                        if (emp.Active == 1)
                        {
                            if (string.IsNullOrEmpty(emp.Device_Id))
                                emp.Device_Id = device.Device_ID;

                            userflag = objBLL.CheckRealEmployeeExistInDB(emp.Employee_ID, device.Device_ID);
                            if (userflag)
                            {
                                if (emp.Password == "")
                                    flag = hm.DeleteUserfromMachine(emp.Employee_ID, device.Device_ID, account.UserName);

                                else if (emp.Card_No == "")
                                    flag = hm.DeleteUserfromMachine(emp.Employee_ID, device.Device_ID, account.UserName);
                            }
                        }
                    }
                    if (userCodes.Count() <= 2)
                    {
                        Thread.Sleep(1000);
                    }
                    if (userCodes.Count() <= 50 && userCodes.Count() > 2)
                    {
                        Thread.Sleep(2000);
                    }
                    if (userCodes.Count() <= 100 && userCodes.Count() > 50)
                    {
                        Thread.Sleep(3000);
                    }
                    if (userCodes.Count() <= 500 && userCodes.Count() > 100)
                    {
                        Thread.Sleep(5000);
                    }
                    if (userCodes.Count() <= 10000 && userCodes.Count() > 500)
                    {
                        Thread.Sleep(8000);
                    }

                    for (int j = 0; j < userCodes.Count(); j++)
                    {

                        string userCode = userCodes[j];
                        // LogService.WriteLog("[SendUserToDevice]:userCode " + userCode);

                        Employee_P emp = new Employee_P();
                        if (action == 1)
                        {
                            emp = objEmployee.GetEmployeeByCode(Formatter.SetValidValueToInt(userCode));
                            if (emp.Active == 0)
                            {
                                flag = hm.DeleteUserfromMachine(emp.Employee_ID, emp.Device_Id, account.UserName, false);
                                //flag = objBLL.RestoreUserFromDB(emp.Employee_ID, 2);
                                //emp.Active = 1;
                            }
                        }
                        else if (action == 2)
                        {
                            flag = objBLL.RestoreUserFromDB(userCode, action);
                            emp = objEmployee.GetEmployeeByID(userCode);

                        }
                        if (emp.Active == 1)
                        {
                            if (string.IsNullOrEmpty(emp.Device_Id))
                                emp.Device_Id = device.Device_ID;

                            //if (flag)
                            //{
                            flag = hm.SendEmployeeToDevice(device.Device_ID, emp, account.UserName);

                            //emp.Valid_DateEnd = Formatter.SetValidValueToDateTime(emp.Valid_DateEnd).ToString("yyyyMMdd");
                            //emp.Valid_DateStart = Formatter.SetValidValueToDateTime(emp.Valid_DateStart).ToString("yyyyMMdd");
                            //Thread.Sleep(1000);
                            //if (emp.Valid_DateEnd != "00010101" || emp.Valid_DateStart != "00010101")
                            //    flag = hm.SetPassTime(device.Device_ID, emp, account.UserName);
                            //}
                            Thread.Sleep(1000);
                            emp.Valid_DateEnd = Formatter.SetValidValueToDateTime(emp.Valid_DateEnd).ToString("yyyyMMdd");
                            emp.Valid_DateStart = Formatter.SetValidValueToDateTime(emp.Valid_DateStart).ToString("yyyyMMdd");

                            if (emp.Valid_DateEnd != "00010101" || emp.Valid_DateStart != "00010101")
                                flag = hm.SetPassTime(device.Device_ID, emp, account.UserName);
                            flag = hm.SyncDevice(device.Device_ID, account.UserName);
                            if (flag)
                                msg = "success";
                            else
                                msg = "template is not compaitable";

                        }
                        else
                        {
                            flag = false;
                            msg = "User is InActive";
                        }
                    }

                }
                flag = true;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return flag;
        }


        [HttpPost]
        public JsonResult AddLocation(Location_P location)
        {

            bool flag = false;
            try
            {
                flag = objLocBLL.AddUpdateLocation(location);
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
        public JsonResult DeleteLocation(string[] locationCodes)
        {

            bool flag = false;
            try
            {
                for (int i = 0; i < locationCodes.Count(); i++)
                {
                    flag = objLocBLL.DeleteLocation(Formatter.SetValidValueToInt(locationCodes[i]));
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
        public JsonResult AddEmpType(EmployeeType_P emptype)
        {

            bool flag = false;
            try
            {
                flag = objETBLL.AddUpdateEmployeeType(emptype);
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
        public JsonResult DeleteEmpType(string[] emptypeCodes)
        {

            bool flag = false;
            try
            {
                for (int i = 0; i < emptypeCodes.Count(); i++)
                {
                    flag = objETBLL.DeleteEmployeeType(Formatter.SetValidValueToInt(emptypeCodes[i]));
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
        public JsonResult AddDesignation(Designation_P designation)
        {

            bool flag = false;
            try
            {
                flag = objDesBLL.AddUpdateDesignation(designation);
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
        public JsonResult DeleteDesignation(string[] designationCodes)
        {

            bool flag = false;
            try
            {
                for (int i = 0; i < designationCodes.Count(); i++)
                {
                    flag = objDesBLL.DeleteDesignation(Formatter.SetValidValueToInt(designationCodes[i]));
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
        public JsonResult AddDepartment(Department_P department)
        {

            bool flag = false;
            try
            {
                flag = objDeptBLL.AddUpdateDepartment(department);
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
        public JsonResult DeleteDepartment(string[] deptCodes)
        {

            bool flag = false;
            try
            {
                for (int i = 0; i < deptCodes.Count(); i++)
                {
                    flag = objDeptBLL.DeleteDepartment(Formatter.SetValidValueToInt(deptCodes[i]));
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
        public JsonResult GetAllLocations()
        {

            List<Location_P> List = new List<Location_P>();
            try
            {
                List = objLocBLL.GetAllLocation();
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
        public JsonResult GetAllDepartment()
        {

            List<Department_P> List = new List<Department_P>();
            try
            {
                List = objDeptBLL.GetAllDepartment();
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
        public JsonResult GetAllDesignation()
        {

            List<Designation_P> List = new List<Designation_P>();
            try
            {
                List = objDesBLL.GetAllDesignation();
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
        public JsonResult GetAllEmpType()
        {

            List<EmployeeType_P> List = new List<EmployeeType_P>();
            try
            {
                List = objETBLL.GetAllEmployeeType();
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

        public static void SaveJpeg(string path, Image img, int quality)
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

        private void ReduceImageSize(Stream sourcePath, string targetPath)
        {
            try
            {

                Image image = Image.FromStream(sourcePath);
                SaveJpeg(targetPath, image, 50);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            //System.Drawing.Imaging.Encoder myencoder = System.Drawing.Imaging.Encoder.ColorDepth;
            //using (var images = System.Drawing.Image.FromStream(sourcePath))
            //{
            //    var newWidth = 200;
            //    var newHeight = 200;
            //    var thumbnailImg = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            //    var thumbGraph = Graphics.FromImage(thumbnailImg);
            //    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            //    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            //    thumbGraph.InterpolationMode = InterpolationMode.Low;
            //    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            //    thumbGraph.DrawImage(images, imageRectangle);
            //    thumbnailImg.Save(targetPath, images.RawFormat);
            //}
        }


        public JsonResult Export(string[] lstUserCode)
        {
            bool flag = false;
            string filename = "";
            string path = "";
            string name = "";
            List<Employee_P> rptList = new List<Employee_P>();
            try
            {

                var employee = objBLL.GetEmployeeByCodes(lstUserCode).OrderBy(x => Int32.Parse(x.Employee_ID)).ToList();
                int empcount = employee.Count();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reports");
                Sheet.Cells["A1"].Value = "Employee ID";
                Sheet.Cells["B1"].Value = "Employee Name";
                Sheet.Cells["C1"].Value = "Location";
                Sheet.Cells["D1"].Value = "Status";
                IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<WebHub>();
                int total = 0;
                Progress = 1;
                int row = 2;
                foreach (var emp in employee)
                {

                    Employee_P rpt = new Employee_P
                    {
                        Employee_ID = emp.Employee_ID,
                        Employee_Name = emp.Employee_Name,
                        Location = emp.Location,
                        Active = emp.Active
                    };

                    if (rpt.Active == 0)
                    {
                        rpt.status = "InActive";

                    }
                    else if (rpt.Active == 1)
                    {

                        rpt.status = "Active";
                    }

                    Sheet.Cells[string.Format("A{0}", row)].Value = rpt.Employee_ID;
                    Sheet.Cells[string.Format("B{0}", row)].Value = rpt.Employee_Name;
                    Sheet.Cells[string.Format("C{0}", row)].Value = rpt.Location;
                    Sheet.Cells[string.Format("D{0}", row)].Value = rpt.status;
                    //Sheet.Cells[string.Format("E{0}", row)].Value = rpt.EmployeeType;
                    //Sheet.Cells[string.Format("F{0}", row)].Value = rpt.Designation;
                    //Sheet.Cells[string.Format("G{0}", row)].Value = rpt.TimeIn;
                    //Sheet.Cells[string.Format("H{0}", row)].Value = rpt.TimeOutDate;
                    //Sheet.Cells[string.Format("I{0}", row)].Value = rpt.TimeOut;
                    //Sheet.Cells[string.Format("J{0}", row)].Value = rpt.Device;
                    //Sheet.Cells[string.Format("K{0}", row)].Value = rpt.dt_workedhours;
                    //Sheet.Cells[string.Format("L{0}", row)].Value = rpt.att_count;
                    // Sheet.Cells[string.Format("H{0}", row)].Value = att.Status;
                    total = employee.Count();
                    percent = (Progress * 100) / total;

                    hubContext.Clients.All.ExportUser(emp, total, Progress, percent);
                    Progress++;
                    row++;

                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                name = "Reports_" + DateTime.Now.ToString("ddMMyyyy");
                filename = name + ".xlsx";
                bool exist = Directory.Exists(Server.MapPath("~/Reports/"));
                if (!exist)
                {
                    Directory.CreateDirectory(Server.MapPath("~/Reports/"));
                }
                path = Path.Combine(Server.MapPath("~/Reports/"), filename);
                System.IO.File.WriteAllBytes(path, Ep.GetAsByteArray());
                flag = true;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                fullname = filename
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string filename)
        {
            bool flag = false;
            string msg = "";
            try
            {
                string dest = Server.MapPath("~/Reports/");
                string destpath = System.IO.Path.Combine(dest, filename);
                DeleteFile(destpath);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = flag,
                msg,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion



        public JsonResult GetRecordsByFilter(string loc, string status)
        {
            var list = objBLL.GetRecordsByFilter(loc, status);

            return Json(new
            {
                users = list.ToArray()
            }, JsonRequestBehavior.AllowGet);


        }
    }
}