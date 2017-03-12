using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellLogEffectDurabilityDamageParams
    {
        public ulong Victim;
        public int ItemID;
        public int Amount;
    }
}
