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

        public static HttpResponse Render(HttpRequest client, string templateName, Dictionary<string, string> context = null)
        {
            return new HttpResponse { Data= new TemplateEngine(File.ReadAllText(templateName), context).Process(), ContentType = "text/html" };
        }
    }
}