using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.CMSG_BLACK_MARKET_BID_ON_ITEM)]
        public static void HandleBlackMarketBidOnItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadInt32("MarketID");
            packet.Translator.ReadUInt64("BidAmount");
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_OUTBID)]
        [Parser(Opcode.SMSG_BLACK_MARKET_WON)]
        public static void HandleBlackMarketOutbidOrWon(Packet packet)
        {
            packet.Translator.ReadInt32("MarketID");
            packet.Translator.ReadInt32("RandomPropertiesID");
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT)]
        public static void HandleBlackMarketBidOnItemResult(Packet packet)
        {
            packet.Translator.ReadInt32("MarketID");
            packet.Translator.ReadInt32("Result");
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item");
        }

        public static void ReadBlackMarketItem(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("MarketID", idx);
            packet.Translator.ReadInt32<UnitId>("SellerNPC", idx);
            packet.Translator.ReadInt32("Quantity", idx);
            packet.Translator.ReadUInt64("MinBid", idx);
            packet.Translator.ReadUInt64("MinIncrement", idx);
            packet.Translator.ReadUInt64("CurrentBid", idx);
            packet.Translator.ReadInt32("SecondsRemaining", idx);
            packet.Translator.ReadInt32("NumBids", idx);
            V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet, "Item", idx);
            packet.Translator.ReadBit("HighBid", idx);
            packet.Translator.ResetBitReader();
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            packet.Translator.ReadTime("LastUpdateID");
            var count = packet.Translator.ReadInt32("ItemsCount");

            for (int i = 0; i < count; i++)
                ReadBlackMarketItem(packet, "Items", i);
        }
    }
}
