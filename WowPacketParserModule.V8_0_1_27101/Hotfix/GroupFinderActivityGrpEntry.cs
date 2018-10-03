using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GroupFinderActivityGrp, HasIndexInData = false)]
    public class GroupFinderActivityGrpEntry
    {
        public string Name { get; set; }
        public byte OrderIndex { get; set; }
    }
}
