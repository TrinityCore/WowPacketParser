using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_power_rank")]
    public sealed record ArtifactPowerRankHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RankIndex")]
        public byte? RankIndex;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ItemBonusListID")]
        public ushort? ItemBonusListID;

        [DBFieldName("AuraPointsOverride")]
        public float? AuraPointsOverride;

        [DBFieldName("ArtifactPowerID")]
        public uint? ArtifactPowerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_power_rank")]
    public sealed record ArtifactPowerRankHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RankIndex")]
        public byte? RankIndex;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ItemBonusListID")]
        public ushort? ItemBonusListID;

        [DBFieldName("AuraPointsOverride")]
        public float? AuraPointsOverride;

        [DBFieldName("ArtifactPowerID")]
        public int? ArtifactPowerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
