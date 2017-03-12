using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallegeModeRewards
    {
        public List<MapChallengeModeReward> Rewards;
        public List<ItemReward> TierRewards;
    }
}
