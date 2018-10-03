using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEffectAutoDescription, HasIndexInData = false)]
    public class SpellEffectAutoDescriptionEntry
    {
        public string EffectDescription { get; set; }
        public string AuraDescription { get; set; }
        public int SpellEffectType { get; set; }
        public int AuraEffectType { get; set; }
        public sbyte PointsSign { get; set; }
        public sbyte TargetType { get; set; }
        public sbyte SchoolMask { get; set; }
        public int EffectOrderIndex { get; set; }
        public int AuraOrderIndex { get; set; }
    }
}
