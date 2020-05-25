using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePowerSetMember, ClientVersionBuild.V8_1_0_28724, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
    public class AzeritePowerSetMemberEntry
    {
        public ushort AzeritePowerSetID { get; set; }
        public ushort AzeritePowerID { get; set; }
        public byte Class { get; set; }
        public byte Tier { get; set; }
        public byte OrderIndex { get; set; }
    }
}
