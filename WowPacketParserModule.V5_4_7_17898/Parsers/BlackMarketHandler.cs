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

            packet.Translator.ReadInt32("Int18");
            packet.Translator.StartBitStream(guid2, 4, 2, 6, 3, 1, 7, 5, 0);
            packet.Translator.ParseBitStream(guid2, 6, 4, 0, 7, 5, 2, 1, 3);

            packet.Translator.WriteGuid("Guid2", guid2);

        }

        [Parser(Opcode.CMSG_BLACK_MARKET_BID_ON_ITEM)]
        public static void HandleBlackMarketBid(Packet packet)
        {
            var guid2 = new byte[8];

            packet.Translator.ReadInt64("Price");
            packet.Translator.ReadInt32("Item Entry");
            packet.Translator.ReadInt32("Int20");

            packet.Translator.StartBitStream(guid2, 4, 2, 1, 0, 6, 7, 3, 5);
            packet.Translator.ParseBitStream(guid2, 4, 0, 3, 1, 7, 5, 2, 6);
            packet.Translator.WriteGuid("Guid2", guid2);

        }

        [Parser(Opcode.CMSG_BLACK_MARKET_OPEN)]
        public static void HandleBlackMarketHello(Packet packet)
        {
            var guid2 = new byte[8];

            packet.Translator.StartBitStream(guid2, 6, 4, 2, 7, 5, 3, 0, 1);
            packet.Translator.ParseBitStream(guid2, 3, 2, 1, 6, 0, 5, 7, 4);

            packet.Translator.WriteGuid("Guid2", guid2);

        }
        [Parser(Opcode.SMSG_BLACK_MARKET_OPEN_RESULT)]
        public static void HandleBlackMarketOpenResult(Packet packet)
        {
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            var bit18 = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("Guid2", guid2);
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_OUTBID)]
        public static void HandleBlackMarketOutBid(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Item Id");
            packet.Translator.ReadInt32("Int14");
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketItemResult(Packet packet)
        {
            var bit34 = false;

            var bits10 = 0;

            packet.Translator.ReadInt32("Int20");
            bits10 = (int)packet.Translator.ReadBits(18);

            for (var i = 0; i < bits10; ++i)
            {
                bit34 = packet.Translator.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.Translator.ReadInt32("Item Entry", i);
                packet.Translator.ReadInt64("Current Bid", i);
                packet.Translator.ReadInt64("Start Price", i);
                packet.Translator.ReadInt32("Amount of price raise", i);
                packet.Translator.ReadInt32("Auction Id", i);
                packet.Translator.ReadInt32("Seller", i);
                packet.Translator.ReadInt64("Difference in price", i);
                packet.Translator.ReadInt32("Time Left", i);
                packet.Translator.ReadInt32("Int9", i);
                packet.Translator.ReadInt32("Amount", i);
            }
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_BID_ON_ITEM_RESULT)]
        public static void HandleBlackMarketBidOnItemResult(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Item Entry");
            packet.Translator.ReadInt32("Int14");
        }
        [Parser(Opcode.SMSG_BLACK_MARKET_WON)]
        public static void HandleBlackMarketWon(Packet packet)
        {
            packet.Translator.ReadInt32("Int10");
            packet.Translator.ReadInt32("Int14");
            packet.Translator.ReadInt32("Item Entry");
        }
    }
}