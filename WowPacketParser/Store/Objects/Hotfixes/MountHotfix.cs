using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mount")]
    public sealed record MountHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("SourceSpellID")]
        public int? SourceSpellID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("MountFlyRideHeight")]
        public float? MountFlyRideHeight;

        [DBFieldName("UiModelSceneID")]
        public int? UiModelSceneID;

        [DBFieldName("MountSpecialRiderAnimKitID")]
        public int? MountSpecialRiderAnimKitID;

        [DBFieldName("MountSpecialSpellVisualKitID")]
        public int? MountSpecialSpellVisualKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount_locale")]
    public sealed record MountLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount")]
    public sealed record MountHotfix1020 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("SourceSpellID")]
        public int? SourceSpellID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("MountFlyRideHeight")]
        public float? MountFlyRideHeight;

        [DBFieldName("UiModelSceneID")]
        public int? UiModelSceneID;

        [DBFieldName("MountSpecialRiderAnimKitID")]
        public int? MountSpecialRiderAnimKitID;

        [DBFieldName("MountSpecialSpellVisualKitID")]
        public int? MountSpecialSpellVisualKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount_locale")]
    public sealed record MountLocaleHotfix1020 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount")]
    public sealed record MountHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("SourceSpellID")]
        public int? SourceSpellID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("MountFlyRideHeight")]
        public float? MountFlyRideHeight;

        [DBFieldName("UiModelSceneID")]
        public int? UiModelSceneID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount_locale")]
    public sealed record MountLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
