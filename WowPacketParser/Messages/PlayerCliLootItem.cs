using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliLootItem
    {
        public List<LootRequest> Loot;
    }
}
