using DBFileReaderLib.Attributes;
using System;

namespace WowPacketParser.DBC.Structures.Dragonflight
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
        public int ExpansionID;
        public float DmgVariance;
        public int LimitCategory;
        public uint DurationInInventory;
        public float QualityModifier;
        public uint BagFamily;
        public int StartQuestID;
        public int LanguageID;
        public float ItemRange;
        [Cardinality(10)]
        public float[] StatPercentageOfSocket = new float[10];
        [Cardinality(10)]
        public int[] StatPercentEditor = new int[10];
        public int Stackable;
        public int MaxCount;
        public int MinReputation;
        public uint RequiredAbility;
        public uint SellPrice;
        public uint BuyPrice;
        public uint VendorStackCount;
        public float PriceVariance;
        public float PriceRandomValue;
        [Cardinality(4)]
        public int[] Flags = new int[4];
        public int FactionRelated;
        public int ModifiedCraftingReagentItemID;
        public int ContentTuningID;
        public int PlayerLevelToItemLevelCurveID;
        public ushort ItemNameDescriptionID;
        public ushort RequiredTransmogHoliday;
        public ushort RequiredHoliday;
        public ushort GemProperties;
        public ushort SocketMatchEnchantmentId;
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
        public byte ArtifactID;
        public byte SpellWeight;
        public byte SpellWeightCategory;
        [Cardinality(3)]
        public byte[] SocketType = new byte[3];
        public byte SheatheType;
        public byte Material;
        public byte PageMaterialID;
        public byte Bonding;
        public byte DamageDamageType;
        [Cardinality(10)]
        public sbyte[] StatModifierBonusStat = new sbyte[10];
        public byte ContainerSlots;
        public byte RequiredPVPMedal;
        public byte RequiredPVPRank;
        public sbyte RequiredLevel;
        public sbyte InventoryType;
        public sbyte OverallQualityID;
    }
}
