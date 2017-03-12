using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootContents
    {
        public ulong LootObj;
        public ulong Owner;
        public List<LootItem> Items;
    }
}
