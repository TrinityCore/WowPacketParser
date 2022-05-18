using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_queststarter")]
    public sealed record CreatureQuestStarter: IDataModel
    {
        [DBFieldName("id", true)]
        public uint? CreatureID;

        [DBFieldName("quest", true)]
        public uint? QuestID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
