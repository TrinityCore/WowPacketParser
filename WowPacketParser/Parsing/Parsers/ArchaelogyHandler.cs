using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ArchaelogyHandler
    {
        [Parser(Opcode.SMSG_SETUP_RESEARCH_HISTORY, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleResearchSetupHistory434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("ResearchProject.Id", i);
                packet.Translator.ReadInt32("Count", i);
                packet.Translator.ReadTime("Time", i);
            }
        }
    }
}
