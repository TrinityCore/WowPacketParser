using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("ItemSparse")]
    public sealed class ItemSparseEntry
    {
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
        public float ItemRange;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public float StatPercentageOfSocket;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int StatPercentEditor;
        public int Stackable;
        public int MaxCount;
        public uint RequiredAbility;
        public uint SellPrice;
        public uint BuyPrice;
        public uint VendorStackCount;
        public float PriceVariance;
        public float PriceRandomValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int Flags;
        public int OppositeFactionItemID;
        public ushort ItemNameDescriptionID;
        public ushort RequiredTransmogHoliday;
        public ushort RequiredHoliday;
        public ushort LimitCategory;
        public ushort GemProperties;
        public ushort SocketMatchEnchantmentID;
        public ushort TotemCategoryID;
        public ushort InstanceBound;
        public ushort ZoneBound;
        public ushort ItemSet;
        public ushort ItemRandomSuffixGroupID;
        public ushort RandomSelect;
        public ushort LockID;
        public ushort StartQuestID;
        public ushort PageID;
        public ushort ItemDelay;
        public ushort ScalingStatDistributionID;
        public ushort MinFactionID;
        public ushort RequiredSkillRank;
        public ushort RequiredSkill;
        public ushort ItemLevel;
        public short AllowableClass;
        public byte ExpansionID;
        public byte ArtifactID;
        public byte SpellWeight;
        public byte SpellWeightCategory;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte SocketType;
        public byte SheatheType;
        public byte Material;
        public byte PageMaterialID;
        public byte LanguageID;
        public byte Bonding;
        public byte DamageDamageType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte StatModifierBonusStat;
        public byte ContainerSlots;
        public byte MinReputation;
        public byte RequiredPVPMedal;
        public byte RequiredPVPRank;
        public sbyte RequiredLevel;
        public byte InventoryType;
        public byte OverallQualityID;
    }
}
