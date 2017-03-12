using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPushItemsFromTreasure
    {
        public uint TreasureID;
        public ItemContext LootItemContext;
    }
}
