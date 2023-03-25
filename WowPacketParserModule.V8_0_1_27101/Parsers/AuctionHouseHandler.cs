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
            packet.ReadBits("SortOrder", 4, idx); ;
            packet.ReadBit("ReverseSort", idx);
        }

        public static void ReadAuctionListFilterSubClass(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ItemSubclass", idx);
            packet.ReadUInt64("InvTypeMask", idx);
        }

        public static void ReadAuctionListFilterClass(Packet packet, params object[] idx)
        {
            packet.ReadInt32("FilterClass");
            packet.ResetBitReader();
            var subClassFilterCount = packet.ReadBits("SubClassFilterCount", 5);
            for (var i = 0; i < subClassFilterCount; i++)
                ReadAuctionListFilterSubClass(packet, i, "SubClassFilter");
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
            for (var i = 0; i < knownPetsSize; i++)
                packet.ReadByte("KnownPetMask", i);

            packet.ResetBitReader();
            var taintedBy = packet.ReadBit("TaintedBy");
            var nameLen = packet.ReadBits(8);
            var itemClassFilterSize = packet.ReadBits(3);
            var sortsSize = packet.ReadBits(2);

            for (var i = 0; i < sortsSize; i++)
                ReadAuctionSortDef(packet, i, "SortDef");

            if (taintedBy)
                AddonHandler.ReadAddOnInfo(packet, "TaintedBy");

            packet.ReadWoWString("Name", nameLen);

            for (var i = 0; i < itemClassFilterSize; i++)
                ReadAuctionListFilterClass(packet, i, "FilterClass");
        }
    }
}
