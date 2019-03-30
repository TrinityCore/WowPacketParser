using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AuctionHouseHandler
    {
        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadInt32("AuctionItemID", idx);

            packet.ReadPackedGuid128("Owner", idx);

            packet.ReadUInt64("MinBid", idx);
            packet.ReadUInt64("MinIncrement", idx);
            packet.ReadUInt64("BuyoutPrice", idx);

            packet.ReadInt32("DurationLeft", idx);
            packet.ReadByte("DeleteReason", idx);

            var enchantmentsCount = packet.ReadBits("EnchantmentsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);

            var censorServerSideInfo = packet.ReadBit("CensorServerSideInfo");
            var censorBidInfo = packet.ReadBit("CensorBidInfo");

            packet.ResetBitReader();

            for (int i = 0; i < gemsCount; i++)
            {
                ItemHandler.ReadItemGemInstanceData(packet, "Gems", idx, i);
            }

            for (int i = 0; i < enchantmentsCount; i++)
            {
                packet.ReadInt32("ID", idx, i);
                packet.ReadUInt32("Expiration", idx, i);
                packet.ReadInt32("Charges", idx, i);
                packet.ReadByte("Slot", idx, i);
            }

            if (!censorServerSideInfo)
            {
                packet.ReadPackedGuid128("ItemGUID", idx);
                packet.ReadPackedGuid128("OwnerAccountID", idx);
                packet.ReadInt32("EndTime", idx);
            }

            if (!censorBidInfo)
            {
                packet.ReadPackedGuid128("Bidder", idx);
                packet.ReadInt64("BidAmount", idx);
            }
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");

            packet.ReadInt32("TotalCount");
            packet.ReadInt32("DesiredDelay");

            packet.ReadBit("OnlyUsable");

            packet.ResetBitReader();

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.ReadInt32("Offset");
            packet.ReadPackedGuid128("Auctioneer");

            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            packet.ReadInt32E<ItemQuality>("Quality");
            var sort = packet.ReadByte("SortCount");
            var knownPetsCount = packet.ReadUInt32("KnownPetsCount");
            packet.ReadByte("MaxPetLevel");

            for (int i = 0; i < knownPetsCount; ++i)
                packet.ReadByte("KnownPets", i);

            var nameLength = packet.ReadBits(8);
            packet.ReadWoWString("Name", nameLength);

            var classFiltersCount = packet.ReadBits("ClassFiltersCount", 3);

            packet.ReadBit("OnlyUsable");
            packet.ReadBit("ExactMatch");

            packet.ResetBitReader();

            for (int i = 0; i < classFiltersCount; ++i)
            {
                packet.ReadInt32E<ItemClass>("ItemClass", "ClassFilters", i);

                var subClassFiltersCount = packet.ReadBits("SubClassFiltersCount", 5, "ClassFilters", i);
                for (int j = 0; j < subClassFiltersCount; ++j)
                {
                    packet.ReadInt32("ItemSubclass", "ClassFilters", i, "SubClassFilters", j);
                    packet.ReadUInt32("InvTypeMask", "ClassFilters", i, "SubClassFilters", j);
                }
            }

            var size = packet.ReadInt32("DataSize");
            var data = packet.ReadBytes(size);
            var sorts = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            for (var i = 0; i < sort; ++i)
            {
                sorts.ReadByte("Type", i);
                sorts.ReadByte("Direction", i);
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

        [Parser(Opcode.SMSG_AUCTION_REPLICATE_RESPONSE)]
        public static void HandleAuctionReplicateResponse(Packet packet)
        {
            packet.ReadUInt32("Result");
            packet.ReadUInt32("DesiredDelay");
            packet.ReadUInt32("ChangeNumberGlobal");
            packet.ReadUInt32("ChangeNumberCursor");
            packet.ReadUInt32("ChangeNumberTombstone");

            var itemsCount = packet.ReadInt32("ItemsCount");
            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }
    }
}
