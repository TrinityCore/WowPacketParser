using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemAppearance)]
    public class ItemAppearanceEntry
    {
        public int ID { get; set; }
        public uint DisplayID { get; set; }
        public uint FileDataID { get; set; }
    }
}