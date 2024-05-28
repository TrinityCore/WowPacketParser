using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_shapeshift_form")]
    public sealed record SpellShapeshiftFormHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Field115052124002")]
        public uint? Field115052124002;

        [DBFieldName("CreatureType")]
        public sbyte? CreatureType;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("AttackIconFileID")]
        public int? AttackIconFileID;

        [DBFieldName("BonusActionBar")]
        public sbyte? BonusActionBar;

        [DBFieldName("CombatRoundTime")]
        public short? CombatRoundTime;

        [DBFieldName("DamageVariance")]
        public float? DamageVariance;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("CreatureDisplayID", 4)]
        public uint?[] CreatureDisplayID;

        [DBFieldName("PresetSpellID", 8)]
        public uint?[] PresetSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_shapeshift_form_locale")]
    public sealed record SpellShapeshiftFormLocaleHotfix440: IDataModel
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
