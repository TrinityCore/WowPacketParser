using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_0_30898.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItemMilestonePower, ClientVersionBuild.V8_2_0_30898)]
    public class AzeriteItemMilestonePowerEntry
    {
        public uint ID { get; set; }
        public int RequiredLevel { get; set; }
        public int AzeritePowerID { get; set; }
        public int Type { get; set; }
        public int AutoUnlock { get; set; }
    }
}
