using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemModifiedAppearanceExtra, HasIndexInData = false)]
    public class ItemModifiedAppearanceExtraEntry
    {
        public int IconFileDataID { get; set; }
        public int UnequippedIconFileDataID { get; set; }
        public byte SheatheType { get; set; }
        public sbyte DisplayWeaponSubclassID { get; set; }
        public sbyte DisplayInventoryType { get; set; }
    }
}
