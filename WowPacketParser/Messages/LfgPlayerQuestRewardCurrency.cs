using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LfgPlayerQuestRewardCurrency
    {
        public int CurrencyID;
        public int Quantity;
    }
}
