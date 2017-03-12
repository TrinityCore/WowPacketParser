using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVendorInventory
    {
        public byte Reason;
        public List<CliVendorItem> Items;
        public ulong Vendor;
    }
}
