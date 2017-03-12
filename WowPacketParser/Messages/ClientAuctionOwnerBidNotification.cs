using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionOwnerBidNotification
    {
        public ClientAuctionOwnerNotification Info;
        public ulong Bidder;
        public ulong MinIncrement;
    }
}
