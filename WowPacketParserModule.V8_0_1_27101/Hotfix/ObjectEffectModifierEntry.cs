using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ObjectEffectModifier, HasIndexInData = false)]
    public class ObjectEffectModifierEntry
    {
        [HotfixArray(4)]
        public float[] Param { get; set; }
        public byte InputType { get; set; }
        public byte MapType { get; set; }
        public byte OutputType { get; set; }
    }
}
