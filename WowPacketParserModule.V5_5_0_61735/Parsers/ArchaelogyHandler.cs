using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ArchaelogyHandler
    {
        [Parser(Opcode.SMSG_ARCHAEOLOGY_SURVERY_CAST)]
        public static void HandleArchaelogySurveryCast(Packet packet)
        {
            packet.ReadUInt32("TotalFinds");
            packet.ReadUInt32("NumFindsCompleted");
            packet.ReadInt32("ResearchBranchID");
            packet.ReadBit("SuccessfulFind");
        }
    }
}
