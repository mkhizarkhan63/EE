using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{

    /// <summary>
    /// Helper class to format database fields to valid values
    /// </summary>
    public static class Formatter
    {
        public static string ArrayToString(string[] val)
        {
            string result = "";
            foreach (string item in val)
            {
                if (item != null && item != "")
                    result += item + ",";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public static string ArrayListToString(ArrayList val)
        {
            string result = "";
            foreach (string item in val)
            {
                if (item != null && item != "")
                    result += item + ",";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public static string ListToString(List<string> val)
        {
            string result = "";
            foreach (string item in val)
            {
                result += item + ",";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public static DateTime SetValidValueToDateTime(object dc)
        {
            if (dc == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(dc)))
                return DateTime.MinValue;

            return Convert.ToDateTime(dc);
        }

        public static DateTime SetValidValueToDateTime(object dc, bool isNullable)
        {
            if (dc == DBNull.Value)
                return DateTime.MinValue;

            return (DateTime)dc;
        }
        //safdar
        public static bool datetimeIsnull(object dc)
        {
            if (string.IsNullOrEmpty(Convert.ToString(dc)))
                return false;
            else
                return true;
        }

        public static DateTime SetValidValueToDateTimeMax(object dc)
        {
            if (dc == DBNull.Value || dc == "")
                return DateTime.MaxValue;
            else
                return Convert.ToDateTime(dc);
        }

        public static int SetValidValueToInt(object dc)
        {
            if (dc == DBNull.Value || dc == "" || string.IsNullOrEmpty(Convert.ToString(dc)))
                return 0;

            return Convert.ToInt32(dc);
        }
        public static Int64 SetValidValueToInt64(object dc)
        {
            if (dc == DBNull.Value || dc == null)
                return 0;

            return Convert.ToInt64(dc);
        }

        public static int SetValidValueToInt(object dc, bool isNullable)
        {
            if (dc == DBNull.Value || dc == "")
                return 0;

            return (int)dc;
        }
        //safdar
        public static double SetValidValueToDouble(object dc)
        {
            if (dc == DBNull.Value || dc == "" || string.IsNullOrEmpty(Convert.ToString(dc)))
                return 0;

            return Convert.ToDouble(dc);
        }

        public static decimal SetValidValueToDecimal(object dc)
        {
            if (dc == DBNull.Value || dc == "" || string.IsNullOrEmpty(Convert.ToString(dc)))
                return 0;

            return Convert.ToDecimal(dc);
        }

        public static double SetValidValueToDouble(object dc, bool isNullable)
        {
            if (dc == DBNull.Value || dc == "")
                return 0;

            return Convert.ToDouble(dc);
        }


        public static long SetValidValueToLong(object dc)
        {
            if (dc == DBNull.Value || dc == "" || Convert.ToString(dc) == "NaN")
                return 0;

            return Convert.ToInt64(dc);
        }

        public static long SetValidValueToLong(object dc, bool isNullable)
        {
            if (dc == DBNull.Value || dc == "")
                return 0;

            return Convert.ToInt64(dc);
        }

        public static char SetValidValueToChar(object value)
        {
            if (value == DBNull.Value || value == "")
                return '0';

            return Convert.ToChar(value);
        }

        public static string SetValidValueToString(object value)
        {
            if (value == DBNull.Value || value == "")
                return string.Empty;

            return Convert.ToString(value);
        }

        public static string SetValidValueToString(object value, bool isNullable)
        {
            if (value == DBNull.Value && isNullable)
                return string.Empty;

            return Convert.ToString(value);
        }

        public static bool SetValidValueToBool(object value)
        {
            if (value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
                return false;

            
            return Convert.ToBoolean(value);
        }
        
        public static bool SetValidValueToBool(object value, bool isNullable)
        {
            if (value == DBNull.Value && isNullable)
                return false;

            return (bool)value;
        }
        public static int SetBoolToInt(object dc)
        {

            if (dc == DBNull.Value || dc == "" || string.IsNullOrEmpty(Convert.ToString(dc)) || Convert.ToBoolean(dc) == false)
                return 0;
            else
                return 1;
        }
        
        public static bool GetBoolFieldValue(System.Data.DataRow DRow, string FieldName)
        {
            bool FieldValue = false;
            try
            {
                if (DRow[FieldName].ToString() == "1" || DRow[FieldName].ToString().ToUpper() == "True".ToUpper())
                {
                    FieldValue = true;
                }
                else
                {
                    FieldValue = false;
                }
            }
            catch (Exception)
            {
                FieldValue = false;
            }

            return FieldValue;
        }

        public static int GetBoolFieldValueInt(System.Data.DataRow DRow, string FieldName)
        {
            int FieldValue = 0;
            try
            {
                if (DRow[FieldName].ToString() == "1" || DRow[FieldName].ToString().ToUpper() == "True".ToUpper())
                {
                    FieldValue = 1;
                }
                else
                {
                    FieldValue = 0;
                }
            }
            catch (Exception)
            {
                FieldValue = 0;
            }

            return FieldValue;
        }

        public static bool GetBoolFieldValue(object dc)
        {
            bool FieldValue = false;
            try
            {
                if (dc.ToString() == "1" || dc.ToString().ToUpper() == "True".ToUpper())
                {
                    FieldValue = true;
                }
                else
                {
                    FieldValue = false;
                }
            }
            catch (Exception)
            {
                FieldValue = false;
            }

            return FieldValue;
        }

        public static bool SetIntToBool(Int64 value)
        {
            bool ReturnVal = false;
            if (value == 1)
            {
                ReturnVal = true;
            }
            else if (value == 0)
            {
                ReturnVal = false;
            }
            return ReturnVal;
        }

        public static bool SetInt32ToBool(int value)
        {
            bool ReturnVal = false;
            if (value == 1)
            {
                ReturnVal = true;
            }
            else if (value == 0)
            {
                ReturnVal = false;
            }
            return ReturnVal;
        }

        public static bool SetDoubleToBool(double value)
        {
            bool ReturnVal = false;
            if (value == 1)
            {
                ReturnVal = true;
            }
            else if (value == 0)
            {
                ReturnVal = false;
            }
            return ReturnVal;
        }

        public static double SetBoolToDouble(bool value)
        {
            double ReturnVal = 0;

            if (value == true)
                ReturnVal = 1;
            else if (value == false)
                ReturnVal = 0;

            return ReturnVal;
        }
        public static bool CheckValidValueToDateTime(object dc)
        {
            DateTime dt = new DateTime();
            bool istrue = true;
            try
            {
                dt = Convert.ToDateTime(dc);
            }
            catch (Exception)
            {
                return istrue = false;
            }
            return istrue;
        }

        //M. Saqib Khan on 11-Feb-2015
        public static bool ToBool(object value)
        {
            try
            {
                if (value == DBNull.Value || value.ToString() == "0" || value.ToString().ToLower() == "false")
                    return false;
                else if (value.ToString() == "1" || value.ToString().ToLower() == "true")
                    return true;

            }
            catch (Exception)
            { return false; }
            return false;
        }
        /// <summary>
        /// By Saqib Khan on 21-Oct-2019
        /// To get empty string on default date time values i.e
        /// 01-Jan-1900 00:00:00, 01-Jan-0001 00:00:00
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static string SetDateTimeToString(object dc)
        {
            if (dc == null || dc == DBNull.Value)
                return "";
            else
            {
                DateTime dt = DateTime.MinValue;
                DateTime.TryParse(Formatter.SetValidValueToString(dc), out dt);

                if (dt == new DateTime(1900, 1, 1) || dt == DateTime.MinValue)
                    return "";
                else
                    return dt.ToString("dd-MMM-yyyy mm:hh:ss");
            }
        }

        /// <summary>
        /// By Saqib Khan on 21-Oct-2019
        /// To get empty string on default date values i.e
        /// 01-Jan-1900, 01-Jan-0001
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static string SetDateToString(object dc)
        {
            if (dc == null || dc == DBNull.Value)
                return "";
            else
            {
                DateTime dt = DateTime.MinValue;
                DateTime.TryParse(Formatter.SetValidValueToString(dc), out dt);

                if (dt == new DateTime(1900, 1, 1) || dt == DateTime.MinValue)
                    return "";
                else
                    return dt.ToString("dd-MMM-yyyy");
            }
        }
    }

    public class FormatterHtml
    {

        public static string SetValidValue(string value)
        {
            return value.Trim().Replace("'", " ");
        }

    }

}
