using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MinorTalent, HasIndexInData = false)]
    public class MinorTalentEntry
    {
        public int SpellID { get; set; }
        public int OrderIndex { get; set; }
        public int ChrSpecializationID { get; set; }
    }
}
