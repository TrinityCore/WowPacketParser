using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_ability")]
    public sealed record GarrAbilityHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("GarrAbilityCategoryID")]
        public byte? GarrAbilityCategoryID;

        [DBFieldName("GarrFollowerTypeID")]
        public sbyte? GarrFollowerTypeID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("FactionChangeGarrAbilityID")]
        public ushort? FactionChangeGarrAbilityID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_ability_locale")]
    public sealed record GarrAbilityLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
