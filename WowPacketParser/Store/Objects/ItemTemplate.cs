using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class ItemTemplate
    {
        // <= 335 only

        public ItemClass Class;

        public uint SubClass;

        public int UnkInt32;

        public string Name;

        public uint DisplayId;

        public ItemQuality Quality;

        public ItemFlag Flags;

        public ItemFlagExtra ExtraFlags;

        public uint BuyPrice;

        public uint SellPrice;

        public InventoryType InventoryType;

        public ClassMask AllowedClasses;

        public RaceMask AllowedRaces;

        public uint ItemLevel;

        public uint RequiredLevel;

        public uint RequiredSkillId;

        public uint RequiredSkillLevel;

        public uint RequiredSpell;

        public uint RequiredHonorRank;

        public uint RequiredCityRank;

        public uint RequiredRepFaction;

        public uint RequiredRepValue;

        public uint MaxCount;

        public uint MaxStackSize;

        public uint ContainerSlots;

        public uint StatsCount;

        public ItemModType[] StatTypes;

        public int[] StatValues;

        public uint ScalingStatDistribution;

        public uint ScalingStatValue;

        public float[] DamageMins;

        public float[] DamageMaxs;

        public DamageType[] DamageTypes;

        public DamageType[] Resistances; // 0 is armor

        public uint Delay;

        public AmmoType AmmoType;

        public float RangedMod;

        public int[] TriggeredSpellIds;

        public ItemSpellTriggerType[] TriggeredSpellTypes;

        public int[] TriggeredSpellCharges;

        public int[] TriggeredSpellCooldowns;

        public uint[] TriggeredSpellCategories;

        public int[] TriggeredSpellCategoryCooldowns;

        public ItemBonding Bonding;

        public string Description;

        public uint PageText;

        public Language Language;

        public PageMaterial PageMaterial;

        public uint StartQuestId;

        public uint LockId;

        public Material Material;

        public SheathType SheathType;

        public int RandomPropery;

        public uint RandomSuffix;

        public uint Block;

        public uint ItemSet;

        public uint MaxDurability;

        public uint AreaId;

        public uint MapId;

        public BagFamilyMask BagFamily;

        public TotemCategory TotemCategory;

        public ItemSocketColor[] ItemSocketColors;

        public uint[] SocketContent;

        public uint SocketBonus;

        public uint GemProperties;

        public int RequiredDisenchantSkill;

        public float ArmorDamageModifier;

        public int Duration;

        public int ItemLimitCategory;

        public Holiday HolidayId;
    }
}
