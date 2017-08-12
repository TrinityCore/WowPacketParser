using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.QuestMoneyReward, HasIndexInData = false)]
    public class QuestMoneyRewardEntry
    {
        [HotfixArray(10)]
        public uint[] Money { get; set; }
    }
}