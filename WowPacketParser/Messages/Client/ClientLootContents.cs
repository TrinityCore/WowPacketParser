using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootContents
    {
        public ulong LootObj;
        public ulong Owner;
        public List<LootItem> Items;
    }
}
