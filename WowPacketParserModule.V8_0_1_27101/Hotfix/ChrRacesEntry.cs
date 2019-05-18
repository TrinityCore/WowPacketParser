using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrRaces)]
    public class ChrRacesEntry
    {
        public string ClientPrefix { get; set; }
        public string ClientFileString { get; set; }
        public string Name { get; set; }
        public string NameFemale { get; set; }
        public string NameLowercase { get; set; }
        public string NameFemaleLowercase { get; set; }
        public int ID { get; set; }
        public int Flags { get; set; }
        public int MaleDisplayID { get; set; }
        public int FemaleDisplayID { get; set; }
        public int HighResMaleDisplayID { get; set; }
        public int HighResFemaleDisplayID { get; set; }
        public int CreateScreenFileDataID { get; set; }
        public int SelectScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public float[] MaleCustomizeOffset { get; set; }
        [HotfixArray(3)]
        public float[] FemaleCustomizeOffset { get; set; }
        public int LowResScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public int[] AlteredFormStartVisualKitID { get; set; }
        [HotfixArray(3)]
        public int[] AlteredFormFinishVisualKitID { get; set; }
        public int HeritageArmorAchievementID { get; set; }
        public int StartingLevel { get; set; }
        public int UiDisplayOrder { get; set; }
        public int FemaleSkeletonFileDataID { get; set; }
        public int MaleSkeletonFileDataID { get; set; }
        public int HelmVisFallbackRaceID { get; set; }
        public ushort FactionID { get; set; }
        public ushort CinematicSequenceID { get; set; }
        public ushort ResSicknessSpellID { get; set; }
        public ushort SplashSoundID { get; set; }
        public byte BaseLanguage { get; set; }
        public byte CreatureType { get; set; }
        public byte Alliance { get; set; }
        public byte RaceRelated { get; set; }
        public byte UnalteredVisualRaceID { get; set; }
        public byte CharComponentTextureLayoutID { get; set; }
        public byte CharComponentTexLayoutHiResID { get; set; }
        public byte DefaultClassID { get; set; }
        public byte NeutralRaceID { get; set; }
        public byte MaleModelFallbackRaceID { get; set; }
        public byte MaleModelFallbackSex { get; set; }
        public byte FemaleModelFallbackRaceID { get; set; }
        public byte FemaleModelFallbackSex { get; set; }
        public byte MaleTextureFallbackRaceID { get; set; }
        public byte MaleTextureFallbackSex { get; set; }
        public byte FemaleTextureFallbackRaceID { get; set; }
        public byte FemaleTextureFallbackSex { get; set; }
    }
}
