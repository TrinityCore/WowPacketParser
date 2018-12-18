using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("ItemSparse")]
    public sealed class ItemSparseEntry
    {
        public long AllowableRace;
        public string Display;
        public string Display1;
        public string Display2;
        public string Display3;
        public string Description;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] Flags;
        public float PriceRandomValue;
        public float PriceVariance;
        public uint VendorStackCount;
        public uint BuyPrice;
        public uint SellPrice;
        public uint RequiredAbility;
        public int MaxCount;
        public int Stackable;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] StatPercentEditor;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float[] StatPercentageOfSocket;
        public float ItemRange;
        public uint BagFamily;
        public float QualityModifier;
        public uint DurationInInventory;
        public float DmgVariance;
        public short AllowableClass;
        public ushort ItemLevel;
        public ushort RequiredSkill;
        public ushort RequiredSkillRank;
        public ushort MinFactionID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public short[] ItemStatValue;
        public ushort ScalingStatDistributionID;
        public ushort ItemDelay;
        public ushort PageID;
        public ushort StartQuestID;
        public ushort LockID;
        public ushort RandomSelect;
        public ushort ItemRandomSuffixGroupID;
        public ushort ItemSet;
        public ushort ZoneBound;
        public ushort InstanceBound;
        public ushort TotemCategoryID;
        public ushort SocketMatchEnchantmentId;
        public ushort GemProperties;
        public ushort LimitCategory;
        public ushort RequiredHoliday;
        public ushort RequiredTransmogHoliday;
        public ushort ItemNameDescriptionID;
        public byte OverallQualityID;
        public byte InventoryType;
        public sbyte RequiredLevel;
        public byte RequiredPVPRank;
        public byte RequiredPVPMedal;
        public byte MinReputation;
        public byte ContainerSlots;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public sbyte[] StatModifierBonusStat;
        public byte DamageDamageType;
        public byte Bonding;
        public byte LanguageID;
        public byte PageMaterialID;
        public byte Material;
        public byte SheatheType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] SocketType;
        public byte SpellWeightCategory;
        public byte SpellWeight;
        public byte ArtifactID;
        public byte ExpansionID;
    }
}
