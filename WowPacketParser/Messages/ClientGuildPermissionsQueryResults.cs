using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildPermissionsQueryResults
    {
        public int NumTabs;
        public int WithdrawGoldLimit;
        public int Flags;
        public List<GuildRankTabPermissions> Tab;
        public uint RankID;
    }
}
