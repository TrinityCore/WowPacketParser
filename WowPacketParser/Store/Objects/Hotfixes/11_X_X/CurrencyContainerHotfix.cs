using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("currency_container")]
    public sealed record CurrencyContainerHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ContainerName")]
        public string ContainerName;

        [DBFieldName("ContainerDescription")]
        public string ContainerDescription;

        [DBFieldName("MinAmount")]
        public int? MinAmount;

        [DBFieldName("MaxAmount")]
        public int? MaxAmount;

        [DBFieldName("ContainerIconID")]
        public int? ContainerIconID;

        [DBFieldName("ContainerQuality")]
        public sbyte? ContainerQuality;

        [DBFieldName("OnLootSpellVisualKitID")]
        public int? OnLootSpellVisualKitID;

        [DBFieldName("CurrencyTypesID")]
        public uint? CurrencyTypesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("currency_container_locale")]
    public sealed record CurrencyContainerLocaleHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ContainerName_lang")]
        public string ContainerNameLang;

        [DBFieldName("ContainerDescription_lang")]
        public string ContainerDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
