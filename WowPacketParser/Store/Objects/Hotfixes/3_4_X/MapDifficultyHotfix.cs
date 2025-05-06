using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map_difficulty")]
    public sealed record MapDifficultyHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Message")]
        public string Message;

        [DBFieldName("ItemContextPickerID")]
        public uint? ItemContextPickerID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("LockID")]
        public byte? LockID;

        [DBFieldName("ResetInterval")]
        public byte? ResetInterval;

        [DBFieldName("MaxPlayers")]
        public byte? MaxPlayers;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_locale")]
    public sealed record MapDifficultyLocaleHotfix340: IDataModel
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
    public sealed record MapDifficultyHotfix344: IDataModel
    {
        [DBFieldName("Message")]
        public string Message;

        [DBFieldName("ID", true)]
        public uint? ID;

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
        public int? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_locale")]
    public sealed record MapDifficultyLocaleHotfix344: IDataModel
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
