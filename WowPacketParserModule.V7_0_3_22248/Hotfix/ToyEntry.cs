using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Toy)]
    public class ToyEntry
    {
        public uint ItemID { get; set; }
        public string Description { get; set; }
        public byte Flags { get; set; }
        public byte CategoryFilter { get; set; }
        public uint ID { get; set; }
    }
}