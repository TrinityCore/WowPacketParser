using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteKnowledgeMultiplier, HasIndexInData = false)]
    public class AzeriteKnowledgeMultiplierEntry
    {
        public float Multiplier { get; set; }
    }
}
