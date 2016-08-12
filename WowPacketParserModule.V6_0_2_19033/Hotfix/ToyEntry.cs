using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Toy)]
    public class ToyEntry // New structure - 6.0.2
    {
        public uint ID { get; set; }
        public uint ItemID { get; set; }
        public uint Flags { get; set; }
        public string Description { get; set; }
        public Int32 SourceType { get; set; }
    }
}