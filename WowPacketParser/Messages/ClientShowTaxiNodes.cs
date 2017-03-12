using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientShowTaxiNodes
    {
        public ClientShowTaxiNodesWindowInfo WindowInfo; // Optional
        public List<byte> Nodes;
    }
}
