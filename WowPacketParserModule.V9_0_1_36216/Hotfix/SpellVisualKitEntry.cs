using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKit, HasIndexInData = false)]
    public class SpellVisualKitEntry
    {
        public sbyte FallbackPriority { get; set; }
        public int FallbackSpellVisualKitId { get; set; }
        public ushort DelayMin { get; set; }
        public ushort DelayMax { get; set; }
        [HotfixArray(2)]
        public int[] Flags { get; set; }
    }
}
