using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePayConfirmPurchaseResponse
    {
        public ulong ClientCurrentPriceFixedPoint;
        public bool ConfirmPurchase;
        public uint ServerToken;
    }
}
