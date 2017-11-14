using System.Collections.Generic;

namespace WowPacketParser.Messages.Player.Choice
{
    public unsafe struct ResponseReward
    {
        public int TitleID;
        public int PackageID;
        public int SkillLineID;
        public uint SkillPointCount;
        public uint ArenaPointCount;
        public uint HonorPointCount;
        public ulong Money;
        public uint Xp;
        public List<ResponseRewardEntry> Items;
        public List<ResponseRewardEntry> Currencies;
        public List<ResponseRewardEntry> Factions;
        public List<ResponseRewardEntry> ItemChoices;
    }
}
