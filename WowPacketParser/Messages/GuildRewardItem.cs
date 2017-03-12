using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GuildRewardItem
    {
        public uint ItemID;
        public List<uint> AchievementsRequired;
        public uint RaceMask;
        public int MinGuildLevel;
        public int MinGuildRep;
        public ulong Cost;
    }
}
