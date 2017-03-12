using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliAuctionItemEnchant
    {
        public int ID;
        public uint Expiration;
        public int Charges;
        public byte Slot;
    }
}
