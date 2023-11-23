using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("creature_family")]
    public sealed record CreatureFamilyHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("MinScale")]
        public float? MinScale;

        [DBFieldName("MinScaleLevel")]
        public sbyte? MinScaleLevel;

        [DBFieldName("MaxScale")]
        public float? MaxScale;

        [DBFieldName("MaxScaleLevel")]
        public sbyte? MaxScaleLevel;

        [DBFieldName("PetFoodMask")]
        public short? PetFoodMask;

        [DBFieldName("PetTalentType")]
        public sbyte? PetTalentType;

        [DBFieldName("IconFileID")]
        public int? IconFileID;

        [DBFieldName("SkillLine", 2)]
        public short?[] SkillLine;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("creature_family_locale")]
    public sealed record CreatureFamilyLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("creature_family")]
    public sealed record CreatureFamilyHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("MinScale")]
        public float? MinScale;

        [DBFieldName("MinScaleLevel")]
        public sbyte? MinScaleLevel;

        [DBFieldName("MaxScale")]
        public float? MaxScale;

        [DBFieldName("MaxScaleLevel")]
        public sbyte? MaxScaleLevel;

        [DBFieldName("PetFoodMask")]
        public short? PetFoodMask;

        [DBFieldName("PetTalentType")]
        public sbyte? PetTalentType;

        [DBFieldName("CategoryEnumID")]
        public int? CategoryEnumID;

        [DBFieldName("IconFileID")]
        public int? IconFileID;

        [DBFieldName("SkillLine", 2)]
        public short?[] SkillLine;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("creature_family_locale")]
    public sealed record CreatureFamilyLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
