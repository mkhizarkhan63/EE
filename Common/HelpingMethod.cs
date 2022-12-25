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
    public static class HelpingMethod
    {

        private static string mstrProductKey = "notmil-4641";
        



        public static string SetIPFormat(string Ip)
        {
            string[] s = Ip.Split('.');
            string newIP = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (i != s.Length - 1)
                    newIP += s[i].PadLeft(3, '0') + ".";
                else
                    newIP += s[i].PadLeft(3, '0');
            }

            return newIP;

        }

        public static string SetDeviceIPFormat(string Ip)
        {
            string newIP = "";
            char[] s = Ip.ToCharArray();
            if (s.Length == 12)
            {
               
                newIP = Ip.Insert(3, ".");
                newIP = newIP.Insert(7, ".");
                newIP = newIP.Insert(11, ".");
            }

            return newIP;





            //if (s.Length == 12)
            //{
            //    for (int i = 0; i < s.Length; i++)
            //    {
            //        if (i != s.Length - 1)
            //            newIP += s[i].ToString().PadLeft(3,'.');
            //        //else
            //            //newIP += s[i].PadLeft(3, '0');
            //    }
            //}

            //for (int i = 0; i < s.Length; i++)
            //{
            //    if (i != s.Length - 1)
            //        newIP += s[i].PadLeft(3, '0') + ".";
            //    else
            //        newIP += s[i].PadLeft(3, '0');
            //}

        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();

            try
            {
                int TimeOutSec = 10000; //10sec

                PingReply reply = pinger.Send(nameOrAddress, TimeOutSec);
                if (reply.Status != IPStatus.Success)
                {
                    reply = pinger.Send(nameOrAddress);
                }
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }

            return pingable;
        }

        public static bool CheckPortAvailability(int Port)
        {
            bool flag = true;
            try
            {
                IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnArray = iPGlobalProperties.GetActiveTcpConnections();
                foreach (TcpConnectionInformation tcpi in tcpConnArray)
                {
                    if (tcpi.LocalEndPoint.Port == Port)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return flag;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }

        public static bool PortInUse(int port)
        {
            bool _inUse = false;
            try
            {
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
                foreach (IPEndPoint endPoint in ipEndPoints)
                {
                    if (endPoint.Port == port)
                    {
                        _inUse = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return _inUse;
        }

        public static string RemoveZeros(string Ip)
        {
            string[] s = Ip.Split('.');
            string newIP = "";
            try
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (i != s.Length - 1)
                    {
                        string t = s[i].TrimStart(new char[] { '0' }).ToString();
                        if (t == "")
                        {
                            newIP += "0.";
                        }
                        else
                            newIP += t + ".";
                    }
                    else
                        newIP += s[i].TrimStart(new char[] { '0' });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newIP;
        }


        public static void MoveFile(string from, string to)
        {
            try
            {
                File.Copy(from, to);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteFileFromFolder(string path)
        {
            bool flag = false;
            try
            {
                File.Delete(path);
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
        public static bool ContainsAlphabets(string s)
        {
            try
            {
              
                if (Regex.Matches(s,@"[a-zA-Z]").Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }


    }
}