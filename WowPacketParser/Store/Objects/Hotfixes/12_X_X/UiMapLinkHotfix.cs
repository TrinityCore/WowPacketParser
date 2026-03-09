using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_map_link")]
    public sealed record UiMapLinkHotfix1200: IDataModel
    {
        [DBFieldName("UiMinX")]
        public float? UiMinX;

        [DBFieldName("UiMinY")]
        public float? UiMinY;

        [DBFieldName("UiMaxX")]
        public float? UiMaxX;

        [DBFieldName("UiMaxY")]
        public float? UiMaxY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public uint? ParentUiMapID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("ChildUiMapID")]
        public int? ChildUiMapID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("OverrideHighlightFileDataID")]
        public int? OverrideHighlightFileDataID;

        [DBFieldName("OverrideHighlightAtlasID")]
        public int? OverrideHighlightAtlasID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
