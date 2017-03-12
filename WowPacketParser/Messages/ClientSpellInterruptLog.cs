using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellInterruptLog
    {
        public ulong Victim;
        public ulong Caster;
        public int SpellID;
        public int InterruptedSpellID;
    }
}
