using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_item_enchantment")]
    public sealed record SpellItemEnchantmentHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("Charges")]
        public int? Charges;

        [DBFieldName("Effect", 3)]
        public int?[] Effect;

        [DBFieldName("EffectPointsMin", 3)]
        public int?[] EffectPointsMin;

        [DBFieldName("EffectPointsMax", 3)]
        public int?[] EffectPointsMax;

        [DBFieldName("EffectArg", 3)]
        public int?[] EffectArg;

        [DBFieldName("ItemVisual")]
        public int? ItemVisual;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("EffectScalingPoints", 3)]
        public float?[] EffectScalingPoints;

        [DBFieldName("ScalingClass")]
        public int? ScalingClass;

        [DBFieldName("ScalingClassRestricted")]
        public int? ScalingClassRestricted;

        [DBFieldName("GemItemID")]
        public int? GemItemID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("RequiredSkillID")]
        public int? RequiredSkillID;

        [DBFieldName("RequiredSkillRank")]
        public int? RequiredSkillRank;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("Unknown1153_0")]
        public int? Unknown1153_0;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("Unknown1153_1")]
        public int? Unknown1153_1;

        [DBFieldName("Unknown1153_2")]
        public int? Unknown1153_2;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_item_enchantment_locale")]
    public sealed record SpellItemEnchantmentLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("HordeName_lang")]
        public string HordeNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("spell_item_enchantment")]
    public sealed record SpellItemEnchantmentHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("Charges")]
        public int? Charges;

        [DBFieldName("Effect", 3)]
        public int?[] Effect;

        [DBFieldName("EffectPointsMin", 3)]
        public int?[] EffectPointsMin;

        [DBFieldName("EffectPointsMax", 3)]
        public int?[] EffectPointsMax;

        [DBFieldName("EffectArg", 3)]
        public int?[] EffectArg;

        [DBFieldName("ItemVisual")]
        public int? ItemVisual;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("EffectScalingPoints", 3)]
        public float?[] EffectScalingPoints;

        [DBFieldName("ScalingClass")]
        public int? ScalingClass;

        [DBFieldName("ScalingClassRestricted")]
        public int? ScalingClassRestricted;

        [DBFieldName("GemItemID")]
        public int? GemItemID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("RequiredSkillID")]
        public int? RequiredSkillID;

        [DBFieldName("RequiredSkillRank")]
        public int? RequiredSkillRank;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("Unknown1153_0")]
        public int? Unknown1153_0;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("Unknown1153_1")]
        public int? Unknown1153_1;

        [DBFieldName("Unknown1153_2")]
        public int? Unknown1153_2;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_item_enchantment_locale")]
    public sealed record SpellItemEnchantmentLocaleHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("HordeName_lang")]
        public string HordeNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
