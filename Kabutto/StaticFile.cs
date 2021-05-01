using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Kabutto
{
    class StaticFile
    {
        public string Filename;
        public string? ContentType;

        public StaticFile(string filename, string? contentType = null)
        {
            Filename = filename;
            ContentType = contentType;
        }

        public void GenerateResponse(HttpListenerContext client)
        {
            if (ContentType == null)
            {
                switch (Path.GetExtension(Filename))
                {
                    case "css":
                        client.Response.ContentType = "text/css; charset=utf-8";
                        break;
                    case "js":
                        client.Response.ContentType = "text/javascript; charset=utf-8";
                        break;
                }
            }
            else
            {
                client.Response.ContentType = ContentType;
            }

            string data = File.ReadAllText(Filename);
            byte[] baData = Encoding.UTF8.GetBytes(data);
            client.Response.ContentLength64 = baData.Length;
            client.Response.OutputStream.Write(baData, 0, baData.Length);
        }
    }
}
