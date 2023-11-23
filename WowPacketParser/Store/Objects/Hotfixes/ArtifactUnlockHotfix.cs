using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_unlock")]
    public sealed record ArtifactUnlockHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PowerID")]
        public uint? PowerID;

        [DBFieldName("PowerRank")]
        public byte? PowerRank;

        [DBFieldName("ItemBonusListID")]
        public ushort? ItemBonusListID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("ArtifactID")]
        public uint? ArtifactID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_unlock")]
    public sealed record ArtifactUnlockHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PowerID")]
        public uint? PowerID;

        [DBFieldName("PowerRank")]
        public byte? PowerRank;

        [DBFieldName("ItemBonusListID")]
        public ushort? ItemBonusListID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("ArtifactID")]
        public int? ArtifactID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
