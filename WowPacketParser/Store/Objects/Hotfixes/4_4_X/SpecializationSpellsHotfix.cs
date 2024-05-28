using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("specialization_spells")]
    public sealed record SpecializationSpellsHotfix440: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpecID")]
        public ushort? SpecID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("DisplayOrder")]
        public byte? DisplayOrder;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("specialization_spells_locale")]
    public sealed record SpecializationSpellsLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
