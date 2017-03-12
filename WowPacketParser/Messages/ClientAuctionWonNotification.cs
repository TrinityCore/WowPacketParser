using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionWonNotification
    {
        public ClientAuctionBidderNotification Info;
    }
}
