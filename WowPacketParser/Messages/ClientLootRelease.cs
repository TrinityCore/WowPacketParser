using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootRelease
    {
        public ulong LootObj;
        public ulong Owner;
    }
}
