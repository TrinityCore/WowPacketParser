using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientQueryGuildInfo
    {
        public ulong PlayerGuid;
        public ulong GuildGuid;
    }
}
