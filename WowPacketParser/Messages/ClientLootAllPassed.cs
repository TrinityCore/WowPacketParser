using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootAllPassed
    {
        public ulong LootObj;
        public LootItem Item;
    }
}
