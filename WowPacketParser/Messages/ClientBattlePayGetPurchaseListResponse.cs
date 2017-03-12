using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayGetPurchaseListResponse
    {
        public List<BattlePayPurchase> Purchases;
        public uint Result;
    }
}
