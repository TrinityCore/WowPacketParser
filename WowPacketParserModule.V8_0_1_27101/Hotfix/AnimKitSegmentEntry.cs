using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKitSegment, HasIndexInData = false)]
    public class AnimKitSegmentEntry
    {
        public ushort ParentAnimKitID { get; set; }
        public byte OrderIndex { get; set; }
        public ushort AnimID { get; set; }
        public uint AnimStartTime { get; set; }
        public ushort AnimKitConfigID { get; set; }
        public byte StartCondition { get; set; }
        public byte StartConditionParam { get; set; }
        public uint StartConditionDelay { get; set; }
        public byte EndCondition { get; set; }
        public uint EndConditionParam { get; set; }
        public uint EndConditionDelay { get; set; }
        public float Speed { get; set; }
        public ushort SegmentFlags { get; set; }
        public byte ForcedVariation { get; set; }
        public int OverrideConfigFlags { get; set; }
        public sbyte LoopToSegmentIndex { get; set; }
        public ushort BlendInTimeMs { get; set; }
        public ushort BlendOutTimeMs { get; set; }
    }
}
