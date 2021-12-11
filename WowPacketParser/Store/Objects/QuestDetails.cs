using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_details")]
    public sealed record QuestDetails : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Emote", 4)]
        public uint?[] Emote;

        [DBFieldName("EmoteDelay", 4)]
        public uint?[] EmoteDelay;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}