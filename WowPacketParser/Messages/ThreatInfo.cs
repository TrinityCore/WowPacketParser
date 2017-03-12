using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ThreatInfo
    {
        public ulong UnitGUID;
        public int Threat;
    }
}
