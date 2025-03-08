using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chat_channels")]
    public sealed record ChatChannelsHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Shortcut")]
        public string Shortcut;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FactionGroup")]
        public sbyte? FactionGroup;

        [DBFieldName("Ruleset")]
        public int? Ruleset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chat_channels_locale")]
    public sealed record ChatChannelsLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Shortcut_lang")]
        public string ShortcutLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chat_channels")]
    public sealed record ChatChannelsHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Shortcut")]
        public string Shortcut;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FactionGroup")]
        public byte? FactionGroup;

        [DBFieldName("Ruleset")]
        public int? Ruleset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chat_channels_locale")]
    public sealed record ChatChannelsLocaleHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Shortcut_lang")]
        public string ShortcutLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
