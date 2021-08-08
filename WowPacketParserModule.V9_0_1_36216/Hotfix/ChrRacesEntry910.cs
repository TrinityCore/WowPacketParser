using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ChrRaces, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
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
        public int Flags { get; set; }
        public int FactionID { get; set; }
        public int CinematicSequenceID { get; set; }
        public int ResSicknessSpellID { get; set; }
        public int SplashSoundID { get; set; }
        public int Alliance { get; set; }
        public int RaceRelated { get; set; }
        public int UnalteredVisualRaceID { get; set; }
        public int DefaultClassID { get; set; }
        public int CreateScreenFileDataID { get; set; }
        public int SelectScreenFileDataID { get; set; }
        public int NeutralRaceID { get; set; }
        public int LowResScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormStartVisualKitID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormFinishVisualKitID { get; set; }
        public int HeritageArmorAchievementID { get; set; }
        public int StartingLevel { get; set; }
        public int UiDisplayOrder { get; set; }
        public int MaleModelFallbackRaceID { get; set; }
        public int FemaleModelFallbackRaceID { get; set; }
        public int MaleTextureFallbackRaceID { get; set; }
        public int FemaleTextureFallbackRaceID { get; set; }
        public int PlayableRaceBit { get; set; }
        public int HelmetAnimScalingRaceID { get; set; }
        public int TransmogrifyDisabledSlotMask { get; set; }
        public int UnalteredVisualCustomizationRaceID { get; set; }
        [HotfixArray(3)]
        public float[] AlteredFormCustomizeOffsetFallback { get; set; }
        public float AlteredFormCustomizeRotationFallback { get; set; }
        [HotfixArray(3)]
        public float[] Unknown910_1 { get; set; }
        [HotfixArray(3)]
        public float[] Unknown910_2 { get; set; }
        public sbyte BaseLanguage { get; set; }
        public sbyte CreatureType { get; set; }
        public sbyte MaleModelFallbackSex { get; set; }
        public sbyte FemaleModelFallbackSex { get; set; }
        public sbyte MaleTextureFallbackSex { get; set; }
        public sbyte FemaleTextureFallbackSex { get; set; }
    }
}
