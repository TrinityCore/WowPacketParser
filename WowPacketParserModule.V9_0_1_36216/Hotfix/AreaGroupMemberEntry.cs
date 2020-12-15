using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AreaGroupMember, HasIndexInData = false)]
    public class AreaGroupMemberEntry
    {
        public ushort AreaID { get; set; }
        public ushort AreaGroupID { get; set; }
    }
}
