using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ChallengeModeReward
    {
        public List<ItemReward> ItemRewards;
        public List<CurrencyReward> CurrencyRewards;
        public uint Money;
    }
}
