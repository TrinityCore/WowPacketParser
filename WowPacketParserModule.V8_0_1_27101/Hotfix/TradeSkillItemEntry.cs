using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TradeSkillItem, HasIndexInData = false)]
    public class TradeSkillItemEntry
    {
        public ushort ItemLevel { get; set; }
        public sbyte RequiredLevel { get; set; }
    }
}
