using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Curve, HasIndexInData = false)]
    public class CurveEntry
    {
        public byte Type { get; set; }
        public byte Unused { get; set; }
    }
}
