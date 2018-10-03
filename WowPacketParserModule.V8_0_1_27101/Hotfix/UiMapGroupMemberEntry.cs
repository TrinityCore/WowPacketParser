using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiMapGroupMember, HasIndexInData = false)]
    public class UiMapGroupMemberEntry
    {
        public string Name { get; set; }
        public int UiMapGroupID { get; set; }
        public int UiMapID { get; set; }
        public int FloorIndex { get; set; }
        public sbyte RelativeHeightIndex { get; set; }
    }
}
