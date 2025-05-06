using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_item_enchantment")]
    public sealed record SpellItemEnchantmentHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("EffectArg", 3)]
        public uint?[] EffectArg;

        [DBFieldName("EffectScalingPoints", 3)]
        public float?[] EffectScalingPoints;

        [DBFieldName("GemItemID")]
        public uint? GemItemID;

        [DBFieldName("TransmogUnlockConditionID")]
        public uint? TransmogUnlockConditionID;

        [DBFieldName("TransmogCost")]
        public uint? TransmogCost;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("EffectPointsMin", 3)]
        public short?[] EffectPointsMin;

        [DBFieldName("ItemVisual")]
        public ushort? ItemVisual;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("RequiredSkillID")]
        public ushort? RequiredSkillID;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Charges")]
        public byte? Charges;

        [DBFieldName("Effect", 3)]
        public byte?[] Effect;

        [DBFieldName("ScalingClass")]
        public sbyte? ScalingClass;

        [DBFieldName("ScalingClassRestricted")]
        public sbyte? ScalingClassRestricted;

        [DBFieldName("ConditionID")]
        public byte? ConditionID;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public byte? MaxLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_item_enchantment_locale")]
    public sealed record SpellItemEnchantmentLocaleHotfix340: IDataModel
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
    public sealed record SpellItemEnchantmentHotfix344: IDataModel
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

        [DBFieldName("Field115355112009", 3)]
        public float?[] Field115355112009;

        [DBFieldName("Field115355112010")]
        public int? Field115355112010;

        [DBFieldName("ScalingClassRestricted")]
        public int? ScalingClassRestricted;

        [DBFieldName("Field115355112012")]
        public int? Field115355112012;

        [DBFieldName("Field115355112013")]
        public int? Field115355112013;

        [DBFieldName("Field115355112014")]
        public int? Field115355112014;

        [DBFieldName("Field115355112015")]
        public int? Field115355112015;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("Field115355112018")]
        public int? Field115355112018;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("Field115355112020")]
        public int? Field115355112020;

        [DBFieldName("Field115355112021")]
        public int? Field115355112021;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_item_enchantment_locale")]
    public sealed record SpellItemEnchantmentLocaleHotfix344: IDataModel
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
