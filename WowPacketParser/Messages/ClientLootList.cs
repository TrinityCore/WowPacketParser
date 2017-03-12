using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLootList
    {
        public ulong RoundRobinWinner; // Optional
        public ulong Master; // Optional
        public ulong Owner;
    }
}
