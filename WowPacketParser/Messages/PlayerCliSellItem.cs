using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSellItem
    {
        public ulong ItemGUID;
        public ulong VendorGUID;
        public int Amount;
    }
}
