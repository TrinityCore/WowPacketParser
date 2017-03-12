using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTaxiNodeStatus
    {
        public Taxistatus Status;
        public ulong Unit;
    }
}
