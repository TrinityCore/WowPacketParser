using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WindSettings, HasIndexInData = false)]
    public class WindSettingsEntry
    {
        [HotfixArray(3)]
        public float[] BaseDir { get; set; }
        [HotfixArray(3)]
        public float[] VarianceDir { get; set; }
        [HotfixArray(3)]
        public float[] MaxStepDir { get; set; }
        public float BaseMag { get; set; }
        public float VarianceMagOver { get; set; }
        public float VarianceMagUnder { get; set; }
        public float MaxStepMag { get; set; }
        public float Frequency { get; set; }
        public float Duration { get; set; }
        public byte Flags { get; set; }
    }
}
