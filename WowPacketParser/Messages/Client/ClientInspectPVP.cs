using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInspectPVP
    {
        public List<PVPBracketData> Bracket;
        public ulong ClientGUID;
    }
}
