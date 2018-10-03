using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Item, HasIndexInData = false)]
    public class ItemEntry
    {
        public byte ClassID { get; set; }
        public byte SubclassID { get; set; }
        public byte Material { get; set; }
        public sbyte InventoryType { get; set; }
        public byte SheatheType { get; set; }
        public sbyte SoundOverrideSubclassID { get; set; }
        public int IconFileDataID { get; set; }
        public byte ItemGroupSoundsID { get; set; }
    }
}
