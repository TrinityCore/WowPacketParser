using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharTitles, HasIndexInData = false)]
    public class CharTitlesEntry
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public short MaskID { get; set; }
        public sbyte Flags { get; set; }
    }
}
