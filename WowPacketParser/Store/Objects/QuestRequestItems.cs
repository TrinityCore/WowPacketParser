using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_request_items")]
    public sealed record QuestRequestItems : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EmoteOnComplete")]
        public uint? EmoteOnComplete;

        [DBFieldName("EmoteOnIncomplete")]
        public uint? EmoteOnIncomplete;

        [DBFieldName("EmoteOnCompleteDelay", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? EmoteOnCompleteDelay;

        [DBFieldName("EmoteOnIncompleteDelay", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? EmoteOnIncompleteDelay;

        [DBFieldName("CompletionText")]
        public string CompletionText;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild;
    }
}
