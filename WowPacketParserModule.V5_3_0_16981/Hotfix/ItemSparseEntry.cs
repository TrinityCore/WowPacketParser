using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_3_0_16981.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSparse)]
    public class ItemSparseEntry
    {
        public int ID { get; set; }
        public int Quality { get; set; }
        [HotfixArray(3)]
        public uint[] Flags { get; set; }
        public float Unk1 { get; set; }
        public float Unk2 { get; set; }
        public int BuyCount { get; set; }
        public uint BuyPrice { get; set; }
        public uint SellPrice { get; set; }
        public int InventoryType { get; set; }
        public int AllowableRace { get; set; }
        public int AllowableClass { get; set; }
        public int ItemLevel { get; set; }
        public int RequiredLevel { get; set; }
        public int RequiredSkill { get; set; }
        public int RequiredSkillRank { get; set; }
        public uint RequiredSpell { get; set; }
        public int RequiredHonorRank { get; set; }
        public int RequiredCityRank { get; set; }
        public int RequiredReputationFaction { get; set; }
        public int RequiredReputationRank { get; set; }
        public int MaxCount { get; set; }
        public int MaxStackSize { get; set; }
        public int ContainerSlots { get; set; }
        [HotfixArray(10)]
        public int[] StatType { get; set; }
        [HotfixArray(10)]
        public int[] StatValue { get; set; }
        [HotfixArray(10)]
        public int[] ScalingStatValue { get; set; }
        [HotfixArray(10)]
        public int[] SocketCostRate { get; set; }
        public int ScalingStatDistribution { get; set; }
        public int DamageType { get; set; }
        public int Delay { get; set; }
        public int RangedMod { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpell { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpellType { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpellCharge { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpellCooldown { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpellCategories { get; set; }
        [HotfixArray(5)]
        public int[] TriggeredSpellCategoryCooldown { get; set; }
        public int Bonding { get; set; }
        public string Name { get; set; }
        [HotfixArray(4)]
        public string[] Names { get; set; }
        public string Description { get; set; }
        public int PageText { get; set; }
        public int Language { get; set; }
        public int PageMaterial { get; set; }
        public int StartQuest { get; set; }
        public int LockId { get; set; }
        public int Material { get; set; }
        public int SheathType { get; set; }
        public int RandomProperty { get; set; }
        public uint RandomSuffix { get; set; }
        public int ItemSet { get; set; }
        public int AreaID { get; set; }
        public int MapID { get; set; }
        public int BagFamily { get; set; }
        public int TotemCategory { get; set; }
        [HotfixArray(3)]
        public int[] SocketColor { get; set; }
        [HotfixArray(3)]
        public int[] SocketItem { get; set; }
        public int SocketBonus { get; set; }
        public int GemProperties { get; set; }
        public int ArmorDamageModifier { get; set; }
        public int Duration { get; set; }
        public int LimitCategory { get; set; }
        public int Holiday { get; set; }
        public int StatScalingFactor { get; set; }
        public int CurrencySubstitutionID { get; set; }
        public int CurrencySubstitutionCount { get; set; }
    }
}
