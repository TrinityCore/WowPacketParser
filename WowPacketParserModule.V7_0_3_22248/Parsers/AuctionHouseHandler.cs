using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AuctionHouseHandler
    {
        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, idx);

            packet.Translator.ReadInt32("Count", idx);
            packet.Translator.ReadInt32("Charges", idx);
            packet.Translator.ReadInt32("Flags", idx);
            packet.Translator.ReadInt32("AuctionItemID", idx);

            packet.Translator.ReadPackedGuid128("Owner", idx);

            packet.Translator.ReadUInt64("MinBid", idx);
            packet.Translator.ReadUInt64("MinIncrement", idx);
            packet.Translator.ReadUInt64("BuyoutPrice", idx);

            packet.Translator.ReadInt32("DurationLeft", idx);
            packet.Translator.ReadByte("DeleteReason", idx);

            var enchantmentsCount = packet.Translator.ReadBits("EnchantmentsCount", 4, idx);
            var gemsCount = packet.Translator.ReadBits("GemsCount", 2, idx);

            var censorServerSideInfo = packet.Translator.ReadBit("CensorServerSideInfo");
            var censorBidInfo = packet.Translator.ReadBit("CensorBidInfo");

            packet.Translator.ResetBitReader();

            for (int i = 0; i < gemsCount; i++)
            {
                ItemHandler.ReadItemGemInstanceData(packet, "Gems", idx, i);
            }

            for (int i = 0; i < enchantmentsCount; i++)
            {
                packet.Translator.ReadInt32("ID", idx, i);
                packet.Translator.ReadUInt32("Expiration", idx, i);
                packet.Translator.ReadInt32("Charges", idx, i);
                packet.Translator.ReadByte("Slot", idx, i);
            }

            if (!censorServerSideInfo)
            {
                packet.Translator.ReadPackedGuid128("ItemGUID", idx);
                packet.Translator.ReadPackedGuid128("OwnerAccountID", idx);
                packet.Translator.ReadInt32("EndTime", idx);
            }

            if (!censorBidInfo)
            {
                packet.Translator.ReadPackedGuid128("Bidder", idx);
                packet.Translator.ReadInt64("BidAmount", idx);
            }
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.Translator.ReadInt32("ItemsCount");

            packet.Translator.ReadInt32("TotalCount");
            packet.Translator.ReadInt32("DesiredDelay");

            packet.Translator.ReadBit("OnlyUsable");

            packet.Translator.ResetBitReader();

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.Translator.ReadInt32("Offset");
            packet.Translator.ReadPackedGuid128("Auctioneer");

            packet.Translator.ReadByte("MinLevel");
            packet.Translator.ReadByte("MaxLevel");
            packet.Translator.ReadInt32E<ItemQuality>("Quality");
            var sort = packet.Translator.ReadByte("SortCount");
            var knownPetsCount = packet.Translator.ReadUInt32("KnownPetsCount");
            packet.Translator.ReadByte("MaxPetLevel");

            for (int i = 0; i < knownPetsCount; ++i)
                packet.Translator.ReadByte("KnownPets", i);

            var nameLength = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Name", nameLength);

            var classFiltersCount = packet.Translator.ReadBits("ClassFiltersCount", 3);

            packet.Translator.ReadBit("OnlyUsable");
            packet.Translator.ReadBit("ExactMatch");

            packet.Translator.ResetBitReader();

            for (int i = 0; i < classFiltersCount; ++i)
            {
                packet.Translator.ReadInt32E<ItemClass>("ItemClass", "ClassFilters", i);

                var subClassFiltersCount = packet.Translator.ReadBits("SubClassFiltersCount", 5, "ClassFilters", i);
                for (int j = 0; j < subClassFiltersCount; ++j)
                {
                    packet.Translator.ReadInt32("ItemSubclass", "ClassFilters", i, "SubClassFilters", j);
                    packet.Translator.ReadUInt32("InvTypeMask", "ClassFilters", i, "SubClassFilters", j);
                }
            }

            var size = packet.Translator.ReadInt32("DataSize");
            var data = packet.Translator.ReadBytes(size);
            var sorts = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            for (var i = 0; i < sort; ++i)
            {
                sorts.Translator.ReadByte("Type", i);
                sorts.Translator.ReadByte("Direction", i);
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
    }
}
