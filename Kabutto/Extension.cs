using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Kabutto
{
    static class Extension
    {
        public static string ReadAll(this TcpClient client)
        {
            byte[] data = new byte[client.SendBufferSize];
            int readSize = client.GetStream().Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data, 0, readSize);
        }
        public static void Send(this TcpClient client, HttpResponse response)
        {
            byte[] data = Encoding.UTF8.GetBytes(response.ToString());
            client.GetStream().Write(data, 0, data.Length);
        }
        public static Session Get(this List<Session> sessions, string key)
        {
            return sessions.Find(session => session.Key == key);
        }
    }
}
