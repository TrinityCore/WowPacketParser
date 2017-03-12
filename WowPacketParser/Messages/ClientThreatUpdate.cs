using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientThreatUpdate
    {
        public ulong UnitGUID;
        public List<ThreatInfo> ThreatList;
    }
}
