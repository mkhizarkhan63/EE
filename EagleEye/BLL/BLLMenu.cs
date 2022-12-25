using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL;

namespace EagleEye.BLL
{
    public class BLLMenu
    {
        DALMenu objDAL = new DALMenu();
        public List<Menu_P> GetAllMenu()
        {
            List<Menu_P> menu = new List<Menu_P>();
            try
            {
                menu = objDAL.GetAllMenu();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return menu;
        }

        public List<MenuRights_P> GetMenuRights(int userId)
        {
            List<MenuRights_P> menu = new List<MenuRights_P>();
            try
            {
                menu = objDAL.GetMenuRights(userId);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return menu;
        }

        public List<MenuGen> GetMenuGen(int userId)
        {
            List<MenuGen> menu = new List<MenuGen>();
            try
            {
                menu = objDAL.GetMenuGen(userId);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return menu;
        }
        public bool AddUpdateMenuRights(List<MenuRights_P> menurights, int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateMenuRights(menurights, Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteMenus(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteMenus(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
    }
}