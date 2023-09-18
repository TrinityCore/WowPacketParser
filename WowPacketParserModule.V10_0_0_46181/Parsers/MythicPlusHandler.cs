using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class MythicPlusHandler
    {
        [Parser(Opcode.CMSG_REQUEST_MYTHIC_PLUS_SEASON_DATA)]
        public static void HandleMythicPlusZero(Packet packet) { }
    }
}
