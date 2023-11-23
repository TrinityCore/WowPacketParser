using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_choice")]
    public sealed record ChrCustomizationChoiceHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationOptionID")]
        public int? ChrCustomizationOptionID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("ChrCustomizationVisReqID")]
        public int? ChrCustomizationVisReqID;

        [DBFieldName("SortOrder")]
        public ushort? SortOrder;

        [DBFieldName("UiOrderIndex")]
        public ushort? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("SwatchColor", 2)]
        public int?[] SwatchColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_choice_locale")]
    public sealed record ChrCustomizationChoiceLocaleHotfix1000: IDataModel
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
    [DBTableName("chr_customization_choice")]
    public sealed record ChrCustomizationChoiceHotfix1010 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationOptionID")]
        public int? ChrCustomizationOptionID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("ChrCustomizationVisReqID")]
        public int? ChrCustomizationVisReqID;

        [DBFieldName("SortOrder")]
        public ushort? SortOrder;

        [DBFieldName("UiOrderIndex")]
        public ushort? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("SoundKitID")]
        public int? SoundKitID;

        [DBFieldName("SwatchColor", 2)]
        public int?[] SwatchColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_choice_locale")]
    public sealed record ChrCustomizationChoiceLocaleHotfix1010 : IDataModel
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
    [DBTableName("chr_customization_choice")]
    public sealed record ChrCustomizationChoiceHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationOptionID")]
        public int? ChrCustomizationOptionID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("SortOrder")]
        public ushort? SortOrder;

        [DBFieldName("UiOrderIndex")]
        public ushort? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("SwatchColor", 2)]
        public int?[] SwatchColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_choice_locale")]
    public sealed record ChrCustomizationChoiceLocaleHotfix340: IDataModel
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
    [DBTableName("chr_customization_choice")]
    public sealed record ChrCustomizationChoiceHotfix341: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationOptionID")]
        public int? ChrCustomizationOptionID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("ChrCustomizationVisReqID")]
        public int? ChrCustomizationVisReqID;

        [DBFieldName("SortOrder")]
        public ushort? SortOrder;

        [DBFieldName("UiOrderIndex")]
        public ushort? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("SwatchColor", 2)]
        public int?[] SwatchColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_choice_locale")]
    public sealed record ChrCustomizationChoiceLocaleHotfix341: IDataModel
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
    [DBTableName("chr_customization_choice")]
    public sealed record ChrCustomizationChoiceHotfix342: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationOptionID")]
        public int? ChrCustomizationOptionID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("ChrCustomizationVisReqID")]
        public int? ChrCustomizationVisReqID;

        [DBFieldName("SortOrder")]
        public ushort? SortOrder;

        [DBFieldName("UiOrderIndex")]
        public ushort? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("SoundKitID")]
        public int? SoundKitID;

        [DBFieldName("SwatchColor", 2)]
        public int?[] SwatchColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_choice_locale")]
    public sealed record ChrCustomizationChoiceLocaleHotfix342: IDataModel
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
