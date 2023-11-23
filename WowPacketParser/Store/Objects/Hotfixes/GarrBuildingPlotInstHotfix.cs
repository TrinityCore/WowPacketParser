using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_building_plot_inst")]
    public sealed record GarrBuildingPlotInstHotfix1000: IDataModel
    {
        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GarrBuildingID")]
        public byte? GarrBuildingID;

        [DBFieldName("GarrSiteLevelPlotInstID")]
        public ushort? GarrSiteLevelPlotInstID;

        [DBFieldName("UiTextureAtlasMemberID")]
        public ushort? UiTextureAtlasMemberID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_building_plot_inst")]
    public sealed record GarrBuildingPlotInstHotfix340: IDataModel
    {
        [DBFieldName("MapOffsetX")]
        public float? MapOffsetX;

        [DBFieldName("MapOffsetY")]
        public float? MapOffsetY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GarrBuildingID")]
        public byte? GarrBuildingID;

        [DBFieldName("GarrSiteLevelPlotInstID")]
        public ushort? GarrSiteLevelPlotInstID;

        [DBFieldName("UiTextureAtlasMemberID")]
        public ushort? UiTextureAtlasMemberID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
