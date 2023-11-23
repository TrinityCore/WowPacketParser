using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_power")]
    public sealed record ArtifactPowerHotfix1000: IDataModel
    {
        [DBFieldName("DisplayPosX")]
        public float? DisplayPosX;

        [DBFieldName("DisplayPosY")]
        public float? DisplayPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ArtifactID")]
        public byte? ArtifactID;

        [DBFieldName("MaxPurchasableRank")]
        public byte? MaxPurchasableRank;

        [DBFieldName("Label")]
        public int? Label;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_power")]
    public sealed record ArtifactPowerHotfix340: IDataModel
    {
        [DBFieldName("DisplayPosX")]
        public float? DisplayPosX;

        [DBFieldName("DisplayPosY")]
        public float? DisplayPosY;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ArtifactID")]
        public byte? ArtifactID;

        [DBFieldName("MaxPurchasableRank")]
        public byte? MaxPurchasableRank;

        [DBFieldName("Label")]
        public int? Label;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
