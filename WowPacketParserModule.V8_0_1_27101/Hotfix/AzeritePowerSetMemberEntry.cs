using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePowerSetMember, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_0_28724, HasIndexInData = false)]
    public class AzeritePowerSetMemberEntry
    {
        public ushort AzeritePowerID { get; set; }
        public byte Class { get; set; }
        public byte Tier { get; set; }
        public byte OrderIndex { get; set; }
        public ushort AzeritePowerSetID { get; set; }
    }
}
