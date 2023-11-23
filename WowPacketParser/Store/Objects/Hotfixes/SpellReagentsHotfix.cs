using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_reagents")]
    public sealed record SpellReagentsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Reagent", 8)]
        public int?[] Reagent;

        [DBFieldName("ReagentCount", 8)]
        public short?[] ReagentCount;

        [DBFieldName("ReagentRecraftCount", 8)]
        public short?[] ReagentRecraftCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_reagents")]
    public sealed record SpellReagentsHotfix1002: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Reagent", 8)]
        public int?[] Reagent;

        [DBFieldName("ReagentCount", 8)]
        public short?[] ReagentCount;

        [DBFieldName("ReagentRecraftCount", 8)]
        public short?[] ReagentRecraftCount;

        [DBFieldName("ReagentSource", 8)]
        public byte?[] ReagentSource;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_reagents")]
    public sealed record SpellReagentsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Reagent", 8)]
        public int?[] Reagent;

        [DBFieldName("ReagentCount", 8)]
        public short?[] ReagentCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
