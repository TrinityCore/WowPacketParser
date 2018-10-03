using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CriteriaTreeXEffect, HasIndexInData = false)]
    public class CriteriaTreeXEffectEntry
    {
        public short WorldEffectID { get; set; }
        public int CriteriaTreeID { get; set; }
    }
}
