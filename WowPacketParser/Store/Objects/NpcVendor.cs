using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class NpcVendor
    {
        public ICollection<VendorItem> VendorItems;
    }
}
