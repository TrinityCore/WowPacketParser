using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Weather, HasIndexInData = false)]
    public class WeatherEntry
    {
        public byte Type { get; set; }
        public float TransitionSkyBox { get; set; }
        public uint AmbienceID { get; set; }
        public ushort SoundAmbienceID { get; set; }
        public byte EffectType { get; set; }
        public int EffectTextureFileDataID { get; set; }
        public byte WindSettingsID { get; set; }
        public float Scale { get; set; }
        public float Volatility { get; set; }
        public float TwinkleIntensity { get; set; }
        public float FallModifier { get; set; }
        public float RotationalSpeed { get; set; }
        public int ParticulateFileDataID { get; set; }
        public float VolumeEdgeFadeStart { get; set; }
        public int OverrideColor { get; set; }
        public float OverrideColorIntensity { get; set; }
        public float OverrideCount { get; set; }
        public float OverrideOpacity { get; set; }
        public int VolumeFlags { get; set; }
        public int LightningID { get; set; }
        [HotfixArray(2)]
        public float[] Intensity { get; set; }
        [HotfixArray(3)]
        public float[] EffectColor { get; set; }
    }
}
