using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBuySucceeded
    {
        public ulong VendorGUID;
        public uint Muid;
        public uint QuantityBought;
        public int NewQuantity;
    }
}
