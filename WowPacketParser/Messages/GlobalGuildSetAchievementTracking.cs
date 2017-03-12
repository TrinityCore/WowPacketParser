using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildSetAchievementTracking
    {
        public List<int> AchievementIDs;
    }
}
