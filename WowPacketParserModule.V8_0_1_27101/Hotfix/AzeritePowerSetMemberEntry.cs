using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeritePowerSetMember, HasIndexInData = false)]
    public class AzeritePowerSetMemberEntry
    {
        public ushort AzeritePowerID { get; set; }
        public byte Class { get; set; }
        public byte Tier { get; set; }
        public byte OrderIndex { get; set; }
        public ushort AzeritePowerSetID { get; set; }
    }
}
