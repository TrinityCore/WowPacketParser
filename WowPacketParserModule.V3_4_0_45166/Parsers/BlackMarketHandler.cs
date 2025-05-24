using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class BlackMarketHandler
    {
        public static void ReadBlackMarketItem(Packet packet, params object[] idx)
        {
            packet.ReadInt32("MarketID", idx);
            packet.ReadInt32<UnitId>("SellerNPC", idx);
            packet.ReadInt32("Quantity", idx);
            packet.ReadUInt64("MinBid", idx);
            packet.ReadUInt64("MinIncrement", idx);
            packet.ReadUInt64("CurrentBid", idx);
            packet.ReadInt32("SecondsRemaining", idx);
            packet.ReadInt32("NumBids", idx);
            Substructures.ItemHandler.ReadItemInstance(packet, "Item", idx);
            packet.ResetBitReader();
            packet.ReadBit("HighBid", idx);
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBlackMarketBidOnItemResult(Packet packet)
        {
            packet.ReadInt32("MarketID");
            packet.ReadInt32("Result");
            Substructures.ItemHandler.ReadItemInstance(packet, "Item");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_OUTBID, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BLACK_MARKET_WON, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBlackMarketOutbidOrWon(Packet packet)
        {
            packet.ReadInt32("MarketID");
            packet.ReadInt32("RandomPropertiesID");
            Substructures.ItemHandler.ReadItemInstance(packet, "Item");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            packet.ReadTime64("LastUpdateID");
            var count = packet.ReadInt32("ItemsCount");

            for (var i = 0; i < count; ++i)
                ReadBlackMarketItem(packet, "Items", i);
        }

        [Parser(Opcode.CMSG_BLACK_MARKET_OPEN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBlackMarketOpen(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
        }
    }
}
