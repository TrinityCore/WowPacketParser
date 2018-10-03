using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKit, HasIndexInData = false)]
    public class SpellVisualKitEntry
    {
        public int Flags { get; set; }
        public float FallbackPriority { get; set; }
        public uint FallbackSpellVisualKitID { get; set; }
        public ushort DelayMin { get; set; }
        public ushort DelayMax { get; set; }
    }
}
