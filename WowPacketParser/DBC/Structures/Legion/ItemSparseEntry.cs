using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("ItemSparse")]
    public sealed class ItemSparseEntry
    {
        public uint[] Flags;
        public float Unk1;
        public float Unk2;
        public uint BuyCount;
        public uint BuyPrice;
        public uint SellPrice;
        public int AllowableRace;
        public uint RequiredSpell;
        public uint MaxCount;
        public uint Stackable;
        public int[] ItemStatAllocation;
        public float[] ItemStatSocketCostMultiplier;
        public float RangedModRange;
        public string Name;
        public string Name2;
        public string Name3;
        public string Name4;
        public string Description;
        public uint BagFamily;
        public float ArmorDamageModifier;
        public uint Duration;
        public float StatScalingFactor;
        public ushort AllowableClass;
        public ushort ItemLevel;
        public ushort RequiredSkill;
        public ushort RequiredSkillRank;
        public ushort RequiredReputationFaction;
        public short[] ItemStatValue;
        public ushort ScalingStatDistribution;
        public ushort Delay;
        public ushort PageText;
        public ushort StartQuest;
        public ushort LockID;
        public ushort RandomProperty;
        public ushort RandomSuffix;
        public ushort ItemSet;
        public ushort Area;
        public ushort Map;
        public ushort TotemCategory;
        public ushort SocketBonus;
        public ushort GemProperties;
        public ushort ItemLimitCategory;
        public ushort HolidayID;
        public ushort RequiredTransmogHolidayID;
        public ushort ItemNameDescriptionID;
        public byte Quality;
        public byte InventoryType;
        public sbyte RequiredLevel;
        public byte RequiredHonorRank;
        public byte RequiredCityRank;
        public byte RequiredReputationRank;
        public byte ContainerSlots;
        public sbyte[] ItemStatType;
        public byte DamageType;
        public byte Bonding;
        public byte LanguageID;
        public byte PageMaterial;
        public sbyte Material;
        public byte Sheath;
        public byte[] SocketColor;
        public byte CurrencySubstitutionID;
        public byte CurrencySubstitutionCount;
        public byte ArtifactID;
        public byte RequiredExpansion;
    }
}
