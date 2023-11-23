using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_map_x_map_art")]
    public sealed record UiMapXMapArtHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PhaseID")]
        public int? PhaseID;

        [DBFieldName("UiMapArtID")]
        public int? UiMapArtID;

        [DBFieldName("UiMapID")]
        public uint? UiMapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_x_map_art")]
    public sealed record UiMapXMapArtHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PhaseID")]
        public int? PhaseID;

        [DBFieldName("UiMapArtID")]
        public int? UiMapArtID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
