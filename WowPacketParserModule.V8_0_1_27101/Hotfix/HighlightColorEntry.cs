using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.HighlightColor, HasIndexInData = false)]
    public class HighlightColorEntry
    {
        public sbyte Type { get; set; }
        public int StartColor { get; set; }
        public int MidColor { get; set; }
        public int EndColor { get; set; }
        public byte Flags { get; set; }
    }
}
