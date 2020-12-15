using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Toy)]
    public class ToyEntry
    {
        public string SourceText { get; set; }
        public uint ID { get; set; }
        public int ItemID { get; set; }
        public byte Flags { get; set; }
        public sbyte SourceTypeEnum { get; set; }
    }
}
