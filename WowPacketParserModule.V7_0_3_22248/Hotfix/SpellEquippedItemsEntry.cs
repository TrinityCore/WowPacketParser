using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEquippedItems, HasIndexInData = false)]
    public class SpellEquippedItemsEntry
    {
        public uint SpellID { get; set; }
        public int EquippedItemInventoryTypeMask { get; set; }
        public int EquippedItemSubClassMask { get; set; }
        public sbyte EquippedItemClass { get; set; }
    }
}