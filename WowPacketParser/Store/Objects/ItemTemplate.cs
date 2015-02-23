using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_template")]
    public sealed class ItemTemplate
    {
        [DBFieldName("class")]
        public ItemClass Class;

        [DBFieldName("subclass")]
        public uint SubClass;

        [DBFieldName("SoundOverrideSubclass")]
        public int SoundOverrideSubclass;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("displayid")]
        public uint DisplayId;

        [DBFieldName("Quality")]
        public ItemQuality Quality;

        [DBFieldName("Flags")]
        public ItemProtoFlags Flags1;

        [DBFieldName("FlagsExtra")]
        public ItemFlagExtra Flags2;

        [DBFieldName("Unk430_1", ClientVersionBuild.V4_3_0_15005)]
        public float Unk430_1;

        [DBFieldName("Unk430_2", ClientVersionBuild.V4_3_0_15005)]
        public float Unk430_2;

        //[DBFieldName("Flags 3", ClientVersionBuild.V5_3_0_16981)] Added in soon
        public uint Flags3;

        [DBFieldName("BuyCount")]
        public uint BuyCount;

        [DBFieldName("BuyPrice")]
        public long BuyPrice;

        [DBFieldName("SellPrice")]
        public uint SellPrice;

        [DBFieldName("InventoryType")]
        public InventoryType InventoryType;

        [DBFieldName("AllowableClass")]
        public ClassMask AllowedClasses;

        [DBFieldName("AllowableRace")]
        public RaceMask AllowedRaces;

        [DBFieldName("ItemLevel")]
        public uint ItemLevel;

        [DBFieldName("RequiredLevel")]
        public uint RequiredLevel;

        [DBFieldName("RequiredSkill")]
        public uint RequiredSkillId;

        [DBFieldName("RequiredSkillRank")]
        public uint RequiredSkillLevel;

        [DBFieldName("requiredspell")]
        public uint RequiredSpell;

        [DBFieldName("requiredhonorrank")]
        public uint RequiredHonorRank;

        [DBFieldName("RequiredCityRank")]
        public uint RequiredCityRank;

        [DBFieldName("RequiredReputationFaction")]
        public uint RequiredRepFaction;

        [DBFieldName("RequiredReputationRank")]
        public uint RequiredRepValue;

        [DBFieldName("maxcount")]
        public int MaxCount;

        [DBFieldName("stackable")]
        public int MaxStackSize;

        [DBFieldName("ContainerSlots")]
        public uint ContainerSlots;

        [DBFieldName("StatsCount", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public uint StatsCount;

        [DBFieldName("stat_type", 10)]
        public ItemModType[] StatTypes;

        [DBFieldName("stat_value", 10)]
        public int[] StatValues;

        [DBFieldName("scaling_value", ClientVersionBuild.V4_0_1_13164, 10)]
        public int[] ScalingValue;

        [DBFieldName("socket_cost_rate", ClientVersionBuild.V4_0_1_13164, 10)]
        public int[] SocketCostRate;

        [DBFieldName("ScalingStatDistribution")]
        public int ScalingStatDistribution;

        [DBFieldName("ScalingStatValue", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public uint ScalingStatValue;

        [DBFieldName("dmg_min", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, 2)]
        public float[] DamageMins;

        [DBFieldName("dmg_max", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, 2)]
        public float[] DamageMaxs;

        [DBFieldName("dmg_type", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, 2)]
        public DamageType[] DamageTypes;

        //[DBFieldName(ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, "armor", "holy_res", "fire_res", "nature_res", "frost_res", "shadow_res", "arcane_res")]
        public DamageType[] Resistances; // armor is included

        [DBFieldName("DamageType", ClientVersionBuild.V4_0_1_13164)]
        public DamageType DamageType;

        [DBFieldName("delay")]
        public uint Delay;

        [DBFieldName("ammo_type", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public AmmoType AmmoType;

        [DBFieldName("RangedModRange")]
        public float RangedMod;

        [DBFieldName("spellid_", 5)]
        public int[] TriggeredSpellIds;

        [DBFieldName("spelltrigger_", 5)]
        public ItemSpellTriggerType[] TriggeredSpellTypes;

        [DBFieldName("spellcharges_", 5)]
        public int[] TriggeredSpellCharges;

        [DBFieldName("spellcooldown_", 5)]
        public int[] TriggeredSpellCooldowns;

        [DBFieldName("spellcategory_", 5)]
        public uint[] TriggeredSpellCategories;

        [DBFieldName("spellcategorycooldown_", 5)]
        public int[] TriggeredSpellCategoryCooldowns;

        [DBFieldName("bonding")]
        public ItemBonding Bonding;

        [DBFieldName("description")]
        public string Description;

        [DBFieldName("PageText")]
        public uint PageText;

        [DBFieldName("LanguageID")]
        public Language Language;

        [DBFieldName("PageMaterial")]
        public PageMaterial PageMaterial;

        [DBFieldName("startquest")]
        public uint StartQuestId;

        [DBFieldName("lockid")]
        public uint LockId;

        [DBFieldName("Material")]
        public Material Material;

        [DBFieldName("sheath")]
        public SheathType SheathType;

        [DBFieldName("RandomProperty")]
        public int RandomPropery;

        [DBFieldName("RandomSuffix")]
        public uint RandomSuffix;

        [DBFieldName("block", ClientVersionBuild.Zero, ClientVersionBuild.V4_1_0_13914)]
        public uint Block;

        [DBFieldName("itemset")]
        public uint ItemSet;

        [DBFieldName("MaxDurability")]
        public uint MaxDurability;

        [DBFieldName("area")]
        public uint AreaId;

        [DBFieldName("Map")]
        public int MapId;

        [DBFieldName("BagFamily")]
        public BagFamilyMask BagFamily;

        [DBFieldName("TotemCategory")]
        public TotemCategory TotemCategory;

        [DBFieldName("socketColor_", 3)]
        public ItemSocketColor[] ItemSocketColors;

        [DBFieldName("socketContent_", 3)]
        public uint[] SocketContent;

        [DBFieldName("socketBonus")]
        public int SocketBonus;

        [DBFieldName("GemProperties")]
        public int GemProperties;

        [DBFieldName("RequiredDisenchantSkill", ClientVersionBuild.Zero, ClientVersionBuild.V4_1_0_13914)]
        public int RequiredDisenchantSkill;

        [DBFieldName("ArmorDamageModifier")]
        public float ArmorDamageModifier;

        [DBFieldName("duration")]
        public uint Duration;

        [DBFieldName("ItemLimitCategory")]
        public int ItemLimitCategory;

        [DBFieldName("HolidayId")]
        public Holiday HolidayId;

        [DBFieldName("StatScalingFactor", ClientVersionBuild.V4_0_1_13164)]
        public float StatScalingFactor;

        [DBFieldName("CurrencySubstitutionId", ClientVersionBuild.V4_0_1_13164)]
        public uint CurrencySubstitutionId;

        [DBFieldName("CurrencySubstitutionCount", ClientVersionBuild.V4_0_1_13164)]
        public uint CurrencySubstitutionCount;

        [DBFieldName("ItemNameDescriptionID", ClientVersionBuild.V6_0_2_19033)]
        public uint ItemNameDescriptionId;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
