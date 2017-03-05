using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
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