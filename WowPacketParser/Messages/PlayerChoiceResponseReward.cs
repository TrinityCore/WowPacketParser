using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerChoiceResponseReward
    {
        public int TitleID;
        public int PackageID;
        public int SkillLineID;
        public uint SkillPointCount;
        public uint ArenaPointCount;
        public uint HonorPointCount;
        public ulong Money;
        public uint Xp;
        public List<PlayerChoiceResponseRewardEntry> Items;
        public List<PlayerChoiceResponseRewardEntry> Currencies;
        public List<PlayerChoiceResponseRewardEntry> Factions;
        public List<PlayerChoiceResponseRewardEntry> ItemChoices;
    }
}
