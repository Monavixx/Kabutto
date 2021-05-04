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
                return new HttpResponse { Data = "Error 404", ContentType = "text/html", StatusCode = 404, StatusDescription = "NotFound" };
        }
    }
}
