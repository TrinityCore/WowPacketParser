using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootUpdated
    {
        public LootItem Item;
        public ulong LootObj;
    }
}
