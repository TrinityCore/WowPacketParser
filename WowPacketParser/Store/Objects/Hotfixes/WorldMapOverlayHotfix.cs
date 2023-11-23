using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("world_map_overlay")]
    public sealed record WorldMapOverlayHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMapArtID")]
        public uint? UiMapArtID;

        [DBFieldName("TextureWidth")]
        public ushort? TextureWidth;

        [DBFieldName("TextureHeight")]
        public ushort? TextureHeight;

        [DBFieldName("OffsetX")]
        public int? OffsetX;

        [DBFieldName("OffsetY")]
        public int? OffsetY;

        [DBFieldName("HitRectTop")]
        public int? HitRectTop;

        [DBFieldName("HitRectBottom")]
        public int? HitRectBottom;

        [DBFieldName("HitRectLeft")]
        public int? HitRectLeft;

        [DBFieldName("HitRectRight")]
        public int? HitRectRight;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("AreaID", 4)]
        public uint?[] AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("world_map_overlay")]
    public sealed record WorldMapOverlayHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiMapArtID")]
        public uint? UiMapArtID;

        [DBFieldName("TextureWidth")]
        public ushort? TextureWidth;

        [DBFieldName("TextureHeight")]
        public ushort? TextureHeight;

        [DBFieldName("OffsetX")]
        public int? OffsetX;

        [DBFieldName("OffsetY")]
        public int? OffsetY;

        [DBFieldName("HitRectTop")]
        public int? HitRectTop;

        [DBFieldName("HitRectBottom")]
        public int? HitRectBottom;

        [DBFieldName("HitRectLeft")]
        public int? HitRectLeft;

        [DBFieldName("HitRectRight")]
        public int? HitRectRight;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("AreaID", 4)]
        public uint?[] AreaID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
