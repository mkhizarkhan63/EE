
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
using EagleEye.Models;
using Common;

namespace EagleEye.Controllers
{

    public class masterController : Controller
    {
        public AppSettingModel Setting = new AppSettingModel();

        public masterController()
        {

            try
            {
                Enumeration.WeigandProtocol = Weigand();
                Setting = (AppSettingModel)System.Web.HttpContext.Current.Session["Setting"];
            }
            catch (Exception)
            {

            }


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

        public List<Wiegand> Weigand()
        {

            List<Wiegand> List = new List<Wiegand>();
            try
            {
                Wiegand w0 = new Wiegand { Code = "26", Name = "26bits" };
                Wiegand w1 = new Wiegand { Code = "34", Name = "34bits" };

                List.Add(w0);
                List.Add(w1);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.Controller, GetCurrentMethod());
            }
            return List;
        }
    }
}