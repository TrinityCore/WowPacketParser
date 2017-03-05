using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AuctionHouseHandler
    {
        // TODO: Use this in more places
        private static readonly TypeCode AuctionSize = ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623) ? TypeCode.UInt64 : TypeCode.UInt32;

        [Parser(Opcode.MSG_AUCTION_HELLO)]
        public static void HandleAuctionHello(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.Translator.ReadUInt32("AuctionHouse ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                packet.Translator.ReadBool("Enabled");
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.Translator.ReadGuid("Auctioneer GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_2_2a_10505))
            {
                packet.Translator.ReadGuid("Item Guid");
                packet.Translator.ReadUInt32("Item Count");
            }
            else
            {
                if (!packet.CanRead()) // dword_F4955C <= (unsigned int)dword_F49578[v13]
                    return;

                var count = packet.Translator.ReadUInt32("Count");
                for (int i = 0; i < count; ++i)
                {
                    packet.Translator.ReadGuid("Item Guid", i);
                    packet.Translator.ReadInt32("", i);
                }
            }

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
            {
                packet.Translator.ReadUInt64("Bid");
                packet.Translator.ReadUInt64("Buyout");
            }
            else
            {
                packet.Translator.ReadUInt32("Bid");
                packet.Translator.ReadUInt32("Buyout");
            }

            packet.Translator.ReadUInt32("Expire Time");
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.Translator.ReadGuid("Auctioneer GUID");
            packet.Translator.ReadUInt32("Auction Id");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.Translator.ReadGuid("Auctioneer GUID");
            packet.Translator.ReadUInt32("Auction House Id");
            packet.Translator.ReadCString("Search Pattern");
            packet.Translator.ReadByte("Min Level");
            packet.Translator.ReadByte("Max Level");
            packet.Translator.ReadInt32("Slot ID");
            packet.Translator.ReadInt32("Category");
            packet.Translator.ReadInt32("Sub Category");
            packet.Translator.ReadInt32("Quality");
            packet.Translator.ReadByte("Usable");
            packet.Translator.ReadBool("GetAll");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadByte("Unk Byte");
            var count = packet.Translator.ReadByte("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Unk Byte 2", i);
                packet.Translator.ReadByte("Unk Byte 3", i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.Translator.ReadGuid("Auctioneer GUID");
            packet.Translator.ReadUInt32("Auction Id");

            // I think Blizz got this wrong. Auction Id should be 64 on 4.x, not price.
            packet.Translator.ReadValue("Price", AuctionSize);
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Auction ID");
            var action = packet.Translator.ReadUInt32E<AuctionHouseAction>("Action");
            var error = packet.Translator.ReadUInt32E<AuctionHouseError>("Error");

            if (error == AuctionHouseError.Inventory)
                packet.Translator.ReadUInt32E<InventoryResult>("Equip Error");

            switch (error)
            {
                case AuctionHouseError.Ok:
                    if (action == AuctionHouseAction.Bid)
                        packet.Translator.ReadValue("Diff", AuctionSize);
                    break;
                case AuctionHouseError.HigherBid:
                    packet.Translator.ReadGuid("Bidder");
                    packet.Translator.ReadValue("Bid", AuctionSize);
                    packet.Translator.ReadValue("Diff", AuctionSize);
                    break;
            }
        }

        [Parser(Opcode.SMSG_AUCTION_BIDDER_NOTIFICATION)]
        public static void HandleAuctionBidderNotification(Packet packet)
        {
            packet.Translator.ReadUInt32("Auction House ID");
            packet.Translator.ReadUInt32("Auction ID");
            packet.Translator.ReadGuid("Bidder GUID");
            packet.Translator.ReadValue("Bid", AuctionSize);
            packet.Translator.ReadValue("Diff", AuctionSize);
            packet.Translator.ReadUInt32<ItemId>("Item Entry");
            packet.Translator.ReadUInt32("Unk UInt32 1");
        }

        [Parser(Opcode.SMSG_AUCTION_OWNER_NOTIFICATION)]
        public static void HandleAuctionOwnerNotification(Packet packet)
        {
            packet.Translator.ReadUInt32("Auction ID");
            packet.Translator.ReadValue("Bid", AuctionSize);
            packet.Translator.ReadValue("Diff", AuctionSize);
            packet.Translator.ReadGuid("Bidder GUID");
            packet.Translator.ReadUInt32<ItemId>("Item Entry");
            packet.Translator.ReadUInt32("Unk UInt32 4");
            packet.Translator.ReadSingle("Unk float 5");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS)]
        [Parser(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS)]
        public static void HandleAuctionListBidderItems(Packet packet)
        {
            packet.Translator.ReadGuid("Auctioneer GUID");
            packet.Translator.ReadUInt32("List From");
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, Direction.ClientToServer))
                return;

            var count = packet.Translator.ReadUInt32("Outbidded Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadUInt32("Auction Id", i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT)]
        [Parser(Opcode.SMSG_AUCTION_LIST_RESULT)]
        public static void HandleAuctionListItemsResult(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Auction Id", i);
                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);

                int enchantmentCount = ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005) ? 10 : ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545) ? 9 : ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing) ? 7 : 6;
                for (var j = 0; j < enchantmentCount; ++j)
                {
                    packet.Translator.ReadUInt32("Item Enchantment ID", i, j);
                    packet.Translator.ReadUInt32("Item Enchantment Duration", i, j);
                    packet.Translator.ReadUInt32("Item Enchantment Charges", i, j);
                }

                packet.Translator.ReadInt32("Item Random Property ID", i);
                packet.Translator.ReadUInt32("Item Suffix", i);
                packet.Translator.ReadUInt32("Item Count", i);
                packet.Translator.ReadInt32("Item Spell Charges", i);
                //packet.Translator.ReadUInt32E<ItemProtoFlags>("Item Flags", i);
                packet.Translator.ReadUInt32("Unk UInt32 1", i);
                packet.Translator.ReadGuid("Owner", i);
                packet.Translator.ReadValue("Start Bid", AuctionSize, i);
                packet.Translator.ReadValue("Out Bid", AuctionSize, i);
                packet.Translator.ReadValue("Buyout ", AuctionSize, i);
                packet.Translator.ReadUInt32("Time Left", i);
                packet.Translator.ReadGuid("Bidder", i);
                packet.Translator.ReadValue("Bid", AuctionSize, i);
            }

            packet.Translator.ReadUInt32("Total item count");
            packet.Translator.ReadUInt32("Desired delay time");
        }

        [Parser(Opcode.SMSG_AUCTION_REMOVED_NOTIFICATION)]
        public static void HandleAuctionRemovedNotification(Packet packet)
        {
            packet.Translator.ReadInt32("Auction ID");
            packet.Translator.ReadUInt32<ItemId>("Item Entry");
            packet.Translator.ReadInt32("Item Random Property ID");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionListPendingSales(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_PENDING_SALES)]
        public static void HandleAuctionListPendingSalesResult(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Pending Sales Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadCString("Unk String 1", i);
                packet.Translator.ReadCString("Unk String 2", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                    packet.Translator.ReadUInt64("Unk UInt32 1", i);
                else
                    packet.Translator.ReadUInt32("Unk UInt32 1", i);
                packet.Translator.ReadUInt32("Unk UInt32 2", i);
                packet.Translator.ReadSingle("Unk Single", i);
            }
        }
    }
}
