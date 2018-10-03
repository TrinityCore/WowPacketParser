using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEffectEmission, HasIndexInData = false)]
    public class SpellEffectEmissionEntry
    {
        public float EmissionRate { get; set; }
        public float ModelScale { get; set; }
        public short AreaModelID { get; set; }
        public sbyte Flags { get; set; }
    }
}
