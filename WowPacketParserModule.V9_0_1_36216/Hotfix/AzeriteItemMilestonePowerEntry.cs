using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItemMilestonePower)]
    public class AzeriteItemMilestonePowerEntry
    {
        public uint ID { get; set; }
        public int RequiredLevel { get; set; }
        public int AzeritePowerID { get; set; }
        public int Type { get; set; }
        public int AutoUnlock { get; set; }
    }
}
