using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ArchaelogyHandler
    {
        public static void ReadResearchHistory(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProjectID", idx);
            packet.ReadTime("FirstCompleted", idx);
            packet.ReadInt32("CompletionCount", idx);
        }

        [Parser(Opcode.SMSG_SETUP_RESEARCH_HISTORY)]
        public static void HandleResearchSetupHistory(Packet packet)
        {
            var count = packet.ReadInt32("ResearchHistoryCount");

            for (var i = 0; i < count; ++i)
                ReadResearchHistory(packet, "History", i);
        }

        [Parser(Opcode.SMSG_RESEARCH_COMPLETE)]
        public static void HandleResearchComplete(Packet packet)
        {
            ReadResearchHistory(packet, "Research");
        }

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
