using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGPlayerReward
    {
        public int RewardMoney;
        public uint QueuedSlot;
        public List<LFGPlayerRewards> Rewards;
        public int AddedXP;
        public uint ActualSlot;
    }
}
