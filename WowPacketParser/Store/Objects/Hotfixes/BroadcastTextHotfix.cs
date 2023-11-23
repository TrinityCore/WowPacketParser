using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("broadcast_text")]
    public sealed record BroadcastTextHotfix1000: IDataModel
    {
        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("Text1")]
        public string Text1;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("EmotesID")]
        public ushort? EmotesID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ChatBubbleDurationMs")]
        public uint? ChatBubbleDurationMs;

        [DBFieldName("VoiceOverPriorityID")]
        public int? VoiceOverPriorityID;

        [DBFieldName("SoundKitID", 2)]
        public uint?[] SoundKitID;

        [DBFieldName("EmoteID", 3)]
        public ushort?[] EmoteID;

        [DBFieldName("EmoteDelay", 3)]
        public ushort?[] EmoteDelay;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("broadcast_text_locale")]
    public sealed record BroadcastTextLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Text_lang")]
        public string TextLang;

        [DBFieldName("Text1_lang")]
        public string Text1Lang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("broadcast_text")]
    public sealed record BroadcastTextHotfix340: IDataModel
    {
        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("Text1")]
        public string Text1;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("ConditionID")]
        public int? ConditionID;

        [DBFieldName("EmotesID")]
        public ushort? EmotesID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ChatBubbleDurationMs")]
        public uint? ChatBubbleDurationMs;

        [DBFieldName("VoiceOverPriorityID")]
        public int? VoiceOverPriorityID;

        [DBFieldName("SoundKitID", 2)]
        public uint?[] SoundKitID;

        [DBFieldName("EmoteID", 3)]
        public ushort?[] EmoteID;

        [DBFieldName("EmoteDelay", 3)]
        public ushort?[] EmoteDelay;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("broadcast_text_locale")]
    public sealed record BroadcastTextLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Text_lang")]
        public string TextLang;

        [DBFieldName("Text1_lang")]
        public string Text1Lang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
