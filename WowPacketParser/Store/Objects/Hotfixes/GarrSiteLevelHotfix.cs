using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_site_level")]
    public sealed record GarrSiteLevelHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TownHallUiPosX")]
        public float? TownHallUiPosX;

        [DBFieldName("TownHallUiPosY")]
        public float? TownHallUiPosY;

        [DBFieldName("GarrSiteID")]
        public uint? GarrSiteID;

        [DBFieldName("GarrLevel")]
        public byte? GarrLevel;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("UpgradeMovieID")]
        public ushort? UpgradeMovieID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("MaxBuildingLevel")]
        public byte? MaxBuildingLevel;

        [DBFieldName("UpgradeCost")]
        public ushort? UpgradeCost;

        [DBFieldName("UpgradeGoldCost")]
        public ushort? UpgradeGoldCost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_site_level")]
    public sealed record GarrSiteLevelHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TownHallUiPosX")]
        public float? TownHallUiPosX;

        [DBFieldName("TownHallUiPosY")]
        public float? TownHallUiPosY;

        [DBFieldName("GarrSiteID")]
        public uint? GarrSiteID;

        [DBFieldName("GarrLevel")]
        public byte? GarrLevel;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("UpgradeMovieID")]
        public ushort? UpgradeMovieID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("MaxBuildingLevel")]
        public byte? MaxBuildingLevel;

        [DBFieldName("UpgradeCost")]
        public ushort? UpgradeCost;

        [DBFieldName("UpgradeGoldCost")]
        public ushort? UpgradeGoldCost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
