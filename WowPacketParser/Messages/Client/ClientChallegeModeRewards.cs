using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallegeModeRewards
    {
        public List<MapChallengeModeReward> Rewards;
        public List<ItemReward> TierRewards;
    }
}
