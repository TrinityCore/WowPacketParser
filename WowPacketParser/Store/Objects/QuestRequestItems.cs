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

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild;

        public void CheckVerifiedBuild()
        {
            if (EmoteOnComplete >= 0 && EmoteOnIncomplete >= 0 && EmoteOnCompleteDelay >= 0 && EmoteOnIncompleteDelay >= 0)
                VerifiedBuild = ClientVersion.BuildInt;
            else
                VerifiedBuild = 0;
        }
    }
}
