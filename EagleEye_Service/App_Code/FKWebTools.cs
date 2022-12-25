using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace FKWeb
{
  public class FKWebTools
  {

        static public int BACKUP_FP_0 = 0;     // Finger 1
        static public int BACKUP_FP_1 = 1;     // Finger 2
        static public int BACKUP_FP_2 = 2;     // Finger 3
        static public int BACKUP_FP_3 = 3;     // Finger 4
        static public int BACKUP_FP_4 = 4;     // Finger 5
        static public int BACKUP_FP_5 = 5;     // Finger 6
        static public int BACKUP_FP_6 = 6;     // Finger 7
        static public int BACKUP_FP_7 = 7;     // Finger 8
        static public int BACKUP_FP_8 = 8;     // Finger 9
        static public int BACKUP_FP_9 = 9;     // Finger 10
        static public int BACKUP_PSW = 10;    // Password
        static public int BACKUP_CARD = 11;    // Card
        static public int BACKUP_FACE = 12;    // Face
        static public int BACKUP_PALM_1 = 13;    // Palm 1
        static public int BACKUP_PALM_2 = 14;    // Palm 2
        static public int BACKUP_USER_PHOTO = 15;    // user enroll photo
        static public int BACKUP_MAX = BACKUP_USER_PHOTO;


        static public bool mbDebugTest = false;
        static public byte[][] mFinger = new byte[10][];
        static public byte[] mFace;
        static public byte[] mPhoto;
        static public byte[][] mPalm = new byte[2][];

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern void OutputDebugString(string message);

    static public void PrintDebug(string astrFunction, string astrMsg)
    {
      int nThreadId = Thread.CurrentThread.ManagedThreadId;
      DateTime dtNow = DateTime.Now;
      String strOut = String.Format("{0:D2}:{1:D2}:{2:D2} Thrd={3:D}",
              dtNow.Hour, dtNow.Minute, dtNow.Second, nThreadId);

      strOut = astrFunction + " - " + astrMsg + " - " + strOut;
      OutputDebugString(strOut);
    }

        static public int GetBinIndex(string aStrBin)
        {
            int vLen = aStrBin.Length;
            if (vLen < 5) return -1;

            if (aStrBin.Substring(0, 3) != "BIN") return -1;
            return Convert.ToInt32(aStrBin.Substring(4, vLen - 4));
        }

        static public int GetBinarySize(byte[] binary, out byte[] outbinary)
        {
            outbinary = new byte[0];
            int lenText;
            if (binary.Length < 4)
                return 0;

            try
            {
                lenText = BitConverter.ToInt32(binary, 0);
                if (lenText > binary.Length - 4)
                    return 0;
                //lenText = binary.Length - 4;
                if (lenText == 0)
                    return 0;
                outbinary = new byte[binary.Length - 4];
                Array.Copy(binary, 4, outbinary, 0, binary.Length - 4);

                //return System.Text.Encoding.UTF8.GetString(abytBuffer, 4, lenText);
            }
            catch
            {
                return 0;
            }
            return lenText;
        }

        static public int GetBinaryData(byte[] abytCmdParamBin, int length, out byte[] abytParam, out byte[] outbinary)
        {
            outbinary = new byte[0];
            abytParam = new byte[0];
            int lenText;
            if (abytCmdParamBin.Length == 0)
                return 0;

            try
            {
                //lenText = BitConverter.ToInt32(binary, 0);
                //if (lenText > binary.Length - 4)
                //    return 0;
                lenText = abytCmdParamBin.Length - length;
                if (lenText < 0)
                    return 0;
                outbinary = new byte[lenText];
                abytParam = new byte[length];
                Array.Copy(abytCmdParamBin, length, outbinary, 0, lenText);
                Array.Copy(abytCmdParamBin, 0, abytParam, 0, length);

                //return System.Text.Encoding.UTF8.GetString(abytBuffer, 4, lenText);
            }
            catch
            {
                return 0;
            }
            return lenText;
        }

        static public String GetBinIndexString(int index)
        {
            return "BIN_" + Convert.ToString(index);
        }

        static public bool IsEngOrDigit(char aChar)
    {
      if (Char.IsLower(aChar))
        return true;
      if (Char.IsUpper(aChar))
        return true;
      if (Char.IsDigit(aChar))
        return true;

      return false;
    }

    static public void ConcateByteArray(ref byte[] abytDest, byte[] abytSrc)
    {
      int len_dest = abytDest.Length + abytSrc.Length;

      if (abytSrc.Length == 0)
        return;

      byte[] bytTmp = new byte[len_dest];
      Buffer.BlockCopy(abytDest, 0, bytTmp, 0, abytDest.Length);
      Buffer.BlockCopy(abytSrc, 0, bytTmp, abytDest.Length, abytSrc.Length);
      abytDest = bytTmp;
    }

    static public bool IsValidEngDigitString(string astrVal, int anMaxLength)
    {
      if (String.IsNullOrEmpty(astrVal))
        return false;
      if ((astrVal.Length < 1) || (astrVal.Length > anMaxLength))
        return false;

      for (int k = 0; k < astrVal.Length; k++)
      {
        if (!IsEngOrDigit(astrVal[k]) && !astrVal[k].Equals('_'))
          return false;
      }
      return true;
    }

    static public bool IsValidTimeString(string astrVal)
    {
      if (String.IsNullOrEmpty(astrVal))
        return false;

      DateTime datetime;
      try
      {
        if (!DateTime.TryParseExact(astrVal, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out datetime))
          return false;

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    static public string TimeToString(DateTime aTimeVal)
    {
      string strRet;
      try
      {
        strRet = "" + aTimeVal.Year + "-" + aTimeVal.Month + "-" + aTimeVal.Day + " " +
            aTimeVal.Hour + ":" + aTimeVal.Minute + ":" + aTimeVal.Second;
        return strRet;
      }
      catch (Exception)
      {
        return "1970-1-1 1:0:0";
      }
    }

    static public string GetFKTimeString14(DateTime aTimeVal)
    {
      try
      {
        string strDateTime;
        strDateTime = String.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
            aTimeVal.Year, aTimeVal.Month, aTimeVal.Day, aTimeVal.Hour, aTimeVal.Minute, aTimeVal.Second);

        return strDateTime;
      }
      catch (Exception)
      {
        return "19700101010000";
      }
    }

    static public string ConvertFKTimeToNormalTimeString(string astrFKTime14)
    {
      string strRet = "";

      if (astrFKTime14.Length != 14)
        return strRet;

      strRet = astrFKTime14.Substring(0, 4) + "-" +
              astrFKTime14.Substring(4, 2) + "-" +
              astrFKTime14.Substring(6, 2) + " " +
              astrFKTime14.Substring(8, 2) + ":" +
              astrFKTime14.Substring(10, 2) + ":" +
              astrFKTime14.Substring(12, 2);

      return strRet;
    }

    static public string GetStringFromObject(object aObj)
    {
      try
      {
        return Convert.ToString(aObj);
      }
      catch (Exception)
      {
        return "";
      }
    }

    static public int GetIntFromObject(object aObj)
    {
      try
      {
        return Convert.ToInt32(aObj);
      }
      catch (Exception)
      {
        return 0;
      }
    }

    static public int CompareStringNoCase(string as1, string as2)
    {
      as1 = as1.ToLower();
      as2 = as2.ToLower();
      if (as1 == as2)
        return 0;
      else
        return 1;
    }

    static public string GetHexString(byte[] abytBuffer)
    {
      try
      {
        if (abytBuffer.Length == 0)
          return "";

        string sHex = BitConverter.ToString(abytBuffer).Replace("-", string.Empty);
        return sHex;
      }
      catch (Exception)
      {
        return "";
      }
    }

    static public bool savefiledata(string filename, byte[] abytBuffer)
    {
      FileStream f = null;
      try
      {
        f = File.Create(filename);
        f.Write(abytBuffer, 0, abytBuffer.Length);
        f.Flush();
        f.Close();
        f = null;
      }
      catch
      {
        if (f != null)
          f.Close();
        f = null;
        return false;
      }
      return true;
    }

        // SQL지령이 수행되여 결과가 필요없는 간단한 지령들을 수행한다.
        static public void ExecuteSimpleCmd(SqlConnection asqlConn, string asSql)
        {
            try
            {

                SqlCommand sqlCmd = new SqlCommand(asSql, asqlConn);
                if (asqlConn.State == ConnectionState.Closed)
                {
                    asqlConn.Open();
                }
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();

            }
            catch (Exception)
            {
                //ex.Message();
            }

        }

        static protected string GetNewTransId(SqlConnection msqlConn)
        {
            int nTransId;
            string sTransId = "";
            string sSql;
            sSql = "SELECT MAX(Convert(int,trans_id)) from tbl_fkcmd_trans";

            SqlCommand sqlCmd = new SqlCommand(sSql, msqlConn);
            SqlDataReader sqlDr = sqlCmd.ExecuteReader();

            if (sqlDr.HasRows)
            {
                if (sqlDr.Read())
                    sTransId = sqlDr[0].ToString();
            }
            sqlDr.Close();
            sqlCmd.Dispose();

            if (sTransId == "")
                sTransId = "1000";

            nTransId = Convert.ToInt32(sTransId);
            return Convert.ToString(nTransId + 1);
        }

        static protected string GetNewTransIdForOffline(SqlConnection msqlConn)
        {
            int nTransId;
            string sTransId = "";
            string sSql;
            sSql = "SELECT MAX(Convert(int,trans_id)) from tbl_fkcmd_trans_offline";

            SqlCommand sqlCmd = new SqlCommand(sSql, msqlConn);
            SqlDataReader sqlDr = sqlCmd.ExecuteReader();

            if (sqlDr.HasRows)
            {
                if (sqlDr.Read())
                    sTransId = sqlDr[0].ToString();
            }
            sqlDr.Close();
            sqlCmd.Dispose();

            if (sTransId == "")
                sTransId = "1000";

            nTransId = Convert.ToInt32(sTransId);
            return Convert.ToString(nTransId + 1);
        }

        static public string MakeCmd(SqlConnection msqlConn, string Cmd, string mDevId, byte[] mParam)
        {
            string sSql;
            SqlCommand sqlCmd;
            SqlParameter sqlParamCmdParam;
            SqlParameter sqlParamFpDataParam;
            string mTransID;
            mTransID = GetNewTransId(msqlConn);

            switch (Cmd)
            {
                case "GET_USER_ID_LIST": break;
                case "CONVER_FP":
                    sSql = "insert into tbl_Fpdata_Conv";
                    sSql += "(FpData)";
                    sSql += "values(@FpData)";
                    break;
            }

            sSql = "delete from tbl_fkcmd_trans_cmd_param where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();

            sSql = "delete from tbl_fkcmd_trans_cmd_result where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();
            if (mParam != null && mParam.Length > 0)
            {
                sSql = "insert into tbl_fkcmd_trans_cmd_param";
                sSql += "(trans_id, device_id, cmd_param)";
                sSql += "values(@trans_id, @device_id, @cmd_param)";

                //txtStatus.Text = sSql;

                sqlCmd = new SqlCommand(sSql, msqlConn);

                sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@trans_id"].Value = mTransID;

                sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@device_id"].Value = mDevId;

                sqlParamFpDataParam = sqlCmd.Parameters.Add("@FpData", SqlDbType.VarBinary);
                sqlParamFpDataParam.Direction = ParameterDirection.Input;
                sqlParamFpDataParam.Size = mParam.Length;
                sqlParamFpDataParam.Value = mParam;

                sqlParamCmdParam = sqlCmd.Parameters.Add("@cmd_param", SqlDbType.VarBinary);
                sqlParamCmdParam.Direction = ParameterDirection.Input;
                sqlParamCmdParam.Size = mParam.Length;
                sqlParamCmdParam.Value = mParam;

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }
            //msqlConn.Close();

            sSql = "insert into tbl_fkcmd_trans";
            sSql += "(trans_id, device_id, cmd_code, status, update_time)";
            sSql += "values(@trans_id, @device_id, @cmd_code, 'WAIT',  @update_time)";

            sqlCmd = new SqlCommand(sSql, msqlConn);

            sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@trans_id"].Value = mTransID;

            sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@device_id"].Value = mDevId;

            sqlCmd.Parameters.Add("@cmd_code", SqlDbType.VarChar);
            sqlCmd.Parameters["@cmd_code"].Value = Cmd;

            sqlCmd.Parameters.Add("@update_time", SqlDbType.VarChar);
            sqlCmd.Parameters["@update_time"].Value = FKWebTools.TimeToString(DateTime.Now);

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            msqlConn.Close();
            return mTransID;
        }
        static public string MakeCmdForTZ(SqlConnection msqlConn, string Cmd, string mDevId, byte[] mParam,string timezone_no)
        {
            string sSql;
            SqlCommand sqlCmd;
            SqlParameter sqlParamCmdParam;
            SqlParameter sqlParamFpDataParam;
            string mTransID;
            mTransID = GetNewTransId(msqlConn);

            switch (Cmd)
            {
                case "GET_USER_ID_LIST": break;
                case "CONVER_FP":
                    sSql = "insert into tbl_Fpdata_Conv";
                    sSql += "(FpData)";
                    sSql += "values(@FpData)";
                    break;
            }

            sSql = "delete from tbl_fkcmd_trans_cmd_param where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();

            sSql = "delete from tbl_fkcmd_trans_cmd_result where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();
            if (mParam != null && mParam.Length > 0)
            {
                sSql = "insert into tbl_fkcmd_trans_cmd_param";
                sSql += "(trans_id, device_id, cmd_param)";
                sSql += "values(@trans_id, @device_id, @cmd_param)";

                //txtStatus.Text = sSql;

                sqlCmd = new SqlCommand(sSql, msqlConn);

                sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@trans_id"].Value = mTransID;

                sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@device_id"].Value = mDevId;

                sqlParamFpDataParam = sqlCmd.Parameters.Add("@FpData", SqlDbType.VarBinary);
                sqlParamFpDataParam.Direction = ParameterDirection.Input;
                sqlParamFpDataParam.Size = mParam.Length;
                sqlParamFpDataParam.Value = mParam;

                sqlParamCmdParam = sqlCmd.Parameters.Add("@cmd_param", SqlDbType.VarBinary);
                sqlParamCmdParam.Direction = ParameterDirection.Input;
                sqlParamCmdParam.Size = mParam.Length;
                sqlParamCmdParam.Value = mParam;

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }
            //msqlConn.Close();

            sSql = "insert into tbl_fkcmd_trans";
            sSql += "(trans_id, device_id, cmd_code, status, update_time,timezone_no)";
            sSql += "values(@trans_id, @device_id, @cmd_code, 'WAIT',  @update_time, @timezone_no)";

            sqlCmd = new SqlCommand(sSql, msqlConn);

            sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@trans_id"].Value = mTransID;

            sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@device_id"].Value = mDevId;

            sqlCmd.Parameters.Add("@cmd_code", SqlDbType.VarChar);
            sqlCmd.Parameters["@cmd_code"].Value = Cmd;

            sqlCmd.Parameters.Add("@update_time", SqlDbType.VarChar);
            sqlCmd.Parameters["@update_time"].Value = FKWebTools.TimeToString(DateTime.Now);

            sqlCmd.Parameters.Add("@timezone_no", SqlDbType.VarChar);
            sqlCmd.Parameters["@timezone_no"].Value = timezone_no;

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            msqlConn.Close();
            return mTransID;
        }

        static public string MakeCmdForTZOffline(SqlConnection msqlConn, string Cmd, string mDevId, byte[] mParam, string timezone_no)
        {
            string sSql;
            SqlCommand sqlCmd;
            SqlParameter sqlParamCmdParam;
            SqlParameter sqlParamFpDataParam;
            string mTransID;
            mTransID = GetNewTransIdForOffline(msqlConn);

            switch (Cmd)
            {
                case "GET_USER_ID_LIST": break;
                case "CONVER_FP":
                    sSql = "insert into tbl_Fpdata_Conv";
                    sSql += "(FpData)";
                    sSql += "values(@FpData)";
                    break;
            }

            sSql = "delete from tbl_fkcmd_trans_cmd_param_offline where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();

            sSql = "delete from tbl_fkcmd_trans_cmd_result where trans_id = '" + mTransID + "'";

            ExecuteSimpleCmd(msqlConn, sSql);
            // msqlConn.Close();
            if (mParam != null && mParam.Length > 0)
            {
                sSql = "insert into tbl_fkcmd_trans_cmd_param_offline";
                sSql += "(trans_id, device_id, cmd_param)";
                sSql += "values(@trans_id, @device_id, @cmd_param)";

                //txtStatus.Text = sSql;

                sqlCmd = new SqlCommand(sSql, msqlConn);

                sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@trans_id"].Value = mTransID;

                sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
                sqlCmd.Parameters["@device_id"].Value = mDevId;

                sqlParamFpDataParam = sqlCmd.Parameters.Add("@FpData", SqlDbType.VarBinary);
                sqlParamFpDataParam.Direction = ParameterDirection.Input;
                sqlParamFpDataParam.Size = mParam.Length;
                sqlParamFpDataParam.Value = mParam;

                sqlParamCmdParam = sqlCmd.Parameters.Add("@cmd_param", SqlDbType.VarBinary);
                sqlParamCmdParam.Direction = ParameterDirection.Input;
                sqlParamCmdParam.Size = mParam.Length;
                sqlParamCmdParam.Value = mParam;

                sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
            }
            //msqlConn.Close();

            sSql = "insert into tbl_fkcmd_trans_offline";
            sSql += "(trans_id, device_id, cmd_code, status, update_time,timezone_no)";
            sSql += "values(@trans_id, @device_id, @cmd_code, 'WAIT',  @update_time, @timezone_no)";

            sqlCmd = new SqlCommand(sSql, msqlConn);

            sqlCmd.Parameters.Add("@trans_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@trans_id"].Value = mTransID;

            sqlCmd.Parameters.Add("@device_id", SqlDbType.VarChar);
            sqlCmd.Parameters["@device_id"].Value = mDevId;

            sqlCmd.Parameters.Add("@cmd_code", SqlDbType.VarChar);
            sqlCmd.Parameters["@cmd_code"].Value = Cmd;

            sqlCmd.Parameters.Add("@update_time", SqlDbType.VarChar);
            sqlCmd.Parameters["@update_time"].Value = FKWebTools.TimeToString(DateTime.Now);

            sqlCmd.Parameters.Add("@timezone_no", SqlDbType.VarChar);
            sqlCmd.Parameters["@timezone_no"].Value = timezone_no;

            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            msqlConn.Close();
            return mTransID;
        }
    }
}
