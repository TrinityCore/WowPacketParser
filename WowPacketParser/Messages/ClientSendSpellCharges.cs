using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendSpellCharges
    {
        public List<SpellChargeEntry> Entries;
    }
}
