using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellHealPrediction
    {
        public ulong BeaconGUID;
        public int Points;
        public byte Type;
    }
}
