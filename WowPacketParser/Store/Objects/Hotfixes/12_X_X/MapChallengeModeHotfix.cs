using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map_challenge_mode")]
    public sealed record MapChallengeModeHotfix1200 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MaxMedals")]
        public int? MaxMedals;

        [DBFieldName("ExpansionLevel")]
        public uint? ExpansionLevel;

        [DBFieldName("RequiredWorldStateID")]
        public int? RequiredWorldStateID;

        [DBFieldName("CriteriaCount", 5)]
        public short?[] CriteriaCount;

        [DBFieldName("FirstRewardQuestID", 6)]
        public int?[] FirstRewardQuestID;

        [DBFieldName("RewardQuestID", 6)]
        public int?[] RewardQuestID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_challenge_mode_locale")]
    public sealed record MapChallengeModeLocaleHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
