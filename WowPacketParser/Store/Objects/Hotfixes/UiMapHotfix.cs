using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("ui_map")]
    public sealed record UiMapHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public int? ParentUiMapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("System")]
        public uint? System;

        [DBFieldName("Type")]
        public uint? Type;

        [DBFieldName("BountySetID")]
        public int? BountySetID;

        [DBFieldName("BountyDisplayLocation")]
        public uint? BountyDisplayLocation;

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

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix1000: IDataModel
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
    public sealed record UiMapHotfix1015 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public int? ParentUiMapID;

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

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix1015 : IDataModel
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
    public sealed record UiMapHotfix1020 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public int? ParentUiMapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("System")]
        public sbyte? System;

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

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix1020 : IDataModel
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
    public sealed record UiMapHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public int? ParentUiMapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("System")]
        public uint? System;

        [DBFieldName("Type")]
        public uint? Type;

        [DBFieldName("BountySetID")]
        public int? BountySetID;

        [DBFieldName("BountyDisplayLocation")]
        public uint? BountyDisplayLocation;

        [DBFieldName("VisibilityPlayerConditionID")]
        public int? VisibilityPlayerConditionID;

        [DBFieldName("HelpTextPosition")]
        public sbyte? HelpTextPosition;

        [DBFieldName("BkgAtlasID")]
        public int? BkgAtlasID;

        [DBFieldName("LevelRangeMin")]
        public uint? LevelRangeMin;

        [DBFieldName("LevelRangeMax")]
        public uint? LevelRangeMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix340: IDataModel
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
    public sealed record UiMapHotfix343: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentUiMapID")]
        public int? ParentUiMapID;

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
        public uint? AlternateUiMapGroup;

        [DBFieldName("ContentTuningID")]
        public uint? ContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("ui_map_locale")]
    public sealed record UiMapLocaleHotfix343: IDataModel
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
