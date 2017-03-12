using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayPurchaseUpdate
    {
        public List<BattlePayPurchase> Purchases;
    }
}
