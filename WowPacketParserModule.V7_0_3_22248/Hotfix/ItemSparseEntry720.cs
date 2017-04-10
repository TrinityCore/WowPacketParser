using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSparse, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class ItemSparseEntry
    {
        [HotfixArray(3)]
        public uint[] Flags { get; set; }
        public float Unk1 { get; set; }
        public float Unk2 { get; set; }
        public uint BuyCount { get; set; }
        public uint BuyPrice { get; set; }
        public uint SellPrice { get; set; }
        public int AllowableRace { get; set; }
        public uint RequiredSpell { get; set; }
        public uint MaxCount { get; set; }
        public uint Stackable { get; set; }
        [HotfixArray(10)]
        public int[] ItemStatAllocation { get; set; }
        [HotfixArray(10)]
        public float[] ItemStatSocketCostMultiplier { get; set; }
        public float RangedModRange { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Description { get; set; }
        public uint BagFamily { get; set; }
        public float ArmorDamageModifier { get; set; }
        public uint Duration { get; set; }
        public float StatScalingFactor { get; set; }
        public ushort AllowableClass { get; set; }
        public ushort ItemLevel { get; set; }
        public ushort RequiredSkill { get; set; }
        public ushort RequiredSkillRank { get; set; }
        public ushort RequiredReputationFaction { get; set; }
        [HotfixArray(10)]
        public short[] ItemStatValue { get; set; }
        public ushort ScalingStatDistribution { get; set; }
        public ushort Delay { get; set; }
        public ushort PageText { get; set; }
        public ushort StartQuest { get; set; }
        public ushort LockID { get; set; }
        public ushort RandomProperty { get; set; }
        public ushort RandomSuffix { get; set; }
        public ushort ItemSet { get; set; }
        public ushort Area { get; set; }
        public ushort Map { get; set; }
        public ushort TotemCategory { get; set; }
        public ushort SocketBonus { get; set; }
        public ushort GemProperties { get; set; }
        public ushort ItemLimitCategory { get; set; }
        public ushort HolidayID { get; set; }
        public ushort RequiredTransmogHolidayID { get; set; }
        public ushort ItemNameDescriptionID { get; set; }
        public byte Quality { get; set; }
        public byte InventoryType { get; set; }
        public sbyte RequiredLevel { get; set; }
        public byte RequiredHonorRank { get; set; }
        public byte RequiredCityRank { get; set; }
        public byte RequiredReputationRank { get; set; }
        public byte ContainerSlots { get; set; }
        [HotfixArray(10)]
        public sbyte[] ItemStatType { get; set; }
        public byte DamageType { get; set; }
        public byte Bonding { get; set; }
        public byte LanguageID { get; set; }
        public byte PageMaterial { get; set; }
        public sbyte Material { get; set; }
        public byte Sheath { get; set; }
        [HotfixArray(3)]
        public byte[] SocketColor { get; set; }
        public byte CurrencySubstitutionID { get; set; }
        public byte CurrencySubstitutionCount { get; set; }
        public byte ArtifactID { get; set; }
        public byte RequiredExpansion { get; set; }
    }
}