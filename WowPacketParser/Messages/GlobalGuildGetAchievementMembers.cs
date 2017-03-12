using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildGetAchievementMembers
    {
        public ulong GuildGUID;
        public ulong PlayerGUID;
        public int AchievementID;
    }
}
