using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map_difficulty")]
    public sealed record MapDifficultyHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Message")]
        public string Message;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("LockID")]
        public int? LockID;

        [DBFieldName("ResetInterval")]
        public byte? ResetInterval;

        [DBFieldName("MaxPlayers")]
        public int? MaxPlayers;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ItemContextPickerID")]
        public int? ItemContextPickerID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_locale")]
    public sealed record MapDifficultyLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Message_lang")]
        public string MessageLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty")]
    public sealed record MapDifficultyHotfix1102 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Message")]
        public string Message;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("LockID")]
        public int? LockID;

        [DBFieldName("ResetInterval")]
        public byte? ResetInterval;

        [DBFieldName("MaxPlayers")]
        public int? MaxPlayers;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ItemContextPickerID")]
        public int? ItemContextPickerID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("WorldStateExpressionID")]
        public int? WorldStateExpressionID;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_locale")]
    public sealed record MapDifficultyLocaleHotfix1102 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Message_lang")]
        public string MessageLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
