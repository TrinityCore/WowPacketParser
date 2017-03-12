using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMGrantAchievement
    {
        public int AchievementID;
        public ulong Guid;
        public string Target;
    }
}
