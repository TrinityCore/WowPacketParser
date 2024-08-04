using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class AuctionHouseHandler
    {
        public static void ReadAuctionSortDef(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadByte("SortOrder", idx);
            else
                packet.ReadBits("SortOrder", 4, idx);
            packet.ReadBit("ReverseSort", idx);
        }

        public static void ReadAuctionListFilterSubClass(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ItemSubclass", idx);
            packet.ReadUInt64("InvTypeMask", idx);
        }

        public static void ReadAuctionListFilterClass(Packet packet, params object[] idx)
        {
            packet.ReadInt32("FilterClass", idx);
            packet.ResetBitReader();
            var subClassFilterCount = packet.ReadBits("SubClassFilterCount", 5, idx);
            for (var i = 0; i < subClassFilterCount; i++)
                ReadAuctionListFilterSubClass(packet, i, "SubClassFilter", i, idx);
        }

        public static void ReadAuctionBucketKey(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBits("ItemID", 20, idx);

            var hasBattlePetSpeciesID = packet.ReadBit();

            packet.ReadBits("ItemLevel", 11, idx);

            var hasSuffixItemNameDescriptionID = packet.ReadBit();

            if (hasBattlePetSpeciesID)
                packet.ReadUInt16("BattlePetSpeciesID", idx);

            if (hasSuffixItemNameDescriptionID)
                packet.ReadUInt16("SuffixItemNameDescriptionID", idx);
        }

        [Parser(Opcode.CMSG_AUCTION_BROWSE_QUERY)]
        public static void HandleAuctionBrowseQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_7_48676))
            {
                packet.ReadByte("Unused1007_1");
                packet.ReadByte("Unused1007_2");
            }
            packet.ReadUInt32("Filters");

            var knownPetsSize = packet.ReadUInt32();
            packet.ReadByte("MaxPetLevel");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadUInt32("Unused1026");

            for (var i = 0; i < knownPetsSize; i++)
                packet.ReadByte("KnownPetMask", i);

            packet.ResetBitReader();
            var taintedBy = packet.ReadBit("TaintedBy");
            var nameLen = packet.ReadBits(8);
            var itemClassFilterSize = packet.ReadBits(3);
            var sortsSize = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortsSize; i++)
                    ReadAuctionSortDef(packet, i, "SortDef");
            }

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            packet.ReadWoWString("Name", nameLen);

            for (var i = 0; i < itemClassFilterSize; i++)
                ReadAuctionListFilterClass(packet, i, "FilterClass");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortsSize; i++)
                    ReadAuctionSortDef(packet, i, "SortDef");
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BIDDED_ITEMS)]
        public static void HandleAuctionListBiddedItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();

            var auctionIdsCount= packet.ReadBits(7);
            var sortCount = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < auctionIdsCount; i++)
                packet.ReadUInt32("AuctionID", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_BUCKETS_BY_BUCKET_KEYS)]
        public static void HandleAuctionListBucketsByBucketKeys(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");

            var taintedBy = packet.ReadBit();

            var bucketKeysCount = packet.ReadBits(7);
            var sortCount = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            for (var i = 0; i < bucketKeysCount; i++)
                ReadAuctionBucketKey(packet, i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS_BY_BUCKET_KEY)]
        public static void HandleAuctionListItemsByBucketKey(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");
            packet.ReadByte("Unknown830");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }

            ReadAuctionBucketKey(packet, "BucketKey");

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_ITEMS_BY_ITEM_ID)]
        public static void HandleAuctionListItemsByItemID(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32("ItemID");
            packet.ReadInt32("SuffixItemNameDescriptionID");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }
        }

        [Parser(Opcode.CMSG_AUCTION_LIST_OWNED_ITEMS)]
        public static void HandleAuctionListOwnedItems(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadUInt32("Offset");

            var taintedBy = packet.ReadBit();
            var sortCount = packet.ReadBits(2);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
            {
                for (var i = 0; i < sortCount; i++)
                    ReadAuctionSortDef(packet, i);
            }
        }
    }
}
