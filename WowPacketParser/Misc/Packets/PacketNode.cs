using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    class PacketNode
    {
        public PacketNode(string name)
        {
            Name = name;
            Attributes = new List<KeyValuePair<string,string>>();
            InnerNodes = new List<PacketNode>();
            InnerContent = "";

        }

        public string Name { get; set; }
        public List<KeyValuePair<string,string>> Attributes { get; set; }
        public List<PacketNode> InnerNodes { get; set; }
        public string InnerContent { get; set; }
    }
}
