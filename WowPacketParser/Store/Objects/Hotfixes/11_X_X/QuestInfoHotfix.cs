using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("quest_info")]
    public sealed record QuestInfoHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("InfoName")]
        public string InfoName;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("Modifiers")]
        public int? Modifiers;

        [DBFieldName("Profession")]
        public ushort? Profession;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("quest_info_locale")]
    public sealed record QuestInfoLocaleHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("InfoName_lang")]
        public string InfoNameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
