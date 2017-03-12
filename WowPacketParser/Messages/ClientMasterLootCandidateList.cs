using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMasterLootCandidateList
    {
        public List<ulong> Players;
        public ulong LootObj;
    }
}
