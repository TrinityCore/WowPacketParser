using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildBankLogQueryResults
    {
        public ulong? WeeklyBonusMoney; // Optional
        public List<GuildBankLogEntry> Entry;
        public int Tab;
    }
}
