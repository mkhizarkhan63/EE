using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.DAL.Partial;
using System.Data.Entity.Validation;
using EagleEye.Models;

namespace EagleEye.DAL
{
    public class DALMenu
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<Menu_P> GetAllMenu()
        {
            List<Menu_P> menu = new List<Menu_P>();
            try
            {
                menu = (from d in objModel.tbl_menu
                        where d.Parent != "0" && d.IsActive != false
                        select new Menu_P
                        {
                            Menu_Id = d.Menu_Id,
                            Menu_Name = d.Menu_Name,
                            Menu_Action = d.Menu_Action,
                            Menu_Controller = d.Menu_Controller,
                            Icon = d.Icon,
                            isActive = d.IsActive
                        }).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return menu;
        }

        public List<MenuRights_P> GetMenuRights(int userId)
        {
            List<MenuRights_P> menuRights = new List<MenuRights_P>();
            try
            {
                menuRights = (from m in objModel.tbl_menu
                              join mr in objModel.tbl_menurights on m.Menu_Id equals mr.Menu_Id into rights
                              from mr in rights.DefaultIfEmpty()
                              where mr.User_Id == userId
                              select new MenuRights_P
                              {
                                  Menu_Id = mr.Menu_Id,
                                  Menu_Rights_Id = mr.Menu_Rights_Id,
                                  Insert = mr.Insert,
                                  Update = mr.Update,
                                  Delete = mr.Delete,
                                  View = mr.View,
                                  User_Id = mr.User_Id,
                                  IsActive = mr.IsActive
                              }).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return menuRights;
        }

        public List<MenuGen> GetMenuGen(int userId)
        {
            List<MenuGen> menuRights = new List<MenuGen>();
            try
            {
                menuRights = (from m in objModel.tbl_menu
                              join mr in objModel.tbl_menurights on m.Menu_Id equals mr.Menu_Id into rights
                              from mr in rights.DefaultIfEmpty()
                              where mr.User_Id == userId && m.IsActive == true
                              select new MenuGen
                              {
                                  Menu_Id = m.Menu_Id,
                                  Menu_Name = m.Menu_Name,
                                  Menu_Action = m.Menu_Action,
                                  Menu_Controller = m.Menu_Controller,
                                  Icon = m.Icon,
                                  Insert = mr.Insert,
                                  Update = mr.Update,
                                  Delete = mr.Delete,
                                  View = mr.View
                              }).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }

            return menuRights;
        }

        public bool AddUpdateMenuRights(List<MenuRights_P> menu, int Code)
        {
            bool flag = false;
            try
            {
                foreach (var mr in menu)
                {
                    tbl_menurights menuRights = objModel.tbl_menurights.Where(x => x.Menu_Id == mr.Menu_Id && x.User_Id == Code).FirstOrDefault();
                    if (menuRights == null)
                        menuRights = new tbl_menurights();

                    menuRights.Menu_Id = mr.Menu_Id;
                    menuRights.IsActive = true;
                    menuRights.Insert = mr.Insert;
                    menuRights.Update = mr.Update;
                    if (mr.Insert == true || mr.Update == true || mr.Delete == true && mr.View == false)
                    {
                        menuRights.View = true;
                    }
                    else
                    {
                        menuRights.View = mr.View;
                    }
                    menuRights.Delete = mr.Delete;
                    menuRights.User_Id = Code;

                    if (menuRights.Menu_Rights_Id == 0)
                    {
                        objModel.tbl_menurights.Add(menuRights);
                    }
                    else
                    {
                        objModel.Entry(menuRights).State = System.Data.Entity.EntityState.Modified;
                    }
                    int res = objModel.SaveChanges();

                    if (res > 0)
                        flag = true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteMenus(int Code)
        {
            bool flag = false;
            try
            {
                List<tbl_menurights> mr = objModel.tbl_menurights.Where(x => x.User_Id == Code).ToList();
                foreach (var menu in mr)
                {
                    objModel.tbl_menurights.Remove(menu);
                }
                objModel.SaveChanges();
                flag = true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogValidationException(ve.ErrorMessage, ExceptionLayer.DAL, GetCurrentMethod());
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return flag;
        }
    }
}