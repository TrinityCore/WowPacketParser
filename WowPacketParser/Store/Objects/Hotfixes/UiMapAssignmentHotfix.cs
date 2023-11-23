using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_map_assignment")]
    public sealed record UiMapAssignmentHotfix1000: IDataModel
    {
        [DBFieldName("UiMinX")]
        public float? UiMinX;

        [DBFieldName("UiMinY")]
        public float? UiMinY;

        [DBFieldName("UiMaxX")]
        public float? UiMaxX;

        [DBFieldName("UiMaxY")]
        public float? UiMaxY;

        [DBFieldName("Region1X")]
        public float? Region1X;

        [DBFieldName("Region1Y")]
        public float? Region1Y;

        [DBFieldName("Region1Z")]
        public float? Region1Z;

        [DBFieldName("Region2X")]
        public float? Region2X;

        [DBFieldName("Region2Y")]
        public float? Region2Y;

        [DBFieldName("Region2Z")]
        public float? Region2Z;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("AreaID")]
        public int? AreaID;

        [DBFieldName("WmoDoodadPlacementID")]
        public int? WmoDoodadPlacementID;

        [DBFieldName("WmoGroupID")]
        public int? WmoGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_assignment")]
    public sealed record UiMapAssignmentHotfix340: IDataModel
    {
        [DBFieldName("UiMinX")]
        public float? UiMinX;

        [DBFieldName("UiMinY")]
        public float? UiMinY;

        [DBFieldName("UiMaxX")]
        public float? UiMaxX;

        [DBFieldName("UiMaxY")]
        public float? UiMaxY;

        [DBFieldName("Region1X")]
        public float? Region1X;

        [DBFieldName("Region1Y")]
        public float? Region1Y;

        [DBFieldName("Region1Z")]
        public float? Region1Z;

        [DBFieldName("Region2X")]
        public float? Region2X;

        [DBFieldName("Region2Y")]
        public float? Region2Y;

        [DBFieldName("Region2Z")]
        public float? Region2Z;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMapID")]
        public int? UiMapID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("AreaID")]
        public int? AreaID;

        [DBFieldName("WmoDoodadPlacementID")]
        public int? WmoDoodadPlacementID;

        [DBFieldName("WmoGroupID")]
        public int? WmoGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
