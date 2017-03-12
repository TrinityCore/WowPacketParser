using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTaxiNodeStatus
    {
        public Taxistatus Status;
        public ulong Unit;
    }
}
