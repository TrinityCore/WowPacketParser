using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_reagents")]
    public sealed record SpellReagentsHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

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
}
