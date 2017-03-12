using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDuelRequested
    {
        public ulong ArbiterGUID;
        public ulong RequestedByGUID;
    }
}
