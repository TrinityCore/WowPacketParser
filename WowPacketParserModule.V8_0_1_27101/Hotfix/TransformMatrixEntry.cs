using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TransformMatrix, HasIndexInData = false)]
    public class TransformMatrixEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float Scale { get; set; }
    }
}
