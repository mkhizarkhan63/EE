using EagleEye.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.Partial;
using EagleEye.BLL;
using EagleEye.DAL.DTO;
using EagleEye.DAL;
using OfficeOpenXml;
using Microsoft.AspNet.SignalR;
using EagleEye.Hubs;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Common;

namespace EagleEye.Controllers
{
    public class PolicyReportController : masterController
    {
        #region Actions
        BLLEmployee objEmp = new BLLEmployee();
        BLLPolicyReport objBLL = new BLLPolicyReport();

        [CheckAuthorization]
        public ActionResult Index()
        {
            List<Employee_P> ls = new List<Employee_P>();

            ls = objEmp.GetAllEmployees();


            return View(ls);
        }

        #endregion



        public JsonResult GetTotalReport(JqueryDatatableParam param, string emp, string st, string et)
        {
            int totalRecords = 0;
            List<PolicyReport> ls = new List<PolicyReport>();
            BLLPolicyReport ObjBLL = new BLLPolicyReport();
            try
            {
                ls = ObjBLL.GetAllPolicyReport(param, emp, st, et, out totalRecords);
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
                aaData = ls
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExportSheet(string emp, string startDt, string endDt)
        {
            bool flag = false;
            string filename = "";
            string path = "";
            string name = "";
            List<PolicyReport> rptList = new List<PolicyReport>();
            try
            {

                rptList = objBLL.GetPolicyReportByDateRange(emp , startDt, endDt);
                int ListCount = rptList.Count();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reports");
                Sheet.Cells["A1"].Value = "Sr.";
                Sheet.Cells["B1"].Value = "Employee ID";
                Sheet.Cells["C1"].Value = "Employee Name";
                Sheet.Cells["D1"].Value = "Work Hour";
                Sheet.Cells["E1"].Value = "Overtime";
                Sheet.Cells["F1"].Value = "Breakhour";
                Sheet.Cells["G1"].Value = "Extrahour";
                Sheet.Cells["H1"].Value = "Policy Name";
                Sheet.Cells["I1"].Value = "CreatedOn";
                int sr = 1;
                int row = 2;
                foreach (var item in rptList)
                {

                    var created_Date = item.CreatedOn?.ToString("MMMM dd, yyyy HH:mm:ss");
                    Sheet.Cells[string.Format("A{0}", row)].Value = sr;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.EmpID;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.EmpName;
                    Sheet.Cells[string.Format("D{0}", row)].Value = string.IsNullOrEmpty(item.Workhour) ? "-" : item.Workhour;
                    Sheet.Cells[string.Format("E{0}", row)].Value = string.IsNullOrEmpty(item.Overtime) ? "-" : item.Overtime;
                    Sheet.Cells[string.Format("F{0}", row)].Value = string.IsNullOrEmpty(item.Breakhour) ? "-" : item.Breakhour;
                    Sheet.Cells[string.Format("G{0}", row)].Value = string.IsNullOrEmpty(item.Extrahour) ? "-" : item.Extrahour;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.Policyname;
                    Sheet.Cells[string.Format("I{0}", row)].Value = created_Date;

                    row++;
                    sr++;
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                //filesave config
                name = "PolicyReports_" + DateTime.Now.ToString("ddMMyyyy");
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


        public JsonResult PDFReport(string emp, string startDt, string endDt)
        {
            bool flag = false;
            List<PolicyReport> rptList = new List<PolicyReport>();
            var StartDate = Formatter.SetValidValueToDateTime(startDt);
            var EndDate = Formatter.SetValidValueToDateTime(endDt);

            try
            {
                rptList = objBLL.GetPolicyReportByDateRange(emp ,startDt, endDt);

                if (rptList.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table><tr><td><h1 style='text-align:center;'>Policy Report</h1></td></tr><tr><td><h5 style='text-align:center;'>Dated:  " + StartDate.ToString("dd-MMMM-yyyy") + " - " + EndDate.ToString("dd-MMMM-yyyy") + "</h5></td></tr></table> <br />");
                    int i = 1;
                    int srno = 1;
                    sb.Append("<table style='font-size:x-small' border='1'>");
                    sb.Append("<thead><tr bgcolor='black' style='color:white;'>");
                    sb.Append("<th>Sr.</th>");
                    sb.Append("<th>Employee ID</th>");
                    sb.Append("<th>Employee Name</th>");
                    sb.Append("<th>Work Hour</th>");
                    sb.Append("<th>Overtime</th>");
                    sb.Append("<th>Breakhour</th>");
                    sb.Append("<th>Extrahour</th>");
                    sb.Append("<th>Policy Name</th>");
                    sb.Append("<th>CreatedOn</th>");
                    sb.Append("</tr></thead>");
                    sb.Append("<tbody>");


                    foreach (var item in rptList)
                    {
                        //PolicyReport rpt = new PolicyReport
                        //{
                        //    EmpID = item.EmpID,
                        //    EmpName = item.EmpName,
                        //    Workhour = item.Workhour,
                        //    Overtime = item.Overtime,
                        //    Breakhour = item.Breakhour,
                        //    Extrahour = item.Extrahour,
                        //    Policyname = item.Policyname,
                        //    CreatedOn = item.CreatedOn,

                        //};




                        string color = "white";
                        if (i % 2 == 0)
                        {
                            color = "#f0f0f0";
                        }


                        sb.Append("<tr bgcolor='" + color + "'>");
                        sb.Append("<td style='width:100px !important'>" + srno + "</td>");
                        sb.Append("<td>" + item.EmpID + "</td>");
                        sb.Append("<td>" + item.EmpName + "</td>");
                        sb.Append("<td>" + item.Workhour + "</td>");
                        sb.Append("<td>" + item.Overtime + "</td>");
                        sb.Append("<td>" + item.Breakhour + "</td>");
                        sb.Append("<td>" + item.Extrahour + "</td>");
                        sb.Append("<td>" + item.Policyname + "</td>");
                        sb.Append("<td >" + item.CreatedOn + "</td>");
                        sb.Append("</tr>");
                        i++;
                        srno++;
                    }


                    sb.Append("</tbody>");
                    sb.Append("</table>");
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
                        System.IO.File.WriteAllBytes(Server.MapPath("~/Reports/Report_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf"), bytes);
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
                fullname = "Report_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string filepath)
        {
            bool flag = false;
            string msg = "";
            try
            {
                string dest = Server.MapPath("~/Reports/");
                string destpath = System.IO.Path.Combine(dest, filepath);
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
    }
}