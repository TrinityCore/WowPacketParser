using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WeaponTrail, HasIndexInData = false)]
    public class WeaponTrailEntry
    {
        public int FileDataID { get; set; }
        public float Roll { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        [HotfixArray(3)]
        public int[] TextureFileDataID { get; set; }
        [HotfixArray(3)]
        public float[] TextureScrollRateU { get; set; }
        [HotfixArray(3)]
        public float[] TextureScrollRateV { get; set; }
        [HotfixArray(3)]
        public float[] TextureScaleU { get; set; }
        [HotfixArray(3)]
        public float[] TextureScaleV { get; set; }
    }
}
