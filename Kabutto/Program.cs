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
            return Render(request, "index.html", new Dictionary<string, string> { { "projectname", Configure.ProjectName }, { "anatar", request.GET.ContainsKey("anatar") ? request.GET["anatar"] : "loh"} });
        }

        public override HttpResponse Post(HttpRequest request)
        {
            return Render(request, "index.html", new Dictionary<string, string> { { "projectname", Configure.ProjectName }, { "anatar", request.POST["anatar"]} });
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
