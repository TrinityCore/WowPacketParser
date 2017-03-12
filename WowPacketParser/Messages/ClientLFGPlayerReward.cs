using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
