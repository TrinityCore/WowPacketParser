using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellLogEffectPowerDrainParams
    {
        public ulong Victim;
        public uint Points;
        public uint PowerType;
        public float Amplitude;
    }
}
