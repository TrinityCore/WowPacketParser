using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClasses)]
    public class ChrClassesEntry
    {
        public string PetNameToken { get; set; }
        public string Name { get; set; }
        public string NameFemale { get; set; }
        public string NameMale { get; set; }
        public string Filename { get; set; }
        public uint CreateScreenFileDataID { get; set; }
        public uint SelectScreenFileDataID { get; set; }
        public uint LowResScreenFileDataID { get; set; }
        public ushort Flags { get; set; }
        public ushort CinematicSequenceID { get; set; }
        public ushort DefaultSpec { get; set; }
        public byte PowerType { get; set; }
        public byte SpellClassSet { get; set; }
        public byte AttackPowerPerStrength { get; set; }
        public byte AttackPowerPerAgility { get; set; }
        public byte RangedAttackPowerPerAgility { get; set; }
        public byte IconFileDataID { get; set; }
        public byte Unk1 { get; set; }
        public uint ID { get; set; }
    }
}