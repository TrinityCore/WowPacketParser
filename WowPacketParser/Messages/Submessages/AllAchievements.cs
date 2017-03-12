using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct AllAchievements
    {
        public List<EarnedAchievement> Earned;
        public List<CriteriaProgress> Progress;
    }
}
