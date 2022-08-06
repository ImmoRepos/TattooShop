using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooStudio.data
{
    public struct NUICommand
    {
        public NUICommand(string type, string data)
        {
            this.type = type;
            this.data = data;
        }

        public NUICommand(string type)
        {
            this.type = type;
            this.data = "";
        }

        public string type;
        public string data;
    }
}
