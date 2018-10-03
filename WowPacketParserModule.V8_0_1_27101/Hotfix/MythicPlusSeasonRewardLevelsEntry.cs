using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MythicPlusSeasonRewardLevels, HasIndexInData = false)]
    public class MythicPlusSeasonRewardLevelsEntry
    {
        public int DifficultyLevel { get; set; }
        public int WeeklyRewardLevel { get; set; }
        public int EndOfRunRewardLevel { get; set; }
        public int Season { get; set; }
    }
}
