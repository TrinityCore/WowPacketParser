using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Misc
{
    using PacketDataList = LinkedList<PacketData>;
    public struct PacketData
    {
        public string name;
        public Object item;
        public PacketData(string n, Object i)
        {
            name = n;
            item = i;
        }
    }
}
