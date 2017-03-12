using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionHelloResponse
    {
        public ulong Auctioneer;
        public bool OpenForBusiness;
    }
}
