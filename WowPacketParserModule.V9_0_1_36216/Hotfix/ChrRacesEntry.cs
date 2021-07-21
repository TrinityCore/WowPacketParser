using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ChrRaces, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class ChrRacesEntry
    {
        public string ClientPrefix { get; set; }
        public string ClientFileString { get; set; }
        public string Name { get; set; }
        public string NameFemale { get; set; }
        public string NameLowercase { get; set; }
        public string NameFemaleLowercase { get; set; }
        public string NameS { get; set; }
        public string NameFemaleS { get; set; }
        public string NameLowercaseS { get; set; }
        public string NameFemaleLowercaseS { get; set; }
        public string RaceFantasyDescription { get; set; }
        public string NameL { get; set; }
        public string NameFemaleL { get; set; }
        public string NameLowercaseL { get; set; }
        public string NameFemaleLowercaseL { get; set; }
        public uint ID { get; set; }
        public int Flags { get; set; }
        public int BaseLanguage { get; set; }
        public int ResSicknessSpellID { get; set; }
        public int SplashSoundID { get; set; }
        public int CreateScreenFileDataID { get; set; }
        public int SelectScreenFileDataID { get; set; }
        public int LowResScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormStartVisualKitID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormFinishVisualKitID { get; set; }
        public int HeritageArmorAchievementID { get; set; }
        public int StartingLevel { get; set; }
        public int UiDisplayOrder { get; set; }
        public int PlayableRaceBit { get; set; }
        public int HelmetAnimScalingRaceID { get; set; }
        public int TransmogrifyDisabledSlotMask { get; set; }
        [HotfixArray(3)]
        public float[] AlteredFormCustomizeOffsetFallback { get; set; }
        public float AlteredFormCustomizeRotationFallback { get; set; }
        public short FactionID { get; set; }
        public short CinematicSequenceID { get; set; }
        public sbyte CreatureType { get; set; }
        public sbyte Alliance { get; set; }
        public sbyte RaceRelated { get; set; }
        public sbyte UnalteredVisualRaceID { get; set; }
        public sbyte DefaultClassID { get; set; }
        public sbyte NeutralRaceID { get; set; }
        public sbyte MaleModelFallbackRaceID { get; set; }
        public sbyte MaleModelFallbackSex { get; set; }
        public sbyte FemaleModelFallbackRaceID { get; set; }
        public sbyte FemaleModelFallbackSex { get; set; }
        public sbyte MaleTextureFallbackRaceID { get; set; }
        public sbyte MaleTextureFallbackSex { get; set; }
        public sbyte FemaleTextureFallbackRaceID { get; set; }
        public sbyte FemaleTextureFallbackSex { get; set; }
        public sbyte UnalteredVisualCustomizationRaceID { get; set; }
    }
}
