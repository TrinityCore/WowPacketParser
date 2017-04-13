using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.SpellShapeshiftForm, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class SpellShapeshiftFormEntry
    {
        public string Name { get; set; }
        public float WeaponDamageVariance { get; set; }
        public uint Flags { get; set; }
        public ushort CombatRoundTime { get; set; }
        public ushort MountTypeID { get; set; }
        public sbyte CreatureType { get; set; }
        public byte BonusActionBar { get; set; }
        public uint AttackIconFileDataID { get; set; }
        [HotfixArray(4)]
        public uint[] CreatureDisplayID { get; set; }
        [HotfixArray(8)]
        public uint[] PresetSpellID { get; set; }
    }
}