using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.CMSG_BLACK_MARKET_BID)]
        public static void HandleBlackMarketBid(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadInt32("Id");
            packet.ReadInt32("Item Entry");
            packet.ReadInt64("Price");

            packet.StartBitStream(guid2, 0, 5, 4, 3, 7, 6, 1, 2);
            packet.ParseBitStream(guid2, 4, 3, 6, 5, 7, 1, 0, 2);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_BLACK_MARKET_HELLO)]
        public static void HandleBlackMarketHello(Packet packet)
        {
            var guid2 = new byte[8];

            packet.StartBitStream(guid2, 4, 5, 2, 7, 0, 1, 3, 6);
            packet.ParseBitStream(guid2, 3, 5, 0, 6, 4, 1, 7, 2);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_BLACK_MARKET_REQUEST_ITEMS)]
        public static void HandleBlackMarketRequestItems(Packet packet)
        {
            var guid2 = new byte[8];

            packet.ReadInt32("Timestamp");
            packet.StartBitStream(guid2, 2, 6, 0, 3, 4, 5, 1, 7);
            packet.ParseBitStream(guid2, 6, 2, 3, 5, 7, 4, 1, 0);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_HELLO)]
        public static void HandleServerBlackMarketHello(Packet packet)
        {
            var guid = new byte[8];

            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid2", guid);
        }

        [Parser(Opcode.SMSG_BLACK_MARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            var bit34 = false;
            var bits10 = 0;

            packet.ReadInt32("Unk");
            bits10 = (int)packet.ReadBits(18); // item count

            for (var i = 0; i < bits10; ++i)
            {
                bit34 = packet.ReadBit();
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("unk1", i);
                packet.ReadInt32("Amount", i); // ?
                packet.ReadInt32("Time Left", i); // ?
                packet.ReadInt32("unk2", i); // Amount of price raise ???
                packet.ReadInt32("Item Entry", i);
                packet.ReadInt64("Start Price", i);
                packet.ReadInt64("Current Bid", i);
                packet.ReadInt64("Difference in price", i); // ?
                packet.ReadInt32("Seller (npc entry)", i);
                packet.ReadInt32("Auction Id", i); // ?
            }
        }

        [Parser(Opcode.SMSG_BLACKMARKET_BID_RESULT)]
        public static void HandleBlackMarketBidResult(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
