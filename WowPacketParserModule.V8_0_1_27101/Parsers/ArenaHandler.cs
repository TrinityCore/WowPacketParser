using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ArenaHandler
    {
        [Parser(Opcode.SMSG_PVP_SEASON, ClientVersionBuild.V8_1_0_28724)]
        public static void HandlePvPSeason(Packet packet)
        {
            packet.ReadUInt32("CurrentSeason");
            packet.ReadUInt32("PreviousSeason");
            packet.ReadUInt32("PvPTier");
        }
    }
}
