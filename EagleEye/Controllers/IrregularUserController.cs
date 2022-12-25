using EagleEye.BLL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using Common;
using System.Linq;
using System.Net;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using EagleEye.Common;
using System.Threading;

namespace EagleEye.Controllers
{
    public class IrregularUserController : Controller
    {
        BLLIrregularEmployee objBLL = new BLLIrregularEmployee();

        #region Action Methods


        [CheckAuthorization]
        public ActionResult Index()
        {
            List<IrregularEmployee_P> list = new List<IrregularEmployee_P>();
            try
            {
                if (!CheckRights("Irregular Users"))
                {
                    return RedirectToAction("Index", "Home");
                }

                // list = objBLL.GetAllEmployees();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return View();
        }

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

        [HttpPost]
        public JsonResult Delete(string Code)
        {
            EagleEyeManagement m = new EagleEyeManagement();
            IrregularEmployee_P irr = new IrregularEmployee_P();
            bool flag = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                irr = objBLL.GetIrrEmployee(Code);
                flag = objBLL.DeleteEmployee(Formatter.SetValidValueToInt(Code));
                flag = m.DeleteUserfromMachine(irr.Employee_ID, irr.Device_Id, account.UserName);
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
        public JsonResult DeleteAll()
        {
            EagleEyeManagement m = new EagleEyeManagement();
            bool flag = false;
            bool empDeletedFromDb = false;
            try
            {
                Account_P account = (Account_P)Session["User"];
                List<IrregularEmployee_P> emp = objBLL.GetAllEmployees();
                foreach (var item in emp)
                {
                    int id = Formatter.SetValidValueToInt(item.Employee_ID);
                    empDeletedFromDb = objBLL.DeleteEmployee(id);

                    flag = m.DeleteUserfromMachine(item.Employee_ID, item.Device_Id, account.UserName);
                    if (empDeletedFromDb)
                        flag = true;

                }

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
        public JsonResult TransferbyDB()
        {
            bool flag = false;
            string msg = "";
            try
            {
                Account_P account = (Account_P)Session["User"];
                List<IrregularEmployee_P> ListIrremp = objBLL.GetAllEmployees();
                BLLEmployee objEmployee = new BLLEmployee();

                BLLDevice objDevices = new BLLDevice();
                List<Device_P> ListDev = objDevices.GetAllConnectedDevices();

                if (ListDev.Count != 0)
                {
                    for (int i = 0; i < ListDev.Count; i++)
                    {
                        foreach (var item in ListIrremp)
                        {
                            Employee_P emp = new Employee_P();
                            emp = objEmployee.GetEmployeeByID(item.Employee_ID);
                            if (emp != null)
                            {
                                EagleEyeManagement hm = new EagleEyeManagement();
                                flag = hm.DeleteUserfromMachine(item.Employee_ID, ListDev[i].Device_ID, account.UserName);
                            }
                        }
                        if (ListIrremp.Count() <= 2)
                        {
                            Thread.Sleep(1000);
                        }
                        if (ListIrremp.Count() <= 50 && ListIrremp.Count() > 2)
                        {
                            Thread.Sleep(2000);
                        }
                        if (ListIrremp.Count() <= 100 && ListIrremp.Count() > 50)
                        {
                            Thread.Sleep(3000);
                        }
                        if (ListIrremp.Count() <= 500 && ListIrremp.Count() > 100)
                        {
                            Thread.Sleep(5000);
                        }
                        if (ListIrremp.Count() <= 10000 && ListIrremp.Count() > 500)
                        {
                            Thread.Sleep(8000);
                        }
                        foreach (var item in ListIrremp)
                        {
                            Employee_P emp = new Employee_P();
                            emp = objEmployee.GetEmployeeByID(item.Employee_ID);
                            if (emp != null)
                            {
                                EagleEyeManagement hm = new EagleEyeManagement();
                                if (flag)
                                {
                                    flag = hm.SendEmployeeToDevice(ListDev[i].Device_ID, emp, account.UserName);
                                }
                            }
                        }
                    }
                    foreach (var item in ListIrremp)
                    {
                        Employee_P emp = new Employee_P();
                        emp = objEmployee.GetEmployeeByID(item.Employee_ID);
                        if (emp != null)
                        {
                            int id = Formatter.SetValidValueToInt(item.Code);
                            objBLL.DeleteEmployee(id);
                            flag = true;
                        }

                    }
                }
                else
                {
                    msg = "No Device Connected...";
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                flag = false;
            }
            return Json(new
            {
                result = flag,
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TransferbyMachine()
        {
            bool flag = false;
            try
            {
                List<IrregularEmployee_P> ListIrremp = objBLL.GetAllEmployees();
                BLLEmployee objEmployee = new BLLEmployee();
                BLLIrregularEmployee objIrr = new BLLIrregularEmployee();
                BLLDevice objDevice = new BLLDevice();


                foreach (var item in ListIrremp)
                {
                    int id = Formatter.SetValidValueToInt(item.Code);
                    var deviceLoc = objDevice.GetDeviceByDevice_ID(item.Device_Id);

                    Employee_P emp = new Employee_P
                    {
                        Employee_ID = item.Employee_ID,
                        Employee_Name = item.Employee_Name,
                        Employee_Photo = item.Employee_Photo,
                        Card_No = item.Card_No,
                        Address = item.Address,
                        Device_Id = item.Device_Id,
                        Gender = item.Gender,
                        Telephone = item.Telephone,
                        Active = 1,
                        Update_Date = item.Update_Date,
                        Cmd_Param = item.Cmd_Param,
                        User_Privilege = item.User_Privilege,
                        FingerPrint = item.FingerPrint,
                        fkLocation_Code = Formatter.SetValidValueToInt(deviceLoc.Device_Location),
                        Face = item.Face,
                        Palm = item.Palm,
                        Password = item.Password,
                        finger_0 = item.finger_0,
                        finger_1 = item.finger_1,
                        finger_2 = item.finger_2,
                        finger_3 = item.finger_3,
                        finger_4 = item.finger_4,
                        finger_5 = item.finger_5,
                        finger_6 = item.finger_6,
                        finger_7 = item.finger_7,
                        finger_8 = item.finger_8,
                        finger_9 = item.finger_9,
                        face_data = item.face_data,
                        palm_0 = item.palm_0,
                        palm_1 = item.palm_1,
                        photo_data = item.photo_data
                    };
                    if (emp.Card_No != "")
                    {
                        Employee_P employee;
                        flag = objEmployee.CheckCardExistInDB(emp.Card_No, out employee);
                        if (!flag)
                        {
                            flag = objEmployee.TransferByMachine(emp);
                            if (flag)
                            {
                                objBLL.DeleteEmployee(id);
                            }
                        }
                        else
                        {
                            string msg = "Card already assigned to " + employee.Employee_Name + " In DB";
                            flag = objIrr.AddUpdateIrrEmployee(msg, id);
                        }
                    }
                    //else if (emp.Password != "")
                    //{
                    //    Employee_P employee;
                    //    flag = objEmployee.CheckPwdExistInDB(emp.Password, out employee);
                    //    if (!flag)
                    //    {
                    //        flag = objEmployee.TransferByMachine(emp);
                    //        if (flag)
                    //        {
                    //            objBLL.DeleteEmployee(id);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        string msg = "Password already assigned to " + employee.Employee_Name + " In DB";
                    //        flag = objIrr.AddUpdateIrrEmployee(msg, id);
                    //    }
                    //}
                    else if (emp.Employee_ID != "")
                    {
                        Employee_P employee;
                        flag = objEmployee.CheckUserExistInDB(emp.Employee_ID, out employee);
                        if (!flag)
                        {
                            flag = objEmployee.TransferByMachine(emp);
                            if (flag)
                            {
                                objBLL.DeleteEmployee(id);
                            }
                            flag = true;
                        }
                        else
                        {
                            string msg = "UserID already assigned to " + employee.Employee_Name + " In DB";
                            flag = false;
                        }

                    }
                    else
                    {
                        flag = objEmployee.TransferByMachine(emp);
                        if (flag)
                        {
                            objBLL.DeleteEmployee(id);
                        }
                    }
                    flag = true;


                }

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
        public JsonResult TransferUserbyDB(string Code)
        {
            bool flag = false;
            string msg = "";
            try
            {
                Account_P account = (Account_P)Session["User"];
                IrregularEmployee_P Irremp = objBLL.GetIrrEmployee(Code);
                BLLEmployee objEmployee = new BLLEmployee();

                BLLDevice objDevices = new BLLDevice();
                List<Device_P> ListDev = objDevices.GetAllConnectedDevices();

                if (ListDev.Count != 0)
                {
                    Employee_P emp = new Employee_P();
                    for (int i = 0; i < ListDev.Count; i++)
                    {
                        emp = objEmployee.GetEmployeeByID(Irremp.Employee_ID);
                        if (emp != null)
                        {
                            EagleEyeManagement hm = new EagleEyeManagement();
                            flag = hm.DeleteUserfromMachine(Irremp.Employee_ID, ListDev[i].Device_ID, account.UserName);
                        }

                        Thread.Sleep(1000);
                        // emp = objEmployee.GetEmployeeByID(Irremp.Employee_ID);
                        if (emp != null)
                        {
                            EagleEyeManagement hm = new EagleEyeManagement();
                            if (flag)
                            {
                                flag = hm.SendEmployeeToDevice(ListDev[i].Device_ID, emp, account.UserName);
                            }
                        }
                    }
                    emp = objEmployee.GetEmployeeByID(Irremp.Employee_ID);
                    if (emp != null)
                    {
                        int id = Formatter.SetValidValueToInt(Irremp.Code);
                        objBLL.DeleteEmployee(id);
                        flag = true;
                    }
                }
                else
                {
                    msg = "No Device Connected...";
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
                flag = false;
            }
            return Json(new
            {
                result = flag,
                msg
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TransferUserbyMachine(string Code)
        {
            bool flag = false;
            string msg = "";
            try
            {
                //List<IrregularEmployee_P> ListIrremp = objBLL.GetAllEmployees();
                BLLEmployee objEmployee = new BLLEmployee();
                IrregularEmployee_P Irremp = objBLL.GetIrrEmployee(Code);
                BLLDevice objDevice = new BLLDevice();
                var deviceLoc = objDevice.GetDeviceByDevice_ID(Irremp.Device_Id);
                //foreach (var item in ListIrremp)
                //{
                int id = Formatter.SetValidValueToInt(Irremp.Code);
                Employee_P emp = new Employee_P
                {
                    Employee_ID = Irremp.Employee_ID,
                    Employee_Name = Irremp.Employee_Name,
                    Employee_Photo = Irremp.Employee_Photo,
                    Card_No = Irremp.Card_No,
                    Address = Irremp.Address,
                    Device_Id = Irremp.Device_Id,
                    fkLocation_Code = Formatter.SetValidValueToInt(deviceLoc.Device_Location),
                    Gender = Irremp.Gender,
                    Telephone = Irremp.Telephone,
                    Active = 1,
                    Update_Date = Irremp.Update_Date,
                    Cmd_Param = Irremp.Cmd_Param,
                    User_Privilege = Irremp.User_Privilege,
                    FingerPrint = Irremp.FingerPrint,
                    Face = Irremp.Face,
                    Palm = Irremp.Palm,
                    Password = Irremp.Password,
                    finger_0 = Irremp.finger_0,
                    finger_1 = Irremp.finger_1,
                    finger_2 = Irremp.finger_2,
                    finger_3 = Irremp.finger_3,
                    finger_4 = Irremp.finger_4,
                    finger_5 = Irremp.finger_5,
                    finger_6 = Irremp.finger_6,
                    finger_7 = Irremp.finger_7,
                    finger_8 = Irremp.finger_8,
                    finger_9 = Irremp.finger_9,
                    face_data = Irremp.face_data,
                    palm_0 = Irremp.palm_0,
                    palm_1 = Irremp.palm_1,
                    photo_data = Irremp.photo_data
                };
                if (Irremp.Card_No != "")
                {
                    Employee_P employee;
                    //string Employee_Name = "";
                    flag = objEmployee.CheckCardExistInDB(Irremp.Card_No, out employee);
                    if (!flag)
                    {
                        //New Work
                        var newEmp = pictureManagement(emp);

                        //Old Work
                        //if (emp.Employee_Photo != "")
                        //{

                        //    string fileName = "Profile_" + employee.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                        //    string destinationPath = Server.MapPath("~/profiles/");
                        //    string MainPath = Server.MapPath("~/irregularProfiles/");
                        //    DirectoryInfo d = new DirectoryInfo(MainPath);
                        //    FileInfo[] files = d.GetFiles();

                        //    foreach (var item in files)
                        //    {
                        //        //var itemname = item.Name.Split('.');
                        //        //var changingFilename = fileName.Split('.');
                        //        string filetoMove = MainPath + item;
                        //        if (System.IO.File.Exists(MainPath))
                        //        {
                        //            string moveTo = destinationPath + item;
                        //            if (System.IO.File.Exists(moveTo))
                        //                System.IO.File.Delete(moveTo);

                        //            System.IO.File.Move(filetoMove, moveTo);
                        //            System.IO.File.Delete(filetoMove);
                        //        }
                        //    }


                        //}
                        //var empPhotoChangingPath = emp.Employee_Photo.Split('/');
                        //empPhotoChangingPath[1] = "profiles";
                        //emp.Employee_Photo = "/" + empPhotoChangingPath[1] + "/" + empPhotoChangingPath[2];
                        flag = objEmployee.TransferByMachine(newEmp);
                        if (flag)
                        {
                            objBLL.DeleteEmployee(Formatter.SetValidValueToInt(emp.Employee_ID));
                        }
                        flag = true;
                    }
                    else
                    {
                        msg = "Card already assigned to " + employee.Employee_Name + " In DB";
                        flag = false;
                    }
                }
                else if (Irremp.Employee_ID != "")
                {
                    Employee_P employee;
                    flag = objEmployee.CheckUserExistInDB(Irremp.Employee_ID, out employee);
                    if (flag)
                    {

                        //New Work
                        var newEmp = pictureManagement(emp);

                        //if (emp.Employee_Photo != "")
                        //{

                        //    string fileName = "Profile_" + employee.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                        //    string destinationPath = Server.MapPath("~/profiles/");
                        //    string MainPath = Server.MapPath("~/irregularProfiles/");
                        //    DirectoryInfo d = new DirectoryInfo(MainPath);
                        //    FileInfo[] files = d.GetFiles();

                        //    foreach (var item in files)
                        //    {
                        //        if (item.Name == fileName)
                        //        {
                        //            string filetoMove = MainPath + item;
                        //            string moveTo = destinationPath + item;
                        //            if (System.IO.File.Exists(moveTo))
                        //                System.IO.File.Delete(moveTo);

                        //            System.IO.File.Move(filetoMove, moveTo);
                        //            System.IO.File.Delete(filetoMove);

                        //        }
                        //    }


                        //}

                        //var empPhotoChangingPath = emp.Employee_Photo.Split('/');
                        //empPhotoChangingPath[1] = "profiles";
                        //emp.Employee_Photo = "/" + empPhotoChangingPath[1] + "/" + empPhotoChangingPath[2];
                        flag = objEmployee.TransferByMachine(newEmp);
                        if (flag)
                        {
                            objBLL.DeleteEmployee(Formatter.SetValidValueToInt(emp.Employee_ID));
                        }
                        flag = true;
                    }
                    else if (!flag && employee.Employee_ID == null)
                    {
                        var newEmp = pictureManagement(emp);
                        flag = objEmployee.TransferByMachine(newEmp);
                        if (flag)
                        {
                            objBLL.DeleteEmployee(Formatter.SetValidValueToInt(emp.Employee_ID));
                        }
                        flag = true;
                    }
                    else
                    {
                        msg = "UserID already assigned to " + emp.Employee_Name + " In DB";
                        flag = false;
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


        public Employee_P pictureManagement(Employee_P emp)
        {

            try
            {
                if (emp.Employee_Photo != "")
                {
                    string fileName = "Profile_" + emp.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                    string destinationPath = Server.MapPath("~/profiles/");
                    string MainPath = Server.MapPath("~/irregularProfiles/");
                    DirectoryInfo d = new DirectoryInfo(MainPath);
                    FileInfo[] files = d.GetFiles();

                    foreach (var item in files)
                    {
                        if (!Directory.Exists(MainPath))
                            Directory.CreateDirectory(MainPath);
                        //var itemname = item.Name.Split('.');
                        //var changingFilename = fileName.Split('.');
                        string filetoMove = MainPath + item;
                        if (Directory.Exists(MainPath))
                        {
                            string moveTo = destinationPath + item;
                            if (System.IO.File.Exists(moveTo))
                                System.IO.File.Delete(moveTo);

                            System.IO.File.Move(filetoMove, moveTo);
                            System.IO.File.Delete(filetoMove);
                        }
                    }


                }

                var empPhotoChangingPath = emp.Employee_Photo.Split('\\');

                emp.Employee_Photo = @"\profiles\" + empPhotoChangingPath[2];

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return emp;
        }

        [HttpPost]
        public JsonResult TransferUserbyMachineForce(string Code)
        {
            bool flag = false;
            try
            {

                //List<IrregularEmployee_P> ListIrremp = objBLL.GetAllEmployees();
                BLLEmployee objEmployee = new BLLEmployee();

                IrregularEmployee_P Irremp = objBLL.GetIrrEmployee(Code);
                //foreach (var item in ListIrremp)
                //{
                int id = Formatter.SetValidValueToInt(Irremp.Code);

                Employee_P emp = new Employee_P
                {
                    Employee_ID = Irremp.Employee_ID,
                    Employee_Name = Irremp.Employee_Name,
                    Employee_Photo = Irremp.Employee_Photo,
                    Card_No = Irremp.Card_No,
                    Address = Irremp.Address,
                    Device_Id = Irremp.Device_Id,
                    Gender = Irremp.Gender,
                    Telephone = Irremp.Telephone,
                    Active = 1,
                    Update_Date = Irremp.Update_Date,
                    Cmd_Param = Irremp.Cmd_Param,
                    User_Privilege = Irremp.User_Privilege,
                    FingerPrint = Irremp.FingerPrint,
                    Face = Irremp.Face,
                    Palm = Irremp.Palm,
                    Password = Irremp.Password,
                    finger_0 = Irremp.finger_0,
                    finger_1 = Irremp.finger_1,
                    finger_2 = Irremp.finger_2,
                    finger_3 = Irremp.finger_3,
                    finger_4 = Irremp.finger_4,
                    finger_5 = Irremp.finger_5,
                    finger_6 = Irremp.finger_6,
                    finger_7 = Irremp.finger_7,
                    finger_8 = Irremp.finger_8,
                    finger_9 = Irremp.finger_9,
                    face_data = Irremp.face_data,
                    palm_0 = Irremp.palm_0,
                    palm_1 = Irremp.palm_1,
                    photo_data = Irremp.photo_data
                };
                //if (emp.Employee_Photo != "")
                //{

                //    string fileName = "Profile_" + Irremp.Employee_ID + "." + emp.Employee_Photo.Split('.')[1];
                //    string destinationPath = Server.MapPath("~/profiles/");
                //    string MainPath = Server.MapPath("~/irregularProfiles/");
                //    DirectoryInfo d = new DirectoryInfo(MainPath);
                //    FileInfo[] files = d.GetFiles();

                //    foreach (var item in files)
                //    {
                //        if (item.Name == fileName)
                //        {
                //            string filetoMove = MainPath + item;
                //            string moveTo = destinationPath + item;
                //            if (System.IO.File.Exists(moveTo))
                //                System.IO.File.Delete(moveTo);

                //            System.IO.File.Move(filetoMove, moveTo);
                //            System.IO.File.Delete(filetoMove);

                //        }
                //    }


                //}
                //var empPhotoChangingPath = emp.Employee_Photo.Split('/');
                //empPhotoChangingPath[1] = "profiles";
                //emp.Employee_Photo = "/" + empPhotoChangingPath[1] + "/" + empPhotoChangingPath[2];
                //New Work
                var newEmp = pictureManagement(emp);
                flag = objEmployee.TransferByMachine(newEmp);
                objBLL.DeleteEmployee(Formatter.SetValidValueToInt(Code));
                //if (flag)
                //{
                //    Employee_P employee;
                //    objBLL.DeleteEmployee(id);
                //    if (emp.Card_No != "")
                //    {
                //        //string Employee_Name = "";
                //        flag = objEmployee.CheckCardExistInDB(emp.Card_No, out employee);
                //        flag = objEmployee.DeleteEmployee(employee.Employee_ID);
                //    }
                //    else if (Irremp.Employee_ID != "")
                //    {
                //        flag = objEmployee.CheckUserExistInDB(emp.Employee_ID, out employee);
                //        flag = objEmployee.DeleteEmployee(employee.Employee_ID);
                //    }
                //else if (emp.Password != "")
                //{
                //    Employee_P employee;
                //    flag = objEmployee.CheckPwdExistInDB(emp.Password, out employee);
                //    flag = objEmployee.DeleteEmployee(employee.Employee_ID);
                //}
                // }
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


        public JsonResult showProfiles(string emp_id)
        {
            IrregularEmployee_P emp = new IrregularEmployee_P();
            try
            {
                emp = objBLL.GetIrrEmployeeByEmpID(emp_id);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = emp,
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Datatable

        public JsonResult GetRecords(JqueryDatatableParam param)
        {
            int TotalRecords = 0;


            var list = objBLL.GetDataTable(param, out TotalRecords).ToList();


            return Json(new
            {
                param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = list

            }, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region Private Methods
        private void ReduceImageSize(Stream sourcePath, string targetPath)
        {
            Image image = Image.FromStream(sourcePath);
            SaveJpeg(targetPath, image, 50);

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

        #endregion


    }
}




//[HttpPost]
//public JsonResult TransferAll()
//{

//    bool flag = false;

//    try
//    {
//        List<IrregularEmployee_P> ListIrremp = objBLL.GetAllEmployees();
//        BLLEmployee objEmployee = new BLLEmployee();
//        BLLDevice objDevices = new BLLDevice();
//        List<Device_P> ListDev = objDevices.GetAllConnectedDevices();
//        string[] Devices = new string[ListDev.Count];
//        string[] msg = new string[ListDev.Count];
//        bool[] f = new bool[ListDev.Count];

//        for (int i = 0; i < ListDev.Count; i++)
//        {
//            Devices[i] = ListDev[i].Device_ID;
//        }


//        foreach (var item in ListIrremp)
//        {
//            int id = Formatter.SetValidValueToInt(item.Code);

//            Employee_P emp = new Employee_P
//            {
//                Employee_ID = item.Employee_ID,
//                Employee_Name = item.Employee_Name,
//                Employee_Photo = item.Employee_Photo,
//                Card_No = item.Card_No,
//                Address = item.Address,
//                Device_Id = item.Device_Id,
//                Gender = item.Gender,
//                Telephone = item.Telephone,
//                Active = 1,
//                Update_Date = item.Update_Date,
//                Cmd_Param = item.Cmd_Param,
//                User_Privilege = item.User_Privilege,
//                FingerPrint = item.FingerPrint,
//                Face = item.Face,
//                Palm = item.Palm,
//                Password = item.Password,
//                finger_0 = item.finger_0,
//                finger_1 = item.finger_1,
//                finger_2 = item.finger_2,
//                finger_3 = item.finger_3,
//                finger_4 = item.finger_4,
//                finger_5 = item.finger_5,
//                finger_6 = item.finger_6,
//                finger_7 = item.finger_7,
//                finger_8 = item.finger_8,
//                finger_9 = item.finger_9,
//                face_data = item.face_data,
//                palm_0 = item.palm_0,
//                palm_1 = item.palm_1,
//                photo_data = item.photo_data
//            };


//            flag = objEmployee.TransferAllEmployee(emp);
//            if (flag)
//            {
//                objBLL.DeleteEmployee(id);
//            }

//        }
//        flag = true;
//    }
//    catch (Exception ex)
//    {
//        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
//    }
//    return Json(new
//    {
//        result = flag,
//    }, JsonRequestBehavior.AllowGet);

//}
