using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AuctionHandler
    {
        [Parser(Opcode.CMSG_AUCTION_HELLO_REQUEST)]
        public static void HandleClientAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBit("OpenForBusiness");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("AuctionItemID");
            packet.ReadUInt32E<AuctionHouseAction>("Command");
            packet.ReadUInt32E<AuctionHouseError>("ErrorCode");
            packet.ReadUInt32("BagResult");
            packet.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.ReadUInt64("MinIncrement");
            packet.ReadUInt64("Money");
        }

        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            var enchantmentsCount = packet.ReadInt32("EnchantmentsCount", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadInt32("AuctionItemID", idx);

            packet.ReadPackedGuid128("Owner", idx);

            packet.ReadUInt64("MinBid", idx);
            packet.ReadUInt64("MinIncrement", idx);
            packet.ReadUInt64("BuyoutPrice", idx);

            packet.ReadInt32("DurationLeft", idx);
            packet.ReadByte("DeleteReason", idx);

            for (int i = 0; i < enchantmentsCount; i++)
            {
                packet.ReadInt32("ID", idx, i);
                packet.ReadUInt32("Expiration", idx, i);
                packet.ReadInt32("Charges", idx, i);
                packet.ReadByte("Slot", idx, i);
            }

            packet.ResetBitReader();

            var bit141 = !packet.ReadBit("CensorServerSideInfo", idx);
            var bit142 = !packet.ReadBit("CensorBidInfo", idx);

            if (bit141)
            {
                packet.ReadPackedGuid128("ItemGUID", idx);
                packet.ReadPackedGuid128("OwnerAccountID", idx);
                packet.ReadInt32("EndTime", idx);
            }

            if (bit142)
            {
                packet.ReadPackedGuid128("Bidder", idx);
                packet.ReadInt64("BidAmount", idx);
            }
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");

            packet.ReadInt32("DesiredDelay");
            packet.ReadInt32("TotalCount");

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);

            packet.ResetBitReader();

            packet.ReadBit("OnlyUsable");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.ReadInt32("Offset");
            packet.ReadPackedGuid128("Auctioneer");

            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            packet.ReadInt32("InvType");
            packet.ReadInt32("ItemClass");
            packet.ReadInt32("ItemSubclass");
            packet.ReadInt32("Quality");
            var sort = packet.ReadByte("SortCount");

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("Name", len);

            packet.ReadBit("OnlyUsable");
            packet.ReadBit("ExactMatch");

            var size = packet.ReadInt32("DataSize");
            var data = packet.ReadBytes(size);
            var sorts = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            for (var i = 0; i < sort; ++i)
            {
                sorts.ReadByte("UnkByte1", i);
                sorts.ReadByte("UnkByte2", i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS)]
        public static void HandleAuctionListBidderItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var count = packet.ReadBits("AuctionItemIDsCount", 7);
            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("AuctionItemID", i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT)]
        public static void HandleAuctionListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");
            packet.ReadUInt32("TotalCount");
            packet.ReadUInt32("DesiredDelay");

            for (var i = 0; i < itemsCount; ++i) // Items
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS)]
        public static void HandleAuctionListOwnerItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt64("MinBit");
            packet.ReadInt64("BuyoutPrice");
            packet.ReadInt32("RunTime");

            var count = packet.ReadBits("ItemsCount", 5);
            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("UseCount");
            }
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionItemID");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_PENDING_SALES_RESULT)]
        public static void HandleAuctionListPendingSalesResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; i++) // Mails
                MailHandler.ReadMailListEntry(packet, i, "MailListEntry");
        }

        public static void ReadClientAuctionOwnerNotification(Packet packet, params object[] idx)
        {
            packet.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.ReadUInt64("BidAmount", idx);
            ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        public static void ReadClientAuctionBidderNotification(Packet packet, params object[] idx)
        {
            packet.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.ReadPackedGuid128("Bidder", idx);
            ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        [Parser(Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION)]
        public static void HandleAuctionClosedNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadSingle("ProceedsMailDelay");
            packet.ReadBit("Sold");
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");

            packet.ReadUInt64("BidAmount");
            packet.ReadUInt64("MinIncrement");
        }

        [Parser(Opcode.SMSG_AUCTION_WON_NOTIFICATION)]
        public static void HandleAuctionWonNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");
        }

        [Parser(Opcode.SMSG_AUCTION_OWNER_BID_NOTIFICATION)]
        public static void HandleAuctionOwnerBidNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadUInt64("MinIncrement");
            packet.ReadPackedGuid128("Bidder");
        }

        [Parser(Opcode.SMSG_AUCTION_REPLICATE_RESPONSE)]
        public static void HandleAuctionReplicateResponse(Packet packet)
        {
            // TODO: Order is not confirmed
            packet.ReadUInt32("ChangeNumberCursor");
            packet.ReadUInt32("ChangeNumberGlobal");
            packet.ReadUInt32("DesiredDelay");
            packet.ReadUInt32("ChangeNumberTombstone");
            packet.ReadUInt32("Result");

            var itemsCount = packet.ReadInt32("ItemsCount");
            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.CMSG_AUCTION_REPLICATE_ITEMS)]
        public static void HandleAuctionReplicateItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("Count");
            packet.ReadInt32("ChangeNumberGlobal");
            packet.ReadInt32("ChangeNumberCursor");
            packet.ReadInt32("ChangeNumberTombstone");
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionItemID");
            packet.ReadInt64("BidAmount");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionZero(Packet packet)
        {
        }
    }
}
