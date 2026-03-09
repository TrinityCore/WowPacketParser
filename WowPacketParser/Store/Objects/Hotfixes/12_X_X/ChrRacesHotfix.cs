using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_races")]
    public sealed record ChrRacesHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClientPrefix")]
        public string ClientPrefix;

        [DBFieldName("ClientFileString")]
        public string ClientFileString;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("NameFemale")]
        public string NameFemale;

        [DBFieldName("NameLowercase")]
        public string NameLowercase;

        [DBFieldName("NameFemaleLowercase")]
        public string NameFemaleLowercase;

        [DBFieldName("LoreName")]
        public string LoreName;

        [DBFieldName("LoreNameFemale")]
        public string LoreNameFemale;

        [DBFieldName("LoreNameLower")]
        public string LoreNameLower;

        [DBFieldName("LoreNameLowerFemale")]
        public string LoreNameLowerFemale;

        [DBFieldName("LoreDescription")]
        public string LoreDescription;

        [DBFieldName("ShortName")]
        public string ShortName;

        [DBFieldName("ShortNameFemale")]
        public string ShortNameFemale;

        [DBFieldName("ShortNameLower")]
        public string ShortNameLower;

        [DBFieldName("ShortNameLowerFemale")]
        public string ShortNameLowerFemale;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("CinematicSequenceID")]
        public int? CinematicSequenceID;

        [DBFieldName("ResSicknessSpellID")]
        public int? ResSicknessSpellID;

        [DBFieldName("SplashSoundID")]
        public int? SplashSoundID;

        [DBFieldName("CreateScreenFileDataID")]
        public int? CreateScreenFileDataID;

        [DBFieldName("SelectScreenFileDataID")]
        public int? SelectScreenFileDataID;

        [DBFieldName("LowResScreenFileDataID")]
        public int? LowResScreenFileDataID;

        [DBFieldName("AlteredFormStartVisualKitID", 3)]
        public int?[] AlteredFormStartVisualKitID;

        [DBFieldName("AlteredFormFinishVisualKitID", 3)]
        public int?[] AlteredFormFinishVisualKitID;

        [DBFieldName("HeritageArmorAchievementID")]
        public int? HeritageArmorAchievementID;

        [DBFieldName("StartingLevel")]
        public int? StartingLevel;

        [DBFieldName("UiDisplayOrder")]
        public int? UiDisplayOrder;

        [DBFieldName("PlayableRaceBit")]
        public int? PlayableRaceBit;

        [DBFieldName("TransmogrifyDisabledSlotMask")]
        public int? TransmogrifyDisabledSlotMask;

        [DBFieldName("AlteredFormCustomizeOffsetFallback", 3)]
        public float?[] AlteredFormCustomizeOffsetFallback;

        [DBFieldName("AlteredFormCustomizeRotationFallback")]
        public float? AlteredFormCustomizeRotationFallback;

        [DBFieldName("Unknown910_1", 3)]
        public float?[] Unknown910_1;

        [DBFieldName("Unknown910_2", 3)]
        public float?[] Unknown910_2;

        [DBFieldName("BaseLanguage")]
        public sbyte? BaseLanguage;

        [DBFieldName("CreatureType")]
        public byte? CreatureType;

        [DBFieldName("Alliance")]
        public sbyte? Alliance;

        [DBFieldName("RaceRelated")]
        public sbyte? RaceRelated;

        [DBFieldName("UnalteredVisualRaceID")]
        public sbyte? UnalteredVisualRaceID;

        [DBFieldName("DefaultClassID")]
        public sbyte? DefaultClassID;

        [DBFieldName("NeutralRaceID")]
        public sbyte? NeutralRaceID;

        [DBFieldName("MaleModelFallbackRaceID")]
        public sbyte? MaleModelFallbackRaceID;

        [DBFieldName("MaleModelFallbackSex")]
        public sbyte? MaleModelFallbackSex;

        [DBFieldName("FemaleModelFallbackRaceID")]
        public sbyte? FemaleModelFallbackRaceID;

        [DBFieldName("FemaleModelFallbackSex")]
        public sbyte? FemaleModelFallbackSex;

        [DBFieldName("MaleTextureFallbackRaceID")]
        public sbyte? MaleTextureFallbackRaceID;

        [DBFieldName("MaleTextureFallbackSex")]
        public sbyte? MaleTextureFallbackSex;

        [DBFieldName("FemaleTextureFallbackRaceID")]
        public sbyte? FemaleTextureFallbackRaceID;

        [DBFieldName("FemaleTextureFallbackSex")]
        public sbyte? FemaleTextureFallbackSex;

        [DBFieldName("HelmetAnimScalingRaceID")]
        public sbyte? HelmetAnimScalingRaceID;

        [DBFieldName("UnalteredVisualCustomizationRaceID")]
        public sbyte? UnalteredVisualCustomizationRaceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_races_locale")]
    public sealed record ChrRacesLocaleHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("NameFemale_lang")]
        public string NameFemaleLang;

        [DBFieldName("NameLowercase_lang")]
        public string NameLowercaseLang;

        [DBFieldName("NameFemaleLowercase_lang")]
        public string NameFemaleLowercaseLang;

        [DBFieldName("LoreName_lang")]
        public string LoreNameLang;

        [DBFieldName("LoreNameFemale_lang")]
        public string LoreNameFemaleLang;

        [DBFieldName("LoreNameLower_lang")]
        public string LoreNameLowerLang;

        [DBFieldName("LoreNameLowerFemale_lang")]
        public string LoreNameLowerFemaleLang;

        [DBFieldName("LoreDescription_lang")]
        public string LoreDescriptionLang;

        [DBFieldName("ShortName_lang")]
        public string ShortNameLang;

        [DBFieldName("ShortNameFemale_lang")]
        public string ShortNameFemaleLang;

        [DBFieldName("ShortNameLower_lang")]
        public string ShortNameLowerLang;

        [DBFieldName("ShortNameLowerFemale_lang")]
        public string ShortNameLowerFemaleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
