using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrSiteLevel, HasIndexInData = false)]
    public class GarrSiteLevelEntry
    {
        [HotfixArray(2, true)]
        public float[] TownHallUiPos { get; set; }
        public uint GarrSiteID { get; set; }
        public byte GarrLevel { get; set; }
        public ushort MapID { get; set; }
        public ushort UpgradeMovieID { get; set; }
        public ushort UiTextureKitID { get; set; }
        public byte MaxBuildingLevel { get; set; }
        public ushort UpgradeCost { get; set; }
        public ushort UpgradeGoldCost { get; set; }
    }
}
