using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpecSetMember, HasIndexInData = false)]
    public class SpecSetMemberEntry
    {
        public int ChrSpecializationID { get; set; }
        public int SpecSetID { get; set; }
    }
}
