using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ScenarioStep, HasIndexInData = false)]
    public class ScenarioStepEntry
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public ushort CriteriaTreeID { get; set; }
        public ushort ScenarioID { get; set; }
        public ushort PreviousStepID { get; set; }
        public ushort QuestRewardID { get; set; }
        public byte Step { get; set; }
        public byte Flags { get; set; }
        public uint BonusRequiredStepID { get; set; }
    }
}
