using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ScenarioStep, HasIndexInData = false)]
    public class ScenarioStepEntry
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public ushort ScenarioID { get; set; }
        public uint Criteriatreeid { get; set; }
        public int RewardQuestID { get; set; }
        public int RelatedStep { get; set; }
        public ushort Supersedes { get; set; }
        public byte OrderIndex { get; set; }
        public byte Flags { get; set; }
        public uint VisibilityPlayerConditionID { get; set; }
        public ushort WidgetSetID { get; set; }
    }
}
