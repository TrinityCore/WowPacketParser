using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LFGPlayerRewards
    {
        public int RewardItem;
        public uint RewardItemQuantity;
        public int BonusCurrency;
        public bool IsCurrency;
    }
}
