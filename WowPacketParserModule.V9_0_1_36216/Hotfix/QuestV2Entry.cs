using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.QuestV2, HasIndexInData = false)]
    public class QuestV2Entry
    {
        public ushort UniqueBitFlag { get; set; }
    }
}
