using EagleEye.DAL.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.Common
{
    public class SessionHandling
    {
        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        private static Communication_P _objCommunication = null;
        private static bool _objstop = false;

        public static Communication_P Communication
        {
            get
            {
                try
                {
                    if (Context.Session[SessionVariables.Session_Communication] != null)
                    {
                        _objCommunication = (Communication_P)Context.Session[SessionVariables.Session_Communication];
                    }
                }
                catch
                {

                }
                return _objCommunication;
            }
        }

        public static bool Stop
        {
            get
            {
                try
                {
                    if (Context.Session[SessionVariables.Session_Stop] != null)
                    {
                        _objstop = (bool)Context.Session[SessionVariables.Session_Stop];
                    }
                }
                catch
                {

                }
                return _objstop;
            }
        }
    }
}