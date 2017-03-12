using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ChallengeModeReward
    {
        public List<ItemReward> ItemRewards;
        public List<CurrencyReward> CurrencyRewards;
        public uint Money;
    }
}
