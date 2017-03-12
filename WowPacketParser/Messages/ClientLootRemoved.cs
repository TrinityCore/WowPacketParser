using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootRemoved
    {
        public ulong LootObj;
        public ulong Owner;
        public byte LootListID;
    }
}
