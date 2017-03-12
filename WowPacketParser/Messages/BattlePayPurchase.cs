using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct BattlePayPurchase
    {
        public ulong PurchaseID;
        public uint Status;
        public uint ResultCode;
        public uint ProductID;
        public string WalletName;
    }
}
