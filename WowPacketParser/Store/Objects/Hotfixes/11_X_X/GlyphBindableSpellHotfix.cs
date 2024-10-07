using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("glyph_bindable_spell")]
    public sealed record GlyphBindableSpellHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("GlyphPropertiesID")]
        public uint? GlyphPropertiesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
