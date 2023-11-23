using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("reward_pack")]
    public sealed record RewardPackHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CharTitleID")]
        public int? CharTitleID;

        [DBFieldName("Money")]
        public uint? Money;

        [DBFieldName("ArtifactXPDifficulty")]
        public sbyte? ArtifactXPDifficulty;

        [DBFieldName("ArtifactXPMultiplier")]
        public float? ArtifactXPMultiplier;

        [DBFieldName("ArtifactXPCategoryID")]
        public byte? ArtifactXPCategoryID;

        [DBFieldName("TreasurePickerID")]
        public uint? TreasurePickerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("reward_pack")]
    public sealed record RewardPackHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CharTitleID")]
        public int? CharTitleID;

        [DBFieldName("Money")]
        public uint? Money;

        [DBFieldName("ArtifactXPDifficulty")]
        public sbyte? ArtifactXPDifficulty;

        [DBFieldName("ArtifactXPMultiplier")]
        public float? ArtifactXPMultiplier;

        [DBFieldName("ArtifactXPCategoryID")]
        public byte? ArtifactXPCategoryID;

        [DBFieldName("TreasurePickerID")]
        public uint? TreasurePickerID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
