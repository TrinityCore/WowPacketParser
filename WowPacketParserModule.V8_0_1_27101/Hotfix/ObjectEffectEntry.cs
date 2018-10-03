using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ObjectEffect, HasIndexInData = false)]
    public class ObjectEffectEntry
    {
        [HotfixArray(3)]
        public float[] Offset { get; set; }
        public ushort ObjectEffectGroupID { get; set; }
        public byte TriggerType { get; set; }
        public byte EventType { get; set; }
        public byte EffectRecType { get; set; }
        public uint EffectRecID { get; set; }
        public sbyte Attachment { get; set; }
        public uint ObjectEffectModifierID { get; set; }
    }
}
