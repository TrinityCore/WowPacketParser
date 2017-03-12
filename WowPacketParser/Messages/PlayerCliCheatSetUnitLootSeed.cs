using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliCheatSetUnitLootSeed
    {
        public ulong Unit;
        public uint LootSeed;
    }
}
