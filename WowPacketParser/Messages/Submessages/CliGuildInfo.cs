using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
