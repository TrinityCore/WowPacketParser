using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_shapeshift_form")]
    public sealed record SpellShapeshiftFormHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("CreatureDisplayID")]
        public uint? CreatureDisplayID;

        [DBFieldName("CreatureType")]
        public byte? CreatureType;

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

        [DBFieldName("PresetSpellID", 8)]
        public uint?[] PresetSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_shapeshift_form_locale")]
    public sealed record SpellShapeshiftFormLocaleHotfix1200 : IDataModel
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
