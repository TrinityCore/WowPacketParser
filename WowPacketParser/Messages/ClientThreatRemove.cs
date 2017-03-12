using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientThreatRemove
    {
        public ulong AboutGUID;
        public ulong UnitGUID;
    }
}
