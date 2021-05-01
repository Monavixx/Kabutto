using System;

namespace Kabutto
{
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
