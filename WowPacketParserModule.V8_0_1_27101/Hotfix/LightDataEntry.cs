using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LightData, HasIndexInData = false)]
    public class LightDataEntry
    {
        public ushort LightParamID { get; set; }
        public ushort Time { get; set; }
        public int DirectColor { get; set; }
        public int AmbientColor { get; set; }
        public int SkyTopColor { get; set; }
        public int SkyMiddleColor { get; set; }
        public int SkyBand1Color { get; set; }
        public int SkyBand2Color { get; set; }
        public int SkySmogColor { get; set; }
        public int SkyFogColor { get; set; }
        public int SunColor { get; set; }
        public int CloudSunColor { get; set; }
        public int CloudEmissiveColor { get; set; }
        public int CloudLayer1AmbientColor { get; set; }
        public int CloudLayer2AmbientColor { get; set; }
        public int OceanCloseColor { get; set; }
        public int OceanFarColor { get; set; }
        public int RiverCloseColor { get; set; }
        public int RiverFarColor { get; set; }
        public int ShadowOpacity { get; set; }
        public float FogEnd { get; set; }
        public float FogScaler { get; set; }
        public float FogDensity { get; set; }
        public float FogHeight { get; set; }
        public float FogHeightScaler { get; set; }
        public float FogHeightDensity { get; set; }
        public float SunFogAngle { get; set; }
        public float CloudDensity { get; set; }
        public uint ColorGradingFileDataID { get; set; }
        public int HorizonAmbientColor { get; set; }
        public int GroundAmbientColor { get; set; }
        public uint EndFogColor { get; set; }
        public float EndFogColorDistance { get; set; }
        public uint SunFogColor { get; set; }
        public float SunFogStrength { get; set; }
        public uint FogHeightColor { get; set; }
    }
}
