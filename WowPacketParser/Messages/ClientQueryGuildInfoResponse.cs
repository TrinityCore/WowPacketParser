using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryGuildInfoResponse
    {
        public ulong GuildGuid;
        public bool Allow;
        public CliGuildInfo Info;
    }
}
