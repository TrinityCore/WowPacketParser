using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_map")]
    public sealed record UiMapHotfix1100 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public uint? ParentUiMapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("System")]
        public byte? System;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("BountySetID")]
        public int? BountySetID;

        [DBFieldName("BountyDisplayLocation")]
        public uint? BountyDisplayLocation;

        [DBFieldName("VisibilityPlayerConditionID2")]
        public int? VisibilityPlayerConditionID2;

        [DBFieldName("VisibilityPlayerConditionID")]
        public int? VisibilityPlayerConditionID;

        [DBFieldName("HelpTextPosition")]
        public sbyte? HelpTextPosition;

        [DBFieldName("BkgAtlasID")]
        public int? BkgAtlasID;

        [DBFieldName("AlternateUiMapGroup")]
        public int? AlternateUiMapGroup;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("AdventureMapTextureKitID")]
        public int? AdventureMapTextureKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map")]
    public sealed record UiMapHotfix1102 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public uint? ParentUiMapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("System")]
        public byte? System;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("BountySetID")]
        public int? BountySetID;

        [DBFieldName("BountyDisplayLocation")]
        public uint? BountyDisplayLocation;

        [DBFieldName("VisibilityPlayerConditionID2")]
        public int? VisibilityPlayerConditionID2;

        [DBFieldName("VisibilityPlayerConditionID")]
        public int? VisibilityPlayerConditionID;

        [DBFieldName("HelpTextPosition")]
        public sbyte? HelpTextPosition;

        [DBFieldName("BkgAtlasID")]
        public int? BkgAtlasID;

        [DBFieldName("AlternateUiMapGroup")]
        public int? AlternateUiMapGroup;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("AdventureMapTextureKitID")]
        public int? AdventureMapTextureKitID;

        [DBFieldName("MapArtZoneTextPosition")]
        public sbyte? MapArtZoneTextPosition;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix1102 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
