using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LfgDungeonGroup, HasIndexInData = false)]
    public class LfgDungeonGroupEntry
    {
        public string Name { get; set; }
        public byte TypeID { get; set; }
        public byte ParentGroupID { get; set; }
        public ushort OrderIndex { get; set; }
    }
}
