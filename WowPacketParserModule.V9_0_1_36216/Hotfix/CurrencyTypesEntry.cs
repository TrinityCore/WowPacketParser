using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CurrencyTypes, HasIndexInData = false)]
    public class CurrencyTypesEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int InventoryIconFileID { get; set; }
        public uint SpellWeight { get; set; }
        public byte SpellCategory { get; set; }
        public uint MaxQty { get; set; }
        public uint MaxEarnablePerWeek { get; set; }
        public sbyte Quality { get; set; }
        public int FactionID { get; set; }
        public int ItemGroupSoundsID { get; set; }
        public int XpQuestDifficulty { get; set; }
        public int AwardConditionID { get; set; }
        public int MaxQtyWorldStateID { get; set; }
        [HotfixArray(2)]
        public int[] Flags { get; set; }
    }
}
