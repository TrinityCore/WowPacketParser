using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellShapeshiftForm, HasIndexInData = false)]
    public class SpellShapeshiftFormEntry
    {
        public string Name { get; set; }
        public sbyte CreatureType { get; set; }
        public int Flags { get; set; }
        public int AttackIconFileID { get; set; }
        public sbyte BonusActionBar { get; set; }
        public short CombatRoundTime { get; set; }
        public float DamageVariance { get; set; }
        public ushort MountTypeID { get; set; }
        [HotfixArray(4)]
        public uint[] CreatureDisplayID { get; set; }
        [HotfixArray(8)]
        public uint[] PresetSpellID { get; set; }
    }
}
