using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Kabutto
{
    abstract class View
    {
        public Config Configure;
        public Dictionary<string, string> GlobalContext;

        public virtual HttpResponse Response(HttpRequest request)
        {
            switch(request.Method)
            {
                case "GET":
                    return Get(request);
                case "POST":
                    return Post(request);
                default:
                    throw new Exception("Raw Method Exception");
            }
        }
        public abstract HttpResponse Get(HttpRequest client);
        public abstract HttpResponse Post(HttpRequest client);

        public static HttpResponse Render(HttpRequest client, string templateName, Dictionary<string, string> context = null, ushort statusCode = 200, string statusDescription = "OK")
        {
            return new HttpResponse { Data = new TemplateEngine(File.ReadAllText(templateName), context).Process(), ContentType = "text/html", StatusCode = statusCode, StatusDescription = statusDescription };
        }
        public static bool CheckCSRF(HttpRequest request, Config config)
        {
            return request.POST[config.NameCSRFToken] != request.Sessions.Get(config.NameCSRFToken).Data;
        }
        public static HttpResponse Redirect(string location, ushort code = 301, string description = "Move Permanently")
        {
            return new HttpResponse { Location = location, StatusCode = code, StatusDescription = description };
        }
    }
}