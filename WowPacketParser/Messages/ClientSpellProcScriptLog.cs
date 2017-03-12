using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellProcScriptLog
    {
        public ulong Caster;
        public int SpellID;
        public sbyte Result;
    }
}
