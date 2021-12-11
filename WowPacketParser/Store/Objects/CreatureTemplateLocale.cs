using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_locale")]
    public sealed record CreatureTemplateLocale : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name", nullable: true)]
        public string Name;

        [DBFieldName("NameAlt", nullable: true)]
        public string NameAlt;

        [DBFieldName("Title", nullable: true)]
        public string Title;

        [DBFieldName("TitleAlt", nullable: true)]
        public string TitleAlt;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
