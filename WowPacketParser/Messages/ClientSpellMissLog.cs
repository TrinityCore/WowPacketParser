using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellMissLog
    {
        public List<ClientSpellLogMissEntry> Entries;
        public int SpellID;
        public ulong Caster;
    }
}
