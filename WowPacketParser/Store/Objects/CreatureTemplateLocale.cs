using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_locale")]
    public sealed class CreatureTemplateLocale : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = BinaryPacketReader.GetClientLocale();

        [DBFieldName("Name")]
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
