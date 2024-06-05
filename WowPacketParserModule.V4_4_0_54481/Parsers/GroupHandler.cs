using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        public static void HandleGroupNull(Packet packet)
        {
        }
    }
}
