using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestFactionReward, HasIndexInData = false)]
    public class QuestFactionRewardEntry
    {
        [HotfixArray(10)]
        public ushort[] Difficulty { get; set; }
    }
}
