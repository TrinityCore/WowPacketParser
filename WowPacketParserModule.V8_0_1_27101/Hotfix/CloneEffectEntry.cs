using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CloneEffect, HasIndexInData = false)]
    public class CloneEffectEntry
    {
        public int DurationMs { get; set; }
        public int DelayMs { get; set; }
        public int FadeInTimeMs { get; set; }
        public int FadeOutTimeMs { get; set; }
        public int StateSpellVisualKitID { get; set; }
        public int StartSpellVisualKitID { get; set; }
        public int OffsetMatrixID { get; set; }
        public int Flags { get; set; }
    }
}
