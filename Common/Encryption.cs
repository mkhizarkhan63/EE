using Decdr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Encryption
    {
        private static LICDE decrdr = new LICDE();
        public static string EncryptedData(string rawData)
        {
            return decrdr.EncryptData(rawData, "!LoveTis.Net-V3MadeByL!imtonInn0vativeSyst3m");
        }
    }
}
