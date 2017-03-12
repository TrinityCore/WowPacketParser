using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildInvite
    {
        public uint EmblemColor;
        public int Level;
        public uint EmblemStyle;
        public ulong GuildGUID;
        public string GuildName;
        public uint OldGuildVirtualRealmAddress;
        public string OldGuildName;
        public uint Background;
        public ulong OldGuildGUID;
        public uint BorderStyle;
        public uint GuildVirtualRealmAddress;
        public string InviterName;
        public uint BorderColor;
        public uint InviterVirtualRealmAddress;
    }
}
