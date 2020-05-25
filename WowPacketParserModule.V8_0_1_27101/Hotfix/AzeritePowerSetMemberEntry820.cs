using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_0_30898.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePowerSetMember, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
    public class AzeritePowerSetMemberEntry
    {
        public int AzeritePowerSetID { get; set; }
        public int AzeritePowerID { get; set; }
        public int Class { get; set; }
        public int Tier { get; set; }
        public int OrderIndex { get; set; }
    }
}
