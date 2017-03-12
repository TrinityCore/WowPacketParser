using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildRanks
    {
        public List<CliGuildRankData> Ranks;
    }
}
