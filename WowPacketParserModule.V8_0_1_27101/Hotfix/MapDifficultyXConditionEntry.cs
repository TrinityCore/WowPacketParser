using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MapDifficultyXCondition, HasIndexInData = false)]
    public class MapDifficultyXConditionEntry
    {
        public string FailureDescription { get; set; }
        public uint PlayerConditionID { get; set; }
        public int OrderIndex { get; set; }
        public uint MapDifficultyID { get; set; }
    }
}
