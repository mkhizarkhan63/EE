using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;

namespace EagleEye.BLL
{
    public class BLLDevice
    {
        DALDevice objDAL = new DALDevice();

        public List<Device_P> GetAllDevices()
        {

            List<Device_P> list = new List<Device_P>();
            try
            {
                list = objDAL.GetAllDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        public Device_P GetDeviceByCode(int Code)
        {
            Device_P device = new Device_P();
            try
            {
                device = objDAL.GetDeviceByCode(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return device;
        }

        public Device_P GetDeviceByDevice_ID(string Device_ID)
        {
            Device_P device = new Device_P();
            try
            {
                device = objDAL.GetDeviceByDevice_ID(Device_ID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return device;
        }

        public bool UpdateMachineInfo(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.UpdateMachineInfo(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool UpdateDeviceInfo(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.UpdateDeviceInfo(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool UpdateDeviceStatus(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.UpdateDeviceStatus(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool AddDevice(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddDevice(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteDevice(int Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteDevice(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool DeleteDeviceFK(string Code)
        {
            bool flag = false;
            try
            {
                flag = objDAL.DeleteDeviceFK(Code);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }
        public bool CheckDeviceExistInDB(string DeviceID)
        {
            bool flag = false;
            try
            {
                flag = objDAL.CheckDeviceExistInDB(DeviceID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public int GetDeviceCountByStatus(int Status)
        {
            int status = 0;
            try
            {
                status = objDAL.GetDeviceCountByStatus(Status);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return status;
        }

        public bool UpdateDeviceName(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.UpdateDeviceName(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public bool UpdateServerSetting(Device_P device)
        {
            bool flag = false;
            try
            {
                flag = objDAL.UpdateServerSetting(device);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }



        public List<Device_P> GetAllConnectedDevices()
        {

            List<Device_P> list = new List<Device_P>();
            try
            {
                list = objDAL.GetAllConnectedDevices();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return list;

        }

        #region Not in USE


        public Device_P GetDeviceStatus(string devID)
        {
            Device_P device = new Device_P();
            try
            {
                device = objDAL.GetDeviceStatus(devID);
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }

            return device;
        }

        #endregion


        public string GetDeviceName(string deviceID)
        {
            string deviceName = "";
                try
                {
                deviceName = objDAL.GetDeviceName(deviceID);
                }
                catch (Exception ex)
                {
                    LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
                }
                return deviceName;
            }
    }
}