using EagleEye.DAL.Partial;
using EagleEye.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;
using System.Data.Entity.Validation;

namespace EagleEye.DAL
{
    public class DALCommunication
    {
        EagleEyeEntities objModel = new EagleEyeEntities();
        public Communication_P GetData()
        {
            Communication_P data = new Communication_P();
            try
            {
                data = (from d in objModel.tbl_communication
                        select new Communication_P
                        {
                            Code = d.Code,
                            Server_IP = d.Server_IP,
                            SignalR_Port = d.SignalR_Port,
                            Server_Port = d.Server_Port
                        }).FirstOrDefault();

                if (data == null)
                {
                    data = new Communication_P
                    {
                        Server_IP = "",
                        SignalR_Port = ""
                    };
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

            return data;
        }

        public bool SaveData(Communication_P data)
        {

            bool flag = false;
            try
            {
                tbl_communication c = new tbl_communication();
                c = objModel.tbl_communication.Where(x => x.Code == data.Code).FirstOrDefault();



                if (c == null)
                {
                    c = new tbl_communication
                    {
                        Server_IP = data.Server_IP,
                        SignalR_Port = data.SignalR_Port,
                        Server_Port = data.Server_Port
                    };

                    objModel.tbl_communication.Add(c);
                    int res = objModel.SaveChanges();

                    if (res > 0)
                        flag = true;
                }
                else
                {
                    c.Server_IP = data.Server_IP;
                    c.SignalR_Port = data.SignalR_Port;
                    c.Server_Port = data.Server_Port;
                    int res = objModel.SaveChanges();
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
    }
}