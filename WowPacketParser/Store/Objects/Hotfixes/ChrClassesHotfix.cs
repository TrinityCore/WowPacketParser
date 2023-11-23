using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_classes")]
    public sealed record ChrClassesHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Filename")]
        public string Filename;

        [DBFieldName("NameMale")]
        public string NameMale;

        [DBFieldName("NameFemale")]
        public string NameFemale;

        [DBFieldName("PetNameToken")]
        public string PetNameToken;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("RoleInfoString")]
        public string RoleInfoString;

        [DBFieldName("DisabledString")]
        public string DisabledString;

        [DBFieldName("HyphenatedNameMale")]
        public string HyphenatedNameMale;

        [DBFieldName("HyphenatedNameFemale")]
        public string HyphenatedNameFemale;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreateScreenFileDataID")]
        public uint? CreateScreenFileDataID;

        [DBFieldName("SelectScreenFileDataID")]
        public uint? SelectScreenFileDataID;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("LowResScreenFileDataID")]
        public uint? LowResScreenFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SpellTextureBlobFileDataID")]
        public uint? SpellTextureBlobFileDataID;

        [DBFieldName("RolesMask")]
        public uint? RolesMask;

        [DBFieldName("ArmorTypeMask")]
        public uint? ArmorTypeMask;

        [DBFieldName("CharStartKitUnknown901")]
        public int? CharStartKitUnknown901;

        [DBFieldName("MaleCharacterCreationVisualFallback")]
        public int? MaleCharacterCreationVisualFallback;

        [DBFieldName("MaleCharacterCreationIdleVisualFallback")]
        public int? MaleCharacterCreationIdleVisualFallback;

        [DBFieldName("FemaleCharacterCreationVisualFallback")]
        public int? FemaleCharacterCreationVisualFallback;

        [DBFieldName("FemaleCharacterCreationIdleVisualFallback")]
        public int? FemaleCharacterCreationIdleVisualFallback;

        [DBFieldName("CharacterCreationIdleGroundVisualFallback")]
        public int? CharacterCreationIdleGroundVisualFallback;

        [DBFieldName("CharacterCreationGroundVisualFallback")]
        public int? CharacterCreationGroundVisualFallback;

        [DBFieldName("AlteredFormCharacterCreationIdleVisualFallback")]
        public int? AlteredFormCharacterCreationIdleVisualFallback;

        [DBFieldName("CharacterCreationAnimLoopWaitTimeMsFallback")]
        public int? CharacterCreationAnimLoopWaitTimeMsFallback;

        [DBFieldName("CinematicSequenceID")]
        public ushort? CinematicSequenceID;

        [DBFieldName("DefaultSpec")]
        public ushort? DefaultSpec;

        [DBFieldName("PrimaryStatPriority")]
        public byte? PrimaryStatPriority;

        [DBFieldName("DisplayPower")]
        public byte? DisplayPower;

        [DBFieldName("RangedAttackPowerPerAgility")]
        public byte? RangedAttackPowerPerAgility;

        [DBFieldName("AttackPowerPerAgility")]
        public byte? AttackPowerPerAgility;

        [DBFieldName("AttackPowerPerStrength")]
        public byte? AttackPowerPerStrength;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("ClassColorR")]
        public byte? ClassColorR;

        [DBFieldName("ClassColorG")]
        public byte? ClassColorG;

        [DBFieldName("ClassColorB")]
        public byte? ClassColorB;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes_locale")]
    public sealed record ChrClassesLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("NameMale_lang")]
        public string NameMaleLang;

        [DBFieldName("NameFemale_lang")]
        public string NameFemaleLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("RoleInfoString_lang")]
        public string RoleInfoStringLang;

        [DBFieldName("DisabledString_lang")]
        public string DisabledStringLang;

        [DBFieldName("HyphenatedNameMale_lang")]
        public string HyphenatedNameMaleLang;

        [DBFieldName("HyphenatedNameFemale_lang")]
        public string HyphenatedNameFemaleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes")]
    public sealed record ChrClassesHotfix1017 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Filename")]
        public string Filename;

        [DBFieldName("NameMale")]
        public string NameMale;

        [DBFieldName("NameFemale")]
        public string NameFemale;

        [DBFieldName("PetNameToken")]
        public string PetNameToken;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("RoleInfoString")]
        public string RoleInfoString;

        [DBFieldName("DisabledString")]
        public string DisabledString;

        [DBFieldName("HyphenatedNameMale")]
        public string HyphenatedNameMale;

        [DBFieldName("HyphenatedNameFemale")]
        public string HyphenatedNameFemale;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreateScreenFileDataID")]
        public uint? CreateScreenFileDataID;

        [DBFieldName("SelectScreenFileDataID")]
        public uint? SelectScreenFileDataID;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("LowResScreenFileDataID")]
        public uint? LowResScreenFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SpellTextureBlobFileDataID")]
        public uint? SpellTextureBlobFileDataID;

        [DBFieldName("ArmorTypeMask")]
        public uint? ArmorTypeMask;

        [DBFieldName("CharStartKitUnknown901")]
        public int? CharStartKitUnknown901;

        [DBFieldName("MaleCharacterCreationVisualFallback")]
        public int? MaleCharacterCreationVisualFallback;

        [DBFieldName("MaleCharacterCreationIdleVisualFallback")]
        public int? MaleCharacterCreationIdleVisualFallback;

        [DBFieldName("FemaleCharacterCreationVisualFallback")]
        public int? FemaleCharacterCreationVisualFallback;

        [DBFieldName("FemaleCharacterCreationIdleVisualFallback")]
        public int? FemaleCharacterCreationIdleVisualFallback;

        [DBFieldName("CharacterCreationIdleGroundVisualFallback")]
        public int? CharacterCreationIdleGroundVisualFallback;

        [DBFieldName("CharacterCreationGroundVisualFallback")]
        public int? CharacterCreationGroundVisualFallback;

        [DBFieldName("AlteredFormCharacterCreationIdleVisualFallback")]
        public int? AlteredFormCharacterCreationIdleVisualFallback;

        [DBFieldName("CharacterCreationAnimLoopWaitTimeMsFallback")]
        public int? CharacterCreationAnimLoopWaitTimeMsFallback;

        [DBFieldName("CinematicSequenceID")]
        public ushort? CinematicSequenceID;

        [DBFieldName("DefaultSpec")]
        public ushort? DefaultSpec;

        [DBFieldName("PrimaryStatPriority")]
        public byte? PrimaryStatPriority;

        [DBFieldName("DisplayPower")]
        public byte? DisplayPower;

        [DBFieldName("RangedAttackPowerPerAgility")]
        public byte? RangedAttackPowerPerAgility;

        [DBFieldName("AttackPowerPerAgility")]
        public byte? AttackPowerPerAgility;

        [DBFieldName("AttackPowerPerStrength")]
        public byte? AttackPowerPerStrength;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("ClassColorR")]
        public byte? ClassColorR;

        [DBFieldName("ClassColorG")]
        public byte? ClassColorG;

        [DBFieldName("ClassColorB")]
        public byte? ClassColorB;

        [DBFieldName("RolesMask")]
        public byte? RolesMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes_locale")]
    public sealed record ChrClassesLocaleHotfix1017 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("NameMale_lang")]
        public string NameMaleLang;

        [DBFieldName("NameFemale_lang")]
        public string NameFemaleLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("RoleInfoString_lang")]
        public string RoleInfoStringLang;

        [DBFieldName("DisabledString_lang")]
        public string DisabledStringLang;

        [DBFieldName("HyphenatedNameMale_lang")]
        public string HyphenatedNameMaleLang;

        [DBFieldName("HyphenatedNameFemale_lang")]
        public string HyphenatedNameFemaleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes")]
    public sealed record ChrClassesHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Filename")]
        public string Filename;

        [DBFieldName("NameMale")]
        public string NameMale;

        [DBFieldName("NameFemale")]
        public string NameFemale;

        [DBFieldName("PetNameToken")]
        public string PetNameToken;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreateScreenFileDataID")]
        public uint? CreateScreenFileDataID;

        [DBFieldName("SelectScreenFileDataID")]
        public uint? SelectScreenFileDataID;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("LowResScreenFileDataID")]
        public uint? LowResScreenFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("StartingLevel")]
        public int? StartingLevel;

        [DBFieldName("RolesMask")]
        public uint? RolesMask;

        [DBFieldName("ArmorTypeMask")]
        public uint? ArmorTypeMask;

        [DBFieldName("CinematicSequenceID")]
        public ushort? CinematicSequenceID;

        [DBFieldName("DefaultSpec")]
        public ushort? DefaultSpec;

        [DBFieldName("HasStrengthAttackBonus")]
        public byte? HasStrengthAttackBonus;

        [DBFieldName("PrimaryStatPriority")]
        public byte? PrimaryStatPriority;

        [DBFieldName("DisplayPower")]
        public byte? DisplayPower;

        [DBFieldName("RangedAttackPowerPerAgility")]
        public byte? RangedAttackPowerPerAgility;

        [DBFieldName("AttackPowerPerAgility")]
        public byte? AttackPowerPerAgility;

        [DBFieldName("AttackPowerPerStrength")]
        public byte? AttackPowerPerStrength;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("DamageBonusStat")]
        public byte? DamageBonusStat;

        [DBFieldName("HasRelicSlot")]
        public byte? HasRelicSlot;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes_locale")]
    public sealed record ChrClassesLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("NameMale_lang")]
        public string NameMaleLang;

        [DBFieldName("NameFemale_lang")]
        public string NameFemaleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("chr_classes")]
    public sealed record ChrClassesHotfix343: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Filename")]
        public string Filename;

        [DBFieldName("NameMale")]
        public string NameMale;

        [DBFieldName("NameFemale")]
        public string NameFemale;

        [DBFieldName("PetNameToken")]
        public string PetNameToken;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreateScreenFileDataID")]
        public uint? CreateScreenFileDataID;

        [DBFieldName("SelectScreenFileDataID")]
        public uint? SelectScreenFileDataID;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("LowResScreenFileDataID")]
        public uint? LowResScreenFileDataID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("StartingLevel")]
        public int? StartingLevel;

        [DBFieldName("ArmorTypeMask")]
        public uint? ArmorTypeMask;

        [DBFieldName("CinematicSequenceID")]
        public ushort? CinematicSequenceID;

        [DBFieldName("DefaultSpec")]
        public ushort? DefaultSpec;

        [DBFieldName("HasStrengthAttackBonus")]
        public byte? HasStrengthAttackBonus;

        [DBFieldName("PrimaryStatPriority")]
        public byte? PrimaryStatPriority;

        [DBFieldName("DisplayPower")]
        public byte? DisplayPower;

        [DBFieldName("RangedAttackPowerPerAgility")]
        public byte? RangedAttackPowerPerAgility;

        [DBFieldName("AttackPowerPerAgility")]
        public byte? AttackPowerPerAgility;

        [DBFieldName("AttackPowerPerStrength")]
        public byte? AttackPowerPerStrength;

        [DBFieldName("SpellClassSet")]
        public byte? SpellClassSet;

        [DBFieldName("RolesMask")]
        public byte? RolesMask;

        [DBFieldName("DamageBonusStat")]
        public byte? DamageBonusStat;

        [DBFieldName("HasRelicSlot")]
        public byte? HasRelicSlot;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes_locale")]
    public sealed record ChrClassesLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("NameMale_lang")]
        public string NameMaleLang;

        [DBFieldName("NameFemale_lang")]
        public string NameFemaleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
