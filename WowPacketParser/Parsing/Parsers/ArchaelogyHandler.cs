using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ArchaelogyHandler
    {
        [Parser(Opcode.SMSG_RESEARCH_SETUP_HISTORY)] // 4.3.4
        public static void HandleResearchSetupHistory(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("ResearchProject.Id", i);
                packet.ReadInt32("Count", i);
                packet.ReadTime("Time", i);
            }
        }
    }
}
