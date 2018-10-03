using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCategory, HasIndexInData = false)]
    public class SpellCategoryEntry
    {
        public string Name { get; set; }
        public sbyte Flags { get; set; }
        public byte UsesPerWeek { get; set; }
        public sbyte MaxCharges { get; set; }
        public int ChargeRecoveryTime { get; set; }
        public int TypeMask { get; set; }
    }
}
