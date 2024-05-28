using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("glyph_required_spec")]
    public sealed record GlyphRequiredSpecHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrSpecializationID")]
        public ushort? ChrSpecializationID;

        [DBFieldName("GlyphPropertiesID")]
        public int? GlyphPropertiesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
