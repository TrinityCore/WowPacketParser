using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellMissLog
    {
        public List<ClientSpellLogMissEntry> Entries;
        public int SpellID;
        public ulong Caster;
    }
}
