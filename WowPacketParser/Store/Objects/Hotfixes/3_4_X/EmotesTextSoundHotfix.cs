using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("emotes_text_sound")]
    public sealed record EmotesTextSoundHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceID")]
        public byte? RaceID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SexID")]
        public byte? SexID;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("EmotesTextID")]
        public int? EmotesTextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("emotes_text_sound")]
    public sealed record EmotesTextSoundHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceID")]
        public sbyte? RaceID;

        [DBFieldName("ClassID")]
        public sbyte? ClassID;

        [DBFieldName("SexID")]
        public sbyte? SexID;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("EmotesTextID")]
        public int? EmotesTextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
