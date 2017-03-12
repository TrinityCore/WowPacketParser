using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AllAchievements
    {
        public List<EarnedAchievement> Earned;
        public List<CriteriaProgress> Progress;
    }
}
