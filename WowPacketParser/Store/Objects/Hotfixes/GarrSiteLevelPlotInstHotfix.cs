using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_site_level_plot_inst")]
    public sealed record GarrSiteLevelPlotInstHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMarkerPosX")]
        public float? UiMarkerPosX;

        [DBFieldName("UiMarkerPosY")]
        public float? UiMarkerPosY;

        [DBFieldName("GarrSiteLevelID")]
        public ushort? GarrSiteLevelID;

        [DBFieldName("GarrPlotInstanceID")]
        public byte? GarrPlotInstanceID;

        [DBFieldName("UiMarkerSize")]
        public byte? UiMarkerSize;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_site_level_plot_inst")]
    public sealed record GarrSiteLevelPlotInstHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMarkerPosX")]
        public float? UiMarkerPosX;

        [DBFieldName("UiMarkerPosY")]
        public float? UiMarkerPosY;

        [DBFieldName("GarrSiteLevelID")]
        public ushort? GarrSiteLevelID;

        [DBFieldName("GarrPlotInstanceID")]
        public byte? GarrPlotInstanceID;

        [DBFieldName("UiMarkerSize")]
        public byte? UiMarkerSize;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
