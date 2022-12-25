using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleEye_Service.App_Code
{
    public static class AppContext
    {
        public static bool _Lock { get; set; }
        public static Dictionary<string, object> Keys = new Dictionary<string, object>();

        public static void Lock()
        {
            _Lock = true;
        }

        public static void UnLock()
        {
            _Lock = false;
        }

        public static int Count()
        {
            return Keys.Count();
        }

        public static object Get(string key)
        {
            return Keys.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
        }

        public static object Get(int index)
        {
            return Keys.ElementAt(index).Value;
        }

        public static string GetKey(int index)
        {
            return Keys.ElementAt(index).Key;
        }

        public static void Add(string key, FKWebTransBlockData blk)
        {
            Keys.Add(key, blk);
        }

        public static void Remove(string key)
        {
            Keys.Remove(key);
        }
    }
}
