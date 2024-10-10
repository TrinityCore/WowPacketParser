using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("quest_sort")]
    public sealed record QuestSortHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SortName")]
        public string SortName;

        [DBFieldName("UiOrderIndex")]
        public sbyte? UiOrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_sort_locale")]
    public sealed record QuestSortLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SortName_lang")]
        public string SortNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_sort")]
    public sealed record QuestSortHotfix1102 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SortName")]
        public string SortName;

        [DBFieldName("UiOrderIndex")]
        public sbyte? UiOrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_sort_locale")]
    public sealed record QuestSortLocaleHotfix1102 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SortName_lang")]
        public string SortNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
