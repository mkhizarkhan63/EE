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
using System.IO;

namespace EagleEye.Controllers
{
    public class MonitoringController : masterController
    {
        [CheckAuthorization]
        public ActionResult Index()
        {
            try
            {
                if (!CheckRights("Monitoring"))
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.TIS = Setting.EnableTISIntegration;
                ViewBag.SQL = Setting.EnableSQLIntegration;
                ViewBag.Oracle = Setting.EnableOrclIntegration;
                ViewBag.MySql = Setting.EnableMySqlIntegration;

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return View();
        }


        public JsonResult GetRecords(JqueryDatatableParam param, string start, string end)
        {
            List<Attendance_P> attDb = new List<Attendance_P>();
            Att_Status_P att_status = new Att_Status_P();
            int TotalRecords = 0;
            try
            {
                BLLAttendance objBLLAtt = new BLLAttendance();
                BLLAtt_Status objBLLAtt_Status = new BLLAtt_Status();
                //if (!IsList)
                //{
                attDb = objBLLAtt.GetAttendanceByDate(param, start, end, out TotalRecords);
                // }
                //else
                //{
                //    attDb = objBLLAtt.GetAttendanceByDate(start, end);
                //}
                //att = query.ToList().OrderBy(x => Convert.ToInt32(x.Employee_ID)).ToList();


                //License Lic = new License();

                //string PlainFileName = "";
                //string EncFileName = "";

                //if (Setting.UseFixedPollingFile)
                //{
                //    PlainFileName = "Plain";
                //    EncFileName = Setting.PollingFileName;
                //}


                //if (IsList || IsTIS || IsSQL || IsOrcl || IsMySql)
                //{



                //    BLLDevice objBLL = new BLLDevice();
                //    foreach (Attendance_P attendance in attDb)
                //    {

                //        Device_P device = objBLL.GetDeviceByDevice_ID(attendance.Device_ID);
                //        string Device_Name = "", Reader_ID = "";
                //        if (device != null)
                //        {
                //            Reader_ID = device.Reader_ID.ToString();
                //            Device_Name = device.Device_Name;
                //            //Reader_ID = device.Reader_ID;
                //        }

                //        EventLogs log = new EventLogs
                //        {
                //            DeviceID = attendance.Device_ID,
                //            DeviceName = Device_Name,
                //            UserID = attendance.Employee_ID,
                //            UserName = attendance.Employee,
                //            DateTime = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                //            VerifyMode = attendance.Verify_Mode,
                //            Status = attendance.Status,
                //            WorkCode = attendance.WorkCode,
                //            DoorStatus = attendance.DoorStatus
                //        };


                //        if (IsList && Formatter.SetValidValueToInt(log.UserID) > 0)
                //        {

                //            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                //            {
                //                #region List Generation

                //                string ReaderID = string.IsNullOrEmpty(Reader_ID) ? "0" : Reader_ID;
                //                string RawDate = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime).ToString("31yyyyMMddHHmm");
                //                string RawData = RawDate
                //                    + "00"
                //                    + log.Status.PadLeft(2, '0')
                //                    + log.UserID.PadLeft(10, '0')
                //                + "0"
                //                + ReaderID.PadLeft(3, '0');


                //                if (Setting.UseDateTimePollingFile)
                //                {
                //                    DateTime dt = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime);
                //                    PlainFileName = "Plain_" + dt.ToString("MM") + "_" + dt.ToString("yyyy");
                //                    EncFileName = Setting.PollingFileName + "_" + dt.ToString("MM") + "_" + dt.ToString("yyyy");
                //                }
                //                bool exist = Directory.Exists(Setting.PollingFolderName);
                //                if (!exist)
                //                {
                //                    Directory.CreateDirectory(Setting.PollingFolderName);
                //                }
                //                //Plain List File
                //                if (Setting.AllowPlainList && Lic.LDAT)
                //                {

                //                    ListGeneration.ListDat(RawData, Setting.PollingFolderName + @"\" + PlainFileName + ".dat");
                //                }

                //                //Encrypted List File
                //                ListGeneration.ListDatEncrypted(RawData, Setting.PollingFolderName + @"\" + EncFileName + ".dat");


                //                //Hidden List File
                //                ListGeneration.ListDatEncryptedHidden(RawData, Setting.HiddenFilePath + @"\" + EncFileName + ".dat");


                //                #endregion

                //            }
                //        }

                //        #region TIS Integeration

                //        if (IsTIS)
                //        {

                //            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                //            {
                //                TISIntigeration TIS = new TISIntigeration(Setting);

                //                TIS.InsertData(log, Reader_ID);
                //            }
                //        }

                //        #endregion

                //        #region SQL Integeration

                //        if (IsSQL)
                //        {


                //            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                //            {

                //                SQLIntigeration Sql = new SQLIntigeration(Setting);
                //                Sql.InsertData(log, Reader_ID, Device_Name, Setting);
                //            }
                //        }


                //        #endregion

                //        #region Oracle Integeration

                //        if (IsOrcl)
                //        {

                //            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                //            {
                //                OracleIntigeration Oracle = new OracleIntigeration();
                //                Oracle.InsertData(log, Reader_ID, Device_Name, Setting);
                //            }
                //        }

                //        #endregion

                //        #region MySql Integration

                //        if (IsMySql)
                //        {
                //            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                //            {

                //                MySQLIntegration Sql = new MySQLIntegration(Setting);
                //                Sql.InsertData(log, Reader_ID, Device_Name, Setting);
                //            }

                //        }

                //        #endregion

                //        //if (!string.IsNullOrEmpty(attendance.Attendance_Photo))
                //        //{
                //        //    attendance.Attendance_Photo = "<input type='hidden' id='hdnimg_" + attendance.Code + "' value='" + attendance.Attendance_Photo + "'/><i class='feather icon-image' style='font-size:20px;cursor:pointer;' onclick='showImg(" + attendance.Code + ");'></i>";
                //        //}

                //        attendance.Status = attendance.Status;


                //    }

                //}

                foreach (var item in attDb)
                {
                    if (item.Employee == "Unregistered")
                    {
                        item.Attendance_Photo = "";
                    }
                    item.DateTime = item.Attendance_DateTime?.ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = attDb,

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult proceedList(string start, string end, bool IsList, bool IsTIS, bool IsSQL, bool IsOrcl, bool IsMySql)
        {

            List<Attendance_P> attDb = new List<Attendance_P>();
            BLLAttendance objBLLAtt = new BLLAttendance();
            string msg = "";
            bool flag = false;
            try
            {

                attDb = objBLLAtt.GetAttendanceByDateForList(start, end);

                License Lic = new License();

                string PlainFileName = "";
                string EncFileName = "";

                if (Setting.UseFixedPollingFile)
                {
                    PlainFileName = "Plain";
                    EncFileName = Setting.PollingFileName;
                }


                if (IsList || IsTIS || IsSQL || IsOrcl || IsMySql)
                {

                    BLLDevice objBLL = new BLLDevice();
                    foreach (Attendance_P attendance in attDb)
                    {

                        Device_P device = objBLL.GetDeviceByDevice_ID(attendance.Device_ID);
                        string Device_Name = "", Reader_ID = "";
                        if (device != null)
                        {
                            Reader_ID = device.Reader_ID.ToString();
                            Device_Name = device.Device_Name;
                            //Reader_ID = device.Reader_ID;
                        }

                        EventLogs log = new EventLogs
                        {
                            DeviceID = attendance.Device_ID,
                            DeviceName = Device_Name,
                            UserID = attendance.Employee_ID,
                            UserName = attendance.Employee,
                            DateTime = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                            VerifyMode = attendance.Verify_Mode,
                            Status = attendance.Status,
                            WorkCode = attendance.WorkCode,
                            DoorStatus = attendance.DoorStatus
                        };


                        if (IsList && Formatter.SetValidValueToInt(log.UserID) > 0)
                        {

                            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                            {
                                #region List Generation

                                string ReaderID = string.IsNullOrEmpty(Reader_ID) ? "0" : Reader_ID;
                                string RawDate = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime).ToString("31yyyyMMddHHmm");
                                string RawData = RawDate
                                    + "00"
                                    + log.Status.PadLeft(2, '0')
                                    + log.UserID.PadLeft(10, '0')
                                + "0"
                                + ReaderID.PadLeft(3, '0');


                                if (Setting.UseDateTimePollingFile)
                                {
                                    //DateTime dt = Formatter.SetValidValueToDateTime(attendance.Attendance_DateTime);
                                    DateTime dt = DateTime.Now;
                                    PlainFileName = "Plain_" + dt.ToString("MM") + "_" + dt.ToString("yyyy");
                                    EncFileName = Setting.PollingFileName + "_" + dt.ToString("MM") + "_" + dt.ToString("yyyy");
                                }
                                string drive = Path.GetPathRoot(Setting.PollingFolderName);
                                if (Directory.Exists(drive))
                                {
                                    bool exist = Directory.Exists(Setting.PollingFolderName);
                                    if (!exist)
                                    {
                                        Directory.CreateDirectory(Setting.PollingFolderName);
                                    }
                                }
                                else
                                {
                                    return Json(new
                                    {
                                        msg = "Invalid List File Path",
                                        result = flag
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                //Plain List File
                                if (Setting.AllowPlainList && Lic.LDAT)
                                {

                                    ListGeneration.ListDat(RawData, Setting.PollingFolderName + @"\" + PlainFileName + ".dat");
                                }

                                //Encrypted List File
                                ListGeneration.ListDatEncrypted(RawData, Setting.PollingFolderName + @"\" + EncFileName + ".dat");


                                //Hidden List File
                                ListGeneration.ListDatEncryptedHidden(RawData, Setting.HiddenFilePath + @"\" + EncFileName + ".dat");


                                #endregion

                            }
                        }

                        #region TIS Integeration

                        if (IsTIS)
                        {

                            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                            {
                                TISIntigeration TIS = new TISIntigeration(Setting);

                                TIS.InsertData(log, Reader_ID);
                            }
                        }

                        #endregion

                        #region SQL Integeration

                        if (IsSQL)
                        {


                            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                            {

                                SQLIntigeration Sql = new SQLIntigeration(Setting);
                                Sql.InsertData(log, Reader_ID, Device_Name, Setting);
                            }
                        }


                        #endregion

                        #region Oracle Integeration

                        if (IsOrcl)
                        {

                            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                            {
                                OracleIntigeration Oracle = new OracleIntigeration();
                                Oracle.InsertData(log, Reader_ID, Device_Name, Setting);
                            }
                        }

                        #endregion

                        #region MySql Integration

                        if (IsMySql)
                        {
                            if (log.UserID != "0" && log.UserName != "Status Monitoring Log")
                            {

                                MySQLIntegration Sql = new MySQLIntegration(Setting);
                                Sql.InsertData(log, Reader_ID, Device_Name, Setting);
                            }

                        }

                        #endregion

                        //if (!string.IsNullOrEmpty(attendance.Attendance_Photo))
                        //{
                        //    attendance.Attendance_Photo = "<input type='hidden' id='hdnimg_" + attendance.Code + "' value='" + attendance.Attendance_Photo + "'/><i class='feather icon-image' style='font-size:20px;cursor:pointer;' onclick='showImg(" + attendance.Code + ");'></i>";
                        //}

                        // attendance.Status = attendance.Status;


                    }
                    flag = true;
                }
                else
                {
                    msg = "Please Select any option to Proceed";
                    flag = false;
                }
                //foreach (var item in attDb)
                //{
                //    if (item.Employee == "Unregistered")
                //    {
                //        item.Attendance_Photo = "";
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());

            }
            return Json(new
            {
                msg = msg,
                result = flag
            }, JsonRequestBehavior.AllowGet);
        }
    }
}