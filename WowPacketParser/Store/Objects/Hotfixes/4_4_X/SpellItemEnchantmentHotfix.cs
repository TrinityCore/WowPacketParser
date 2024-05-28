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

        [DBFieldName("Unk440_1")]
        public int? Unk440_1;

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

        [DBFieldName("Unk440_2")]
        public int? Unk440_2;

        [DBFieldName("Unk440_3")]
        public int? Unk440_3;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("Unk440_4")]
        public int? Unk440_4;

        [DBFieldName("Unk440_5")]
        public int? Unk440_5;

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
}
