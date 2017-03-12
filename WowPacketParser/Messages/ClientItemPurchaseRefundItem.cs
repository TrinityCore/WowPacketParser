using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemPurchaseRefundItem
    {
        public int ItemID;
        public int ItemCount;
    }
}
