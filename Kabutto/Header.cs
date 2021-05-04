using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    public sealed class Header
    {
        public string Name, Value;
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
