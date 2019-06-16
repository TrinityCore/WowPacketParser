using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellAuraOptions, HasIndexInData = false)]
    public class SpellAuraOptionsEntry
    {
        public byte DifficultyID { get; set; }
        public ushort CumulativeAura { get; set; }
        public int ProcCategoryRecovery { get; set; }
        public byte ProcChance { get; set; }
        public int ProcCharges { get; set; }
        public ushort SpellProcsPerMinuteID { get; set; }
        [HotfixArray(2)]
        public int[] ProcTypeMask { get; set; }
        public int SpellID { get; set; }
    }
}
