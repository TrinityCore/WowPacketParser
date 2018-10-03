using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TradeSkillCategory)]
    public class TradeSkillCategoryEntry
    {
        public string Name { get; set; }
        public string HordeName { get; set; }
        public int ID { get; set; }
        public ushort ParentTradeSkillCategoryID { get; set; }
        public ushort SkillLineID { get; set; }
        public ushort OrderIndex { get; set; }
        public byte Flags { get; set; }
    }
}
