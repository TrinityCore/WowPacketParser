using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_7_17898.Hotfix
{
    [HotfixStructure(DB2Hash.Item)]
    public class ItemEntry
    {
        public int ID { get; set; }
        public int Class { get; set; }
        public int Subclass { get; set; }
        public int SoundOverrideSubclass { get; set; }
        public int Material { get; set; }
        public int DisplayID { get; set; }
        public int InventoryType { get; set; }
        public int SheathType { get; set; }
    }
}
