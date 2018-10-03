using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClasses)]
    public class ChrClassesEntry
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public string NameMale { get; set; }
        public string NameFemale { get; set; }
        public string PetNameToken { get; set; }
        public int ID { get; set; }
        public uint CreateScreenFileDataID { get; set; }
        public uint SelectScreenFileDataID { get; set; }
        public uint IconFileDataID { get; set; }
        public uint LowResScreenFileDataID { get; set; }
        public int StartingLevel { get; set; }
        public ushort Flags { get; set; }
        public ushort CinematicSequenceID { get; set; }
        public ushort DefaultSpec { get; set; }
        public byte PrimaryStatPriority { get; set; }
        public byte DisplayPower { get; set; }
        public byte RangedAttackPowerPerAgility { get; set; }
        public byte AttackPowerPerAgility { get; set; }
        public byte AttackPowerPerStrength { get; set; }
        public byte SpellClassSet { get; set; }
    }
}
