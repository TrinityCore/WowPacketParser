using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestCurrency
    {
        public int CurrencyID;
        public int Amount;
    }
}
