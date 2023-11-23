using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_tier")]
    public sealed record ArtifactTierHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ArtifactTier")]
        public uint? ArtifactTier;

        [DBFieldName("MaxNumTraits")]
        public uint? MaxNumTraits;

        [DBFieldName("MaxArtifactKnowledge")]
        public uint? MaxArtifactKnowledge;

        [DBFieldName("KnowledgePlayerCondition")]
        public uint? KnowledgePlayerCondition;

        [DBFieldName("MinimumEmpowerKnowledge")]
        public uint? MinimumEmpowerKnowledge;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_tier")]
    public sealed record ArtifactTierHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ArtifactTier")]
        public uint? ArtifactTier;

        [DBFieldName("MaxNumTraits")]
        public uint? MaxNumTraits;

        [DBFieldName("MaxArtifactKnowledge")]
        public uint? MaxArtifactKnowledge;

        [DBFieldName("KnowledgePlayerCondition")]
        public uint? KnowledgePlayerCondition;

        [DBFieldName("MinimumEmpowerKnowledge")]
        public uint? MinimumEmpowerKnowledge;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
