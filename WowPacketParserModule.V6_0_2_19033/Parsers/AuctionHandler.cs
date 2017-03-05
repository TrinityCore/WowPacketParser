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
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadBit("OpenForBusiness");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32("AuctionItemID");
            packet.Translator.ReadUInt32E<AuctionHouseAction>("Command");
            packet.Translator.ReadUInt32E<AuctionHouseError>("ErrorCode");
            packet.Translator.ReadUInt32("BagResult");
            packet.Translator.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.Translator.ReadUInt64("MinIncrement");
            packet.Translator.ReadUInt64("Money");
        }

        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            ItemHandler.ReadItemInstance(packet, idx);

            packet.Translator.ReadInt32("Count", idx);
            packet.Translator.ReadInt32("Charges", idx);
            var enchantmentsCount = packet.Translator.ReadInt32("EnchantmentsCount", idx);
            packet.Translator.ReadInt32("Flags", idx);
            packet.Translator.ReadInt32("AuctionItemID", idx);

            packet.Translator.ReadPackedGuid128("Owner", idx);

            packet.Translator.ReadUInt64("MinBid", idx);
            packet.Translator.ReadUInt64("MinIncrement", idx);
            packet.Translator.ReadUInt64("BuyoutPrice", idx);

            packet.Translator.ReadInt32("DurationLeft", idx);
            packet.Translator.ReadByte("DeleteReason", idx);

            for (int i = 0; i < enchantmentsCount; i++)
            {
                packet.Translator.ReadInt32("ID", idx, i);
                packet.Translator.ReadUInt32("Expiration", idx, i);
                packet.Translator.ReadInt32("Charges", idx, i);
                packet.Translator.ReadByte("Slot", idx, i);
            }

            packet.Translator.ResetBitReader();

            var bit141 = !packet.Translator.ReadBit("CensorServerSideInfo", idx);
            var bit142 = !packet.Translator.ReadBit("CensorBidInfo", idx);

            if (bit141)
            {
                packet.Translator.ReadPackedGuid128("ItemGUID", idx);
                packet.Translator.ReadPackedGuid128("OwnerAccountID", idx);
                packet.Translator.ReadInt32("EndTime", idx);
            }

            if (bit142)
            {
                packet.Translator.ReadPackedGuid128("Bidder", idx);
                packet.Translator.ReadInt64("BidAmount", idx);
            }
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.Translator.ReadInt32("ItemsCount");

            packet.Translator.ReadInt32("DesiredDelay");
            packet.Translator.ReadInt32("TotalCount");

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("OnlyUsable");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.Translator.ReadInt32("Offset");
            packet.Translator.ReadPackedGuid128("Auctioneer");

            packet.Translator.ReadByte("MinLevel");
            packet.Translator.ReadByte("MaxLevel");
            packet.Translator.ReadInt32("InvType");
            packet.Translator.ReadInt32("ItemClass");
            packet.Translator.ReadInt32("ItemSubclass");
            packet.Translator.ReadInt32("Quality");
            var sort = packet.Translator.ReadByte("SortCount");

            packet.Translator.ResetBitReader();

            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Name", len);

            packet.Translator.ReadBit("OnlyUsable");
            packet.Translator.ReadBit("ExactMatch");

            var size = packet.Translator.ReadInt32("DataSize");
            var data = packet.Translator.ReadBytes(size);
            var sorts = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            for (var i = 0; i < sort; ++i)
            {
                sorts.Translator.ReadByte("UnkByte1", i);
                sorts.Translator.ReadByte("UnkByte2", i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS)]
        public static void HandleAuctionListBidderItems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadUInt32("Offset");

            var count = packet.Translator.ReadBits("AuctionItemIDsCount", 7);
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32("AuctionItemID", i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT)]
        public static void HandleAuctionListItemsResult(Packet packet)
        {
            var itemsCount = packet.Translator.ReadInt32("ItemsCount");
            packet.Translator.ReadUInt32("TotalCount");
            packet.Translator.ReadUInt32("DesiredDelay");

            for (var i = 0; i < itemsCount; ++i) // Items
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS)]
        public static void HandleAuctionListOwnerItems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadUInt32("Offset");
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadInt64("MinBit");
            packet.Translator.ReadInt64("BuyoutPrice");
            packet.Translator.ReadInt32("RunTime");

            var count = packet.Translator.ReadBits("ItemsCount", 5);
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadInt32("UseCount");
            }
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadInt32("AuctionItemID");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_PENDING_SALES_RESULT)]
        public static void HandleAuctionListPendingSalesResult(Packet packet)
        {
            var mailsCount = packet.Translator.ReadInt32("MailsCount");
            packet.Translator.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; i++) // Mails
                MailHandler.ReadMailListEntry(packet, i, "MailListEntry");
        }

        public static void ReadClientAuctionOwnerNotification(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.Translator.ReadUInt64("BidAmount", idx);
            ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        public static void ReadClientAuctionBidderNotification(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.Translator.ReadPackedGuid128("Bidder", idx);
            ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        [Parser(Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION)]
        public static void HandleAuctionClosedNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.Translator.ReadSingle("ProceedsMailDelay");
            packet.Translator.ReadBit("Sold");
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");

            packet.Translator.ReadUInt64("BidAmount");
            packet.Translator.ReadUInt64("MinIncrement");
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

            packet.Translator.ReadUInt64("MinIncrement");
            packet.Translator.ReadPackedGuid128("Bidder");
        }

        [Parser(Opcode.SMSG_AUCTION_REPLICATE_RESPONSE)]
        public static void HandleAuctionReplicateResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Result");
            packet.Translator.ReadUInt32("DesiredDelay");
            packet.Translator.ReadUInt32("ChangeNumberGlobal");
            packet.Translator.ReadUInt32("ChangeNumberCursor");
            packet.Translator.ReadUInt32("ChangeNumberTombstone");

            var itemsCount = packet.Translator.ReadInt32("ItemsCount");
            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.CMSG_AUCTION_REPLICATE_ITEMS)]
        public static void HandleAuctionReplicateItems(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadInt32("ChangeNumberGlobal");
            packet.Translator.ReadInt32("ChangeNumberCursor");
            packet.Translator.ReadInt32("ChangeNumberTombstone");
            packet.Translator.ReadInt32("Count");
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Auctioneer");
            packet.Translator.ReadInt32("AuctionItemID");
            packet.Translator.ReadInt64("BidAmount");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionZero(Packet packet)
        {
        }
    }
}
