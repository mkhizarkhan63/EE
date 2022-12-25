using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web.Mvc;
using System.Web.Hosting;
using EagleEye.Models;
using Common;
using EagleEye.Controllers;
using EagleEye.Common;

namespace HMS.Controllers
{
    public class ListController : masterController
    {

        List<ListFileModel> filelist = new List<ListFileModel>();
        // GET: List
        [CheckAuthorization]
        public ActionResult Index()
        {
            if (!CheckRights("Download List"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(GetFiles());
        }

        public List<ListFileModel> GetFiles()
        {
            try
            {
                string[] ListFiles = System.IO.Directory.GetFiles(Setting.PollingFolderName);

                foreach (var files in ListFiles)
                {
                    ListFileModel file = new ListFileModel();
                    var fileInfo = new FileInfo(files);
                    if (fileInfo.Extension == ".dat" || fileInfo.Extension == ".txt")
                    {
                        file.filepath = fileInfo.FullName;
                        file.filename = fileInfo.Name;
                        file.size = Convert.ToString((fileInfo.Length / 1000 + " KB"));
                        file.lastmodified = fileInfo.LastWriteTime.ToString("dd-MM-yyyy HH:mm:ss");
                        filelist.Add(file);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return filelist;
        }


        [HttpPost]
        public JsonResult DownloadFile(string filePath, string filename)
        {
            string msg = "";
            string[] name = filename.Split('.');
            string fullname = name[0] + ".txt";

            try
            {
                string dest = Server.MapPath("~");
                string destpath = System.IO.Path.Combine(dest, fullname);
                MoveFile(filePath, destpath);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return Json(new
            {
                result = fullname,
                result2 = filename,
                msg,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string filename)
        {
            bool flag = false;
            string msg = "";
            try
            {
                string dest = Server.MapPath("~");
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
    }
}