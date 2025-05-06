using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
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
    [Hotfix]
    [DBTableName("chr_classes")]
    public sealed record ChrClassesHotfix344: IDataModel
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

        [DBFieldName("Field115759706005")]
        public uint? Field115759706005;

        [DBFieldName("Field115759706006")]
        public uint? Field115759706006;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("Field115759706008")]
        public uint? Field115759706008;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("StartingLevel")]
        public int? StartingLevel;

        [DBFieldName("SpellTextureBlobFileDataID")]
        public uint? SpellTextureBlobFileDataID;

        [DBFieldName("Field115759706012")]
        public uint? Field115759706012;

        [DBFieldName("Field115759706013")]
        public ushort? Field115759706013;

        [DBFieldName("Field115759706014")]
        public ushort? Field115759706014;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("HasStrengthAttackBonus")]
        public byte? HasStrengthAttackBonus;

        [DBFieldName("Field115759706017")]
        public byte? Field115759706017;

        [DBFieldName("DisplayPower")]
        public sbyte? DisplayPower;

        [DBFieldName("Field115759706019")]
        public byte? Field115759706019;

        [DBFieldName("Field115759706020")]
        public byte? Field115759706020;

        [DBFieldName("Field115759706021")]
        public byte? Field115759706021;

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
    public sealed record ChrClassesLocaleHotfix344: IDataModel
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
