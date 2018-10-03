using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.RelicSlotTierRequirement, HasIndexInData = false)]
    public class RelicSlotTierRequirementEntry
    {
        public byte RelicIndex { get; set; }
        public byte RelicTier { get; set; }
        public int PlayerConditionID { get; set; }
    }
}
