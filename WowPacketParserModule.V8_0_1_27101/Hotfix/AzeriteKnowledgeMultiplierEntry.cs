using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteKnowledgeMultiplier, HasIndexInData = false)]
    public class AzeriteKnowledgeMultiplierEntry
    {
        public float Multiplier { get; set; }
    }
}
