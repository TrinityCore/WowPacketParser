using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellExecuteLog
    {
        public int SpellID;
        public ulong Caster;
        public List<ClientSpellLogEffect> Effects;
        public SpellCastLogData LogData; // Optional
    }
}
