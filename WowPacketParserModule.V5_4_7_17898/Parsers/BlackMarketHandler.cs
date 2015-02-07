using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.CMSG_BLACK_MARKET_REQUEST_ITEMS)]
        public static void HandleBlackMarketRequestItems(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadInt32("Int18");
            packet.StartBitStream(guid2, 4, 2, 6, 3, 1, 7, 5, 0);
            packet.ParseBitStream(guid2, 6, 4, 0, 7, 5, 2, 1, 3);

            packet.WriteGuid("Guid2", guid2);

        }

        [Parser(Opcode.CMSG_BLACK_MARKET_BID_ON_ITEM)]
        public static void HandleBlackMarketBid(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadInt64("Price");
            packet.ReadInt32("Item Entry");
            packet.ReadInt32("Int20");

            packet.StartBitStream(guid2, 4, 2, 1, 0, 6, 7, 3, 5);
            packet.ParseBitStream(guid2, 4, 0, 3, 1, 7, 5, 2, 6);
            packet.WriteGuid("Guid2", guid2);

        }

        [Parser(Opcode.CMSG_BLACK_MARKET_OPEN)]
        public static void HandleBlackMarketHello(Packet packet)
        {
            var guid2 = new byte[8];

            packet.StartBitStream(guid2, 6, 4, 2, 7, 5, 3, 0, 1);
            packet.ParseBitStream(guid2, 3, 2, 1, 6, 0, 5, 7, 4);

            packet.WriteGuid("Guid2", guid2);

        }
        [Parser(Opcode.SMSG_BLACK_MARKET_OPEN_RESULT)]
        public static void HandleBlackMarketOpenResult(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("Guid2", guid2);
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_OUTBID)]
        public static void HandleBlackMarketOutBid(Packet packet)
        {
            packet.ReadInt32("Int10");
            packet.ReadInt32("Item Id");
            packet.ReadInt32("Int14");
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketItemResult(Packet packet)
        {
            var bit34 = false;

            var bits10 = 0;

            packet.ReadInt32("Int20");
            bits10 = (int)packet.ReadBits(18);

            for (var i = 0; i < bits10; ++i)
            {
                bit34 = packet.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("Item Entry", i);
                packet.ReadInt64("Current Bid", i);
                packet.ReadInt64("Start Price", i);
                packet.ReadInt32("Amount of price raise", i);
                packet.ReadInt32("Auction Id", i);
                packet.ReadInt32("Seller", i);
                packet.ReadInt64("Difference in price", i);
                packet.ReadInt32("Time Left", i);
                packet.ReadInt32("Int9", i);
                packet.ReadInt32("Amount", i);
            }
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT)]
        public static void HandleBlackMarketBidOnItemResult(Packet packet)
        {
            packet.ReadInt32("Int10");
            packet.ReadInt32("Item Entry");
            packet.ReadInt32("Int14");
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_WON)]
        public static void HandleBlackMarketWon(Packet packet)
        {
            packet.ReadInt32("Int10");
            packet.ReadInt32("Int14");
            packet.ReadInt32("Item Entry");
        }
    }
}