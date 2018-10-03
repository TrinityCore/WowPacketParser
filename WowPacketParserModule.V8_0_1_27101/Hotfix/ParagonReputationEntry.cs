using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ParagonReputation, HasIndexInData = false)]
    public class ParagonReputationEntry
    {
        public uint FactionID { get; set; }
        public int LevelThreshold { get; set; }
        public int QuestID { get; set; }
    }
}
