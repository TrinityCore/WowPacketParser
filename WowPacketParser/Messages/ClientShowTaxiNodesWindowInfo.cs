using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientShowTaxiNodesWindowInfo
    {
        public ulong UnitGUID;
        public int CurrentNode;
    }
}
