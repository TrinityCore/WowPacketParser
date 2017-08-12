using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemClass, HasIndexInData = false)]
    public class ItemClassEntry
    {
        public float PriceMod { get; set; }
        public string Name { get; set; }
        public byte Flags { get; set; }
    }
}