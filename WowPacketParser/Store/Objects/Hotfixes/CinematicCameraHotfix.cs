using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("cinematic_camera")]
    public sealed record CinematicCameraHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OriginX")]
        public float? OriginX;

        [DBFieldName("OriginY")]
        public float? OriginY;

        [DBFieldName("OriginZ")]
        public float? OriginZ;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("OriginFacing")]
        public float? OriginFacing;

        [DBFieldName("FileDataID")]
        public uint? FileDataID;

        [DBFieldName("ConversationID")]
        public uint? ConversationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("cinematic_camera")]
    public sealed record CinematicCameraHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OriginX")]
        public float? OriginX;

        [DBFieldName("OriginY")]
        public float? OriginY;

        [DBFieldName("OriginZ")]
        public float? OriginZ;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("OriginFacing")]
        public float? OriginFacing;

        [DBFieldName("FileDataID")]
        public uint? FileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
