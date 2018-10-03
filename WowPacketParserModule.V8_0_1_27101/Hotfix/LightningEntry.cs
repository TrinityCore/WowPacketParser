using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Lightning, HasIndexInData = false)]
    public class LightningEntry
    {
        [HotfixArray(2)]
        public float[] BoltDirection { get; set; }
        [HotfixArray(3)]
        public int[] SoundKitID { get; set; }
        public float BoltDirectionVariance { get; set; }
        public float MinDivergence { get; set; }
        public float MaxDivergence { get; set; }
        public float MinConvergenceSpeed { get; set; }
        public float MaxConvergenceSpeed { get; set; }
        public float SegmentSize { get; set; }
        public float MinBoltWidth { get; set; }
        public float MaxBoltWidth { get; set; }
        public float MinBoltHeight { get; set; }
        public float MaxBoltHeight { get; set; }
        public int MaxSegmentCount { get; set; }
        public float MinStrikeTime { get; set; }
        public float MaxStrikeTime { get; set; }
        public float MinEndTime { get; set; }
        public float MaxEndTime { get; set; }
        public float MinFadeTime { get; set; }
        public float MaxFadeTime { get; set; }
        public float MinStrikeDelay { get; set; }
        public float MaxStrikeDelay { get; set; }
        public int FlashColor { get; set; }
        public int BoltColor { get; set; }
        public float Brightness { get; set; }
        public float MinCloudDepth { get; set; }
        public float MaxCloudDepth { get; set; }
        public float MinFadeInStrength { get; set; }
        public float MaxFadeInStrength { get; set; }
        public float MinStrikeStrength { get; set; }
        public float MaxStrikeStrength { get; set; }
        public float GroundBrightnessScalar { get; set; }
        public float BoltBrightnessScalar { get; set; }
        public float CloudBrightnessScalar { get; set; }
        public float SoundEmitterDistance { get; set; }
    }
}
