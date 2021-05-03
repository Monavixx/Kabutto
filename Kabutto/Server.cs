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
        private IPAddress ipAddress;
        private short port;
        public TcpListener server;
        public Dictionary<string, View> Views;
        public string[] StaticFilesPath;
        private List<string> staticFiles = new List<string>();

        public Server(string ip, short port)
        {
            ipAddress = IPAddress.Parse(ip);
            this.port = port;
        }
        public Server(short port)
        {
            ipAddress = IPAddress.Any;
            this.port = port;
        }

        public void Run()
        {
            server = new TcpListener(ipAddress, port);

            string urladdress = "http://" + ipAddress.ToString() + ":" + port + "/";
            Console.WriteLine(urladdress);
            server.Start();

            while (true)
            {
                Console.WriteLine("Ожидание подключений... ");

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Подключен клиент. Выполнение запроса...");

                HttpRequest httpRequest = new HttpRequest(client.ReadAll());
                
                httpRequest.Parse();

                HttpResponse response = null;

                if (Views.ContainsKey(httpRequest.Path))
                {
                    response = Views[httpRequest.Path].Response(httpRequest);
                }
                else if (staticFiles.Contains(httpRequest.Path))
                {
                    StaticFile staticFile = new StaticFile(httpRequest.Path[1..^1]);
                    response = staticFile.GenerateResponse(httpRequest);
                }
                else
                {
                    response = new HttpResponse { Data = "404", ContentType = "text/plain", StatusCode = 404, StatusDescription = "Error" };
                }

                client.Send(response);
                client.Close();
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
                        data = data.Replace("\\", "/") + "/";

                        staticFiles.Add(data);
                    }
            }
        }
    }
}
