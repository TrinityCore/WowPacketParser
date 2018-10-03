using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WeaponTrailParam, HasIndexInData = false)]
    public class WeaponTrailParamEntry
    {
        public byte Hand { get; set; }
        public float Duration { get; set; }
        public float FadeOutTime { get; set; }
        public float EdgeLifeSpan { get; set; }
        public float InitialDelay { get; set; }
        public float SmoothSampleAngle { get; set; }
        public sbyte OverrideAttachTop { get; set; }
        public sbyte OverrideAttachBot { get; set; }
        public byte Flags { get; set; }
        public ushort WeaponTrailID { get; set; }
    }
}
