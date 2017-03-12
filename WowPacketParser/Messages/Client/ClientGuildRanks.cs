using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildRanks
    {
        public List<CliGuildRankData> Ranks;
    }
}
