using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemPurchaseContents
    {
        public uint Money;
        public ClientItemPurchaseRefundItem[/*5*/] Items;
        public ClientItemPurchaseRefundCurrency[/*5*/] Currencies;
    }
}
