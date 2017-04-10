using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.CriteriaTree, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class CriteriaTreeEntry
    {
        public uint Amount { get; set; }
        public string Description { get; set; }
        public ushort Parent { get; set; }
        public ushort Flags { get; set; }
        public byte Operator { get; set; }
        public uint CriteriaID { get; set; }
        public uint OrderIndex { get; set; }
    }
}