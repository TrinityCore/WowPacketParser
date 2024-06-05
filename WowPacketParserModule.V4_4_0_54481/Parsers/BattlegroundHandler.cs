using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_ARENA_TEAM_ROSTER)]
        public static void HandleArenaTeamQuery(Packet packet)
        {
            packet.ReadUInt32("TeamID");
        }

        [Parser(Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_RATED_PVP_INFO)]
        public static void HandleBattlegroundZero(Packet packet)
        {
        }
    }
}
