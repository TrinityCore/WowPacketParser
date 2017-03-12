using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildGetRoster
    {
        public ulong PlayerGUID;
        public ulong GuildGUID;
    }
}
