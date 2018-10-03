using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualKitEffect, HasIndexInData = false)]
    public class SpellVisualKitEffectEntry
    {
        public int EffectType { get; set; }
        public int Effect { get; set; }
        public int ParentSpellVisualKitID { get; set; }
    }
}
