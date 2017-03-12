using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildNameChanged
    {
        public ulong GuildGUID;
        public string GuildName;
    }
}
