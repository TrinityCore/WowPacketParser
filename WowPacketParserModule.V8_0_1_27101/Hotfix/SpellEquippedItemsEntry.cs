using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEquippedItems, HasIndexInData = false)]
    public class SpellEquippedItemsEntry
    {
        public int SpellID { get; set; }
        public sbyte EquippedItemClass { get; set; }
        public int EquippedItemInvTypes { get; set; }
        public int EquippedItemSubclass { get; set; }
    }
}
