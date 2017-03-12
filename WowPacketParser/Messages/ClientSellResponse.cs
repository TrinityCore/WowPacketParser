using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSellResponse
    {
        public ulong VendorGUID;
        public ulong ItemGUID;
        public byte Reason;
    }
}
