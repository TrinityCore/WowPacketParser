using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootItemList
    {
        public List<LootItem> Items;
        public ulong LootObj;
    }
}
