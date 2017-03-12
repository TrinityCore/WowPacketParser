using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellOrDamageImmune
    {
        public ulong VictimGUID;
        public bool IsPeriodic;
        public uint SpellID;
        public ulong CasterGUID;
    }
}
