using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Curve, HasIndexInData = false)]
    public class CurveEntry
    {
        public byte Type { get; set; }
        public byte Flags { get; set; }
    }
}
