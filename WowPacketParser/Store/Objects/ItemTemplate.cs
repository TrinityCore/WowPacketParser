using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_template")]
    public sealed record ItemTemplate : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("class")]
        public ItemClass? Class;

        [DBFieldName("subclass")]
        public uint? SubClass;

        [DBFieldName("SoundOverrideSubclass")]
        public int? SoundOverrideSubclass;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("displayid")]
        public uint? DisplayID;

        [DBFieldName("Quality")]
        public ItemQuality? Quality;

        [DBFieldName("Flags")]
        public ItemProtoFlags? Flags;

        [DBFieldName("FlagsExtra")]
        public ItemFlagExtra? FlagsExtra;

        [DBFieldName("Unk430_1", TargetedDatabaseFlag.SinceCataclysm)]
        public float? Unk430_1;

        [DBFieldName("Unk430_2", TargetedDatabaseFlag.SinceCataclysm)]
        public float? Unk430_2;

        [DBFieldName("BuyCount")]
        public uint? BuyCount;

        [DBFieldName("BuyPrice")]
        public long? BuyPrice;

        [DBFieldName("SellPrice")]
        public uint? SellPrice;

        [DBFieldName("InventoryType")]
        public InventoryType? InventoryType;

        [DBFieldName("AllowableClass")]
        public ClassMask? AllowedClasses;

        [DBFieldName("AllowableRace")]
        public RaceMask? AllowedRaces;

        [DBFieldName("ItemLevel")]
        public uint? ItemLevel;

        [DBFieldName("RequiredLevel")]
        public uint? RequiredLevel;

        [DBFieldName("RequiredSkill")]
        public uint? RequiredSkillId;

        [DBFieldName("RequiredSkillRank")]
        public uint? RequiredSkillLevel;

        [DBFieldName("requiredspell")]
        public uint? RequiredSpell;

        [DBFieldName("requiredhonorrank")]
        public uint? RequiredHonorRank;

        [DBFieldName("RequiredCityRank")]
        public uint? RequiredCityRank;

        [DBFieldName("RequiredReputationFaction")]
        public uint? RequiredRepFaction;

        [DBFieldName("RequiredReputationRank")]
        public uint? RequiredRepValue;

        [DBFieldName("maxcount")]
        public int? MaxCount;

        [DBFieldName("stackable")]
        public int? MaxStackSize;

        [DBFieldName("ContainerSlots")]
        public uint? ContainerSlots;

        [DBFieldName("StatsCount", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? StatsCount;

        [DBFieldName("stat_type", 10)]
        public ItemModType?[] StatTypes;

        [DBFieldName("stat_value", 10)]
        public int?[] StatValues;

        [DBFieldName("scaling_value", TargetedDatabaseFlag.SinceCataclysm, 10)]
        public int?[] ScalingValue;

        [DBFieldName("socket_cost_rate", TargetedDatabaseFlag.SinceCataclysm, 10)]
        public int?[] SocketCostRate;

        [DBFieldName("ScalingStatDistribution")]
        public int? ScalingStatDistribution;

        [DBFieldName("ScalingStatValue", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? ScalingStatValue;

        [DBFieldName("dmg_min", TargetedDatabaseFlag.TillWrathOfTheLichKing, 2)]
        public float?[] DamageMins;

        [DBFieldName("dmg_max", TargetedDatabaseFlag.TillWrathOfTheLichKing, 2)]
        public float?[] DamageMaxs;

        [DBFieldName("dmg_type", TargetedDatabaseFlag.TillWrathOfTheLichKing, 2)]
        public DamageType?[] DamageTypes;


        [DBFieldName("armor", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? Armor;

        [DBFieldName("holy_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? HolyResistance;

        [DBFieldName("fire_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? FireResistance;

        [DBFieldName("nature_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? NatureResistance;

        [DBFieldName("frost_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? FrostResistance;

        [DBFieldName("shadow_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? ShadowResistance;

        [DBFieldName("arcane_res", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? ArcaneResistance;

        [DBFieldName("DamageType", TargetedDatabaseFlag.SinceCataclysm)]
        public DamageType? DamageType;

        [DBFieldName("delay")]
        public uint? Delay;

        [DBFieldName("ammo_type", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public AmmoType? AmmoType;

        [DBFieldName("RangedModRange")]
        public float? RangedMod;

        [DBFieldName("spellid_", 5)]
        public int?[] TriggeredSpellIds;

        [DBFieldName("spelltrigger_", 5)]
        public ItemSpellTriggerType?[] TriggeredSpellTypes;

        [DBFieldName("spellcharges_", 5)]
        public int?[] TriggeredSpellCharges;

        [DBFieldName("spellppmRate_", TargetedDatabaseFlag.TillWrathOfTheLichKing, 5)]
        public float? TriggeredSpellPpmRate;

        [DBFieldName("spellcooldown_", 5)]
        public int?[] TriggeredSpellCooldowns;

        [DBFieldName("spellcategory_", 5)]
        public uint?[] TriggeredSpellCategories;

        [DBFieldName("spellcategorycooldown_", 5)]
        public int?[] TriggeredSpellCategoryCooldowns;

        [DBFieldName("bonding")]
        public ItemBonding? Bonding;

        [DBFieldName("description")]
        public string Description;

        [DBFieldName("PageText")]
        public uint? PageText;

        [DBFieldName("LanguageID")]
        public Language? Language;

        [DBFieldName("PageMaterial")]
        public PageMaterial? PageMaterial;

        [DBFieldName("startquest")]
        public uint? StartQuestId;

        [DBFieldName("lockid")]
        public uint? LockId;

        [DBFieldName("Material")]
        public Material? Material;

        [DBFieldName("sheath")]
        public SheathType? SheathType;

        [DBFieldName("RandomProperty")]
        public int? RandomPropery;

        [DBFieldName("RandomSuffix")]
        public uint? RandomSuffix;

        [DBFieldName("block", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? Block;

        [DBFieldName("itemset")]
        public uint? ItemSet;

        [DBFieldName("MaxDurability")]
        public uint? MaxDurability;

        [DBFieldName("area")]
        public uint? AreaID;

        [DBFieldName("Map")]
        public int? MapID;

        [DBFieldName("BagFamily")]
        public BagFamilyMask? BagFamily;

        [DBFieldName("TotemCategory")]
        public TotemCategory? TotemCategory;

        [DBFieldName("socketColor_", 3)]
        public ItemSocketColor?[] ItemSocketColors;

        [DBFieldName("socketContent_", 3)]
        public uint?[] SocketContent;

        [DBFieldName("socketBonus")]
        public int? SocketBonus;

        [DBFieldName("GemProperties")]
        public int? GemProperties;

        [DBFieldName("RequiredDisenchantSkill", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public int? RequiredDisenchantSkill;

        [DBFieldName("ArmorDamageModifier")]
        public float? ArmorDamageModifier;

        [DBFieldName("duration")]
        public uint? Duration;

        [DBFieldName("ItemLimitCategory")]
        public int? ItemLimitCategory;

        [DBFieldName("HolidayId")]
        public Holiday? HolidayID;

        [DBFieldName("ScriptName", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public string ScriptName;

        [DBFieldName("DisenchantID", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? DisenchantID;

        [DBFieldName("FoodType", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? FoodType;

        [DBFieldName("minMoneyLoot", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint MinMoneyLoot;

        [DBFieldName("maxMoneyLoot", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint MaxMoneyLoot;

        [DBFieldName("StatScalingFactor", TargetedDatabaseFlag.SinceCataclysm)]
        public float? StatScalingFactor;

        [DBFieldName("CurrencySubstitutionId", TargetedDatabaseFlag.SinceCataclysm)]
        public uint? CurrencySubstitutionID;

        [DBFieldName("CurrencySubstitutionCount", TargetedDatabaseFlag.SinceCataclysm)]
        public uint? CurrencySubstitutionCount;

        [DBFieldName("flagsCustom")]
        public uint? FlagsCustom;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
