using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CorruptionEffects, HasIndexInData = false)]
    public class CorruptionEffectsEntry
    {
        public float MinCorruption { get; set; }
        public int Aura { get; set; }
        public int PlayerConditionID { get; set; }
        public int Flags { get; set; }
    }
}
