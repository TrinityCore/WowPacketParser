using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BeamEffect, HasIndexInData = false)]
    public class BeamEffectEntry
    {
        public int BeamID { get; set; }
        public float SourceMinDistance { get; set; }
        public float FixedLength { get; set; }
        public int Flags { get; set; }
        public int SourceOffset { get; set; }
        public int DestOffset { get; set; }
        public int SourceAttachID { get; set; }
        public int DestAttachID { get; set; }
        public int SourcePositionerID { get; set; }
        public int DestPositionerID { get; set; }
    }
}
