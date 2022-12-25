using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using EagleEye.Models;
using System.Data.Entity.Validation;

namespace EagleEye.DAL
{
    public class DALTempTable
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public bool InsertCommand(string deviceID, string cmd, string empID)
        {
            bool flag = false;
            try
            {
                tbl_temptable t = objModel.tbl_temptable.Where(x => x.Device_ID == deviceID && x.Cmd == cmd && x.Employee_ID == empID).FirstOrDefault();

                if (t == null)
                    t = new tbl_temptable();

                t.DateTime = DateTime.Now;
                t.Device_ID = deviceID;
                t.Employee_ID = empID;
                t.Cmd = cmd;

                if (t.Code == 0)
                {
                    objModel.tbl_temptable.Add(t);
                }
                else
                {
                    objModel.Entry(t).State = System.Data.Entity.EntityState.Modified;
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

    }
}