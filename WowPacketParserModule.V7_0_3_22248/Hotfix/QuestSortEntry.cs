using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.QuestSort, HasIndexInData = false)]
    public class QuestSortEntry
    {
        public string SortName { get; set; }
        public byte SortOrder { get; set; }
    }
}