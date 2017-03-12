using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemPurchaseRefundCurrency
    {
        public int CurrencyID;
        public int CurrencyCount;
    }
}
