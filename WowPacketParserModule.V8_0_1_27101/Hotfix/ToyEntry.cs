using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Toy)]
    public class ToyEntry
    {
        public string SourceText { get; set; }
        public int ID { get; set; }
        public int ItemID { get; set; }
        public byte Flags { get; set; }
        public sbyte SourceTypeEnum { get; set; }
    }
}
