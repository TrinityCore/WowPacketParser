using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("soulbind_conduit_rank")]
    public sealed record SoulbindConduitRankHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RankIndex")]
        public int? RankIndex;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("AuraPointsOverride")]
        public float? AuraPointsOverride;

        [DBFieldName("SoulbindConduitID")]
        public uint? SoulbindConduitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
