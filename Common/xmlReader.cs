using System;
using System.Collections.Generic;
using System.Linq;
using AMS.Profile;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Net;
using System.IO;
using Decdr;
using Algorithm;
using System.Data;
using System.Web;

namespace Common
{
    public static class xmlReader
    {
        static Xml profile = null;
        private static LICDE decrdr = new LICDE();

        

        private static string tmpPath = "";
        static string PASS = "$L!mt0n#L!C&HPM&$";
        static Xml pf = null;
        static Xml pfRead = null;
        static Xml pfWrite = null;

       

        #region Utilities


        private static void FileWriteAccess(string path)
        {
            try
            {
                var strXML = File.ReadAllText(tmpPath);
                if (!String.IsNullOrEmpty(strXML))
                {
                    strXML = decrdr.EncryptData(strXML, PASS);
                    File.WriteAllText(path, strXML);
                    pfWrite = GetXmlFile("Read", path);
                }
                else
                {
                }
            }
            catch (Exception)
            {
                //WriteErrorsLog(excp, MethodBase.GetCurrentMethod().Name,
                //    "Error Ocurred While Writing File in Common");
            }
        }

        private static int GetPingTimeOut(string path)
        {
            int Sec = 0;

            if (profile == null)
            {
                profile = GetProfile("Read",path);
            }

            Sec = int.Parse(profile.GetValue("Machine Setting", "PingTimeOut", "").ToString());

            return Sec;
        }

        public static void SaveDataIntxt(string GetData)
        {
            try
            {
                string FilePath = HttpContext.Current.Server.MapPath(@"\ErrLog.txt");


                using (StreamWriter SW = new StreamWriter(FilePath, true))
                {
                    SW.WriteLine(GetData);
                    SW.Flush();
                }
            }
            catch (Exception)
            {
                //clsMessageBox.ShowError(excp.Message, Application.ProductName);
            }
            finally
            {
                GetData = "";
            }


        }

        public static string EncryptDecryptString(string Input, bool IsEncrypt)
        {
            Algo.Password = "MDDMWAlgo";

            string ResultantString = IsEncrypt ?
                Algo.Encrypt(Input) : Algo.Decrypt2String(Input);

            return ResultantString;
        }

        public static Xml GetProfile(string XmlType,string path)
        {
            try
            {
                Xml profile = GetXmlFile(XmlType, path);

                return profile;
            }
            catch (Exception)
            {
                //MessageBox.Show(excp.Message);                
                //WriteErrorsLog(excp, MethodBase.GetCurrentMethod().Name,
                //    "Error Ocurred While Loading xmlFile.");

                return null;
            }
        }


        private static Xml GetXmlFile(string XmlType,string path)
        {
            try
            {
                Xml profile = null;

                if (XmlType == "Read")
                {
                    bool IsValid = FileReadAccess(path);
                    //MessageBox.Show("Isvalid read "+IsValid);
                    ReadWriteFile("read");
                    profile = pfRead;
                }
                else
                {
                    FileWriteAccess(path);
                    ReadWriteFile("write");
                    profile = pfWrite;
                }

                return profile;
            }
            catch (Exception)
            {
                //WriteErrorsLog(excp, MethodBase.GetCurrentMethod().Name,
                //    "Error Ocurred While getting xmlFile.");
                return null;
            }
        }

        private static void ReadWriteFile(string type)
        {
            try
            {
                DataSet dsFile = pfRead.GetDataSet();
                DataSet dsUpdated = new DataSet();
                int Total = dsFile.Tables.Count;
                string TableName = String.Empty;
                if (Total > 0)
                {
                    for (int i = 0; i < Total; i++)
                    {
                        DataTable dt = dsFile.Tables[i];
                        int colCount = dt.Columns.Count;
                        int rowCount = dt.Rows.Count;
                        for (int j = 0; j < colCount; j++)
                        {
                            for (int k = 0; k < rowCount; k++)
                            {
                                if (type == "read")
                                    TableName = decrdr.DecryptData(dt.Rows[k][j].ToString(), PASS);
                                else if (type == "write")
                                    TableName = decrdr.EncryptData(dt.Rows[k][j].ToString(), PASS);
                                dt.Rows[k][j] = TableName;
                            }
                        }

                        DataTable dtXml = dt.Copy();

                        dsUpdated.Tables.Add(dtXml);
                    }

                    pfRead.SetDataSet(dsUpdated);
                }

            }
            catch (Exception)
            {

            }

        }

        private static bool FileReadAccess(string path)
        {
            try
            {
                //Get Encrypted XML file
                var xmlStrng = File.ReadAllText(path);
                if (String.IsNullOrEmpty(xmlStrng))
                {
                    //LogService.WriteLog("AppSetting file is corrupted.");
                    //MessageBox.Show("AppSetting file is corrupted.");
                    return false;
                }

                // Decrypted XML file for execution                
                xmlStrng = decrdr.DecryptData(xmlStrng, PASS);
                //MessageBox.Show("2");
                //Save at temp location
                //tmpPath = Path.GetTempFileName();
                //tmpPath = GetSettingsPath();   
                tmpPath = Path.Combine(Path.GetTempPath(), "cKte.tmp");

                using (StreamWriter sw = new StreamWriter(tmpPath))
                {
                    sw.WriteLine(xmlStrng);
                }

                pfRead = new Xml(tmpPath);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion


    }
}
