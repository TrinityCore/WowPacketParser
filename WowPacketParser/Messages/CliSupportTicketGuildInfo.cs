using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSupportTicketGuildInfo
    {
        public string GuildName;
        public ulong GuildID;
    }
}
