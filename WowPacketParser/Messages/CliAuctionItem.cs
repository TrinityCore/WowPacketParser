using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliAuctionItem
    {
        public ItemInstance Item;
        public int Count;
        public int Charges;
        public List<CliAuctionItemEnchant> Enchantments;
        public int Flags;
        public int AuctionItemID;
        public ulong Owner;
        public ulong MinBid;
        public ulong MinIncrement;
        public ulong BuyoutPrice;
        public int DurationLeft;
        public byte DeleteReason;
        public bool CensorServerSideInfo;
        public bool CensorBidInfo;
        public ulong ItemGUID;
        public uint OwnerAccountID;
        public uint EndTime;
        public ulong Bidder;
        public ulong BidAmount;
    }
}
