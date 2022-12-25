using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;

namespace EagleEye.BLL
{
    public class BLLCommunication
    {
        DALCommunication objDAL = new DALCommunication();

        public Communication_P GetData()
        {

            Communication_P data = new Communication_P();
            try
            {
                data = objDAL.GetData();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return data;

        }

        public bool SaveData(Communication_P data)
        {
            bool flag = false;
            try
            {
                flag = objDAL.SaveData(data);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
    }
}