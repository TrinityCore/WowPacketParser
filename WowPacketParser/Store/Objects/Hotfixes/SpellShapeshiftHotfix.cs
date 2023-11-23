using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_shapeshift")]
    public sealed record SpellShapeshiftHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("StanceBarOrder")]
        public sbyte? StanceBarOrder;

        [DBFieldName("ShapeshiftExclude", 2)]
        public int?[] ShapeshiftExclude;

        [DBFieldName("ShapeshiftMask", 2)]
        public int?[] ShapeshiftMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_shapeshift")]
    public sealed record SpellShapeshiftHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("StanceBarOrder")]
        public sbyte? StanceBarOrder;

        [DBFieldName("ShapeshiftExclude", 2)]
        public int?[] ShapeshiftExclude;

        [DBFieldName("ShapeshiftMask", 2)]
        public int?[] ShapeshiftMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
