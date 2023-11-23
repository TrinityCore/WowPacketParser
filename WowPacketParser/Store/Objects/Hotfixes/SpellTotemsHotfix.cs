using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_totems")]
    public sealed record SpellTotemsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("RequiredTotemCategoryID", 2)]
        public ushort?[] RequiredTotemCategoryID;

        [DBFieldName("Totem", 2)]
        public int?[] Totem;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_totems")]
    public sealed record SpellTotemsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("RequiredTotemCategoryID", 2)]
        public ushort?[] RequiredTotemCategoryID;

        [DBFieldName("Totem", 2)]
        public int?[] Totem;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
