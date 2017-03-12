using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionItemForSale
    {
        public ulong Guid;
        public uint UseCount;
    }
}
