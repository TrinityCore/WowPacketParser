using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Item)]
    public class ItemEntry
    {
        public int ID { get; set; }
        public int Class { get; set; }
        public int Subclass { get; set; }
        public int SoundOverrideSubclass { get; set; }
        public int Material { get; set; }
        public int InventoryType { get; set; }
        public int SheathType { get; set; }
        public int FileDataID { get; set; }
        public int GroupSoundsID { get; set; }
    }
}
