using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LiquidType, HasIndexInData = false)]
    public class LiquidTypeEntry
    {
        public string Name { get; set; }
        [HotfixArray(6)]
        public string[] Texture { get; set; }
        public ushort Flags { get; set; }
        public byte SoundBank { get; set; }
        public uint SoundID { get; set; }
        public uint SpellID { get; set; }
        public float MaxDarkenDepth { get; set; }
        public float FogDarkenIntensity { get; set; }
        public float AmbDarkenIntensity { get; set; }
        public float DirDarkenIntensity { get; set; }
        public ushort LightID { get; set; }
        public float ParticleScale { get; set; }
        public byte ParticleMovement { get; set; }
        public byte ParticleTexSlots { get; set; }
        public byte MaterialID { get; set; }
        public int MinimapStaticCol { get; set; }
        [HotfixArray(6)]
        public byte[] FrameCountTexture { get; set; }
        [HotfixArray(2)]
        public int[] Color { get; set; }
        [HotfixArray(18)]
        public float[] Float { get; set; }
        [HotfixArray(4)]
        public uint[] Int { get; set; }
        [HotfixArray(4)]
        public float[] Coefficient { get; set; }
    }
}
