using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendSpellHistory
    {
        public List<SpellHistoryEntry> Entries;
    }
}
