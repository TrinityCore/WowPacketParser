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

        [DBFieldName("Unk430_1", TargetedDatabase.Cataclysm)]
        public float? Unk430_1;

        [DBFieldName("Unk430_2", TargetedDatabase.Cataclysm)]
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

        [DBFieldName("StatsCount", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? StatsCount;

        [DBFieldName("stat_type", 10)]
        public ItemModType?[] StatTypes;

        [DBFieldName("stat_value", 10)]
        public int?[] StatValues;

        [DBFieldName("scaling_value", TargetedDatabase.Cataclysm, 10)]
        public int?[] ScalingValue;

        [DBFieldName("socket_cost_rate", TargetedDatabase.Cataclysm, 10)]
        public int?[] SocketCostRate;

        [DBFieldName("ScalingStatDistribution")]
        public int? ScalingStatDistribution;

        [DBFieldName("ScalingStatValue", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? ScalingStatValue;

        [DBFieldName("dmg_min", TargetedDatabase.Zero, TargetedDatabase.Cataclysm, 2)]
        public float?[] DamageMins;

        [DBFieldName("dmg_max", TargetedDatabase.Zero, TargetedDatabase.Cataclysm, 2)]
        public float?[] DamageMaxs;

        [DBFieldName("dmg_type", TargetedDatabase.Zero, TargetedDatabase.Cataclysm, 2)]
        public DamageType?[] DamageTypes;


        [DBFieldName("armor", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? Armor;

        [DBFieldName("holy_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? HolyResistance;

        [DBFieldName("fire_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? FireResistance;

        [DBFieldName("nature_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? NatureResistance;

        [DBFieldName("frost_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? FrostResistance;

        [DBFieldName("shadow_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? ShadowResistance;

        [DBFieldName("arcane_res", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? ArcaneResistance;

        [DBFieldName("DamageType", TargetedDatabase.Cataclysm)]
        public DamageType? DamageType;

        [DBFieldName("delay")]
        public uint? Delay;

        [DBFieldName("ammo_type", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public AmmoType? AmmoType;

        [DBFieldName("RangedModRange")]
        public float? RangedMod;

        [DBFieldName("spellid_", 5)]
        public int?[] TriggeredSpellIds;

        [DBFieldName("spelltrigger_", 5)]
        public ItemSpellTriggerType?[] TriggeredSpellTypes;

        [DBFieldName("spellcharges_", 5)]
        public int?[] TriggeredSpellCharges;

        [DBFieldName("spellppmRate_", TargetedDatabase.Zero, TargetedDatabase.Cataclysm, 5)]
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

        [DBFieldName("block", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
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

        [DBFieldName("RequiredDisenchantSkill", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public int? RequiredDisenchantSkill;

        [DBFieldName("ArmorDamageModifier")]
        public float? ArmorDamageModifier;

        [DBFieldName("duration")]
        public uint? Duration;

        [DBFieldName("ItemLimitCategory")]
        public int? ItemLimitCategory;

        [DBFieldName("HolidayId")]
        public Holiday? HolidayID;

        [DBFieldName("ScriptName", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public string ScriptName;

        [DBFieldName("DisenchantID", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? DisenchantID;

        [DBFieldName("FoodType", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? FoodType;

        [DBFieldName("minMoneyLoot", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint MinMoneyLoot;

        [DBFieldName("maxMoneyLoot", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint MaxMoneyLoot;

        [DBFieldName("StatScalingFactor", TargetedDatabase.Cataclysm)]
        public float? StatScalingFactor;

        [DBFieldName("CurrencySubstitutionId", TargetedDatabase.Cataclysm)]
        public uint? CurrencySubstitutionID;

        [DBFieldName("CurrencySubstitutionCount", TargetedDatabase.Cataclysm)]
        public uint? CurrencySubstitutionCount;

        [DBFieldName("flagsCustom")]
        public uint? FlagsCustom;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
