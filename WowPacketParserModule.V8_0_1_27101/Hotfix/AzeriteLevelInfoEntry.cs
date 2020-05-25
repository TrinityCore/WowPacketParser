using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteLevelInfo, HasIndexInData = false)]
    public class AzeriteLevelInfoEntry
    {
        public ulong BaseExperienceToNextLevel { get; set; }
        public ulong MinimumExperienceToNextLevel { get; set; }
        public int ItemLevel { get; set; }
    }
}
