using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class BlackMarketHandler
    {
        [Parser(Opcode.CMSG_BLACKMARKET_BID)]
        public static void HandleBlackMarketBid(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_BLACKMARKET_HELLO)]
        public static void HandleBlackMarketHello(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_BLACKMARKET_REQUEST_ITEMS)]
        public static void HandleBlackMarketRequestItems(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BLACKMARKET_HELLO)]
        public static void HandleServerBlackMarketHello(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BLACKMARKET_REQUEST_ITEMS_RESULT)]
        public static void HandleBlackMarketRequestItemsResult(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_BLACKMARKET_BID_RESULT)]
        public static void HandleBlackMarketBidResult(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
