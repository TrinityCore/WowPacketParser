using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.CriteriaTree)]
    public class CriteriaTreeEntry
    {
        public int ID { get; set; }
        public int CriteriaID { get; set; }
        public long Amount { get; set; }
        public int Operator { get; set; }
        public int Parent { get; set; }
        public int Flags { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
    }
}
