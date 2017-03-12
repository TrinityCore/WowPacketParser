using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayConfirmPurchase
    {
        public ulong CurrentPriceFixedPoint;
        public ulong PurchaseID;
        public uint ServerToken;
    }
}
