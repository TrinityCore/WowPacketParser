using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRefreshSpellHistory
    {
        public List<SpellHistoryEntry> Entries;
    }
}
