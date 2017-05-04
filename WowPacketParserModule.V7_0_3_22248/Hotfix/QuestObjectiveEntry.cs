using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.QuestObjective, HasIndexInData = false)]
    public class QuestObjectiveEntry
    {
        public uint Amount { get; set; }
        public uint ObjectID { get; set; }
        public string Description { get; set; }
        public ushort QuestID { get; set; }
        public byte Type { get; set; }
        public byte ObjectivePanelorder { get; set; }
        public byte MapOrder { get; set; }
        public byte Flags { get; set; }
    }
}
