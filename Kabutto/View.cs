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
        public void GenerateResponse(HttpListenerContext client)
        {
            byte[] baResponse = Encoding.UTF8.GetBytes(Response(client));
            client.Response.ContentLength64 = baResponse.Length;
            client.Response.ContentType = "text/html; charset=utf-8";
            client.Response.OutputStream.Write(baResponse, 0, baResponse.Length);
        }

        public virtual string Response(HttpListenerContext client)
        {
            switch(client.Request.HttpMethod)
            {
                case "GET":
                    return Get(client);
                case "POST":
                    return Post(client);
                default:
                    return "";
            }
        }
        public abstract string Get(HttpListenerContext client);
        public abstract string Post(HttpListenerContext client);

        public static string Render(HttpListenerContext client, string templateName, Dictionary<string, string> context = null)
        {
            return new TemplateEngine(File.ReadAllText(templateName), context).Process();
        }
    }
    class ViewTest : View
    {
        public override string Get(HttpListenerContext client)
        {
            return Render(client, "index.html", new Dictionary<string, string> { { "projectname", Configure.ProjectName } });
        }

        public override string Post(HttpListenerContext client)
        {
            throw new NotImplementedException();
        }
    }
}