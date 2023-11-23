using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("glyph_properties")]
    public sealed record GlyphPropertiesHotfix1000: IDataModel
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

    [Hotfix]
    [DBTableName("glyph_properties")]
    public sealed record GlyphPropertiesHotfix340: IDataModel
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

        [DBFieldName("GlyphSlotFlags")]
        public uint? GlyphSlotFlags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
