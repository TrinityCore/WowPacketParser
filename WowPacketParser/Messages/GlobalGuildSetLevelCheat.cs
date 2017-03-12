using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildSetLevelCheat
    {
        public ulong GuildGUID;
        public int Level;
    }
}
