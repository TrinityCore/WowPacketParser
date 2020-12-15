using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpecSetMember, HasIndexInData = false)]
    public class SpecSetMemberEntry
    {
        public int ChrSpecializationID { get; set; }
        public uint SpecSetID { get; set; }
    }
}
