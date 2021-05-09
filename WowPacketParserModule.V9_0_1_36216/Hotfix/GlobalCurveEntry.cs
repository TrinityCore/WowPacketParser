using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GlobalCurve, HasIndexInData = false)]
    public class GlobalCurveEntry
    {
        public int CurveID { get; set; }
        public int Type { get; set; }
    }
}
