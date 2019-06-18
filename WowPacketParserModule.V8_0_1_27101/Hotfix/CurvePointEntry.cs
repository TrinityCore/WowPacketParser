using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CurvePoint, HasIndexInData = false)]
    public class CurvePointEntry
    {
        [HotfixArray(2, true)]
        public float[] Pos { get; set; }
        public ushort CurveID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
