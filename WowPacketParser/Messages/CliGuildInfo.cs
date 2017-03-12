using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliGuildInfo
    {
        public ulong GuildGUID;
        public uint VirtualRealmAddress;
        public string GuildName;
        public List<CliGuildInfoRank> Ranks;
        public int EmblemStyle;
        public int EmblemColor;
        public int BorderStyle;
        public int BorderColor;
        public int BackgroundColor;
    }
}
