using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CinematicCamera, HasIndexInData = false)]
    public class CinematicCameraEntry
    {
        public string Model { get; set; }
        [HotfixArray(3)]
        public float[] Origin { get; set; }
        public float OriginFacing { get; set; }
        public ushort SoundID { get; set; }
    }
}
