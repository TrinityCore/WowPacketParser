using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellRadius, HasIndexInData = false)]
    public class SpellRadiusEntry
    {
        public float Radius { get; set; }
        public float RadiusPerLevel { get; set; }
        public float RadiusMin { get; set; }
        public float RadiusMax { get; set; }
    }
}
