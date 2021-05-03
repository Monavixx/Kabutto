using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Kabutto
{
    class Config
    {
        public string Filename;

        public Config(string filename)
        {
            Filename = filename;
        }

        public string ProjectName;
        public string StaticFilesPath;
        public string IpAddress;
        public short Port;

        public void Parse()
        {
            XDocument document = XDocument.Parse(File.ReadAllText(Filename));
            foreach (var item in document.Root.Elements())
            {
                if (item.Name == "ProjectName")
                    ProjectName = item.Value;
                else if (item.Name == "StaticFilesPath")
                    StaticFilesPath = item.Value;
                else if(item.Name == "Run")
                {
                    foreach (var item2 in item.Elements())
                    {
                        if (item2.Name == "IpAddress")
                            IpAddress = item2.Value;
                        else if (item2.Name == "Port")
                            Port = short.Parse(item2.Value);
                    }
                }
            }
        }
    }
}
