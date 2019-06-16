using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CinematicCamera, HasIndexInData = false)]
    public class CinematicCameraEntry
    {
        [HotfixArray(3, true)]
        public float[] Origin { get; set; }
        public uint SoundID { get; set; }
        public float OriginFacing { get; set; }
        public uint FileDataID { get; set; }
    }
}
