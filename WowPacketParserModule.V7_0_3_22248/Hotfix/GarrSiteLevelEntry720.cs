using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.GarrSiteLevel, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class GarrSiteLevelEntry
    {
        [HotfixArray(2)]
        public float[] TownHall { get; set; }
        public ushort MapID { get; set; }
        public ushort SiteID { get; set; }
        public ushort MovieID { get; set; }
        public ushort UpgradeResourceCost { get; set; }
        public ushort UpgradeMoneyCost { get; set; }
        public byte Level { get; set; }
        public byte UITextureKitID { get; set; }
        public byte Level2 { get; set; }
    }
}