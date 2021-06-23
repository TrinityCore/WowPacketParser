using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class AuctionHouseHandler
    {
        [Parser(Opcode.CMSG_AUCTION_GET_COMMODITY_QUOTE)]
        public static void HandleAuctionHouseGetCommodityQuote(Packet packet)
        {
            packet.ReadPackedGuid128("Auctioneer");
            packet.ReadInt32<ItemId>("ItemID");
            packet.ReadUInt32("Quantity");
            if (packet.ReadBit())
                ReadAddonInfo(packet, "TaintedBy");
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
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                    packet.ReadUInt64("TotalPrice");
                else
                    packet.ReadUInt32("TotalPrice");
            }

            if (hasQuantity)
                packet.ReadUInt32("Quantity");

            if (hasQuoteDuration)
                packet.ReadInt64("QuoteDuration");
        }

        public static void ReadAddonInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            var nameLength = (int)packet.ReadBits(10);
            var versionLength = (int)packet.ReadBits(10);
            packet.ReadBit("Loaded", indexes);
            packet.ReadBit("Disabled", indexes);
            if (nameLength > 1)
                packet.ReadDynamicString("Name", nameLength - 1, indexes);

            if (versionLength > 1)
                packet.ReadDynamicString("Version", versionLength - 1, indexes);
        }
    }
}
