using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClasses)]
    public class ChrClassesEntry
    {
        public string Name { get; set; }
        public string Filename { get; set; }
        public string NameMale { get; set; }
        public string NameFemale { get; set; }
        public string PetNameToken { get; set; }
        public string Description { get; set; }
        public string RoleInfoString { get; set; }
        public string DisabledString { get; set; }
        public string HyphenatedNameMale { get; set; }
        public string HyphenatedNameFemale { get; set; }
        public uint ID { get; set; }
        public uint CreateScreenFileDataID { get; set; }
        public uint SelectScreenFileDataID { get; set; }
        public uint IconFileDataID { get; set; }
        public uint LowResScreenFileDataID { get; set; }
        public uint Flags { get; set; }
        public uint SpellTextureBlobFileDataID { get; set; }
        public uint RolesMask { get; set; }
        public uint ArmorTypeMask { get; set; }
        public int CharStartKitUnknown901 { get; set; }
        public int MaleCharacterCreationVisualFallback { get; set; }
        public int MaleCharacterCreationIdleVisualFallback { get; set; }
        public int FemaleCharacterCreationVisualFallback { get; set; }
        public int FemaleCharacterCreationIdleVisualFallback { get; set; }
        public int CharacterCreationIdleGroundVisualFallback { get; set; }
        public int CharacterCreationGroundVisualFallback { get; set; }
        public int AlteredFormCharacterCreationIdleVisualFallback { get; set; }
        public int CharacterCreationAnimLoopWaitTimeMsFallback { get; set; }
        public ushort CinematicSequenceID { get; set; }
        public ushort DefaultSpec { get; set; }
        public byte PrimaryStatPriority { get; set; }
        public byte DisplayPower { get; set; }
        public byte RangedAttackPowerPerAgility { get; set; }
        public byte AttackPowerPerAgility { get; set; }
        public byte AttackPowerPerStrength { get; set; }
        public byte SpellClassSet { get; set; }
        public byte ChatColorR { get; set; }
        public byte ChatColorG { get; set; }
        public byte ChatColorB { get; set; }
    }
}
