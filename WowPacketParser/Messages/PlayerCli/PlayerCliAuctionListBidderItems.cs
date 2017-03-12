using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAuctionListBidderItems
    {
        public uint Offset;
        public List<uint> AuctionItemIDs;
        public ulong Auctioneer;
    }
}
