using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ManagedWorldStateInput, HasIndexInData = false)]
    public class ManagedWorldStateInputEntry
    {
        public int ManagedWorldStateID { get; set; }
        public int QuestID { get; set; }
        public int ValidInputConditionID { get; set; }
    }
}
