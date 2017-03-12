using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellInstakillLog
    {
        public ulong Target;
        public ulong Caster;
        public int SpellID;
    }
}
