using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Kabutto
{
    static class Error
    {
        public static HttpResponse e404(HttpRequest request)
        {
            if (File.Exists("404.html"))
                return View.Render(request, "404.html", statusCode: 404, statusDescription: "NotFound");
            else
                return new HttpResponse { Data = "<b>HTTP 404 error: Not found</b>", ContentType = "text/html", StatusCode = 404, StatusDescription = "NotFound" };
        }

        public static HttpResponse Custom(HttpRequest request, ushort code, string description = "Error")
        {
            return new HttpResponse { Data = $"<b>HTTP {code} error: {description}</b>", ContentType = "text/html", StatusCode = code, StatusDescription = description };
        }
    }
}
