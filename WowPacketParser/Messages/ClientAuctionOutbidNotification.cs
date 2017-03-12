using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionOutbidNotification
    {
        public ClientAuctionBidderNotification Info;
        public ulong BidAmount;
        public ulong MinIncrement;
    }
}
