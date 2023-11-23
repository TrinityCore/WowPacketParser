using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_item_enchantment")]
    public sealed record SpellItemEnchantmentHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("HordeName")]
        public string HordeName;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("EffectArg", 3)]
        public uint?[] EffectArg;

        [DBFieldName("EffectScalingPoints", 3)]
        public float?[] EffectScalingPoints;

        [DBFieldName("IconFileDataID")]
        public uint? IconFileDataID;

        [DBFieldName("MinItemLevel")]
        public int? MinItemLevel;

        [DBFieldName("MaxItemLevel")]
        public int? MaxItemLevel;

        [DBFieldName("TransmogUseConditionID")]
        public uint? TransmogUseConditionID;

        [DBFieldName("TransmogCost")]
        public uint? TransmogCost;

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
    public sealed record SpellItemEnchantmentLocaleHotfix1000: IDataModel
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
}
