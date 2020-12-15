using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CurvePoint, HasIndexInData = false)]
    public class CurvePointEntry
    {
        [HotfixArray(2, true)]
        public float[] Pos { get; set; }
        [HotfixArray(2, true)]
        public float[] PosPreSquish { get; set; }
        public ushort CurveID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
