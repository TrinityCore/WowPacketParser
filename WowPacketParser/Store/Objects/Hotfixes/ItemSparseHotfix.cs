using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_sparse")]
    public sealed record ItemSparseHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AllowableRace")]
        public long? AllowableRace;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Display3")]
        public string Display3;

        [DBFieldName("Display2")]
        public string Display2;

        [DBFieldName("Display1")]
        public string Display1;

        [DBFieldName("Display")]
        public string Display;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("DmgVariance")]
        public float? DmgVariance;

        [DBFieldName("LimitCategory")]
        public int? LimitCategory;

        [DBFieldName("DurationInInventory")]
        public uint? DurationInInventory;

        [DBFieldName("QualityModifier")]
        public float? QualityModifier;

        [DBFieldName("BagFamily")]
        public uint? BagFamily;

        [DBFieldName("StartQuestID")]
        public int? StartQuestID;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("ItemRange")]
        public float? ItemRange;

        [DBFieldName("StatPercentageOfSocket", 10)]
        public float?[] StatPercentageOfSocket;

        [DBFieldName("StatPercentEditor", 10)]
        public int?[] StatPercentEditor;

        [DBFieldName("Stackable")]
        public int? Stackable;

        [DBFieldName("MaxCount")]
        public int? MaxCount;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("RequiredAbility")]
        public uint? RequiredAbility;

        [DBFieldName("SellPrice")]
        public uint? SellPrice;

        [DBFieldName("BuyPrice")]
        public uint? BuyPrice;

        [DBFieldName("VendorStackCount")]
        public uint? VendorStackCount;

        [DBFieldName("PriceVariance")]
        public float? PriceVariance;

        [DBFieldName("PriceRandomValue")]
        public float? PriceRandomValue;

        [DBFieldName("Flags", 4)]
        public int?[] Flags;

        [DBFieldName("FactionRelated")]
        public int? FactionRelated;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("PlayerLevelToItemLevelCurveID")]
        public int? PlayerLevelToItemLevelCurveID;

        [DBFieldName("ItemNameDescriptionID")]
        public ushort? ItemNameDescriptionID;

        [DBFieldName("RequiredTransmogHoliday")]
        public ushort? RequiredTransmogHoliday;

        [DBFieldName("RequiredHoliday")]
        public ushort? RequiredHoliday;

        [DBFieldName("GemProperties")]
        public ushort? GemProperties;

        [DBFieldName("SocketMatchEnchantmentId")]
        public ushort? SocketMatchEnchantmentId;

        [DBFieldName("TotemCategoryID")]
        public ushort? TotemCategoryID;

        [DBFieldName("InstanceBound")]
        public ushort? InstanceBound;

        [DBFieldName("ZoneBound", 2)]
        public ushort?[] ZoneBound;

        [DBFieldName("ItemSet")]
        public ushort? ItemSet;

        [DBFieldName("LockID")]
        public ushort? LockID;

        [DBFieldName("PageID")]
        public ushort? PageID;

        [DBFieldName("ItemDelay")]
        public ushort? ItemDelay;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("RequiredSkill")]
        public ushort? RequiredSkill;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("AllowableClass")]
        public short? AllowableClass;

        [DBFieldName("ArtifactID")]
        public byte? ArtifactID;

        [DBFieldName("SpellWeight")]
        public byte? SpellWeight;

        [DBFieldName("SpellWeightCategory")]
        public byte? SpellWeightCategory;

        [DBFieldName("SocketType", 3)]
        public byte?[] SocketType;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("PageMaterialID")]
        public byte? PageMaterialID;

        [DBFieldName("Bonding")]
        public byte? Bonding;

        [DBFieldName("DamageDamageType")]
        public byte? DamageDamageType;

        [DBFieldName("StatModifierBonusStat", 10)]
        public sbyte?[] StatModifierBonusStat;

        [DBFieldName("ContainerSlots")]
        public byte? ContainerSlots;

        [DBFieldName("RequiredPVPMedal")]
        public byte? RequiredPVPMedal;

        [DBFieldName("RequiredPVPRank")]
        public byte? RequiredPVPRank;

        [DBFieldName("RequiredLevel")]
        public sbyte? RequiredLevel;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("OverallQualityID")]
        public sbyte? OverallQualityID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_sparse_locale")]
    public sealed record ItemSparseLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Display3_lang")]
        public string Display3Lang;

        [DBFieldName("Display2_lang")]
        public string Display2Lang;

        [DBFieldName("Display1_lang")]
        public string Display1Lang;

        [DBFieldName("Display_lang")]
        public string DisplayLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_sparse")]
    public sealed record ItemSparseHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AllowableRace")]
        public long? AllowableRace;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Display3")]
        public string Display3;

        [DBFieldName("Display2")]
        public string Display2;

        [DBFieldName("Display1")]
        public string Display1;

        [DBFieldName("Display")]
        public string Display;

        [DBFieldName("DmgVariance")]
        public float? DmgVariance;

        [DBFieldName("DurationInInventory")]
        public uint? DurationInInventory;

        [DBFieldName("QualityModifier")]
        public float? QualityModifier;

        [DBFieldName("BagFamily")]
        public uint? BagFamily;

        [DBFieldName("StartQuestID")]
        public int? StartQuestID;

        [DBFieldName("ItemRange")]
        public float? ItemRange;

        [DBFieldName("StatPercentageOfSocket", 10)]
        public float?[] StatPercentageOfSocket;

        [DBFieldName("StatPercentEditor", 10)]
        public int?[] StatPercentEditor;

        [DBFieldName("Stackable")]
        public int? Stackable;

        [DBFieldName("MaxCount")]
        public int? MaxCount;

        [DBFieldName("RequiredAbility")]
        public uint? RequiredAbility;

        [DBFieldName("SellPrice")]
        public uint? SellPrice;

        [DBFieldName("BuyPrice")]
        public uint? BuyPrice;

        [DBFieldName("VendorStackCount")]
        public uint? VendorStackCount;

        [DBFieldName("PriceVariance")]
        public float? PriceVariance;

        [DBFieldName("PriceRandomValue")]
        public float? PriceRandomValue;

        [DBFieldName("Flags", 4)]
        public int?[] Flags;

        [DBFieldName("FactionRelated")]
        public int? FactionRelated;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("PlayerLevelToItemLevelCurveID")]
        public int? PlayerLevelToItemLevelCurveID;

        [DBFieldName("MaxDurability")]
        public uint? MaxDurability;

        [DBFieldName("ItemNameDescriptionID")]
        public ushort? ItemNameDescriptionID;

        [DBFieldName("RequiredTransmogHoliday")]
        public ushort? RequiredTransmogHoliday;

        [DBFieldName("RequiredHoliday")]
        public ushort? RequiredHoliday;

        [DBFieldName("LimitCategory")]
        public ushort? LimitCategory;

        [DBFieldName("GemProperties")]
        public ushort? GemProperties;

        [DBFieldName("SocketMatchEnchantmentID")]
        public ushort? SocketMatchEnchantmentID;

        [DBFieldName("TotemCategoryID")]
        public ushort? TotemCategoryID;

        [DBFieldName("InstanceBound")]
        public ushort? InstanceBound;

        [DBFieldName("ZoneBound", 2)]
        public ushort?[] ZoneBound;

        [DBFieldName("ItemSet")]
        public ushort? ItemSet;

        [DBFieldName("LockID")]
        public ushort? LockID;

        [DBFieldName("PageID")]
        public ushort? PageID;

        [DBFieldName("ItemDelay")]
        public ushort? ItemDelay;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("RequiredSkill")]
        public ushort? RequiredSkill;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("AllowableClass")]
        public short? AllowableClass;

        [DBFieldName("ItemRandomSuffixGroupID")]
        public ushort? ItemRandomSuffixGroupID;

        [DBFieldName("RandomSelect")]
        public ushort? RandomSelect;

        [DBFieldName("MinDamage", 5)]
        public ushort?[] MinDamage;

        [DBFieldName("MaxDamage", 5)]
        public ushort?[] MaxDamage;

        [DBFieldName("Resistances", 7)]
        public short?[] Resistances;

        [DBFieldName("ScalingStatDistributionID")]
        public ushort? ScalingStatDistributionID;

        [DBFieldName("StatModifierBonusAmount", 10)]
        public short?[] StatModifierBonusAmount;

        [DBFieldName("ExpansionID")]
        public byte? ExpansionID;

        [DBFieldName("ArtifactID")]
        public byte? ArtifactID;

        [DBFieldName("SpellWeight")]
        public byte? SpellWeight;

        [DBFieldName("SpellWeightCategory")]
        public byte? SpellWeightCategory;

        [DBFieldName("SocketType", 3)]
        public byte?[] SocketType;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("PageMaterialID")]
        public byte? PageMaterialID;

        [DBFieldName("LanguageID")]
        public byte? LanguageID;

        [DBFieldName("Bonding")]
        public byte? Bonding;

        [DBFieldName("DamageDamageType")]
        public byte? DamageDamageType;

        [DBFieldName("StatModifierBonusStat", 10)]
        public sbyte?[] StatModifierBonusStat;

        [DBFieldName("ContainerSlots")]
        public byte? ContainerSlots;

        [DBFieldName("MinReputation")]
        public byte? MinReputation;

        [DBFieldName("RequiredPVPMedal")]
        public byte? RequiredPVPMedal;

        [DBFieldName("RequiredPVPRank")]
        public byte? RequiredPVPRank;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("OverallQualityID")]
        public sbyte? OverallQualityID;

        [DBFieldName("AmmunitionType")]
        public byte? AmmunitionType;

        [DBFieldName("RequiredLevel")]
        public sbyte? RequiredLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_sparse_locale")]
    public sealed record ItemSparseLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Display3_lang")]
        public string Display3Lang;

        [DBFieldName("Display2_lang")]
        public string Display2Lang;

        [DBFieldName("Display1_lang")]
        public string Display1Lang;

        [DBFieldName("Display_lang")]
        public string DisplayLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("item_sparse")]
    public sealed record ItemSparseHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AllowableRace")]
        public long? AllowableRace;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Display3")]
        public string Display3;

        [DBFieldName("Display2")]
        public string Display2;

        [DBFieldName("Display1")]
        public string Display1;

        [DBFieldName("Display")]
        public string Display;

        [DBFieldName("DmgVariance")]
        public float? DmgVariance;

        [DBFieldName("DurationInInventory")]
        public uint? DurationInInventory;

        [DBFieldName("QualityModifier")]
        public float? QualityModifier;

        [DBFieldName("BagFamily")]
        public uint? BagFamily;

        [DBFieldName("StartQuestID")]
        public int? StartQuestID;

        [DBFieldName("ItemRange")]
        public float? ItemRange;

        [DBFieldName("StatPercentageOfSocket", 10)]
        public float?[] StatPercentageOfSocket;

        [DBFieldName("StatPercentEditor", 10)]
        public int?[] StatPercentEditor;

        [DBFieldName("Stackable")]
        public int? Stackable;

        [DBFieldName("MaxCount")]
        public int? MaxCount;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("RequiredAbility")]
        public uint? RequiredAbility;

        [DBFieldName("SellPrice")]
        public uint? SellPrice;

        [DBFieldName("BuyPrice")]
        public uint? BuyPrice;

        [DBFieldName("VendorStackCount")]
        public uint? VendorStackCount;

        [DBFieldName("PriceVariance")]
        public float? PriceVariance;

        [DBFieldName("PriceRandomValue")]
        public float? PriceRandomValue;

        [DBFieldName("Flags", 4)]
        public int?[] Flags;

        [DBFieldName("FactionRelated")]
        public int? FactionRelated;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("PlayerLevelToItemLevelCurveID")]
        public int? PlayerLevelToItemLevelCurveID;

        [DBFieldName("MaxDurability")]
        public uint? MaxDurability;

        [DBFieldName("ItemNameDescriptionID")]
        public ushort? ItemNameDescriptionID;

        [DBFieldName("RequiredTransmogHoliday")]
        public ushort? RequiredTransmogHoliday;

        [DBFieldName("RequiredHoliday")]
        public ushort? RequiredHoliday;

        [DBFieldName("LimitCategory")]
        public ushort? LimitCategory;

        [DBFieldName("GemProperties")]
        public ushort? GemProperties;

        [DBFieldName("SocketMatchEnchantmentID")]
        public ushort? SocketMatchEnchantmentID;

        [DBFieldName("TotemCategoryID")]
        public ushort? TotemCategoryID;

        [DBFieldName("InstanceBound")]
        public ushort? InstanceBound;

        [DBFieldName("ZoneBound", 2)]
        public ushort?[] ZoneBound;

        [DBFieldName("ItemSet")]
        public ushort? ItemSet;

        [DBFieldName("LockID")]
        public ushort? LockID;

        [DBFieldName("PageID")]
        public ushort? PageID;

        [DBFieldName("ItemDelay")]
        public ushort? ItemDelay;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("RequiredSkill")]
        public ushort? RequiredSkill;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("AllowableClass")]
        public short? AllowableClass;

        [DBFieldName("ItemRandomSuffixGroupID")]
        public ushort? ItemRandomSuffixGroupID;

        [DBFieldName("RandomSelect")]
        public ushort? RandomSelect;

        [DBFieldName("MinDamage", 5)]
        public ushort?[] MinDamage;

        [DBFieldName("MaxDamage", 5)]
        public ushort?[] MaxDamage;

        [DBFieldName("Resistances", 7)]
        public short?[] Resistances;

        [DBFieldName("ScalingStatDistributionID")]
        public ushort? ScalingStatDistributionID;

        [DBFieldName("StatModifierBonusAmount", 10)]
        public short?[] StatModifierBonusAmount;

        [DBFieldName("ExpansionID")]
        public byte? ExpansionID;

        [DBFieldName("ArtifactID")]
        public byte? ArtifactID;

        [DBFieldName("SpellWeight")]
        public byte? SpellWeight;

        [DBFieldName("SpellWeightCategory")]
        public byte? SpellWeightCategory;

        [DBFieldName("SocketType", 3)]
        public byte?[] SocketType;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("PageMaterialID")]
        public byte? PageMaterialID;

        [DBFieldName("LanguageID")]
        public byte? LanguageID;

        [DBFieldName("Bonding")]
        public byte? Bonding;

        [DBFieldName("DamageDamageType")]
        public byte? DamageDamageType;

        [DBFieldName("StatModifierBonusStat", 10)]
        public sbyte?[] StatModifierBonusStat;

        [DBFieldName("ContainerSlots")]
        public byte? ContainerSlots;

        [DBFieldName("RequiredPVPMedal")]
        public byte? RequiredPVPMedal;

        [DBFieldName("RequiredPVPRank")]
        public byte? RequiredPVPRank;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("OverallQualityID")]
        public sbyte? OverallQualityID;

        [DBFieldName("AmmunitionType")]
        public byte? AmmunitionType;

        [DBFieldName("RequiredLevel")]
        public sbyte? RequiredLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_sparse_locale")]
    public sealed record ItemSparseLocaleHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Display3_lang")]
        public string Display3Lang;

        [DBFieldName("Display2_lang")]
        public string Display2Lang;

        [DBFieldName("Display1_lang")]
        public string Display1Lang;

        [DBFieldName("Display_lang")]
        public string DisplayLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
