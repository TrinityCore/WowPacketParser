using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientIncreaseCastTimeForSpell
    {
        public ulong Caster;
        public uint CastTime;
        public int SpellID;
    }
}
