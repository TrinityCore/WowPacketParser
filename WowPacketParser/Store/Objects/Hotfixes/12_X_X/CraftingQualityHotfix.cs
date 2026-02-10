using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("crafting_quality")]
    public sealed record CraftingQualityHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QualityTier")]
        public int? QualityTier;

        [DBFieldName("CraftingQualityAtlasSetID")]
        public int? CraftingQualityAtlasSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
