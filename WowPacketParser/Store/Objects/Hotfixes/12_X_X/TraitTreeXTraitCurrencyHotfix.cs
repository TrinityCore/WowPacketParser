using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree_x_trait_currency")]
    public sealed record TraitTreeXTraitCurrencyHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Index")]
        public int? Index;

        [DBFieldName("TraitTreeID")]
        public uint? TraitTreeID;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
