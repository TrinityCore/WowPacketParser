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
        public uint ID { get; set; }
        public int Flags { get; set; }
        public uint MaleDisplayId { get; set; }
        public uint FemaleDisplayId { get; set; }
        public uint HighResMaleDisplayId { get; set; }
        public uint HighResFemaleDisplayId { get; set; }
        public int CreateScreenFileDataID { get; set; }
        public int SelectScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public float[] MaleCustomizeOffset { get; set; }
        [HotfixArray(3)]
        public float[] FemaleCustomizeOffset { get; set; }
        public int LowResScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormStartVisualKitID { get; set; }
        [HotfixArray(3)]
        public uint[] AlteredFormFinishVisualKitID { get; set; }
        public int HeritageArmorAchievementID { get; set; }
        public int StartingLevel { get; set; }
        public int UiDisplayOrder { get; set; }
        public int FemaleSkeletonFileDataID { get; set; }
        public int MaleSkeletonFileDataID { get; set; }
        public int HelmVisFallbackRaceID { get; set; }
        public short FactionID { get; set; }
        public short CinematicSequenceID { get; set; }
        public short ResSicknessSpellID { get; set; }
        public short SplashSoundID { get; set; }
        public sbyte BaseLanguage { get; set; }
        public sbyte CreatureType { get; set; }
        public sbyte Alliance { get; set; }
        public sbyte RaceRelated { get; set; }
        public sbyte UnalteredVisualRaceID { get; set; }
        public sbyte CharComponentTextureLayoutID { get; set; }
        public sbyte CharComponentTexLayoutHiResID { get; set; }
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
    }
}
