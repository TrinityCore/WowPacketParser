using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("ItemSparse")]
    public sealed class ItemSparseEntry
    {
        [Index(true)]
        public uint ID;
        public long AllowableRace;
        public string Description;
        public string Display3;
        public string Display2;
        public string Display1;
        public string Display;
        public float DmgVariance;
        public uint DurationInInventory;
        public float QualityModifier;
        public uint BagFamily;
        public int StartQuestID;
        public float ItemRange;
        [Cardinality(10)]
        public float[] StatPercentageOfSocket = new float[10];
        [Cardinality(10)]
        public int[] StatPercentEditor = new int[10];
        [Cardinality(10)]
        public int[] Unknown1153 = new int[10];
        [Cardinality(10)]
        public int[] StatModifierBonusStat = new int[10];
        public int Stackable;
        public int MaxCount;
        public int MinReputation;
        public uint RequiredAbility;
        public uint SellPrice;
        public uint BuyPrice;
        public uint VendorStackCount;
        public float PriceVariance;
        public float PriceRandomValue;
        [Cardinality(5)]
        public int[] Flags = new int[5];
        public int OppositeFactionItemID;
        public int ModifiedCraftingReagentItemID;
        public int ContentTuningID;
        public int PlayerLevelToItemLevelCurveID;
        public uint MaxDurability;
        public ushort ItemNameDescriptionID;
        public ushort RequiredTransmogHoliday;
        public ushort RequiredHoliday;
        public ushort LimitCategory;
        public ushort GemProperties;
        public ushort SocketMatchEnchantmentID;
        public ushort TotemCategoryID;
        public ushort InstanceBound;
        [Cardinality(2)]
        public ushort[] ZoneBound = new ushort[2];
        public ushort ItemSet;
        public ushort LockID;
        public ushort PageID;
        public ushort ItemDelay;
        public ushort MinFactionID;
        public ushort RequiredSkillRank;
        public ushort RequiredSkill;
        public ushort ItemLevel;
        public short AllowableClass;
        public ushort ItemRandomSuffixGroupID;
        public ushort RandomSelect;
        [Cardinality(5)]
        public ushort[] MinDamage = new ushort[5];
        [Cardinality(5)]
        public ushort[] MaxDamage = new ushort[5];
        [Cardinality(7)]
        public short[] Resistances = new short[7];
        public ushort ScalingStatDistributionID;
        [Cardinality(10)]
        public short[] StatModifierBonusAmount = new short[10];
        public byte ExpansionID;
        public byte ArtifactID;
        public byte SpellWeight;
        public byte SpellWeightCategory;
        [Cardinality(3)]
        public byte[] SocketType = new byte[3];
        public byte SheatheType;
        public byte Material;
        public byte PageMaterialID;
        public byte LanguageID;
        public byte Bonding;
        public byte DamageType;
        public byte ContainerSlots;
        public byte RequiredPVPMedal;
        public byte RequiredPVPRank;
        public sbyte InventoryType;
        public sbyte OverallQualityID;
        public byte AmmunitionType;
        public sbyte RequiredLevel;
    }
}
