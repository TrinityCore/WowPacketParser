using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientHighestThreatUpdate
    {
        public ulong UnitGUID;
        public List<ThreatInfo> ThreatList;
        public ulong HighestThreatGUID;
    }
}
