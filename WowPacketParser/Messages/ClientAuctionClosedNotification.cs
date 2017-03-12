using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionClosedNotification
    {
        public ClientAuctionOwnerNotification Info;
        public float ProceedsMailDelay;
        public bool Sold;
    }
}
