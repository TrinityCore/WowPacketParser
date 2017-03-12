using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetDurabilityCheat
    {
        public ulong Item;
        public uint Durability;
    }
}
