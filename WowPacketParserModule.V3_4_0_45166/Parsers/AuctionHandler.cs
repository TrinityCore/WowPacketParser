using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class AuctionHandler
    {
        public static void ReadClientAuctionOwnerNotification(Packet packet, params object[] idx)
        {
            packet.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.ReadUInt64("BidAmount", idx);
            Substructures.ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        public static void ReadAuctionBucketKey(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("ItemID", 20, idx);
            var hasBattlePetSpeciesID = packet.ReadBit("HasBattlePetSpeciesID", idx);
            packet.ReadBits("ItemLevel", 11, idx);
            var hasSuffixItemNameDescriptionID = packet.ReadBit("HasSuffixItemNameDescriptionID", idx);

            if (hasBattlePetSpeciesID)
                packet.ReadUInt16("BattlePetSpeciesID", idx);

            if (hasSuffixItemNameDescriptionID)
                packet.ReadUInt16("SuffixItemNameDescriptionID", idx);
        }

        public static void ReadCliAuctionItem(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            var hasItem = packet.ReadBit("HasItem", idx);
            var enchantmentsCount = packet.ReadBits("EnchantmentsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);
            var hasMinBid = packet.ReadBit("HasMinBid", idx);

            var hasMinIncrement = packet.ReadBit("HasMinIncrement", idx);
            var hasBuyoutPrice = packet.ReadBit("HasBuyoutPrice", idx);
            var hasUnitPrice = packet.ReadBit("HasUnitPrice", idx);
            var hasCensorServerSideInfo = packet.ReadBit("HasCensorServerSideInfo", idx);
            var hasCensorBidInfo = packet.ReadBit("HasCensorBidInfo", idx);
            var hasAuctionBucketKey = packet.ReadBit("HasAuctionBucketKey", idx);
            var hasCreator = packet.ReadBit("HasCreator", idx);
            var hasBidder = false;
            var hasBidAmount = false;

            if (!hasCensorBidInfo)
            {
                hasBidder = packet.ReadBit("HasBidder", idx);
                hasBidAmount = packet.ReadBit("HasBidAmount", idx);
            }

            packet.ResetBitReader();

            if (hasItem)
                Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadInt32("AuctionItemID", idx);
            packet.ReadPackedGuid128("Owner", idx);
            packet.ReadInt32("DurationLeft", idx);
            packet.ReadByte("DeleteReason", idx);
            packet.ReadInt32("Unk_442", idx);

            for (int i = 0; i < enchantmentsCount; i++)
                Substructures.ItemHandler.ReadItemEnchantData(packet, idx, "Enchantments", i);

            if (hasMinBid)
                packet.ReadUInt64("MinBid", idx);

            if (hasMinIncrement)
                packet.ReadUInt64("MinIncrement", idx);

            if (hasBuyoutPrice)
                packet.ReadUInt64("BuyoutPrice", idx);

            if (hasUnitPrice)
                packet.ReadUInt64("UnitPrice", idx);

            if (!hasCensorServerSideInfo)
            {
                packet.ReadPackedGuid128("ItemGUID", idx);
                packet.ReadPackedGuid128("OwnerAccountID", idx);
                packet.ReadInt32("EndTime", idx);
            }

            if (hasCreator)
                packet.ReadPackedGuid128("Creator", idx);

            if (!hasCensorBidInfo)
            {
                if (hasBidder)
                    packet.ReadPackedGuid128("Bidder", idx);

                if (hasBidAmount)
                    packet.ReadInt64("BidAmount", idx);
            }

            for (int i = 0; i < gemsCount; i++)
                Substructures.ItemHandler.ReadItemGemData(packet, idx, "Gems", i);

            if (hasAuctionBucketKey)
                ReadAuctionBucketKey(packet, idx, "AuctionBucketKey");
        }

        public static void ReadClientAuctionBidderNotification(Packet packet, params object[] idx)
        {
            packet.ReadInt32("AuctionHouseID", idx); // CONFIRM?
            packet.ReadInt32<ItemId>("AuctionItemID", idx); // CONFIRM?
            packet.ReadPackedGuid128("Bidder", idx);
            Substructures.ItemHandler.ReadItemInstance(packet, idx, "Item");
        }

        [Parser(Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionClosedNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadSingle("ProceedsMailDelay");
            packet.ReadBit("Sold");
        }

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionCommandResult(Packet packet)
        {
            packet.ReadUInt32("AuctionItemID");
            packet.ReadUInt32E<AuctionHouseAction>("Command");
            packet.ReadUInt32E<AuctionHouseError>("ErrorCode");
            packet.ReadUInt32("BagResult");
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt64("MinIncrement");
            packet.ReadUInt64("Money");
            packet.ReadUInt32("DesiredDelay");
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleServerAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadUInt32("PurchasedItemDeliveryDelay");
            packet.ReadUInt32("CancelledItemDeliveryDelay");
            packet.ReadUInt32("DeliveryDelay");
            packet.ReadBit("OpenForBusiness");

            CoreParsers.NpcHandler.LastGossipOption.Reset();
            CoreParsers.NpcHandler.TempGossipOptionPOI.Reset();
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDER_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_AUCTION_LIST_OWNER_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");
            packet.ReadUInt32("TotalCount");
            packet.ReadUInt32("DesiredDelay");

            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");
            packet.ReadInt32("TotalCount");
            packet.ReadInt32("DesiredDelay");

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);

            packet.ResetBitReader();
            packet.ReadBit("OnlyUsable");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_PENDING_SALES_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListPendingSalesResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; i++)
                MailHandler.ReadMailListEntry(packet, i, "MailListEntry");
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");

            packet.ReadUInt64("BidAmount");
            packet.ReadUInt64("MinIncrement");
        }

        [Parser(Opcode.SMSG_AUCTION_OWNER_BID_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionOwnerBidNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadUInt64("MinIncrement");
            packet.ReadPackedGuid128("Bidder");
        }

        [Parser(Opcode.SMSG_AUCTION_REPLICATE_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_AUCTION_WON_NOTIFICATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionWonNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");
        }

        [Parser(Opcode.CMSG_AUCTION_HELLO_REQUEST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleClientAuctionHello(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDER_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListBidderItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var auctionIdsCount = packet.ReadBits(7);
            var taintedBy = packet.ReadBit();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < auctionIdsCount; i++)
                packet.ReadUInt32("AuctionID", i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("Offset");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            packet.ReadInt32E<ItemQuality>("Quality");
            var sort = packet.ReadByte("SortCount");
            var knownPetsCount = packet.ReadUInt32("KnownPetsCount");
            packet.ReadByte("MaxPetLevel");

            for (int i = 0; i < knownPetsCount; ++i)
                packet.ReadByte("KnownPets", i);

            var taintedBy = packet.ReadBit();
            var nameLength = packet.ReadBits(8);
            packet.ReadWoWString("Name", nameLength);

            var classFiltersCount = packet.ReadBits("ClassFiltersCount", 3);

            packet.ReadBit("OnlyUsable");
            packet.ReadBit("ExactMatch");

            packet.ResetBitReader();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (int i = 0; i < classFiltersCount; ++i)
            {
                packet.ReadInt32E<ItemClass>("ItemClass", "ClassFilters", i);

                var subClassFiltersCount = packet.ReadBits("SubClassFiltersCount", 5, "ClassFilters", i);
                for (int j = 0; j < subClassFiltersCount; ++j)
                {
                    packet.ReadUInt64("ItemSubclass", "ClassFilters", i, "SubClassFilters", j);
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

        [Parser(Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListOwnerItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        [Parser(Opcode.CMSG_AUCTION_PLACE_BID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionPlaceBid(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionID");
            packet.ReadInt64("BidAmount");

            var taintedBy = packet.ReadBit();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        [Parser(Opcode.CMSG_AUCTION_REMOVE_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionRemoveItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("AuctionID");
            packet.ReadInt32("ItemID");

            var taintedBy = packet.ReadBit();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        [Parser(Opcode.CMSG_AUCTION_REPLICATE_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionReplicateItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("ChangeNumberGlobal");
            packet.ReadInt32("ChangeNumberCursor");
            packet.ReadInt32("ChangeNumberTombstone");
            packet.ReadInt32("Count");

            var taintedBy = packet.ReadBit();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionSellItem(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt64("MinBit");
            packet.ReadInt64("BuyoutPrice");
            packet.ReadInt32("RunTime");

            var taintedBy = packet.ReadBit();

            var count = packet.ReadBits("ItemsCount", 5);
            packet.ResetBitReader();

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadInt32("UseCount");
            }
        }

        public static void ReadBucketInfo(Packet packet, int index)
        {
            ReadAuctionBucketKey(packet, index, "Key");

            packet.ReadInt32("TotalQuantity", index);
            packet.ReadInt32("RequiredLevel", index);
            packet.ReadUInt64("MinPrice", index);
            var itemModifiedAppearanceIDsCount = packet.ReadUInt32();
            for (var i = 0u; i < itemModifiedAppearanceIDsCount; ++i)
                packet.ReadInt32("ItemModifiedAppearanceID", index, i);

            packet.ResetBitReader();
            var hasMaxBattlePetQuality = packet.ReadBit();
            var hasMaxBattlePetLevel = packet.ReadBit();
            var hasBattlePetBreedID = packet.ReadBit();
            var hasBattlePetLevelMask = packet.ReadBit();
            packet.ReadBit("ContainsOwnerItem", index);
            packet.ReadBit("ContainsOnlyCollectedAppearances", index);

            if (hasMaxBattlePetQuality)
                packet.ReadByte("MaxBattlePetQuality", index);

            if (hasMaxBattlePetLevel)
                packet.ReadByte("MaxBattlePetLevel", index);

            if (hasBattlePetBreedID)
                packet.ReadByte("BattlePetBreedID", index);

            if (hasBattlePetLevelMask)
                packet.ReadUInt32("BattlePetLevelMask", index);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BUCKETS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListBucketsResult(Packet packet)
        {
            var bucketCount = packet.ReadUInt32();
            packet.ReadUInt32("DesiredDelay");
            packet.ReadInt32("Unknown830_0");
            packet.ReadInt32("Unknown830_1");
            packet.ReadBit("BrowseMode");
            packet.ReadBit("HasMoreResults");

            for (var i = 0; i < bucketCount; ++i)
                ReadBucketInfo(packet, i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_OWNED_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListOwnedItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32();
            var soldItemsCount = packet.ReadInt32();
            packet.ReadUInt32("DesiredDelay");
            packet.ReadBit("HasMoreResults");

            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);

            for (var i = 0; i < soldItemsCount; ++i)
                ReadCliAuctionItem(packet, "SoldItems", i);
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDED_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListBiddedItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32();
            packet.ReadUInt32("DesiredDelay");
            packet.ReadBit("HasMoreResults");

            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.SMSG_AUCTION_GET_COMMODITY_QUOTE_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionHouseGetCommodityQuoteResult(Packet packet)
        {
            var hasTotalPrice = packet.ReadBit();
            var hasQuantity = packet.ReadBit();
            var hasQuoteDuration = packet.ReadBit();
            packet.ReadInt32("ItemID");
            packet.ReadUInt32("DesiredDelay");

            if (hasTotalPrice)
                packet.ReadUInt64("TotalPrice");

            if (hasQuantity)
                packet.ReadUInt32("Quantity");

            if (hasQuoteDuration)
                packet.ReadInt64("QuoteDuration");
        }

        public static void ReadAuctionFavoriteInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Order", idx);
            packet.ReadUInt32("ItemID", idx);
            packet.ReadUInt32("ItemLevel", idx);
            packet.ReadUInt32("BattlePetSpeciesID", idx);
            packet.ReadUInt32("SuffixItemNameDescriptionID", idx);
        }

        [Parser(Opcode.SMSG_AUCTION_FAVORITE_LIST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionFavoriteList(Packet packet)
        {
            packet.ReadUInt32("DesiredDelay");
            var itemsCount = packet.ReadBits(7);

            for (var i = 0; i < itemsCount; ++i)
                ReadAuctionFavoriteInfo(packet, "FavoriteInfo", i);
        }

        public static void ReadAuctionListFilterSubClass(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("InvTypeMask", idx);
            packet.ReadInt32("ItemSubclass", idx);
        }

        public static void ReadAuctionListFilterClass(Packet packet, params object[] idx)
        {
            packet.ReadInt32("FilterClass", idx);
            packet.ResetBitReader();
            var subClassFilterCount = packet.ReadBits("SubClassFilterCount", 5, idx);
            for (var i = 0; i < subClassFilterCount; i++)
                ReadAuctionListFilterSubClass(packet, i, "SubClassFilter", i, idx);
        }

        public static void ReadAuctionSortDef(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadByte("SortOrder", idx);
            packet.ReadBit("ReverseSort", idx);
        }

        [Parser(Opcode.CMSG_AUCTION_BROWSE_QUERY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionBrowseQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            packet.ReadByte("Unused1007_1");
            packet.ReadByte("Unused1007_2");
            packet.ReadUInt32("Filters");

            var knownPetsSize = packet.ReadUInt32();
            packet.ReadByte("MaxPetLevel");
            packet.ReadUInt32("Unused1026");

            for (var i = 0; i < knownPetsSize; i++)
                packet.ReadByte("KnownPetMask", i);

            packet.ResetBitReader();
            var taintedBy = packet.ReadBit("TaintedBy");
            var nameLen = packet.ReadBits(8);
            var itemClassFilterSize = packet.ReadBits(3);
            var sortsSize = packet.ReadBits(2);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            packet.ReadWoWString("Name", nameLen);

            for (var i = 0; i < itemClassFilterSize; i++)
                ReadAuctionListFilterClass(packet, i, "FilterClass");

            for (var i = 0; i < sortsSize; i++)
                ReadAuctionSortDef(packet, i, "SortDef");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS_BY_BUCKET_KEY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListItemsByBucketKey(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");
            packet.ReadByte("Unknown830");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            ReadAuctionBucketKey(packet, "BucketKey");

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < sortCount; i++)
                ReadAuctionSortDef(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS_BY_ITEM_ID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListItemsByItemID(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("ItemID");
            packet.ReadInt32("SuffixItemNameDescriptionID");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < sortCount; i++)
                ReadAuctionSortDef(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_OWNED_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListOwnedItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < sortCount; i++)
                ReadAuctionSortDef(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDED_ITEMS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListBiddedItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();

            var auctionIdsCount = packet.ReadBits(7);
            var sortCount = packet.ReadBits(2);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < auctionIdsCount; i++)
                packet.ReadUInt32("AuctionID", i);

            for (var i = 0; i < sortCount; i++)
                ReadAuctionSortDef(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BUCKETS_BY_BUCKET_KEYS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionListBucketsByBucketKeys(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");

            var taintedBy = packet.ReadBit();

            var bucketKeysCount = packet.ReadBits(7);
            var sortCount = packet.ReadBits(2);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < bucketKeysCount; i++)
                ReadAuctionBucketKey(packet, i);

            for (var i = 0; i < sortCount; i++)
                ReadAuctionSortDef(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_GET_COMMODITY_QUOTE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_AUCTION_CONFIRM_COMMODITIES_PURCHASE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionHouseGetCommodityQuote(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32<ItemId>("ItemID");
            packet.ReadUInt32("Quantity");
            if (packet.ReadBit())
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        [Parser(Opcode.CMSG_AUCTION_CANCEL_COMMODITIES_PURCHASE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionCancelCommoditiesPurchase(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            if (packet.ReadBit())
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");
        }

        public static void ReadAuctionItemForSale(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt32("UseCount", idx);
        }

        [Parser(Opcode.CMSG_AUCTION_SELL_COMMODITY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionSellCommodity(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt64("UnitPrice");
            packet.ReadUInt32("Runtime");

            var taintedBy = packet.ReadBit();
            var itemsCount = packet.ReadBits(6);

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < itemsCount; i++)
                ReadAuctionItemForSale(packet, i);
        }

        [Parser(Opcode.CMSG_AUCTION_SET_FAVORITE_ITEM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionSetFavoriteItem(Packet packet)
        {
            packet.ReadBit("IsNotFavorite");
            ReadAuctionFavoriteInfo(packet, "FavoriteInfo");
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_PENDING_SALES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAuctionZero(Packet packet)
        {
        }
    }
}
