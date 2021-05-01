using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    class Application
    {
        public string Name;
        public Dictionary<string, View> Views;
        private Server server;
        Config config;

        public Application(string configfile)
        {
            config = new Config(configfile);
            config.Parse();

            Name = config.ProjectName;
            server = new Server(config.IpAddress, config.Port);
            Views = new();
            server.Views = Views;
        }

        public void Start()
        {
            server.StaticFilesPath = new string[] { config.StaticFilesPath };
            server.UpdateStaticFiles();
            server.Run();
        }

        public void Add(string path, View view)
        {
            view.Configure = config;
            Views.Add(path, view);
        }
    }
}
