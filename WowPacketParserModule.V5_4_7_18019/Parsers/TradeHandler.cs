using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using TradeStatus547 = WowPacketParserModule.V5_4_7_18019.Enums.TradeStatus;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        public static void HandleBeginTrade(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            var guid = packet.StartBitStream(1, 2, 4, 5, 3, 0, 7, 6);
            packet.ParseBitStream(guid, 4, 1, 5, 7, 3, 2, 0, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleSetTradeGold(Packet packet)
        {
            packet.ReadUInt64("Gold");
        }
    }
}
