using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_PVP_SEASON)]
        public static void HandlePvPSeason(Packet packet)
        {
            packet.ReadUInt32("Active Season");
            packet.ReadUInt32("Last Season");
        }
    }
}
