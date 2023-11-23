using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("adventure_journal")]
    public sealed record AdventureJournalHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ButtonText")]
        public string ButtonText;

        [DBFieldName("RewardDescription")]
        public string RewardDescription;

        [DBFieldName("ContinueDescription")]
        public string ContinueDescription;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ButtonActionType")]
        public byte? ButtonActionType;

        [DBFieldName("TextureFileDataID")]
        public int? TextureFileDataID;

        [DBFieldName("LfgDungeonID")]
        public ushort? LfgDungeonID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("BattleMasterListID")]
        public ushort? BattleMasterListID;

        [DBFieldName("PriorityMin")]
        public byte? PriorityMin;

        [DBFieldName("PriorityMax")]
        public byte? PriorityMax;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemQuantity")]
        public uint? ItemQuantity;

        [DBFieldName("CurrencyType")]
        public ushort? CurrencyType;

        [DBFieldName("CurrencyQuantity")]
        public uint? CurrencyQuantity;

        [DBFieldName("UiMapID")]
        public ushort? UiMapID;

        [DBFieldName("BonusPlayerConditionID", 2)]
        public uint?[] BonusPlayerConditionID;

        [DBFieldName("BonusValue", 2)]
        public byte?[] BonusValue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_journal_locale")]
    public sealed record AdventureJournalLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("ButtonText_lang")]
        public string ButtonTextLang;

        [DBFieldName("RewardDescription_lang")]
        public string RewardDescriptionLang;

        [DBFieldName("ContinueDescription_lang")]
        public string ContinueDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_journal")]
    public sealed record AdventureJournalHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ButtonText")]
        public string ButtonText;

        [DBFieldName("RewardDescription")]
        public string RewardDescription;

        [DBFieldName("ContinueDescription")]
        public string ContinueDescription;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ButtonActionType")]
        public byte? ButtonActionType;

        [DBFieldName("TextureFileDataID")]
        public int? TextureFileDataID;

        [DBFieldName("LfgDungeonID")]
        public ushort? LfgDungeonID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("BattleMasterListID")]
        public ushort? BattleMasterListID;

        [DBFieldName("PriorityMin")]
        public byte? PriorityMin;

        [DBFieldName("PriorityMax")]
        public byte? PriorityMax;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("ItemQuantity")]
        public uint? ItemQuantity;

        [DBFieldName("CurrencyType")]
        public ushort? CurrencyType;

        [DBFieldName("CurrencyQuantity")]
        public byte? CurrencyQuantity;

        [DBFieldName("UIMapID")]
        public ushort? UIMapID;

        [DBFieldName("BonusPlayerConditionID", 2)]
        public uint?[] BonusPlayerConditionID;

        [DBFieldName("BonusValue", 2)]
        public byte?[] BonusValue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_journal_locale")]
    public sealed record AdventureJournalLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("ButtonText_lang")]
        public string ButtonTextLang;

        [DBFieldName("RewardDescription_lang")]
        public string RewardDescriptionLang;

        [DBFieldName("ContinueDescription_lang")]
        public string ContinueDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
