using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    public class Session
    {
        public IPAddress IpAddress;
        public string Key, Data;
        public DateTime RemovalTime;

        public Session(IPAddress ipAddress, string key, string data)
        {
            Data = data;
            Key = key;
            IpAddress = ipAddress;
            RemovalTime = DateTime.Now.AddMinutes(15);
        }
        public Session(IPAddress ipAddress, string key, string data, DateTime removalTime)
        {
            Data = data;
            Key = key;
            IpAddress = ipAddress;
            RemovalTime = removalTime;
        }

        public static string GenerateCSRFToken()
        {
            string res = string.Empty;
            string symbols = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            for (short i = 0; i < 40; ++i)
            {
                int num = new Random().Next(0, symbols.Length - 1);
                res += symbols[num];
            }
            return res;
        }
    }
}
