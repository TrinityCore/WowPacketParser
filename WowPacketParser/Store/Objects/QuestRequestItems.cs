using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_request_items")]
    public sealed class QuestRequestItems : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EmoteOnComplete")]
        public uint? EmoteOnComplete;

        [DBFieldName("EmoteOnIncomplete")]
        public uint? EmoteOnIncomplete;

        [DBFieldName("EmoteOnCompleteDelay", TargetedDatabase.WarlordsOfDraenor)]
        public uint? EmoteOnCompleteDelay;

        [DBFieldName("EmoteOnIncompleteDelay", TargetedDatabase.WarlordsOfDraenor)]
        public uint? EmoteOnIncompleteDelay;

        [DBFieldName("CompletionText")]
        public string CompletionText;
    }
}
