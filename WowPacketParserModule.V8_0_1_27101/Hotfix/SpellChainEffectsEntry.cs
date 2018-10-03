using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellChainEffects, HasIndexInData = false)]
    public class SpellChainEffectsEntry
    {
        public float AvgSegLen { get; set; }
        public float NoiseScale { get; set; }
        public float TexCoordScale { get; set; }
        public uint SegDuration { get; set; }
        public ushort SegDelay { get; set; }
        public uint Flags { get; set; }
        public ushort JointCount { get; set; }
        public float JointOffsetRadius { get; set; }
        public byte JointsPerMinorJoint { get; set; }
        public byte MinorJointsPerMajorJoint { get; set; }
        public float MinorJointScale { get; set; }
        public float MajorJointScale { get; set; }
        public float JointMoveSpeed { get; set; }
        public float JointSmoothness { get; set; }
        public float MinDurationBetweenJointJumps { get; set; }
        public float MaxDurationBetweenJointJumps { get; set; }
        public float WaveHeight { get; set; }
        public float WaveFreq { get; set; }
        public float WaveSpeed { get; set; }
        public float MinWaveAngle { get; set; }
        public float MaxWaveAngle { get; set; }
        public float MinWaveSpin { get; set; }
        public float MaxWaveSpin { get; set; }
        public float ArcHeight { get; set; }
        public float MinArcAngle { get; set; }
        public float MaxArcAngle { get; set; }
        public float MinArcSpin { get; set; }
        public float MaxArcSpin { get; set; }
        public float DelayBetweenEffects { get; set; }
        public float MinFlickerOnDuration { get; set; }
        public float MaxFlickerOnDuration { get; set; }
        public float MinFlickerOffDuration { get; set; }
        public float MaxFlickerOffDuration { get; set; }
        public float PulseSpeed { get; set; }
        public float PulseOnLength { get; set; }
        public float PulseFadeLength { get; set; }
        public byte Alpha { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte BlendMode { get; set; }
        public byte RenderLayer { get; set; }
        public float WavePhase { get; set; }
        public float TimePerFlipFrame { get; set; }
        public float VariancePerFlipFrame { get; set; }
        public uint TextureParticleFileDataID { get; set; }
        public float StartWidth { get; set; }
        public float EndWidth { get; set; }
        public ushort WidthScaleCurveID { get; set; }
        public byte NumFlipFramesU { get; set; }
        public byte NumFlipFramesV { get; set; }
        public uint SoundKitID { get; set; }
        public float ParticleScaleMultiplier { get; set; }
        public float ParticleEmissionRateMultiplier { get; set; }
        [HotfixArray(11)]
        public ushort[] SpellChainEffectID { get; set; }
        [HotfixArray(3)]
        public float[] TextureCoordScaleU { get; set; }
        [HotfixArray(3)]
        public float[] TextureCoordScaleV { get; set; }
        [HotfixArray(3)]
        public float[] TextureRepeatLengthU { get; set; }
        [HotfixArray(3)]
        public float[] TextureRepeatLengthV { get; set; }
        [HotfixArray(3)]
        public int[] TextureFileDataID { get; set; }
    }
}
