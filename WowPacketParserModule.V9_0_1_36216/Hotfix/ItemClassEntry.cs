using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemClass, HasIndexInData = false)]
    public class ItemClassEntry
    {
        public string ClassName { get; set; }
        public sbyte ClassID { get; set; }
        public float PriceModifier { get; set; }
        public byte Flags { get; set; }
    }
}
