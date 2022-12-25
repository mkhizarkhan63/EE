using EagleEye.DAL;
using EagleEye.DAL.Partial;
using System;
using static EagleEye.Common.Enumeration;
using static EagleEye.Common.ExceptionLogger;
using static Common.HelpingMethod;

namespace EagleEye.BLL
{
    public class BLLSetting
    {
        DALSetting objDAL = new DALSetting();

        public Setting_P GetSetting()
        {

            Setting_P setting = new Setting_P();
            try
            {
                setting = objDAL.GetSetting();

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return setting;
        }

        public bool AddUpdateSetting(Setting_P setting)
        {
            bool flag = false;
            try
            {
                flag = objDAL.AddUpdateSetting(setting);

            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return flag;
        }

        public Setting_P GetServiceName()
        {
            Setting_P serviceName = new Setting_P();
            try
            {
                serviceName = objDAL.GetServiceName();
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionLayer.BLL, GetCurrentMethod());
            }
            return serviceName;

        }

    }
}