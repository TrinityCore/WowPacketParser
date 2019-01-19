using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestMoneyReward, HasIndexInData = false)]
    public class QuestMoneyRewardEntry
    {
        [HotfixArray(10)]
        public uint[] Difficulty { get; set; }
    }
}
