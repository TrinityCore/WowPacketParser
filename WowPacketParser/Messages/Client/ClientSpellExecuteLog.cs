using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellExecuteLog
    {
        public int SpellID;
        public ulong Caster;
        public List<ClientSpellLogEffect> Effects;
        public SpellCastLogData? LogData; // Optional
    }
}
