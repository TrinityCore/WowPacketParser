using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT)]
        public static void HandleReportPvPPlayerAfkResult(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");
            packet.ReadByteE<ReportPvPAFKResult>("Result");
            packet.ReadByte("NumBlackMarksOnOffender");
            packet.ReadByte("NumPlayersIHaveReported");
        }
    }
}
