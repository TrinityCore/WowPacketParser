using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.CMSG_REQUEST_CHALLENGE_MODE_AFFIXES)]
        public static void HandleChallengeModeZero(Packet packet) { }
    }
}
