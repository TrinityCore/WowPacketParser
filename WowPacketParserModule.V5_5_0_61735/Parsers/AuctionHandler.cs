using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AuctionHandler
    {
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

        public static void ReadClientAuctionOwnerNotification(Packet packet, params object[] idx)
        {
            packet.ReadInt32<ItemId>("AuctionItemID", idx);
            packet.ReadUInt64("BidAmount", idx);
            Substructures.ItemHandler.ReadItemInstance(packet, idx, "Item");
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

        public static void ReadAuctionFavoriteInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Order", idx);
            packet.ReadUInt32("ItemID", idx);
            packet.ReadUInt32("ItemLevel", idx);
            packet.ReadUInt32("BattlePetSpeciesID", idx);
            packet.ReadUInt32("SuffixItemNameDescriptionID", idx);
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO_RESPONSE)]
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

        [Parser(Opcode.SMSG_AUCTION_COMMAND_RESULT)]
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

        [Parser(Opcode.SMSG_AUCTION_WON_NOTIFICATION)]
        public static void HandleAuctionWonNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");
        }

        [Parser(Opcode.SMSG_AUCTION_OUTBID_NOTIFICATION)]
        public static void HandleAuctionOutbitNotification(Packet packet)
        {
            ReadClientAuctionBidderNotification(packet, "Info");

            packet.ReadUInt64("BidAmount");
            packet.ReadUInt64("MinIncrement");
        }

        [Parser(Opcode.SMSG_AUCTION_CLOSED_NOTIFICATION)]
        public static void HandleAuctionClosedNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadSingle("ProceedsMailDelay");
            packet.ReadBit("Sold");
        }

        [Parser(Opcode.SMSG_AUCTION_OWNER_BID_NOTIFICATION)]
        public static void HandleAuctionOwnerBidNotification(Packet packet)
        {
            ReadClientAuctionOwnerNotification(packet, "Info");

            packet.ReadUInt64("MinIncrement");
            packet.ReadPackedGuid128("Bidder");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_BUCKETS_RESULT)]
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

        [Parser(Opcode.SMSG_AUCTION_LIST_ITEMS_RESULT)]
        public static void HandleListItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32("ItemsCount");
            packet.ReadInt32("Unknown830");
            packet.ReadInt32("DesiredDelay");

            for (var i = 0; i < itemsCount; i++)
                ReadCliAuctionItem(packet, i);

            packet.ResetBitReader();
            packet.ReadBits("ListType", 2);
            packet.ReadBit("HasMoreResults");
            ReadAuctionBucketKey(packet, "BucketKey");
            packet.ReadUInt32("TotalCount");
        }

        [Parser(Opcode.SMSG_AUCTION_LIST_OWNED_ITEMS_RESULT)]
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

        [Parser(Opcode.SMSG_AUCTION_LIST_BIDDED_ITEMS_RESULT)]
        public static void HandleAuctionListBiddedItemsResult(Packet packet)
        {
            var itemsCount = packet.ReadInt32();
            packet.ReadUInt32("DesiredDelay");
            packet.ReadBit("HasMoreResults");

            for (var i = 0; i < itemsCount; ++i)
                ReadCliAuctionItem(packet, "Items", i);
        }

        [Parser(Opcode.SMSG_AUCTION_GET_COMMODITY_QUOTE_RESULT)]
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

        [Parser(Opcode.SMSG_AUCTION_FAVORITE_LIST)]
        public static void HandleAuctionFavoriteList(Packet packet)
        {
            packet.ReadUInt32("DesiredDelay");
            var itemsCount = packet.ReadBits(7);

            for (var i = 0; i < itemsCount; ++i)
                ReadAuctionFavoriteInfo(packet, "FavoriteInfo", i);
        }
    }
}
