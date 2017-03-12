using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootUpdated
    {
        public LootItem Item;
        public ulong LootObj;
    }
}
