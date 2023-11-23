using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chat_channels")]
    public sealed record ChatChannelsHotfix1000: IDataModel
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
    public sealed record ChatChannelsLocaleHotfix1000: IDataModel
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
    public sealed record ChatChannelsHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Shortcut")]
        public string Shortcut;

        [DBFieldName("ID", true)]
        public uint? ID;

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
    public sealed record ChatChannelsLocaleHotfix340: IDataModel
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
    public sealed record ChatChannelsHotfix343: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Shortcut")]
        public string Shortcut;

        [DBFieldName("ID", true)]
        public uint? ID;

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
    public sealed record ChatChannelsLocaleHotfix343: IDataModel
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
