using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSparse, HasIndexInData = false)]
    public class ItemSparseEntry
    {
        public long AllowableRace { get; set; }
        public string Description { get; set; }
        public string Display3 { get; set; }
        public string Display2 { get; set; }
        public string Display1 { get; set; }
        public string Display { get; set; }
        public float DmgVariance { get; set; }
        public uint DurationInInventory { get; set; }
        public float QualityModifier { get; set; }
        public uint BagFamily { get; set; }
        public float ItemRange { get; set; }
        [HotfixArray(10)]
        public float[] StatPercentageOfSocket { get; set; }
        [HotfixArray(10)]
        public int[] StatPercentEditor { get; set; }
        public int Stackable { get; set; }
        public int MaxCount { get; set; }
        public uint RequiredAbility { get; set; }
        public uint SellPrice { get; set; }
        public uint BuyPrice { get; set; }
        public uint VendorStackCount { get; set; }
        public float PriceVariance { get; set; }
        public float PriceRandomValue { get; set; }
        [HotfixArray(4)]
        public int[] Flags { get; set; }
        public int FactionRelated { get; set; }
        public ushort ItemNameDescriptionID { get; set; }
        public ushort RequiredTransmogHoliday { get; set; }
        public ushort RequiredHoliday { get; set; }
        public ushort LimitCategory { get; set; }
        public ushort GemProperties { get; set; }
        public ushort SocketMatchEnchantmentId { get; set; }
        public ushort TotemCategoryID { get; set; }
        public ushort InstanceBound { get; set; }
        public ushort ZoneBound { get; set; }
        public ushort ItemSet { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_5_29683, true)]
        public ushort ItemRandomSuffixGroupID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_5_29683, true)]
        public ushort RandomSelect { get; set; }
        public ushort LockID { get; set; }
        public ushort StartQuestID { get; set; }
        public ushort PageID { get; set; }
        public ushort ItemDelay { get; set; }
        public ushort ScalingStatDistributionID { get; set; }
        public ushort MinFactionID { get; set; }
        public ushort RequiredSkillRank { get; set; }
        public ushort RequiredSkill { get; set; }
        public ushort ItemLevel { get; set; }
        public short AllowableClass { get; set; }
        public byte ExpansionID { get; set; }
        public byte ArtifactID { get; set; }
        public byte SpellWeight { get; set; }
        public byte SpellWeightCategory { get; set; }
        [HotfixArray(3)]
        public byte[] SocketType { get; set; }
        public byte SheatheType { get; set; }
        public byte Material { get; set; }
        public byte PageMaterialID { get; set; }
        public byte LanguageID { get; set; }
        public byte Bonding { get; set; }
        public byte DamageDamageType { get; set; }
        [HotfixArray(10)]
        public sbyte[] StatModifierBonusStat { get; set; }
        public byte ContainerSlots { get; set; }
        public byte MinReputation { get; set; }
        public byte RequiredPVPMedal { get; set; }
        public byte RequiredPVPRank { get; set; }
        public sbyte RequiredLevel { get; set; }
        public byte InventoryType { get; set; }
        public byte OverallQualityID { get; set; }
    }
}
