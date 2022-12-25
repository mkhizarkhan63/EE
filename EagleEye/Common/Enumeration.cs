using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleEye.Common
{
    public class Enumeration
    {
        public enum DeviceStatus : int
        {
            Connected = 1,
            Disconnected = 0
        }
        public enum ExceptionLayer
        {
            Controller,
            BLL,
            DAL,
            Common,
            Hub
        }

        public static List<Common.Wiegand> WeigandProtocol { get; set; }

        public enum Wiegand_Input
        {
            ID = 0,
            Card = 1
        }
        public enum Wiegand_Output
        {
            ID = 0,
            Card = 1
        }
        public enum DoorMagnetic_Type
        {
            Disable = 0,
            Open = 1,
            Close = 2
        }
        public enum UserStatus : int
        {
            Active = 1,
            InActive = 0,
            NotInDevice = 2
        }
        public enum TZStatus : int
        {
            Valid = 1,
            Invalid = 0
        }

        public enum workHourPolicyEnum : int
        {

            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7


        }
    }
}