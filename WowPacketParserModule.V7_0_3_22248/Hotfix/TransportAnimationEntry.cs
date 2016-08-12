using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.TransportAnimation, HasIndexInData = false)]
    public class TransportAnimationEntry
    {
        public uint TransportID { get; set; }
        public uint TimeIndex { get; set; }
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public byte SequenceID { get; set; }
    }
}