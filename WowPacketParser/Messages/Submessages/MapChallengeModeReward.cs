using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct MapChallengeModeReward
    {
        public int MapID;
        public List<ChallengeModeReward> RewardsPerMedal;
    }
}
