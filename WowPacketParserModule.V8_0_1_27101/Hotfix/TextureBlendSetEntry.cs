using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TextureBlendSet, HasIndexInData = false)]
    public class TextureBlendSetEntry
    {
        [HotfixArray(3)]
        public int[] TextureFileDataID { get; set; }
        public byte SwizzleRed { get; set; }
        public byte SwizzleGreen { get; set; }
        public byte SwizzleBlue { get; set; }
        public byte SwizzleAlpha { get; set; }
        [HotfixArray(3)]
        public float[] TextureScrollRateU { get; set; }
        [HotfixArray(3)]
        public float[] TextureScrollRateV { get; set; }
        [HotfixArray(3)]
        public float[] TextureScaleU { get; set; }
        [HotfixArray(3)]
        public float[] TextureScaleV { get; set; }
        [HotfixArray(4)]
        public float[] ModX { get; set; }
    }
}
