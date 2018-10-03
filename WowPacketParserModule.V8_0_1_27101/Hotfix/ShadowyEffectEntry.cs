using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ShadowyEffect, HasIndexInData = false)]
    public class ShadowyEffectEntry
    {
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public float Duration { get; set; }
        public float Value { get; set; }
        public float FadeInTime { get; set; }
        public float FadeOutTime { get; set; }
        public sbyte AttachPos { get; set; }
        public sbyte Flags { get; set; }
        public float InnerStrength { get; set; }
        public float OuterStrength { get; set; }
        public float InitialDelay { get; set; }
        public int CurveID { get; set; }
        public uint Priority { get; set; }
    }
}
