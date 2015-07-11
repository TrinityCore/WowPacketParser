using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("locales_quest_objectives")]
    public sealed class LocalesQuestObjectives
    {
        [DBFieldName("QuestId")]
        public uint QuestId;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
