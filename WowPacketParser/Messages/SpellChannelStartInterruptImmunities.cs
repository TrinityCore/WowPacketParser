using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellChannelStartInterruptImmunities
    {
        public int SchoolImmunities;
        public int Immunities;
    }
}
