using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellAuraOptions, HasIndexInData = false)]
    public class SpellAuraOptionsEntry
    {
        public uint SpellID { get; set; }
        public uint ProcCharges { get; set; }
        public uint ProcTypeMask { get; set; }
        public uint ProcCategoryRecovery { get; set; }
        public ushort CumulativeAura { get; set; }
        public byte DifficultyID { get; set; }
        public byte ProcChance { get; set; }
        public byte SpellProcsPerMinuteID { get; set; }
    }
}