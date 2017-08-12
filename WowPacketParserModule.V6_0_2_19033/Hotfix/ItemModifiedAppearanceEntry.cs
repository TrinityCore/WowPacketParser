using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemModifiedAppearance)]
    public class ItemModifiedAppearanceEntry
    {
        public uint ID { get; set; }
        public uint ItemID { get; set; }
        public int AppearanceModID { get; set; }
        public int AppearanceID { get; set; }
        public int IconFileDataID { get; set; }
        public int Index { get; set; }
    }
}