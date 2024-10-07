using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_plot_building")]
    public sealed record GarrPlotBuildingHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GarrPlotID")]
        public byte? GarrPlotID;

        [DBFieldName("GarrBuildingID")]
        public byte? GarrBuildingID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
