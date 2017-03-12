using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliRestockVendorCheat
    {
        public ulong Target;
        public bool StockAll;
    }
}
