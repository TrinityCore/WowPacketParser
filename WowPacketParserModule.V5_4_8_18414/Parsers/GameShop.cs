using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class GameShopHandler
    {
        [Parser(Opcode.CMSG_GAME_SHOP_QUERY)]
        public static void HandleClientGameShopQuery(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_GAME_SHOP_QUERY_RESPONSE)]
        public static void HandleServerGameShopQueryresponse(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
