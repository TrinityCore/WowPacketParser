using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRequestPVPRewardsResponse
    {
        public int RatedRewardPointsThisWeek;
        public int ArenaRewardPointsThisWeek;
        public int RatedMaxRewardPointsThisWeek;
        public int ArenaRewardPoints;
        public int RandomRewardPointsThisWeek;
        public int ArenaMaxRewardPointsThisWeek;
        public int RatedRewardPoints;
        public int MaxRewardPointsThisWeek;
        public int RewardPointsThisWeek;
        public int RandomMaxRewardPointsThisWeek;
    }
}
