using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ServerFirstAchievement
    {
        public ulong PlayerGuid;
        public int AchievementID;
    }
}
