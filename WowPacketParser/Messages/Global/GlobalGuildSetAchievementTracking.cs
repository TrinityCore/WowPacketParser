using System.Collections.Generic;

namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGuildSetAchievementTracking
    {
        public List<int> AchievementIDs;
    }
}
