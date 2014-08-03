using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_ARENA_SEASON_WORLD_STATE)]
        public static void HandleArenaSeasonWorldState(Packet packet)
        {
            packet.ReadUInt32("Last Season");
            packet.ReadUInt32("Active Season");
        }
    }
}