using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AuctionHouseHandler
    {
        [Parser(Opcode.MSG_AUCTION_HELLO)]
        public static void HandleAuctionHello(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadUInt32("AuctionHouse ID");
            packet.ReadBoolean("Enabled");
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.ReadGuid("Auctioneer GUID");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadGuid("Item GUID");
            packet.ReadUInt32("Count");
            packet.ReadUInt32("Bid");
            packet.ReadUInt32("Buyout");
            packet.ReadUInt32("Expire Time");
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.ReadGuid("Auctioneer GUID");
            packet.ReadUInt32("Auction Id");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.ReadGuid("Auctioneer GUID");
            packet.ReadUInt32("Auction House Id");
            packet.ReadCString("Search Pattern");
            packet.ReadByte("Min Level");
            packet.ReadByte("Max Level");
            packet.ReadInt32("Slot ID");
            packet.ReadInt32("Category");
            packet.ReadInt32("Sub Category");
            packet.ReadInt32("Quality");
            packet.ReadByte("Usable");
            packet.ReadByte("Unk Byte 1");
            var count = packet.ReadByte("Unk Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Byte 2", i);
                packet.ReadByte("Unk Byte 3", i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.ReadGuid("Auctioneer GUID");
            packet.ReadUInt32("Auction Id");
            packet.ReadUInt32("Price");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("Auction ID");
            var action = packet.ReadEnum<AuctionHouseAction>("Action", TypeCode.UInt32);
            var error = packet.ReadEnum<AuctionHouseAction>("Error", TypeCode.UInt32);
            if (error == 0 && action > 0)
                packet.ReadUInt32("Bid Error");

        }

        [Parser(Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION)]
        public static void HandleAuctionBidderNotification(Packet packet)
        {
            packet.ReadUInt32("Auction House ID");
            packet.ReadUInt32("Auction ID");
            packet.ReadGuid("Bidder GUID");
            packet.ReadUInt32("Bid");
            packet.ReadUInt32("Diff");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry");
            packet.ReadUInt32("Unk UInt32 1");
        }

        [Parser(Opcode.SMSG_AUCTION_OWNER_NOTIFICATION)]
        public static void HandleAuctionOwnerNotification(Packet packet)
        {
            packet.ReadUInt32("Auction ID");
            packet.ReadUInt32("Bid");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry");
            packet.ReadUInt32("Unk UInt32 4");
            packet.ReadUInt32("Unk UInt32 5");
        }


        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS)]
        [Parser(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS)]
        public static void HandleAuctionListBidderItems(Packet packet)
        {
            packet.ReadGuid("Auctioneer GUID");
            packet.ReadUInt32("List From");
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS))
                return;

            var count = packet.ReadUInt32("Outbidded Count");
            for (var i = 0; i < count; ++i)
                packet.ReadUInt32("Auction Id", i);
        }

        [Parser(Opcode.SMSG_AUCTION_BIDDER_LIST_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_OWNER_LIST_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_RESULT)]
        public static void HandleAuctionListBidderResult(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Auction Id", i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", i);
                for (var j = 0; j < 7; ++j)
                {
                    packet.ReadUInt32("Item Enchantment ID", i, j);
                    packet.ReadUInt32("Item Enchantment Duration", i, j);
                    packet.ReadUInt32("Item Enchantment Charges", i, j);
                }
                packet.ReadInt32("Item Random Property ID", i);
                packet.ReadUInt32("Item Suffix", i);
                packet.ReadUInt32("Item Count", i);
                packet.ReadInt32("Item Spell Charges", i);
                //packet.ReadEnum<ItemFlag>("Item Flags", TypeCode.UInt32, i);
                packet.ReadUInt32("Unk UInt32 1", i);
                packet.ReadGuid("Owner", i);
                packet.ReadUInt32("Start Bid", i);
                packet.ReadUInt32("Out Bid", i);
                packet.ReadUInt32("Buyout ", i);
                packet.ReadUInt32("Time Left", i);
                packet.ReadGuid("Bidder", i);
                packet.ReadUInt32("Bid", i);
            }

            packet.ReadUInt32("Own Count");
            packet.ReadUInt32("Unk UInt32 1");
        }

        [Parser(Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION)]
        public static void HandleAuctionRemovedNotification(Packet packet)
        {

        }

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionListPendingSales(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionListPendingSalesResult(Packet packet)
        {
            var count = packet.ReadUInt32("Pending Sales Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadCString("Unk String 1", i);
                packet.ReadCString("Unk String 2", i);
                packet.ReadUInt32("Unk UInt32 1", i);
                packet.ReadUInt32("Unk UInt32 2", i);
                packet.ReadSingle("Unk Single", i);
            }
        }
    }
}
