using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildBankLogQueryResults
    {
        public ulong? WeeklyBonusMoney; // Optional
        public List<GuildBankLogEntry> Entry;
        public int Tab;
    }
}
