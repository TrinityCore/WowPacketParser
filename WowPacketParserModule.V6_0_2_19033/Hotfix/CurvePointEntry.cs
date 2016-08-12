using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.CurvePoint)]
    public class CurvePointEntry
    {
        public uint ID { get; set; }
        public int CurveID { get; set; }
        public int Index { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
