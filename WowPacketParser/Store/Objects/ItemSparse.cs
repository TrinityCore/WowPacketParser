using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item_sparse")]
    public sealed class ItemSparse
    {
        [DBFieldName("Quality")]
        public ItemQuality Quality;

        [DBFieldName("Flags1")]
        public ItemProtoFlags Flags1;

        [DBFieldName("Flags2")]
        public ItemFlagExtra Flags2;

        [DBFieldName("Flags3")]
        public uint Flags3;

        [DBFieldName("Unk1")]
        public float Unk1;

        [DBFieldName("Unk2")]
        public float Unk2;

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
        public uint RequiredSkill;

        [DBFieldName("RequiredSkillRank")]
        public uint RequiredSkillRank;

        [DBFieldName("RequiredSpell")]
        public uint RequiredSpell;

        [DBFieldName("RequiredHonorRank")]
        public uint RequiredHonorRank;

        [DBFieldName("RequiredCityRank")]
        public uint RequiredCityRank;

        [DBFieldName("RequiredReputationFaction")]
        public uint RequiredReputationFaction;

        [DBFieldName("RequiredReputationRank")]
        public uint RequiredReputationRank;

        [DBFieldName("MaxCount")]
        public int MaxCount;

        [DBFieldName("Stackable")]
        public int Stackable;

        [DBFieldName("ContainerSlots")]
        public uint ContainerSlots;

        [DBFieldName("ItemStatType", 10)]
        public ItemModType[] ItemStatType;

        [DBFieldName("ItemStatValue", 10)]
        public int[] ItemStatValue;

        [DBFieldName("ItemStatAllocation", 10)]
        public int[] ItemStatAllocation;

        [DBFieldName("ItemStatSocketCostMultiplier", 10)]
        public int[] ItemStatSocketCostMultiplier;

        [DBFieldName("ScalingStatDistribution")]
        public int ScalingStatDistribution;

        [DBFieldName("DamageType")]
        public DamageType DamageType;

        [DBFieldName("Delay")]
        public uint Delay;

        [DBFieldName("RangedModRange")]
        public float RangedModRange;

        [DBFieldName("Bonding")]
        public ItemBonding Bonding;

        [DBFieldName("Name", 4)]
        public string[] Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("PageText")]
        public uint PageText;

        [DBFieldName("LanguageID")]
        public Language LanguageID;

        [DBFieldName("PageMaterial")]
        public PageMaterial PageMaterial;

        [DBFieldName("StartQuest")]
        public uint StartQuest;

        [DBFieldName("LockID")]
        public uint LockID;

        [DBFieldName("Material")]
        public Material Material;

        [DBFieldName("Sheath")]
        public SheathType Sheath;

        [DBFieldName("RandomProperty")]
        public int RandomProperty;

        [DBFieldName("RandomSuffix")]
        public uint RandomSuffix;

        [DBFieldName("ItemSet")]
        public uint ItemSet;

        [DBFieldName("Area")]
        public uint Area;

        [DBFieldName("Map")]
        public int Map;

        [DBFieldName("BagFamily")]
        public BagFamilyMask BagFamily;

        [DBFieldName("TotemCategory")]
        public TotemCategory TotemCategory;

        [DBFieldName("SocketColor", 3)]
        public ItemSocketColor[] SocketColor;

        [DBFieldName("SocketBonus")]
        public int SocketBonus;

        [DBFieldName("GemProperties")]
        public int GemProperties;

        [DBFieldName("ArmorDamageModifier")]
        public float ArmorDamageModifier;

        [DBFieldName("Duration")]
        public uint Duration;

        [DBFieldName("ItemLimitCategory")]
        public int ItemLimitCategory;

        [DBFieldName("HolidayID")]
        public Holiday HolidayID;

        [DBFieldName("StatScalingFactor")]
        public float StatScalingFactor;

        [DBFieldName("CurrencySubstitutionID")]
        public uint CurrencySubstitutionID;

        [DBFieldName("CurrencySubstitutionCount")]
        public uint CurrencySubstitutionCount;

        [DBFieldName("ItemNameDescriptionID")]
        public uint ItemNameDescriptionID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
