using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GroupFinderCategory, HasIndexInData = false)]
    public class GroupFinderCategoryEntry
    {
        public string Name { get; set; }
        public byte OrderIndex { get; set; }
        public byte Flags { get; set; }
    }
}
