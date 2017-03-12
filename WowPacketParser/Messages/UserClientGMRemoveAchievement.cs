using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMRemoveAchievement
    {
        public ulong Guid;
        public string Target;
        public int AchievementID;
    }
}
