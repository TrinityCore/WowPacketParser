using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LightParams)]
    public class LightParamsEntry
    {
        [HotfixArray(3)]
        public float[] OverrideCelestialSphere { get; set; }
        public int ID { get; set; }
        public byte HighlightSky { get; set; }
        public ushort LightSkyboxID { get; set; }
        public byte CloudTypeID { get; set; }
        public float Glow { get; set; }
        public float WaterShallowAlpha { get; set; }
        public float WaterDeepAlpha { get; set; }
        public float OceanShallowAlpha { get; set; }
        public float OceanDeepAlpha { get; set; }
        public sbyte Flags { get; set; }
        public int SsaoSettingsID { get; set; }
    }
}
