using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBuyFailed
    {
        public ulong VendorGUID;
        public uint Muid;
        public byte Reason;
    }
}
