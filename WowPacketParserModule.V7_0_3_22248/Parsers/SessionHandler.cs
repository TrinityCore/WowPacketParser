using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleQueryTimeResponse(Packet packet)
        {
            packet.ReadTime("CurrentTime");
        }
        [Parser(Opcode.SMSG_ENABLE_ENCRYPTION)]
        public static void HandleSessionZero(Packet packet)
        {
        }
    }
}
