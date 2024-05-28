using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("emotes_text_sound")]
    public sealed record EmotesTextSoundHotfix440: IDataModel
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
}
