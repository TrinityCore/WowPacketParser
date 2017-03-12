using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
