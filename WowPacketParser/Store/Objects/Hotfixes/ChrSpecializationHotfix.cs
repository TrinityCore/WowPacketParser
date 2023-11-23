using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_specialization")]
    public sealed record ChrSpecializationHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("FemaleName")]
        public string FemaleName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public sbyte? ClassID;

        [DBFieldName("OrderIndex")]
        public sbyte? OrderIndex;

        [DBFieldName("PetTalentType")]
        public sbyte? PetTalentType;

        [DBFieldName("Role")]
        public sbyte? Role;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("PrimaryStatPriority")]
        public sbyte? PrimaryStatPriority;

        [DBFieldName("AnimReplacements")]
        public int? AnimReplacements;

        [DBFieldName("MasterySpellID", 2)]
        public int?[] MasterySpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_specialization_locale")]
    public sealed record ChrSpecializationLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("FemaleName_lang")]
        public string FemaleNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_specialization")]
    public sealed record ChrSpecializationHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("FemaleName")]
        public string FemaleName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public sbyte? ClassID;

        [DBFieldName("OrderIndex")]
        public sbyte? OrderIndex;

        [DBFieldName("PetTalentType")]
        public sbyte? PetTalentType;

        [DBFieldName("Role")]
        public sbyte? Role;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("PrimaryStatPriority")]
        public sbyte? PrimaryStatPriority;

        [DBFieldName("AnimReplacements")]
        public int? AnimReplacements;

        [DBFieldName("MasterySpellID", 2)]
        public int?[] MasterySpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_specialization_locale")]
    public sealed record ChrSpecializationLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("FemaleName_lang")]
        public string FemaleNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
