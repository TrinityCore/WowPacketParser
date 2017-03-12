using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSendSpellHistory
    {
        public List<SpellHistoryEntry> Entries;
    }
}
