using LncSlaMang;
using mll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class License
    {
        string Key = "Q3Ae3n75/ZFzw2Yav3Vh0igNKc2z/n4h6jacjrvvwEc=";
        SlaLncMdle lic = null;
        public bool AutoPolling, LDAT, IntLdat, IntTis, IntSql, IntOracle;
        public string IntDatabType;

        public License()
        {
            AutoPolling = Class1.IntAuPoAct().ToLower() == "true";
            LDAT = Class1.LDAT().ToLower() == "true";
            IntLdat = Class1.IntLdatAct().ToLower() == "true";
            IntTis = Class1.IntTisAct().ToLower() == "true";
            IntSql = Class1.IntSqlAct().ToLower() == "true";
            IntOracle = Class1.IntOracleType().ToLower() == "true";
            //IntDatabType = Class1.IntDatabType();
        }

        public bool CheckProductLicense(DateTime date)
        {

            bool flag = false;
            try
            {
                lic = SlaLncMdle.CreateInstance(Key, this.GetType(), false);
                string path = HttpContext.Current.Server.MapPath("~") + "bin";
                flag = lic.Authenticate(date, "20200701", path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public bool CheckProductLicense(DateTime date, string path)
        {

            bool flag = false;
            try
            {
                lic = SlaLncMdle.CreateInstance(Key, this.GetType(), true);
                flag = lic.Authenticate(date, "20200701", path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public bool CheckDeviceLicense(string MachineSNo)
        {

            bool flag = false;
            try
            {
                string path = HttpContext.Current.Server.MapPath("~") + "bin";
                lic = SlaLncMdle.CreateInstance(Key, this.GetType(), false);
                flag = lic.AuthenticateMachine(MachineSNo, path) == "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;

        }


    }
}
