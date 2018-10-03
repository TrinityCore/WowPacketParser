using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Path, HasIndexInData = false)]
    public class PathEntry
    {
        public byte Type { get; set; }
        public byte SplineType { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }
        public byte Flags { get; set; }
    }
}
