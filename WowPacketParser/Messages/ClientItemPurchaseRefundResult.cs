using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemPurchaseRefundResult
    {
        public byte Result;
        public ulong ItemGUID;
        public ClientItemPurchaseContents Contents; // Optional
    }
}
