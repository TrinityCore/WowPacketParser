using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("glyph_properties")]
    public sealed record GlyphPropertiesHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("GlyphType")]
        public byte? GlyphType;

        [DBFieldName("GlyphExclusiveCategoryID")]
        public byte? GlyphExclusiveCategoryID;

        [DBFieldName("SpellIconFileDataID")]
        public int? SpellIconFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
