using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_SERVER_TIME_OFFSET)]
        public static void HandleUITimer(Packet packet)
        {
            packet.ReadTime64("Time");
        }
    }
}
