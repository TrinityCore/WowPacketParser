using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItemMilestonePower, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class AzeriteItemMilestonePowerEntry
    {
        public int RequiredLevel { get; set; }
        public int AzeritePowerID { get; set; }
        public int Type { get; set; }
        public int AutoUnlock { get; set; }
    }
}
