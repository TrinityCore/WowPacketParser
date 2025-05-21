using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("toy")]
    public sealed record ToyHotfix1100: IDataModel
    {
        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("toy_locale")]
    public sealed record ToyLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("toy")]
    public sealed record ToyHotfix1115 : IDataModel
    {
        [DBFieldName("SourceText")]
        public string SourceText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SourceTypeEnum")]
        public sbyte? SourceTypeEnum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("toy_locale")]
    public sealed record ToyLocaleHotfix1115 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("SourceText_lang")]
        public string SourceTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
