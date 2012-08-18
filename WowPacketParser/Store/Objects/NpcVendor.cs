using System.Collections.Generic;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public class NpcVendor : ITextOutputDisabled
    {
        public ICollection<VendorItem> VendorItems;
    }
}
