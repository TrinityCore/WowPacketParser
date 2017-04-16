using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ChrRaces, HasIndexInData = false)]
    public class ChrRacesEntry
    {
        public uint Flags { get; set; }
        public string ClientPrefix { get; set; }
        public string ClientFileString { get; set; }
        public string Name { get; set; }
        public string NameFemale { get; set; }
        public string NameMale { get; set; }
        [HotfixArray(2)]
        public string[] FacialHairCustomization { get; set; }
        public string HairCustomization { get; set; }
        public uint CreateScreenFileDataID { get; set; }
        public uint SelectScreenFileDataID { get; set; }
        [HotfixArray(3)]
        public float[] MaleCustomizeOffset { get; set; }
        [HotfixArray(3)]
        public float[] FemaleCustomizeOffset { get; set; }
        public uint LowResScreenFileDataID { get; set; }
        public ushort FactionID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, true)]
        public ushort ExplorationSoundID { get; set; }
        public ushort MaleDisplayID { get; set; }
        public ushort FemaleDisplayID { get; set; }
        public ushort ResSicknessSpellID { get; set; }
        public ushort SplashSoundID { get; set; }
        public ushort CinematicSequenceID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, true)]
        public ushort UAMaleCreatureSoundDataID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, true)]
        public ushort UAFemaleCreatureSoundDataID { get; set; }
        public byte BaseLanguage { get; set; }
        public byte CreatureType { get; set; }
        public byte TeamID { get; set; }
        public byte RaceRelated { get; set; }
        public byte UnalteredVisualRaceID { get; set; }
        public byte CharComponentTextureLayoutID { get; set; }
        public byte DefaultClassID { get; set; }
        public byte NeutralRaceID { get; set; }
        public byte ItemAppearanceFrameRaceID { get; set; }
        public byte CharComponentTexLayoutHiResID { get; set; }
        public uint HighResMaleDisplayID { get; set; }
        public uint HighResFemaleDisplayID { get; set; }
        [HotfixArray(3)]
        public uint[] Unk { get; set; }
    }
}