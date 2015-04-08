using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.CMSG_BLACK_MARKET_REQUEST_ITEMS)]
        public static void HandleBlackMarketRequestItems(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadTime("LastUpdateID");
        }

        [Parser(Opcode.CMSG_BLACK_MARKET_BID_ON_ITEM)]
        public static void HandleBlackMarketBidOnItem(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadInt32("MarketID");
            ItemHandler.ReadItemInstance(packet);
            packet.ReadUInt64("BidAmount");
        }

        [Parser(Opcode.CMSG_BLACK_MARKET_OPEN)]
        public static void HandleBlackMarketOpen(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_OPEN_RESULT)]
        public static void HandleBlackMarketOpenResult(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadBit("Open");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_OUTBID)]
        [Parser(Opcode.SMSG_BLACK_MARKET_WON)]
        public static void HandleBlackMarketOutbidOrWon(Packet packet)
        {
            packet.ReadInt32("MarketID");
            ItemHandler.ReadItemInstance(packet);
            packet.ReadInt32("RandomPropertiesID");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT)]
        public static void HandleBlackMarketBidOnItemResult(Packet packet)
        {
            packet.ReadInt32("MarketID");
            ItemHandler.ReadItemInstance(packet);
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            packet.ReadTime("LastUpdateID");
            var count = packet.ReadInt32("ItemsCount");

            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("MarketID", i);
                packet.ReadInt32("SellerNPC", i);
                ItemHandler.ReadItemInstance(packet, i);
                packet.ReadInt32("Quantity", i);
                packet.ReadUInt64("MinBid", i);
                packet.ReadUInt64("MinIncrement", i);
                packet.ReadUInt64("CurrentBid", i);
                packet.ReadInt32("SecondsRemaining", i);
                packet.ReadBit("HighBid", i);
                packet.ReadInt32("NumBids", i);
            }
        }
    }
}
