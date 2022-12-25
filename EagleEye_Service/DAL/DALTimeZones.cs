using Common;
using FKWeb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.DAL
{
    public class DALTimeZones : DataAccess
    {
        SqlConnection msqlConn;
        string mDevModel = "";
        string conStr = "";
        string s = AppDomain.CurrentDomain.BaseDirectory + @"\App_Setting.txt";
        public string GetTimeZoneName(string ID)
        {
            string name = "";
            try
            {
                query = @"Select * from tbl_timezone where Timezone_No='" + ID + "'";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    name = dt.Rows[0]["Timezone_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return name;
        }

        public List<Att_Status_P.TimeZone_P> GetAllTimeZones()
        {
            List<Att_Status_P.TimeZone_P> list = new List<Att_Status_P.TimeZone_P>();
            try
            {
                query = @"Select * from tbl_timezone";
                DataTable dt = new DataTable();
                dt = ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Att_Status_P.TimeZone_P tz = new Att_Status_P.TimeZone_P();
                        tz.Code = Formatter.SetValidValueToInt(dt.Rows[i]["Code"].ToString());
                        tz.Timezone_No = dt.Rows[i]["Timezone_No"].ToString();
                        tz.Timezone_Name = dt.Rows[i]["Timezone_Name"].ToString();
                        tz.Period_1_Start = dt.Rows[i]["Period_1_Start"].ToString();
                        tz.Period_1_End = dt.Rows[i]["Period_1_End"].ToString();
                        tz.Period_2_Start = dt.Rows[i]["Period_2_Start"].ToString();
                        tz.Period_2_End = dt.Rows[i]["Period_2_End"].ToString();
                        tz.Period_3_Start = dt.Rows[i]["Period_3_Start"].ToString();
                        tz.Period_3_End = dt.Rows[i]["Period_3_End"].ToString();
                        tz.Period_4_Start = dt.Rows[i]["Period_4_Start"].ToString();
                        tz.Period_4_End = dt.Rows[i]["Period_4_End"].ToString();
                        tz.Period_5_Start = dt.Rows[i]["Period_5_Start"].ToString();
                        tz.Period_5_End = dt.Rows[i]["Period_5_End"].ToString();
                        tz.Period_6_Start = dt.Rows[i]["Period_6_Start"].ToString();
                        tz.Period_6_End = dt.Rows[i]["Period_6_End"].ToString();
                        tz.Status = Formatter.SetValidValueToBool(dt.Rows[i]["Status"].ToString());

                        list.Add(tz);
                    }
                }

            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return list;
        }

        public bool SetTimeZone(string mDevId, Att_Status_P.TimeZone_P tz, int status, AppSettingModel app)
        {
            bool flag = false;
            try
            {
                conStr = File.ReadAllText(s);
                msqlConn = new SqlConnection(conStr);
                msqlConn.Open();
                string mTransIdTxt = "";
                JObject vResultJson = new JObject();
                JObject vT;
                FKWebCmdTrans cmdTrans = new FKWebCmdTrans(app);
                string start = "", end = "";
                vResultJson.Add("TimeZone_No", tz.Timezone_No);

                vT = new JObject();
                start = tz.Period_1_Start.ToString();
                end = tz.Period_1_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T1", vT);

                vT = new JObject();
                start = tz.Period_2_Start.ToString();
                end = tz.Period_2_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T2", vT);

                vT = new JObject();
                start = tz.Period_3_Start.ToString();
                end = tz.Period_3_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T3", vT);

                vT = new JObject();
                start = tz.Period_4_Start.ToString();
                end = tz.Period_4_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T4", vT);

                vT = new JObject();
                start = tz.Period_5_Start.ToString();
                end = tz.Period_5_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T5", vT);

                vT = new JObject();
                start = tz.Period_6_Start.ToString();
                end = tz.Period_6_End.ToString();
                if (string.IsNullOrEmpty(start)) start = "0000";
                if (string.IsNullOrEmpty(end)) end = "0000";
                vT.Add("start", start);
                vT.Add("end", end);
                vResultJson.Add("T6", vT);

                string sFinal = vResultJson.ToString(Formatting.None);
                byte[] strParam = new byte[0];
                if (string.IsNullOrEmpty(mDevModel))
                    cmdTrans.CreateBSCommBufferFromStrings(sFinal, out strParam);
                else
                    strParam = System.Text.Encoding.UTF8.GetBytes(sFinal);

                if (status != 0)
                    mTransIdTxt = FKWebTools.MakeCmdForTZ(msqlConn, "SET_TIMEZONE", mDevId, strParam, tz.Timezone_No);
                else
                    mTransIdTxt = FKWebTools.MakeCmdForTZOffline(msqlConn, "SET_TIMEZONE", mDevId, strParam, tz.Timezone_No);

                flag = true;
            }
            catch (Exception ex)
            {
                clsWriterLog.WriteError(this.GetType().Namespace, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);

            }
            msqlConn = null;
            return flag;
        }


    }
}
