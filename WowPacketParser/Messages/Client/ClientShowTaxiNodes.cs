using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientShowTaxiNodes
    {
        public ClientShowTaxiNodesWindowInfo? WindowInfo; // Optional
        public List<byte> Nodes;
    }
}
