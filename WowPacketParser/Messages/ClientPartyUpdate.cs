using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPartyUpdate
    {
        public byte PartyFlags;
        public CliPartyLootSettings? LootSettings; // Optional
        public CliPartyLFGInfo? LfgInfo; // Optional
        public ulong LeaderGUID;
        public byte PartyType;
        public ulong PartyGUID;
        public byte PartyIndex;
        public List<CliPartyPlayerInfo> PlayerList;
        public uint SequenceNum;
        public CliPartyDifficultySettings? DifficultySettings; // Optional
        public int MyIndex;
    }
}
