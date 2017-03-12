using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellChannelStart
    {
        public int SpellID;
        public SpellChannelStartInterruptImmunities InterruptImmunities; // Optional
        public ulong CasterGUID;
        public SpellTargetedHealPrediction HealPrediction; // Optional
        public uint ChannelDuration;
    }
}
