using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ArchaelogyHandler
    {
        [Parser(Opcode.SMSG_SETUP_RESEARCH_HISTORY, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleResearchSetupHistory434(Packet packet)
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
