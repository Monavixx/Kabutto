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
        public Dictionary<string, string> GlobalContext = new();

        public Application(string configfile)
        {
            config = new Config(configfile);
            config.Parse();

            Name = config.ProjectName;
            server = new Server(config.IpAddress, config.Port, config);
            Views = new();
            server.Views = Views;
            server.GlobalContext = GlobalContext;
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
            view.GlobalContext = GlobalContext;
            Views.Add(path, view);
        }
    }
}
