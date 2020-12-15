using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransportAnimation, HasIndexInData = false)]
    public class TransportAnimationEntry
    {
        [HotfixArray(3, true)]
        public float[] Pos { get; set; }
        public byte SequenceID { get; set; }
        public uint TimeIndex { get; set; }
        public uint TransportID { get; set; }
    }
}
