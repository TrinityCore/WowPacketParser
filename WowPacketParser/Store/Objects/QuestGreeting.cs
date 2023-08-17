using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_greeting")]
    public sealed record QuestGreeting : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type", true)]
        public uint? Type;

        [DBFieldName("GreetEmoteType")]
        public uint? GreetEmoteType;

        [DBFieldName("GreetEmoteDelay")]
        public uint? GreetEmoteDelay;

        [DBFieldName("Greeting", LocaleConstant.enUS)]
        public string Greeting;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
