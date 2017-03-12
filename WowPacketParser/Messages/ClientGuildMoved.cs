using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildMoved
    {
        public string TargetRealmName;
        public ulong GuildGUID;
    }
}
