using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_option")]
    public sealed record ChrCustomizationOptionHotfix1100: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SecondaryID")]
        public ushort? SecondaryID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ChrRacesID")]
        public int? ChrRacesID;

        [DBFieldName("Sex")]
        public int? Sex;

        [DBFieldName("ChrModelID")]
        public uint? ChrModelID;

        [DBFieldName("SortIndex")]
        public int? SortIndex;

        [DBFieldName("ChrCustomizationCategoryID")]
        public int? ChrCustomizationCategoryID;

        [DBFieldName("OptionType")]
        public int? OptionType;

        [DBFieldName("BarberShopCostModifier")]
        public float? BarberShopCostModifier;

        [DBFieldName("ChrCustomizationID")]
        public int? ChrCustomizationID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("UiOrderIndex")]
        public int? UiOrderIndex;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_option_locale")]
    public sealed record ChrCustomizationOptionLocaleHotfix1100: IDataModel
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
    [DBTableName("chr_customization_option")]
    public sealed record ChrCustomizationOptionHotfix1105 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SecondaryID")]
        public ushort? SecondaryID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ChrModelID")]
        public uint? ChrModelID;

        [DBFieldName("SortIndex")]
        public int? SortIndex;

        [DBFieldName("ChrCustomizationCategoryID")]
        public int? ChrCustomizationCategoryID;

        [DBFieldName("OptionType")]
        public int? OptionType;

        [DBFieldName("BarberShopCostModifier")]
        public float? BarberShopCostModifier;

        [DBFieldName("ChrCustomizationID")]
        public int? ChrCustomizationID;

        [DBFieldName("ChrCustomizationReqID")]
        public int? ChrCustomizationReqID;

        [DBFieldName("UiOrderIndex")]
        public int? UiOrderIndex;

        [DBFieldName("AddedInPatch")]
        public int? AddedInPatch;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_option_locale")]
    public sealed record ChrCustomizationOptionLocaleHotfix1105 : IDataModel
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
