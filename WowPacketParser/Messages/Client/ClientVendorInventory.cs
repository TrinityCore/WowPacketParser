using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVendorInventory
    {
        public byte Reason;
        public List<CliVendorItem> Items;
        public ulong Vendor;
    }
}
