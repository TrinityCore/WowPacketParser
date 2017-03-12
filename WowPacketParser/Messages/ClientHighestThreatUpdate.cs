using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientHighestThreatUpdate
    {
        public ulong UnitGUID;
        public List<ThreatInfo> ThreatList;
        public ulong HighestThreatGUID;
    }
}
