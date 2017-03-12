using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellDelayed
    {
        public ulong Caster;
        public int ActualDelay;
    }
}
