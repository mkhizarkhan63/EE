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
    public class BLLAwaitingDevice
    {
        DALAwaitingDevice objDAL = new DALAwaitingDevice();

        public List<AwaitingDevice_P> GetAllAwaitingDevices()
        {

            List<AwaitingDevice_P> list = new List<AwaitingDevice_P>();
            try
            {
                list = objDAL.GetAllAwaitingDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public AwaitingDevice_P GetAwaitingDeviceByCode(int Code)
        {
            AwaitingDevice_P device = new AwaitingDevice_P();
            try
            {
                device = objDAL.GetAwaitingDeviceByCode(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return device;
        }
        public AwaitingDevice_P GetAwaitingDeviceByDeviceID(string Code)
        {
            AwaitingDevice_P device = new AwaitingDevice_P();
            try
            {
                device = objDAL.GetAwaitingDeviceByDeviceID(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return device;
        }
        //public AwaitingDevice_P GetFKDevices(string Code)
        //{
        //    AwaitingDevice_P device = new AwaitingDevice_P();
        //    try
        //    {
        //        device = objDAL.GetFKDevices(Code);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
        //    }

        //    return device;
        //}
        
        public bool DeleteAwaitingDevice(string Device_ID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteAwaitingDevice(Device_ID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool DeleteAwaitingDeviceC(string Device_ID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteAwaitingDeviceC(Device_ID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
    }
}