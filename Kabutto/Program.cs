using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Kabutto
{
    class ViewTest : View
    {
        public override HttpResponse Get(HttpRequest request)
        {
            return Render(request, "index.html", new Dictionary<string, string> { { "CSRFToken", request.Sessions.Get("CSRFToken").Data }, { "projectname", Configure.ProjectName }, { "anatar", request.Cookies.ContainsKey("Name") ? request.Cookies["Name"] : "no cookie" } });
        }

        public override HttpResponse Post(HttpRequest request)
        {
            if (CheckCSRF(request, Configure))
            {
                return new HttpResponse { Data = "CSRF-attack", ContentType = "text/html" };
            }

            HttpResponse response = Render(request, "index.html", new Dictionary<string, string> { { "projectname", Configure.ProjectName }, { "anatar", request.Cookies.ContainsKey("Name") ? request.Cookies["Name"] : "no cookie" } });

            response.Cookies.Add(new Cookie
            {
                Name="Name",
                Value=request.POST["name"],
                MaxAge=60
            });

            return response;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Application ap = new Application("config.xml");

            ap.Add("/", new ViewTest());

            ap.Start();
        }
    }
}
