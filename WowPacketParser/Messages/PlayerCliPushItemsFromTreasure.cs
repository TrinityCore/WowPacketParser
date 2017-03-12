using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPushItemsFromTreasure
    {
        public uint TreasureID;
        public ItemContext LootItemContext;
    }
}
