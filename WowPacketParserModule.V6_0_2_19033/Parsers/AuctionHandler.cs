using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
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

        [Parser(Opcode.SMSG_AUCTION_HELLO)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadBit("OpenForBusiness");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("AuctionItemID");
            packet.ReadEnum<AuctionHouseAction>("Command", TypeCode.UInt32);
            packet.ReadEnum<AuctionHouseError>("ErrorCode", TypeCode.UInt32);
            packet.ReadUInt32("BagResult");
            packet.ReadPackedGuid128("Guid");

            // One of the following is MinIncrement and the other is Money, order still unknown
            packet.ReadUInt64("MinIncrement");
            packet.ReadUInt64("Money");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var int10 = packet.ReadInt32("ItemsCount");

            packet.ReadInt32("DesiredDelay");
            packet.ReadInt32("TotalCount");

            for (int i = 0; i < int10; i++)
            {
                ItemHandler.ReadItemInstance(packet, i);

                packet.ReadInt32("Count", i);
                packet.ReadInt32("Charges", i);
                var int34 = packet.ReadInt32("EnchantmentsCount", i);
                packet.ReadInt32("Flags", i);
                packet.ReadInt32("AuctionItemID", i);

                packet.ReadPackedGuid128("Owner", i);

                packet.ReadUInt64("MinBid", i);
                packet.ReadUInt64("MinIncrement", i);
                packet.ReadUInt64("BuyoutPrice", i);

                packet.ReadInt32("DurationLeft", i);
                packet.ReadByte("DeleteReason", i);

                for (int j = 0; j < int34; j++)
                {
                    packet.ReadInt32("ID", i, j);
                    packet.ReadUInt32("Expiration", i, j);
                    packet.ReadInt32("Charges", i, j);
                    packet.ReadByte("Slot", i, j);
                }

                packet.ResetBitReader();

                var bit141 = !packet.ReadBit("CensorServerSideInfo", i);
                var bit142 = !packet.ReadBit("CensorBidInfo", i);

                if (bit141)
                {
                    packet.ReadPackedGuid128("ItemGUID", i);
                    packet.ReadPackedGuid128("OwnerAccountID", i);
                    packet.ReadInt32("EndTime", i);
                }

                if (bit142)
                {
                    packet.ReadPackedGuid128("Bidder", i);
                    packet.ReadInt64("BidAmount", i);
                }
            }

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
            packet.ReadInt32("ItemClass");
            packet.ReadInt32("InvType");
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

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleZero(Packet packet)
        {
        }
    }
}
