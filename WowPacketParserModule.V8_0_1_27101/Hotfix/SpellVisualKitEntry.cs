using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKit, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_3_0_33062, HasIndexInData = false)]
    public class SpellVisualKitEntry
    {
        public int Flags { get; set; }
        public sbyte FallbackPriority { get; set; }
        public int FallbackSpellVisualKitId { get; set; }
        public ushort DelayMin { get; set; }
        public ushort DelayMax { get; set; }
    }
}
