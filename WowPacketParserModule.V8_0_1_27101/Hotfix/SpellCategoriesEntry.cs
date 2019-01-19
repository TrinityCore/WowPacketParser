using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCategories, HasIndexInData = false)]
    public class SpellCategoriesEntry
    {
        public byte DifficultyID { get; set; }
        public short Category { get; set; }
        public sbyte DefenseType { get; set; }
        public sbyte DispelType { get; set; }
        public sbyte Mechanic { get; set; }
        public sbyte PreventionType { get; set; }
        public short StartRecoveryCategory { get; set; }
        public short ChargeCategory { get; set; }
        public int SpellID { get; set; }
    }
}
