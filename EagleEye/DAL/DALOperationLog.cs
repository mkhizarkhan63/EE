using System;
using System.Collections.Generic;
using System.Linq;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Web;
using EagleEye.Models;
using EagleEye.DAL.Partial;
using System.Data.Entity.Validation;
using Common;
using EagleEye.Common;

namespace EagleEye.DAL
{
    public class DALOperationLog
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public List<OperationLog_P> GetAllLogs(JqueryDatatableParam param, string dt1, string dt2, string status, string devices, out int totalRecords)
        {
            List<OperationLog_P> list = new List<OperationLog_P>();
            totalRecords = 0;
            try
            {
                DateTime StartDate = Formatter.SetValidValueToDateTime(dt1 + " 00:00:00");
                DateTime EndDate = Formatter.SetValidValueToDateTime(dt2 + " 23:59:59");
                objModel.Database.CommandTimeout = 86400;
                IEnumerable<OperationLog_P> query = (from d in objModel.tbl_operationlog
                                                     where d.UpdateTime >= StartDate && d.UpdateTime <= EndDate
                                                     select new OperationLog_P
                                                     {
                                                         Code = d.Code,
                                                         Trans_ID = d.Trans_ID,
                                                         Device_ID = d.Device_ID,
                                                         Device_Name = d.Device_Name,
                                                         Action = d.Action,
                                                         Message = d.Message,
                                                         Status = d.Status,
                                                         UserName = d.UserName,
                                                         Device_Status = d.Device_Status,
                                                         DateTime = d.UpdateTime.ToString(),
                                                         UpdateTime = d.UpdateTime
                                                         // orderByDateTime = d.UpdateTime,

                                                     });
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    query = query.Where(x =>
                    x.Trans_ID.Contains(param.sSearch)
                    || x.Action.Contains(param.sSearch)
                    || x.DateTime.Contains(param.sSearch)

                    );

                }
                list = query.ToList().OrderByDescending(x => x.UpdateTime).ToList();
                totalRecords = list.Count();
                //skiping
                if (param.iDisplayLength != -1)
                {

                    if (totalRecords > param.iDisplayStart)
                        list = list.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength).ToList();
                }

                if (status != "-1")
                    list = list.Where(x => x.Status == status).ToList();

                if (devices != "0")
                    list = list.Where(x => x.Device_ID == devices).ToList();

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
            return list;

        }
        public bool AddUpdateOperationLog(OperationLog_P olog)
        {
            bool flag = false;
            try
            {
                tbl_operationlog e = objModel.tbl_operationlog.Where(x => x.Trans_ID == olog.Trans_ID).FirstOrDefault();

                if (e == null)
                    e = new tbl_operationlog();

                e.Trans_ID = olog.Trans_ID;
                e.Device_ID = olog.Device_ID;
                e.Device_Name = olog.Device_Name;
                e.Action = olog.Action;
                e.Message = olog.Message;
                e.Status = "0";
                e.UserName = olog.UserName;
                e.Device_Status = olog.Device_Status;
                e.UpdateTime = olog.UpdateTime;

                if (e.Code == 0)
                {
                    objModel.tbl_operationlog.Add(e);
                }
                else
                {
                    objModel.Entry(e).State = System.Data.Entity.EntityState.Modified;
                }
                int res = objModel.SaveChanges();


                if (res > 0)
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

        public string GetStatus(string status)
        {
            string statusname = "";
            try
            {
                switch (status)
                {
                    case "0":
                        statusname = "Pending";
                        break;
                    case "1":
                        statusname = "Success";
                        break;
                    case "2":
                        statusname = "Failed";
                        break;
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.DAL, GetCurrentMethod());
            }
            return statusname;
        }

        public bool DeleteLogs(int Code)
        {
            bool flag = false;
            try
            {
                tbl_operationlog e = objModel.tbl_operationlog.Where(x => x.Code == Code).FirstOrDefault();
                objModel.tbl_operationlog.Remove(e);



                Common.EagleEyeManagement objM = new Common.EagleEyeManagement();
                objM.DeleteCommands(e.Trans_ID, e.Device_ID);

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