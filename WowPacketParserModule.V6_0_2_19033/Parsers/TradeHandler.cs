using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("State Index");
        }
    }
}