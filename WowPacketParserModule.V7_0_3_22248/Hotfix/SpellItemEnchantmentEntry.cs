using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellItemEnchantment, HasIndexInData = false)]
    public class SpellItemEnchantmentEntry
    {
        [HotfixArray(3)]
        public uint[] EffectSpellID { get; set; }
        public string Name { get; set; }
        [HotfixArray(3)]
        public float[] EffectScalingPoints { get; set; }
        public uint TransmogCost { get; set; }
        public uint TextureFileDataID { get; set; }
        [HotfixArray(3)]
        public ushort[] EffectPointsMin { get; set; }
        public ushort ItemVisual { get; set; }
        public ushort Flags { get; set; }
        public ushort RequiredSkillID { get; set; }
        public ushort RequiredSkillRank { get; set; }
        public ushort ItemLevel { get; set; }
        public byte Charges { get; set; }
        [HotfixArray(3)]
        public byte[] Effect { get; set; }
        public byte ConditionID { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public sbyte ScalingClass { get; set; }
        public sbyte ScalingClassRestricted { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}