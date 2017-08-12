using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CurvePoint, HasIndexInData = false)]
    public class CurvePointEntry
    {
        public float X { get; set; }
        public float Y { get; set; }
        public ushort CurveID { get; set; }
        public byte Index { get; set; }
    }
}