using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildRanks
    {
        public List<CliGuildRankData> Ranks;
    }
}
