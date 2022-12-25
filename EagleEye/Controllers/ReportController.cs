using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web.Mvc;
using EagleEye.DAL.Partial;
using Common;
using EagleEye.BLL;
using Microsoft.AspNet.SignalR;
using EagleEye.Hubs;
using EagleEye.Common;
using System.Data;
using System.Text;
using System.Threading;
using OfficeOpenXml;
using System.IO;
using EagleEye_Service;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace EagleEye.Controllers
{
    public class ReportController : Controller
    {
        BLLEmployee objEmpBLL = new BLLEmployee();
        BLLAtt_Status objAttBLL = new BLLAtt_Status();
        List<Employee_P> employee = new List<Employee_P>();
        BLLWorkHour objWorkHour = new BLLWorkHour();
        BLLAttendance objatt = new BLLAttendance();
        // GET: Report

        [CheckAuthorization]
        public ActionResult Index()
        {
            try
            {
                if (!CheckRights("Daily Attendence Report"))
                {
                    return RedirectToAction("Index", "Home");
                }

                BLLDevice objDev = new BLLDevice();
                ViewBag.Devices = objDev.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return View();
        }

        [CheckAuthorization]
        public ActionResult Individual(string emp, DateTime dt)
        {
            Report_P rpt = new Report_P();
            try
            {
                rpt = IndividualReport(emp, dt);
                rpt.Att_Status_List = objAttBLL.GetAllReportStatus();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return View(rpt);

        }

        [CheckAuthorization]
        public ActionResult Single()
        {
            Report_P rpt = new Report_P();
            try
            {
                if (!CheckRights("Individual Report"))
                {
                    return RedirectToAction("Index", "Home");
                }
                rpt.Employee_List = objEmpBLL.GetAllEmployees();
                rpt.Att_Status_List = objAttBLL.GetAllReportStatus();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }

            return View(rpt);
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
        public JsonResult GetCurrentTime()
        {
            string dt = "";
            string dt2 = "";
            try
            {
                dt = DateTime.Now.ToString("yyyy-MM-dd");
                dt2 = DateTime.Now.ToString("dd-MMMM-yyyy");
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = dt,
                dt2
            }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetTotalReport(JqueryDatatableParam param, DateTime dt, string devices)
        {

            if (dt == Convert.ToDateTime("2000-01-01"))
            {
                dt = DateTime.Now;
            }
            int TotalRecords = 0;
            int FilteredRecords = 0;
            List<Report_P> rptList = new List<Report_P>();

            try
            {
                string[] DevList = null;
                if (!string.IsNullOrEmpty(devices))
                {
                    DevList = devices.Split(',');
                }

                employee = objEmpBLL.GetEmployeesWParam(param, devices, out TotalRecords, out FilteredRecords);
                int empcount = employee.Count();
                foreach (var emp in employee)
                {
                    Report_P rpt = new Report_P
                    {
                        Employee_ID = emp.Employee_ID,
                        Employee_Name = emp.Employee_Name
                    };
                    if (emp.Employee_Photo != "")
                        rpt.Employee_Photo = emp.Employee_Photo;
                    else
                        rpt.Employee_Photo = "/assets/img/avatars/no-image.jpg";

                    rpt.Department = emp.Department;
                    rpt.Location = emp.Location;
                    rpt.Designation = emp.Designation;
                    rpt.EmployeeType = emp.EmployeeType;
                    //****************Getting Policy list for an employee*******************
                    //var policyDetailList = objWorkHour.GetPolicyEmployeeByDate(emp, dt.ToString("dd-MMM-yyyy"));
                    //foreach (var item in policyDetailList)
                    //{
                    //    rpt.rpt_ActualWorkHour = item.ActualWorkHour;
                    //    rpt.rpt_OverTime = item.OverTime;
                    //    rpt.rpt_BreakHour = item.BreakHour;
                    //}
                    rpt.attendencelist = objatt.GetAttendanceByUSERandDATE(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"), DevList);
                    rpt.att_count = rpt.attendencelist.Count();
                    if (rpt.att_count != 0)
                    {
                        rpt.attendance = objatt.GetAttendanceByMINDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                        rpt.Device = rpt.attendance.Device;
                        //rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                        rpt.TimeIn = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");
                        rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                        rpt.attendance = objatt.GetAttendanceByMAXDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));

                        rpt.TimeOut = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                        if (rpt.TimeIn != "00:00:00" && rpt.TimeOut != "00:00:00")
                        {
                            TimeSpan timeS = Formatter.SetValidValueToDateTime(rpt.TimeOut) - Formatter.SetValidValueToDateTime(rpt.TimeIn);
                            int hour = timeS.Hours;
                            int minutes = timeS.Minutes;
                            int seconds = timeS.Seconds;

                            rpt.dt_workedhours = hour.ToString("00") + ":"
                                + minutes.ToString("00") + ":"
                                + seconds.ToString("00");
                        }
                        if (rpt.dt_workedhours == null)
                            rpt.dt_workedhours = "--:--:--";
                        rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");

                        // rpt.selected_datetime = dt.ToString("yyyy-MM-dd");

                    }
                    else
                    {
                        rpt.TimeIn = "--:--:--";
                        rpt.TimeOut = "--:--:--";
                        rpt.dt_workedhours = "--:--:--";
                    }
                    rptList.Add(rpt);
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
                aaData = rptList
            }, JsonRequestBehavior.AllowGet);
        }

        #region Individual Report
        public Report_P IndividualReport(string emp, DateTime dt)
        {
            Report_P rpt = new Report_P();
            Employee_P employ = new Employee_P();
            try
            {
                employ = objEmpBLL.GetEmployeeByID(emp);
                rpt.Employee_ID = employ.Employee_ID;
                rpt.Employee_Name = employ.Employee_Name;
                rpt.Employee_Photo = employ.Employee_Photo;
                rpt.attendencelist = objatt.GetAttendanceByUSERandDATE(emp, dt.ToString("dd-MMM-yyyy"));
                rpt.att_count = rpt.attendencelist.Count();
                rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return rpt;
        }

        [HttpPost]
        public JsonResult GetReport(string emp, DateTime dt)
        {
            int row_1 = 0, row_2 = 0, row_3 = 0, row_4 = 0, row_5 = 0, row_6 = 0, row_7 = 0, row_8 = 0, row_9 = 0, row_10 = 0, row_0 = 0;
            Report_P rpt = new Report_P();
            string HTMLText = "";
            try
            {
                rpt.attendencelist = objatt.GetAttendanceByUSERandDATE(emp, dt.ToString("dd-MMM-yyyy"));
                DataTable dtTable = new DataTable();

                dtTable.Columns.Add("0");
                dtTable.Columns.Add("1");
                dtTable.Columns.Add("2");
                dtTable.Columns.Add("3");
                dtTable.Columns.Add("4");
                dtTable.Columns.Add("5");
                dtTable.Columns.Add("6");
                dtTable.Columns.Add("7");
                dtTable.Columns.Add("8");
                dtTable.Columns.Add("9");
                dtTable.Columns.Add("10");
                //int row = 0;
                DataRow dr = dtTable.NewRow();
                foreach (var i in dtTable.Columns)
                {
                    dr["" + i + ""] = "";
                }
                dtTable.Rows.Add(dr);
                foreach (var s1 in rpt.attendencelist)
                {
                    switch (s1.Status)
                    {
                        case "1":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_1]["1"].ToString()))
                            {
                                dtTable.Rows[row_1]["1"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_1++;
                            break;
                        case "2":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_2]["2"].ToString()))
                            {
                                dtTable.Rows[row_2]["2"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_2++;
                            break;
                        case "3":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_3]["3"].ToString()))
                            {
                                dtTable.Rows[row_3]["3"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_3++;
                            break;
                        case "4":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_4]["4"].ToString()))
                            {
                                dtTable.Rows[row_4]["4"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_4++;
                            break;
                        case "5":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_5]["5"].ToString()))
                            {
                                dtTable.Rows[row_5]["5"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_5++;
                            break;
                        case "6":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_6]["6"].ToString()))
                            {
                                dtTable.Rows[row_6]["6"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_6++;
                            break;
                        case "7":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_7]["7"].ToString()))
                            {
                                dtTable.Rows[row_7]["7"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_7++;
                            break;
                        case "8":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_8]["8"].ToString()))
                            {
                                dtTable.Rows[row_8]["8"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_8++;
                            break;
                        case "9":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_9]["9"].ToString()))
                            {
                                dtTable.Rows[row_9]["9"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_9++;
                            break;
                        case "10":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_10]["10"].ToString()))
                            {
                                dtTable.Rows[row_10]["10"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_10++;
                            break;
                        case "0":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_0]["0"].ToString()))
                            {
                                dtTable.Rows[row_0]["0"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_0++;
                            break;
                    }

                }
                DataTable dt2 = new DataTable();
                var filtered = dtTable.AsEnumerable().Where(x =>
                !(string.IsNullOrEmpty(x.Field<string>("0")))
                || !(string.IsNullOrEmpty(x.Field<string>("1")))
                || !(string.IsNullOrEmpty(x.Field<string>("2")))
                || !(string.IsNullOrEmpty(x.Field<string>("3")))
                || !(string.IsNullOrEmpty(x.Field<string>("4")))
                || !(string.IsNullOrEmpty(x.Field<string>("5")))
                || !(string.IsNullOrEmpty(x.Field<string>("6")))
                || !(string.IsNullOrEmpty(x.Field<string>("7")))
                || !(string.IsNullOrEmpty(x.Field<string>("8")))
                || !(string.IsNullOrEmpty(x.Field<string>("9")))
                || !(string.IsNullOrEmpty(x.Field<string>("10"))));
                if (filtered.Any())
                {
                    dt2 = filtered.ToList().CopyToDataTable();
                }

                StringBuilder strHtml = new StringBuilder();
                foreach (DataRow myR in dt2.Rows)
                {
                    strHtml.Append("<tr>");

                    foreach (DataColumn dtC in dt2.Columns)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(myR[dtC.ColumnName].ToString());
                        strHtml.Append("</td>");
                    }
                    strHtml.Append("</tr>");
                }
                HTMLText = strHtml.ToString();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = HTMLText

            }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region SingleReport
        public Report_P SingleReportuserInfo(string emp, DateTime dt)
        {
            Report_P rpt = new Report_P();
            Employee_P employ = new Employee_P();
            try
            {
                employ = objEmpBLL.GetEmployeeByID(emp);
                rpt.Employee_ID = employ.Employee_ID;
                rpt.Employee_Name = employ.Employee_Name;
                rpt.Employee_Photo = employ.Employee_Photo;
                rpt.attendencelist = objatt.GetAttendanceByUSERandDATE(emp, dt.ToString("dd-MMM-yyyy"));
                rpt.att_count = rpt.attendencelist.Count();
                if (rpt.att_count != 0)
                {
                    rpt.attendance = objatt.GetAttendanceByMINDate(rpt.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                    rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                    rpt.TimeIn = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                    rpt.attendance = objatt.GetAttendanceByMAXDate(rpt.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                    rpt.TimeOutDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                    rpt.TimeOut = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                    if (rpt.TimeIn != "00:00:00" && rpt.TimeOut != "00:00:00")
                    {
                        TimeSpan timeS = Formatter.SetValidValueToDateTime(rpt.TimeOut) - Formatter.SetValidValueToDateTime(rpt.TimeIn);
                        int hour = timeS.Hours;
                        int minutes = timeS.Minutes;
                        int seconds = timeS.Seconds;

                        rpt.dt_workedhours = hour.ToString("00") + ":"
                                            + minutes.ToString("00") + ":"
                                            + seconds.ToString("00");
                    }
                    if (rpt.dt_workedhours == null)
                        rpt.dt_workedhours = "--:--:--";
                }
                rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return rpt;
        }

        [HttpPost]
        public JsonResult GetSingleReport(string emp, DateTime dt)
        {
            int row_1 = 0, row_2 = 0, row_3 = 0, row_4 = 0, row_5 = 0, row_6 = 0, row_7 = 0, row_8 = 0, row_9 = 0, row_10 = 0, row_0 = 0;
            Report_P rpt = new Report_P();
            string HTMLText = "";
            try
            {
                rpt = SingleReportuserInfo(emp, dt);
                // rpt.attendencelist = objatt.GetAttendanceByUSERandDATE(emp, dt.ToString("dd-MMM-yyyy"));

                DataTable dtTable = new DataTable();

                dtTable.Columns.Add("0");
                dtTable.Columns.Add("1");
                dtTable.Columns.Add("2");
                dtTable.Columns.Add("3");
                dtTable.Columns.Add("4");
                dtTable.Columns.Add("5");
                dtTable.Columns.Add("6");
                dtTable.Columns.Add("7");
                dtTable.Columns.Add("8");
                dtTable.Columns.Add("9");
                dtTable.Columns.Add("10");
                //int row = 0;
                DataRow dr = dtTable.NewRow();
                foreach (var i in dtTable.Columns)
                {
                    dr["" + i + ""] = "";
                }
                dtTable.Rows.Add(dr);
                foreach (var s1 in rpt.attendencelist)
                {
                    switch (s1.Status)
                    {
                        case "1":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_1]["1"].ToString()))
                            {
                                dtTable.Rows[row_1]["1"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_1++;
                            break;
                        case "2":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_2]["2"].ToString()))
                            {
                                dtTable.Rows[row_2]["2"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_2++;
                            break;
                        case "3":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_3]["3"].ToString()))
                            {
                                dtTable.Rows[row_3]["3"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_3++;
                            break;
                        case "4":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_4]["4"].ToString()))
                            {
                                dtTable.Rows[row_4]["4"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_4++;
                            break;
                        case "5":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_5]["5"].ToString()))
                            {
                                dtTable.Rows[row_5]["5"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_5++;
                            break;
                        case "6":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_6]["6"].ToString()))
                            {
                                dtTable.Rows[row_6]["6"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_6++;
                            break;
                        case "7":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_7]["7"].ToString()))
                            {
                                dtTable.Rows[row_7]["7"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_7++;
                            break;
                        case "8":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_8]["8"].ToString()))
                            {
                                dtTable.Rows[row_8]["8"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_8++;
                            break;
                        case "9":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_9]["9"].ToString()))
                            {
                                dtTable.Rows[row_9]["9"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_9++;
                            break;
                        case "10":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_10]["10"].ToString()))
                            {
                                dtTable.Rows[row_10]["10"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_10++;
                            break;
                        case "0":
                            if (string.IsNullOrEmpty(dtTable.Rows[row_0]["0"].ToString()))
                            {
                                dtTable.Rows[row_0]["0"] = s1.DateTime.ToString();
                                dtTable.Rows.Add("", "", "", "", "", "", "", "", "", "");
                            }
                            row_0++;
                            break;
                    }

                }
                DataTable dt2 = new DataTable();
                var filtered = dtTable.AsEnumerable().Where(x => !(string.IsNullOrEmpty(x.Field<string>("0")))
                || !(string.IsNullOrEmpty(x.Field<string>("1")))
                || !(string.IsNullOrEmpty(x.Field<string>("2")))
                || !(string.IsNullOrEmpty(x.Field<string>("3")))
                || !(string.IsNullOrEmpty(x.Field<string>("4")))
                || !(string.IsNullOrEmpty(x.Field<string>("5")))
                || !(string.IsNullOrEmpty(x.Field<string>("6")))
                || !(string.IsNullOrEmpty(x.Field<string>("7")))
                || !(string.IsNullOrEmpty(x.Field<string>("8")))
                || !(string.IsNullOrEmpty(x.Field<string>("9")))
                || !(string.IsNullOrEmpty(x.Field<string>("10"))));
                if (filtered.Any())
                {
                    dt2 = filtered.ToList().CopyToDataTable();
                }

                StringBuilder strHtml = new StringBuilder();
                foreach (DataRow myR in dt2.Rows)
                {
                    strHtml.Append("<tr>");

                    foreach (DataColumn dtC in dt2.Columns)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(myR[dtC.ColumnName].ToString());
                        strHtml.Append("</td>");
                    }
                    strHtml.Append("</tr>");
                }
                HTMLText = strHtml.ToString();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = HTMLText,
                rpt
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Export to Excel
        [HttpPost]
        public JsonResult Export(DateTime dt, string[] devices, bool include)
        {
            bool flag = false;
            string filename = "";
            string path = "";
            string name = "";
            List<Report_P> rptList = new List<Report_P>();
            try
            {
                //string[] DevList = null;
                //if (!string.IsNullOrEmpty(devices))
                //{
                //    DevList = devices.Split(',');
                //}
                if (dt == Convert.ToDateTime("2000-01-01"))
                {

                    dt = DateTime.Now;
                }

                employee = objEmpBLL.GetEmployeesWithAttCount(dt.ToString("dd-MMM-yyyy")).Where(x => x.attcount > 0).ToList();
                int empcount = employee.Count();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reports");
                Sheet.Cells["A1"].Value = "Employee ID";
                Sheet.Cells["B1"].Value = "Employee Name";
                Sheet.Cells["C1"].Value = "Department";
                Sheet.Cells["D1"].Value = "Location";
                Sheet.Cells["E1"].Value = "Employee Type";
                Sheet.Cells["F1"].Value = "Designation";
                Sheet.Cells["G1"].Value = "Time In";
                Sheet.Cells["H1"].Value = "Time Out Date";
                Sheet.Cells["I1"].Value = "Time Out";
                Sheet.Cells["J1"].Value = "Device";
                Sheet.Cells["K1"].Value = "Worked Hours";
                Sheet.Cells["L1"].Value = "All Transactions";

                int row = 2;

                foreach (var emp in employee)
                {
                    Report_P rpt = new Report_P
                    {
                        Employee_ID = emp.Employee_ID,
                        Employee_Name = emp.Employee_Name,
                        Department = emp.Department,
                        Location = emp.Location,
                        Designation = emp.Designation,
                        EmployeeType = emp.EmployeeType,
                        attendencelist = objatt.GetAttendanceByUSERandDATE(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"), devices)

                    };
                    rpt.att_count = emp.attcount;
                    if (emp.attcount != 0)
                    {
                        rpt.attendance = objatt.GetAttendanceByMINDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                        rpt.Device = rpt.attendance.Device;
                        rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                        rpt.TimeIn = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                        rpt.attendance = objatt.GetAttendanceByMAXDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                        rpt.TimeOutDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                        rpt.TimeOut = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                        if (rpt.TimeIn != "00:00:00" && rpt.TimeOut != "00:00:00")
                        {
                            TimeSpan timeS = Formatter.SetValidValueToDateTime(rpt.TimeOut) - Formatter.SetValidValueToDateTime(rpt.TimeIn);
                            int hour = timeS.Hours;
                            int minutes = timeS.Minutes;
                            int seconds = timeS.Seconds;

                            rpt.dt_workedhours = hour.ToString("00") + ":"
                                + minutes.ToString("00") + ":"
                                + seconds.ToString("00");
                        }
                        if (rpt.dt_workedhours == null)
                            rpt.dt_workedhours = "--:--:--";

                        if (rpt.TimeIn == "00:00:00")
                            rpt.TimeIn = "--:--:--";

                        if (rpt.TimeOut == "00:00:00")
                            rpt.TimeOut = "--:--:--";

                        if (rpt.TimeOutDate == "01-01-0001")
                            rpt.TimeOutDate = "--/--/----";

                        rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");


                        // Sheet.Cells["H1"].Value = "Photo";

                        Sheet.Cells[string.Format("A{0}", row)].Value = rpt.Employee_ID;
                        Sheet.Cells[string.Format("B{0}", row)].Value = rpt.Employee_Name;
                        Sheet.Cells[string.Format("C{0}", row)].Value = rpt.Location;
                        Sheet.Cells[string.Format("D{0}", row)].Value = rpt.Department;
                        Sheet.Cells[string.Format("E{0}", row)].Value = rpt.EmployeeType;
                        Sheet.Cells[string.Format("F{0}", row)].Value = rpt.Designation;
                        Sheet.Cells[string.Format("G{0}", row)].Value = rpt.TimeIn;
                        Sheet.Cells[string.Format("H{0}", row)].Value = rpt.TimeOutDate;
                        Sheet.Cells[string.Format("I{0}", row)].Value = rpt.TimeOut;
                        Sheet.Cells[string.Format("J{0}", row)].Value = rpt.Device;
                        Sheet.Cells[string.Format("K{0}", row)].Value = rpt.dt_workedhours;
                        Sheet.Cells[string.Format("L{0}", row)].Value = rpt.att_count;
                        // Sheet.Cells[string.Format("H{0}", row)].Value = att.Status;
                        row++;
                    }
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                name = "Reports_" + dt.ToString("ddMMyyyy");
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

        //[HttpPost]
        //public JsonResult PDFReport(DateTime dt, bool include)
        //{
        //    bool flag = false;
        //    //string filename = "";
        //    //string path = "";
        //    //string name = "";
        //    List<Report_P> rptList = new List<Report_P>();

        //    try
        //    {
        //        if (dt == Convert.ToDateTime("2000-01-01"))
        //        {

        //            dt = DateTime.Now;
        //        }

        //        employee = objEmpBLL.GetAllEmployees();
        //        int empcount = employee.Count();


        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<table ><tr><td><h3>List Report</h3></td> <td><h5 style='text-align:right;'>Dated:  " + dt.ToString("dd-MMM-yyyy") + "</h5></td></tr></table> <br>");
        //        sb.Append("<table style='font-size:10px' border='1'>");
        //        sb.Append("<thead><tr bgcolor='black' style='color:white;'>");
        //        sb.Append("<th>Employee ID</th>");
        //        sb.Append("<th>Employee Name</th>");
        //        sb.Append("<th>Time In</th>");
        //        sb.Append("<th>Time Out Date</th>");
        //        sb.Append("<th>Time Out</th>");
        //        sb.Append("<th>Worked Hours</th>");
        //        sb.Append("<th>All Transactions</th>");
        //        sb.Append("</tr></thead>");
        //        sb.Append("<tbody>");
        //        int i = 1;

        //        foreach (var emp in employee)
        //        {
        //            Report_P rpt = new Report_P
        //            {
        //                Employee_ID = emp.Employee_ID,
        //                Employee_Name = emp.Employee_Name,
        //                attendencelist = objatt.GetAttendanceByUSERandDATE(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"))
        //            };
        //            rpt.att_count = rpt.attendencelist.Count();

        //            if (rpt.att_count != 0)
        //            {

        //                string color = "white";
        //                if (i % 2 == 0)
        //                {
        //                    color = "#f0f0f0";
        //                }

        //                rpt.attendance = objatt.GetAttendanceByMINDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
        //                rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
        //                rpt.TimeIn = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

        //                rpt.attendance = objatt.GetAttendanceByMAXDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
        //                rpt.TimeOutDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
        //                rpt.TimeOut = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

        //                if (rpt.TimeIn != "00:00:00" && rpt.TimeOut != "00:00:00")
        //                {
        //                    TimeSpan timeS = Formatter.SetValidValueToDateTime(rpt.TimeOut) - Formatter.SetValidValueToDateTime(rpt.TimeIn);
        //                    int hour = timeS.Hours;
        //                    int minutes = timeS.Minutes;
        //                    int seconds = timeS.Seconds;

        //                    rpt.dt_workedhours = hour.ToString("00") + ":"
        //                        + minutes.ToString("00") + ":"
        //                        + seconds.ToString("00");
        //                }
        //                if (rpt.dt_workedhours == null)
        //                    rpt.dt_workedhours = "--:--:--";

        //                if (rpt.TimeIn == "00:00:00")
        //                    rpt.TimeIn = "--:--:--";

        //                if (rpt.TimeOut == "00:00:00")
        //                    rpt.TimeOut = "--:--:--";

        //                if (rpt.TimeOutDate == "01-01-0001")
        //                    rpt.TimeOutDate = "--/--/----";

        //                rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");

        //                sb.Append("<tr bgcolor='" + color + "'>");
        //                sb.Append("<td>" + rpt.Employee_ID + "</td>");
        //                sb.Append("<td>" + rpt.Employee_Name + "</td>");
        //                sb.Append("<td>" + rpt.TimeIn + "</td>");
        //                sb.Append("<td>" + rpt.TimeOutDate + "</td>");
        //                sb.Append("<td>" + rpt.TimeOut + "</td>");
        //                sb.Append("<td>" + rpt.dt_workedhours + "</td>");
        //                sb.Append("<td>" + rpt.att_count + "</td>");
        //                sb.Append("</tr>");



        //                if (include)
        //                {
        //                    BLLAttendance objatt = new BLLAttendance();
        //                    //var attendancedetail = objatt.GetAttendanceByDateNID(item.Custom_ID.ToString(), item.DateIN).OrderBy(x => x.Attendance_DateTime).ToList();
        //                    if (rpt.att_count > 0)
        //                    {
        //                        sb.Append("<tr bgcolor='" + color + "'><td colspan='8'><h4>All Transactions</h4></td></tr>");
        //                        sb.Append("<tr bgcolor='black' style='color:white;'>");
        //                        sb.Append("<th>S No</th>");
        //                        sb.Append("<th colspan='2'>Date</th>");
        //                        sb.Append("<th colspan='2'>Time</th>");
        //                        sb.Append("<th colspan='2'>Status</th>");
        //                        sb.Append("</tr>");

        //                        int row = 1;
        //                        foreach (var att in rpt.attendencelist)
        //                        {
        //                            sb.Append("<tr bgcolor='" + color + "'>");
        //                            sb.Append("<td>" + row + "</td>");
        //                            sb.Append("<td colspan='2'>" + att.Attendance_DateTime?.ToString("dd-MMM-yyyy") + "</td>");
        //                            sb.Append("<td colspan='2'>" + att.Attendance_DateTime?.ToString("HH:mm:ss") + "</td>");
        //                            sb.Append("<td colspan='2'>" + att.Status_Description + "</td>");
        //                            sb.Append("</tr>");
        //                            row++;
        //                        }
        //                    }

        //                    sb.Append("<tr bgcolor='" + color + "'><td colspan='8'>    </td></tr>");
        //                }

        //                i++;
        //            }
        //        }
        //            sb.Append("</tbody>");
        //            sb.Append("</table>");
        //            StringReader sr = new StringReader(sb.ToString());


        //            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //                pdfDoc.Open();

        //                htmlparser.Parse(sr);
        //                pdfDoc.Close();

        //                byte[] bytes = memoryStream.ToArray();
        //                memoryStream.Close();
        //                System.IO.File.WriteAllBytes(Server.MapPath("~/Reports/List_Report_"+ dt.ToString("ddMMyyyy") + ".pdf"), bytes);
        //            }

        //            flag = true;


        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
        //    }
        //    return Json(new
        //    {
        //        result = flag,
        //        fullname = "List_Report_"+ dt.ToString("ddMMyyyy") + ".pdf"
        //    }, JsonRequestBehavior.AllowGet);
        //}





        [HttpPost]
        public JsonResult PDFReport(DateTime dt, bool include, string devices)
        {
            bool flag = false;

            try
            {
                if (dt == Convert.ToDateTime("2000-01-01"))
                {

                    dt = DateTime.Now;
                }


                string[] DevList = null;
                if (!string.IsNullOrEmpty(devices))
                {
                    DevList = devices.Split(',');
                    employee = objEmpBLL.GetEmployeesWithAttCount(dt.ToString("dd-MMM-yyyy")).Where(x => x.attcount > 0).Where(x => devices.Contains(x.Device_Id)).ToList();
                }
                else
                {
                    employee = objEmpBLL.GetEmployeesWithAttCount(dt.ToString("dd-MMM-yyyy")).Where(x => x.attcount > 0).ToList();
                }

                if (employee.Count() > 0)
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table><tr><td><h1 style='text-align:center;'>Daily Attendence Report</h1></td></tr><tr><td><h5 style='text-align:center;'>Dated:  " + dt.ToString("dd-MMMM-yyyy") + "</h5></td></tr></table> <br />");
                    int i = 1;
                    if (include == false)
                    {
                        sb.Append("<table style='font-size:x-small' border='1'>");
                        sb.Append("<thead><tr bgcolor='black' style='color:white;'>");
                        sb.Append("<th>Employee ID</th>");
                        sb.Append("<th>Employee Name</th>");
                        sb.Append("<th>Location</th>");
                        sb.Append("<th>Department</th>");
                        sb.Append("<th>Designation</th>");
                        sb.Append("<th>Employee Type</th>");
                        sb.Append("<th>Time In</th>");
                        sb.Append("<th>Time Out Date</th>");
                        sb.Append("<th>Time Out</th>");
                        sb.Append("<th>Device</th>");
                        sb.Append("<th>Worked Hours</th>");
                        sb.Append("<th>All Transactions</th>");
                        sb.Append("</tr></thead>");
                        sb.Append("<tbody>");
                    }

                    foreach (var emp in employee)
                    {
                        Report_P rpt = new Report_P
                        {
                            Employee_ID = emp.Employee_ID,
                            Employee_Name = emp.Employee_Name,
                            Location = emp.Location,
                            Designation = emp.Designation,
                            Department = emp.Department,
                            EmployeeType = emp.EmployeeType,
                            attendencelist = objatt.GetAttendanceByUSERandDATE(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"), DevList)
                        };
                        rpt.att_count = emp.attcount;

                        if (emp.attcount != 0)
                        {

                            string color = "white";
                            if (i % 2 == 0)
                            {
                                color = "#f0f0f0";
                            }

                            rpt.attendance = objatt.GetAttendanceByMINDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                            rpt.Device = rpt.attendance.Device;

                            rpt.TimeInDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                            rpt.TimeIn = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                            rpt.attendance = objatt.GetAttendanceByMAXDate(emp.Employee_ID, dt.ToString("dd-MMM-yyyy"));
                            rpt.TimeOutDate = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("dd-MM-yyyy");
                            rpt.TimeOut = Formatter.SetValidValueToDateTime(rpt.attendance.Attendance_DateTime).ToString("HH:mm:ss");

                            if (rpt.TimeIn != "00:00:00" && rpt.TimeOut != "00:00:00")
                            {
                                TimeSpan timeS = Formatter.SetValidValueToDateTime(rpt.TimeOut) - Formatter.SetValidValueToDateTime(rpt.TimeIn);
                                int hour = timeS.Hours;
                                int minutes = timeS.Minutes;
                                int seconds = timeS.Seconds;

                                rpt.dt_workedhours = hour.ToString("00") + ":"
                                    + minutes.ToString("00") + ":"
                                    + seconds.ToString("00");
                            }
                            if (rpt.dt_workedhours == null)
                                rpt.dt_workedhours = "--:--:--";

                            if (rpt.TimeIn == "00:00:00")
                                rpt.TimeIn = "--:--:--";

                            if (rpt.TimeOut == "00:00:00")
                                rpt.TimeOut = "--:--:--";

                            if (rpt.TimeOutDate == "01-01-0001")
                                rpt.TimeOutDate = "--/--/----";

                            rpt.curr_datetime = dt.ToString("dd-MMMM-yyyy");

                            if (include == false)
                            {

                                sb.Append("<tr bgcolor='" + color + "'>");
                                sb.Append("<td>" + rpt.Employee_ID + "</td>");
                                sb.Append("<td>" + rpt.Employee_Name + "</td>");
                                sb.Append("<td>" + rpt.Location + "</td>");
                                sb.Append("<td>" + rpt.Department + "</td>");
                                sb.Append("<td>" + rpt.Designation + "</td>");
                                sb.Append("<td>" + rpt.EmployeeType + "</td>");
                                sb.Append("<td>" + rpt.TimeIn + "</td>");
                                sb.Append("<td>" + rpt.TimeOutDate + "</td>");
                                sb.Append("<td>" + rpt.TimeOut + "</td>");
                                sb.Append("<td>" + rpt.Device + "</td>");
                                sb.Append("<td>" + rpt.dt_workedhours + "</td>");
                                sb.Append("<td>" + rpt.att_count + "</td>");
                                sb.Append("</tr>");
                            }


                            if (include)
                            {
                                sb.Append("<table style='font-size:x-small' border='1'>");
                                sb.Append("<thead><tr bgcolor='black' style='color:white;'>");
                                sb.Append("<th>Employee ID</th>");
                                sb.Append("<th>Employee Name</th>");
                                sb.Append("<th>Location</th>");
                                sb.Append("<th>Department</th>");
                                sb.Append("<th>Designation</th>");
                                sb.Append("<th>Employee Type</th>");
                                sb.Append("<th>Time In</th>");
                                sb.Append("<th>Time Out Date</th>");
                                sb.Append("<th>Time Out</th>");
                                sb.Append("<th>Worked Hours</th>");
                                sb.Append("<th>All Transactions</th>");
                                sb.Append("</tr></thead>");
                                sb.Append("<tbody>");
                                sb.Append("<tr bgcolor='" + color + "'>");
                                sb.Append("<td>" + rpt.Employee_ID + "</td>");
                                sb.Append("<td>" + rpt.Employee_Name + "</td>");
                                sb.Append("<td>" + rpt.Location + "</td>");
                                sb.Append("<td>" + rpt.Department + "</td>");
                                sb.Append("<td>" + rpt.Designation + "</td>");
                                sb.Append("<td>" + rpt.EmployeeType + "</td>");
                                sb.Append("<td>" + rpt.TimeIn + "</td>");
                                sb.Append("<td>" + rpt.TimeOutDate + "</td>");
                                sb.Append("<td>" + rpt.TimeOut + "</td>");
                                sb.Append("<td>" + rpt.dt_workedhours + "</td>");
                                sb.Append("<td>" + rpt.att_count + "</td>");
                                sb.Append("</tr>");

                                BLLAttendance objatt = new BLLAttendance();
                                //var attendancedetail = objatt.GetAttendanceByDateNID(item.Custom_ID.ToString(), item.DateIN).OrderBy(x => x.Attendance_DateTime).ToList();
                                if (rpt.att_count > 0)
                                {
                                    sb.Append("<tr bgcolor='" + color + "'><td colspan='11'><h4>All Transactions</h4></td></tr>");
                                    sb.Append("<tr bgcolor='lightgrey'>");
                                    sb.Append("<th>S No</th>");
                                    sb.Append("<th colspan='4'>Date</th>");
                                    sb.Append("<th colspan='4'>Time</th>");
                                    sb.Append("<th colspan='2'>Status</th>");
                                    sb.Append("</tr>");

                                    int row = 1;
                                    foreach (var att in rpt.attendencelist)
                                    {
                                        sb.Append("<tr bgcolor='" + color + "'>");
                                        sb.Append("<td>" + row + "</td>");
                                        sb.Append("<td colspan='4'>" + att.Attendance_DateTime?.ToString("dd-MMM-yyyy") + "</td>");
                                        sb.Append("<td colspan='4'>" + att.Attendance_DateTime?.ToString("HH:mm:ss") + "</td>");
                                        sb.Append("<td colspan='2'>" + att.Status_Description + "</td>");
                                        sb.Append("</tr>");
                                        row++;
                                    }
                                    sb.Append("</tbody></table><br/>");
                                }

                                //  sb.Append("<tr bgcolor='" + color + "'><td colspan='8'>    </td></tr>");
                            }

                            i++;
                        }
                    }
                    if (include == false)
                    {
                        sb.Append("</tbody>");
                        sb.Append("</table>");
                    }
                    StringReader sr = new StringReader(sb.ToString());


                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();

                        htmlparser.Parse(sr);
                        pdfDoc.Close();

                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        bool exist = Directory.Exists(Server.MapPath("~/Reports/"));
                        if (!exist)
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Reports/"));
                        }
                        System.IO.File.WriteAllBytes(Server.MapPath("~/Reports/Report_" + dt.ToString("ddMMyyyy") + ".pdf"), bytes);
                    }
                    flag = true;

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
                result = flag,
                fullname = "Report_" + dt.ToString("ddMMyyyy") + ".pdf"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}