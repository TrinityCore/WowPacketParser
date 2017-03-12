using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellChargeEntry
    {
        public uint Category;
        public uint NextRecoveryTime;
        public byte ConsumedCharges;
    }
}
