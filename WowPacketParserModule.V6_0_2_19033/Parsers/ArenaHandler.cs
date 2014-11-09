using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_ARENA_SEASON_WORLD_STATE)]
        public static void HandleArenaSeasonWorldState(Packet packet)
        {
            packet.ReadUInt32("Active Season");
            packet.ReadUInt32("Last Season");
        }
    }
}
