using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientThreatUpdate
    {
        public ulong UnitGUID;
        public List<ThreatInfo> ThreatList;
    }
}
