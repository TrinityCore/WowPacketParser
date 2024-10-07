using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("achievement_category")]
    public sealed record AchievementCategoryHotfix1100: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Parent")]
        public short? Parent;

        [DBFieldName("UiOrder")]
        public byte? UiOrder;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("achievement_category_locale")]
    public sealed record AchievementCategoryLocaleHotfix1100: IDataModel
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
