using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellProceduralEffect)]
    public class SpellProceduralEffectEntry
    {
        public int ID { get; set; }
        public sbyte Type { get; set; }
        [HotfixArray(4)]
        public float[] Value { get; set; }
    }
}
