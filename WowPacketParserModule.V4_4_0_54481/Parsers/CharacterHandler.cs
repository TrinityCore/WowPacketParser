using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_GET_UNDELETE_CHARACTER_COOLDOWN_STATUS)]
        public static void HandleGetUndeleteCooldownStatus(Packet packet)
        {
        }
    }
}
