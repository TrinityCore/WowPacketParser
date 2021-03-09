using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class ArchaeologyHandler
    {
        [Parser(Opcode.SMSG_SETUP_RESEARCH_HISTORY)]
        public static void HandleSetupResearchHistory(Packet packet)
        {
            var historySize = packet.ReadBits("ResearchHistorySize", 22);

            for (var i = 0; i < historySize; ++i)
                ReadResearchHistory(packet, "ResearchHistory", i);
        }

        public static void ReadResearchHistory(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ProjectID", idx);
            packet.ReadInt32("CompletionCount", idx);
            packet.ReadTime("FirstCompleted", idx);
        }

        [Parser(Opcode.SMSG_RESEARCH_COMPLETE)]
        public static void HandleResearchComplete(Packet packet)
        {
            packet.ReadTime("FirstCompleted");
            packet.ReadInt32("CompletionCount");
            packet.ReadInt32("ProjectID");
        }
    }
}