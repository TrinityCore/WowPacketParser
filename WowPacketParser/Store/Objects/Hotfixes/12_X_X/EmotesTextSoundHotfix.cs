using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("emotes_text_sound")]
    public sealed record EmotesTextSoundHotfix1200 : IDataModel
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
        public uint? EmotesTextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
