using Decdr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public static class ListGeneration
    {
        #region Variables
        static string listPath = "";
        private static LICDE decrdr = new LICDE();
        #endregion

        public static void ListDat(string rawData, string path)
        {
            try
            {
                if (!String.IsNullOrEmpty(path))
                {
                    listPath = path;
                    FileStream fs = new FileStream(listPath, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(rawData);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex) { }
        }


        public static void ListDatEncrypted(string rawData, string path)
        {
            try
            {
                if (!String.IsNullOrEmpty(path))
                {
                    rawData = EncryptedData(rawData);
                    listPath = path;
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(rawData);
                    sw.Flush();
                    sw.Close();

                }
            }
            catch (Exception) { }
        }

        public static void ListDatEncryptedHidden(string rawData, string path)
        {
            try
            {
                string[] filePath = path.Split('\\');
                string folderName = filePath[0] + "\\" + filePath[1];
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                if (!String.IsNullOrEmpty(path))
                {


                    rawData = EncryptedData(rawData);
                    listPath = path;
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(rawData);
                    sw.Flush();
                    sw.Close();

                    FileInfo file = new FileInfo(path);
                    file.Attributes |= FileAttributes.Hidden;

                }
            }
            catch (Exception ex) { }
        }

        public static string EncryptedData(string rawData)
        {
            return decrdr.EncryptData(rawData, "!LoveTis.Net-V3MadeByL!imtonInn0vativeSyst3m");
        }
    }
}
