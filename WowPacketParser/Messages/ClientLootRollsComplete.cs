using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootRollsComplete
    {
        public ulong LootObj;
        public byte LootListID;
    }
}
