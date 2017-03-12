using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInspectPVP
    {
        public List<PVPBracketData> Bracket;
        public ulong ClientGUID;
    }
}
