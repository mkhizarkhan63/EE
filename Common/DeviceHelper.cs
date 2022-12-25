using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DeviceHelper
    {
        
        public static string GetAuthority(string Code)
        {
            string Authority = "";
            switch (Code)
            {
                case "0X11":
                    Authority = "AC & TA";
                    break;
                case "0X55":
                    Authority = "TA";
                    break;
                case "0Xaa":
                    Authority = "AC";
                    break;
                case "0":
                    Authority = "Super Administrator";
                    break;
                case "1":
                    Authority = "Normal Administrator";
                    break;
                default:
                    Authority = "None";
                    break;
            }

            return Authority;
        }


        public static string GetAuthorityByName(string Name)
        {
            string Authority = "";
            switch (Name)
            {
                case "AC & TA":
                    Authority = "0X11";
                    break;
                case "TA":
                    Authority = "0X55";
                    break;
                case "AC":
                    Authority = "0Xaa";
                    break;
                case "Super Administrator":
                    Authority = "0";
                    break;
                case "Normal Administrator":
                    Authority = "1";
                    break;
                default:
                    Authority = "None";
                    break;
            }

            return Authority;
        }

        public static string GetStatus(string Code)
        {
            string status = "";
            switch (Code)
            {
                case "1":
                    status = "IN";
                    break;
                case "2":
                    status = "OUT";
                    break;
                default:
                    status = "";
                    break;
            }

            return status;
        }

        public static string GetStatusCode(string Code)
        {
            string status = "";
            switch (Code)
            {
                case "IN":
                    status = "1";
                    break;
                case "OUT":
                    status = "2";
                    break;
                default:
                    status = "0";
                    break;
            }

            return status;
        }


        public static string ConvertHexToNumber(string HexCode)
        {
            string cardNo = "";
            try
            {
                int i = HexCode.Length - 1;
                char[] ch = HexCode.ToCharArray();
                while (i > 1)
                {
                    cardNo += ch[i - 1] + "" + ch[i];
                    i -= 2;
                }

                cardNo = Convert.ToInt64(cardNo, 16).ToString();
                if (cardNo == "-1")
                    cardNo = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return cardNo;
        }

        public static string ConvertNumberToHex(string NumberCode)
        {
            string cardNo = "";
            try
            {
                if (!string.IsNullOrEmpty(NumberCode) && NumberCode != "0Xffffffff")
                {

                    NumberCode = Convert.ToString(Convert.ToInt64(NumberCode), 16);

                    int i = NumberCode.Length;
                    if (i % 2 != 0)
                    { NumberCode = "0" + NumberCode; }

                    i = NumberCode.Length - 1;

                    char[] ch = NumberCode.ToCharArray();
                    while (i >= 0)
                    {
                        cardNo += ch[i - 1] + "" + ch[i];
                        i -= 2;
                    }

                    if (cardNo.Length < 8)
                        cardNo = cardNo + "00";

                    cardNo = "0X" + cardNo;
                }
                else
                {
                    cardNo = "0Xffffffff";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return cardNo;
        }

    }
}
