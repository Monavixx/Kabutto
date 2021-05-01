using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Kabutto
{
    class Server
    {
        private string ipAddress;
        private short port;
        public HttpListener server;
        public Dictionary<string, View> Views;
        public string[] StaticFilesPath;
        private List<string> staticFiles = new List<string>();

        public Server(string ip, short port)
        {
            ipAddress = ip;
            this.port = port;
        }
        public Server(short port)
        {
            ipAddress = "*";
            this.port = port;
        }

        public void Run()
        {
            server = new HttpListener();

            Console.WriteLine("http://" + ipAddress.ToString() + ":" + port + "/");
            server.Prefixes.Add("http://" + ipAddress.ToString() + ":" + port + "/");
            server.Start();

            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                HttpListenerContext client = server.GetContext();
                Console.WriteLine("Подключен клиент. Выполнение запроса...");

                if (Views.ContainsKey(client.Request.RawUrl))
                {
                    Views[client.Request.RawUrl].GenerateResponse(client);
                }
                else if(staticFiles.Contains(client.Request.RawUrl))
                {
                    StaticFile staticFile = new StaticFile(client.Request.RawUrl[1..]);
                    staticFile.GenerateResponse(client);
                }
                else
                {
                    byte[] data = Encoding.UTF8.GetBytes("404");
                    client.Response.ContentType = "text/plain";
                    client.Response.ContentLength64 = data.Length;
                    client.Response.OutputStream.Write(data, 0, data.Length);
                }
            }
        }

        public void UpdateStaticFiles()
        {
            foreach (var item in StaticFilesPath)
            {
                if (Directory.Exists(item))
                    foreach (var item2 in Directory.GetFiles(item))
                    {
                        string data = "/" + item2;
                        data = data.Replace("\\", "/");

                        staticFiles.Add(data);
                    }
            }
        }
    }
}
