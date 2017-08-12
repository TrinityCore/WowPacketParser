using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCategories, HasIndexInData = false)]
    public class SpellCategoriesEntry
    {
        public uint SpellID { get; set; }
        public ushort Category { get; set; }
        public ushort StartRecoveryCategory { get; set; }
        public ushort ChargeCategory { get; set; }
        public byte DifficultyID { get; set; }
        public byte DefenseType { get; set; }
        public byte DispelType { get; set; }
        public byte Mechanic { get; set; }
        public byte PreventionType { get; set; }
    }
}