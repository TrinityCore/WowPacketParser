using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CelestialBody)]
    public class CelestialBodyEntry
    {
        [HotfixArray(3)]
        public float[] Position { get; set; }
        public int ID { get; set; }
        public int BaseFileDataID { get; set; }
        public float BodyBaseScale { get; set; }
        public int AtmosphericMaskFileDataID { get; set; }
        public int AtmosphericModifiedFileDataID { get; set; }
        public float RotateRate { get; set; }
        public float AtmosphericMaskScale { get; set; }
        public short SkyArrayBand { get; set; }
        public int LightMaskFileDataID { get; set; }
        [HotfixArray(2)]
        public int[] GlowMaskFileDataID { get; set; }
        [HotfixArray(2)]
        public float[] GlowMaskScale { get; set; }
        [HotfixArray(2)]
        public int[] GlowModifiedFileDataID { get; set; }
        [HotfixArray(2)]
        public float[] ScrollURate { get; set; }
        [HotfixArray(2)]
        public float[] ScrollVRate { get; set; }
    }
}
