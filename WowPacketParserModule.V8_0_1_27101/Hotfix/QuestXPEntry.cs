using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestXP, HasIndexInData = false)]
    public class QuestXPEntry
    {
        [HotfixArray(10)]
        public ushort[] Difficulty { get; set; }
    }
}
