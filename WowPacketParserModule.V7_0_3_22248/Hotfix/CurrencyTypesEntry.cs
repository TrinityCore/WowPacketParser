using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CurrencyTypes, HasIndexInData = false)]
    public class CurrencyTypesEntry
    {
        public string Name { get; set; }
        [HotfixArray(2)]
        public string[] InventoryIcon { get; set; }
        public uint MaxQty { get; set; }
        public uint MaxEarnablePerWeek { get; set; }
        public uint Flags { get; set; }
        public string Description { get; set; }
        public byte CategoryID { get; set; }
        public byte SpellCategory { get; set; }
        public byte Quality { get; set; }
        public uint SpellWeight { get; set; }
    }
}