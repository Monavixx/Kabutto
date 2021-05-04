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
        public string ContentType;

        public StaticFile(string filename, string contentType = null)
        {
            Filename = filename;
            ContentType = contentType;
        }

        public HttpResponse GenerateResponse(HttpRequest request)
        {
            HttpResponse response = new HttpResponse();

            if (ContentType == null)
            {
                switch (Path.GetExtension(Filename))
                {
                    case "css":
                        response.ContentType = "text/css; charset=utf-8";
                        break;
                    case "js":
                        response.ContentType = "text/javascript; charset=utf-8";
                        break;
                }
            }
            else
            {
                response.ContentType = ContentType;
            }

            response.Data = File.ReadAllText(Filename);

            return response;
        }
    }
}
