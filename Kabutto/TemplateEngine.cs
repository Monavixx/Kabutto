using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    class TemplateEngine
    {
        private string data;
        private Dictionary<string, string> context;

        public TemplateEngine(string data, Dictionary<string, string> context)
        {
            this.data = data;
            this.context = context;
        }

        public string Process()
        {
            string processingData = data;

            if(context != null)
            foreach((string key, string value) in context)
            {
                processingData = processingData.Replace("{{" + key + "}}", value);
            }

            return processingData;
        }
    }
}
