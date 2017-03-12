using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSocketGems
    {
        public fixed ulong GemItem[3];
        public ulong ItemGuid;
    }
}
