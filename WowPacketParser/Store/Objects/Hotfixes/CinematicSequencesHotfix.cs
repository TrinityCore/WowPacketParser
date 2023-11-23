using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("cinematic_sequences")]
    public sealed record CinematicSequencesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("Camera", 8)]
        public ushort?[] Camera;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("cinematic_sequences")]
    public sealed record CinematicSequencesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("Camera", 8)]
        public ushort?[] Camera;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
