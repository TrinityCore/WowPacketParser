using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDuelResponse
    {
        public ulong ArbiterGUID;
        public bool Accepted;
    }
}
