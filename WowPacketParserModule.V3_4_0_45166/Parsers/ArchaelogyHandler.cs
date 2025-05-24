using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ArchaelogyHandler
    {
        [Parser(Opcode.SMSG_ARCHAEOLOGY_SURVERY_CAST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleArchaelogySurveryCast(Packet packet)
        {
            packet.ReadUInt32("TotalFinds");
            packet.ReadUInt32("NumFindsCompleted");
            packet.ReadInt32("ResearchBranchID");
            packet.ReadBit("SuccessfulFind");
        }
    }
}
