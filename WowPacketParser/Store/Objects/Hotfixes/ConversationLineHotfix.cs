using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("conversation_line")]
    public sealed record ConversationLineHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BroadcastTextID")]
        public uint? BroadcastTextID;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("AdditionalDuration")]
        public int? AdditionalDuration;

        [DBFieldName("NextConversationLineID")]
        public ushort? NextConversationLineID;

        [DBFieldName("AnimKitID")]
        public ushort? AnimKitID;

        [DBFieldName("SpeechType")]
        public byte? SpeechType;

        [DBFieldName("StartAnimation")]
        public byte? StartAnimation;

        [DBFieldName("EndAnimation")]
        public byte? EndAnimation;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("conversation_line")]
    public sealed record ConversationLineHotfix1020 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BroadcastTextID")]
        public uint? BroadcastTextID;

        [DBFieldName("Unused1020")]
        public uint? Unused1020;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("AdditionalDuration")]
        public int? AdditionalDuration;

        [DBFieldName("NextConversationLineID")]
        public ushort? NextConversationLineID;

        [DBFieldName("AnimKitID")]
        public ushort? AnimKitID;

        [DBFieldName("SpeechType")]
        public byte? SpeechType;

        [DBFieldName("StartAnimation")]
        public byte? StartAnimation;

        [DBFieldName("EndAnimation")]
        public byte? EndAnimation;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("conversation_line")]
    public sealed record ConversationLineHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BroadcastTextID")]
        public uint? BroadcastTextID;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("AdditionalDuration")]
        public int? AdditionalDuration;

        [DBFieldName("NextConversationLineID")]
        public ushort? NextConversationLineID;

        [DBFieldName("AnimKitID")]
        public ushort? AnimKitID;

        [DBFieldName("SpeechType")]
        public byte? SpeechType;

        [DBFieldName("StartAnimation")]
        public byte? StartAnimation;

        [DBFieldName("EndAnimation")]
        public byte? EndAnimation;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
