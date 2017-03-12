using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayGetPurchaseListResponse
    {
        public List<BattlePayPurchase> Purchases;
        public uint Result;
    }
}
