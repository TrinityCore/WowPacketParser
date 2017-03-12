using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayStartPurchaseResponse
    {
        public ulong PurchaseID;
        public uint PurchaseResult;
        public uint ClientToken;
    }
}
