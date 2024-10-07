using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("currency_types")]
    public sealed record CurrencyTypesHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("CategoryID")]
        public int? CategoryID;

        [DBFieldName("InventoryIconFileID")]
        public int? InventoryIconFileID;

        [DBFieldName("SpellWeight")]
        public uint? SpellWeight;

        [DBFieldName("SpellCategory")]
        public byte? SpellCategory;

        [DBFieldName("MaxQty")]
        public uint? MaxQty;

        [DBFieldName("MaxEarnablePerWeek")]
        public uint? MaxEarnablePerWeek;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("ItemGroupSoundsID")]
        public int? ItemGroupSoundsID;

        [DBFieldName("XpQuestDifficulty")]
        public int? XpQuestDifficulty;

        [DBFieldName("AwardConditionID")]
        public int? AwardConditionID;

        [DBFieldName("MaxQtyWorldStateID")]
        public int? MaxQtyWorldStateID;

        [DBFieldName("RechargingAmountPerCycle")]
        public uint? RechargingAmountPerCycle;

        [DBFieldName("RechargingCycleDurationMS")]
        public uint? RechargingCycleDurationMS;

        [DBFieldName("AccountTransferPercentage")]
        public float? AccountTransferPercentage;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("currency_types_locale")]
    public sealed record CurrencyTypesLocaleHotfix1100: IDataModel
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
