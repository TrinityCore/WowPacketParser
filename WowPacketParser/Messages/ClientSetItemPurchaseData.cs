using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetItemPurchaseData
    {
        public uint PurchaseTime;
        public uint Flags;
        public ClientItemPurchaseContents Contents;
        public ulong ItemGUID;
    }
}
