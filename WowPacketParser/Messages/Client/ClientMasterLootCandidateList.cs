using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMasterLootCandidateList
    {
        public List<ulong> Players;
        public ulong LootObj;
    }
}
