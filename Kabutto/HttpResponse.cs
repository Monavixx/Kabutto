using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    public sealed class HttpResponse
    {
        public ushort StatusCode = 200;
        public string StatusDescription = "OK";
        public string ContentType;
        public string Location = null;
        public long? ContentLength = null;
        public string Data;
        public Dictionary<string, string> Headers;
        public List<Cookie> Cookies = new();

        public string Response;
        
        public override string ToString()
        {
            string response = "HTTP/1.1 " + StatusCode.ToString() + StatusDescription;

            if (ContentType != null)
            {
                ContentLength ??= Encoding.UTF8.GetBytes(Data).Length;
                response += "\nContent-Type: " + ContentType + "\nContent-Length: " + ContentLength.ToString();
            }

            if(Headers != null)
            foreach((string key, string value) in Headers)
            {
                response += "\n" + key + ": " + value;
            }

            if(Cookies != null)
            foreach(var item in Cookies)
            {
                response += "\n" + item.ToString();
            }

            if (Location != null)
                response += "\nLocation: " + Location;

            if(Data != null)
            response += "\n\n" + Data;

            return response;
        }
    }
}
