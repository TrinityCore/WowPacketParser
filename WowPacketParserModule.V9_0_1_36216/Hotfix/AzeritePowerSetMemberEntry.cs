using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePowerSetMember, HasIndexInData = false)]
    public class AzeritePowerSetMemberEntry
    {
        public int AzeritePowerSetID { get; set; }
        public int AzeritePowerID { get; set; }
        public int Class { get; set; }
        public int Tier { get; set; }
        public int OrderIndex { get; set; }
    }
}
